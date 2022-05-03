
using TelegramBot.DataTypes;

namespace TelegramBot.BotCommands
{
    internal interface IBotCommand
    {
        string Key { get; }
        MenuPosition Position { get; }
        bool IsNeedToDisplay(BotCommandContext context);
        Task Invoke(BotCommandContext context);
    }
}
