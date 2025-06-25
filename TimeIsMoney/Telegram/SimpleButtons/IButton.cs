namespace TimeIsMoney.Telegram.SimpleButtons;

public interface IButton
{ 
    string Text { get; }

    Task<MessageAction> ActionAsync(string userId);
}