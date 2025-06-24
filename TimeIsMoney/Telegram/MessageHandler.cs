using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TimeIsMoney.Application;

namespace TimeIsMoney.Telegram;

public class MessageHandler : IMessageHandler
{
    private readonly ITimeService timeService;
    private readonly IRegistrationService registrationService;

    public MessageHandler(IRegistrationService registrationService, ITimeService timeService)
    {
        this.registrationService = registrationService;
        this.timeService = timeService;
    }

    public async Task<MessageAction?> HandleAsync(Message msg, UpdateType type)
    {
        if (msg.Text is null) return null;

        var strUserId = msg.From!.Id.ToString();
        var answer = new MessageAction();
        var decimalParsed = decimal.TryParse(msg.Text, out var number);
        var needRegistration = await registrationService.NeedRegistrationAsync(strUserId);
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