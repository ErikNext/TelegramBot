using TelegramBot.DataTypes;

namespace TelegramBot.BotCommands
{
    internal sealed class StartCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Top;
        public string Key { get; } = "/start";

        public Task Invoke(BotCommandContext context)
        {
            return context.SendKeyboard("Чем займемся? 🕵️", BotStorage.GetCommandsToDisplay(context));
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            return false;
        }
    }
}