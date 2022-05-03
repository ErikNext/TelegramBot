using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class GameConnectCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Bottom;
        public string Key { get; } = "Подключиться к игре";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = new GameConnectStep();
            return context.User.Step.InstructionMessage(context);
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            var userStep = context.User.Step;
            if (userStep is null)
            {
                return true;
            }
            return false;
        }
    }
}
