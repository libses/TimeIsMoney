using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TimeIsMoney.Application;
using TimeIsMoney.Telegram.SimpleButtons;

namespace TimeIsMoney.Telegram;

public class MessageHandler : IMessageHandler
{
    private readonly ITimeService timeService;
    private readonly IRegistrationService registrationService;
    private readonly IEnumerable<IButton> buttons;

    public MessageHandler(IRegistrationService registrationService, ITimeService timeService, IEnumerable<IButton> buttons)
    {
        this.registrationService = registrationService;
        this.timeService = timeService;
        this.buttons = buttons;
    }

    public async Task<MessageAction?> HandleAsync(Message msg, UpdateType type)
    {
        if (msg.Text is null) return null;
        var answer = new MessageAction();

        var strUserId = msg.From!.Id.ToString();
        var buttonTexts = buttons.Select(x => x.Text).ToArray();
        foreach (var button in buttons.Where(button => button.Text == msg.Text))
        {
            var result = await button.ActionAsync(strUserId);
            return result;
        }
        
        
        var decimalParsed = decimal.TryParse(msg.Text, out var number);
        var needRegistration = await registrationService.NeedRegistrationAsync(strUserId);
        answer.ReplyMarkup = [buttonTexts];
        Console.WriteLine($"{strUserId} has been requested, needReg: {needRegistration}, dec parsed: {decimalParsed}");
        
        if (needRegistration && !decimalParsed)
        {
            answer.RespondText = "Введи свою ЗП в месяц";
            return answer;
        }

        if (needRegistration)
        {
            await registrationService.RegisterAsync(strUserId, number);
            answer.RespondText = "Введи цену товара";
            return answer;
        }

        if (!needRegistration && decimalParsed)
        {
            var timeCosts = await timeService.GetTimeCostAsync(strUserId, number);
            answer.RespondText = $"{timeCosts}";
            return answer;
        }

        answer.RespondText = "Не понял тебя, друг";
        return answer;
    }
}