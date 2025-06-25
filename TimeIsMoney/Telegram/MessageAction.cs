namespace TimeIsMoney.Telegram;

public class MessageAction
{
    public string? RespondText { get; set; }
    
    public string[][]? ReplyMarkup { get; set; }

    public MessageAction Merge(MessageAction other)
    {
        var newMessageAction = new MessageAction
        {
            RespondText = (RespondText ?? "") + (other.RespondText ?? ""),
            ReplyMarkup = other.ReplyMarkup ?? ReplyMarkup
        };
        
        return newMessageAction;
    }
}