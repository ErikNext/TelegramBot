using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class GameRestartCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Bottom;
        public string Key { get; } = "Играть еще раз";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = new GameConnectStep();
            return context.User.Step.InstructionMessage(context);
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            var userStep = context.User.Step;
            if (userStep is GameTicTacToeStep)
            {
                return true;
            }
            return false;
        }
    }
}
