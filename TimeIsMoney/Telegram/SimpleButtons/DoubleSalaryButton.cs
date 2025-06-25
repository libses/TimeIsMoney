using TimeIsMoney.Extensions;

namespace TimeIsMoney.Telegram.SimpleButtons;

public class DoubleSalaryButton : IButton
{
    public string Text => "Удвоить зарплату!";
    public Task<MessageAction> ActionAsync(string userId)
    {
        var messageAction = new MessageAction
        {
            RespondText = "Ты уверен?",
            ReplyMarkup = [["Нет"]]
        };
        
        return messageAction.AsTask();
    }
}