using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TimeIsMoney.Telegram;

public interface IMessageHandler
{
    Task<MessageAction?> HandleAsync(Message msg, UpdateType type);
}