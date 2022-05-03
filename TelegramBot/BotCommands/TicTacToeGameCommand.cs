using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class TicTacToeGameCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Bottom;
        public string Key { get; } = "Играть в крестики нолики";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = new GameTicTacToeStep(context);
            return context.User.Step.Invoke(context);
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            if (context.User.Step == null)
                return true;
            else
                return false;
        }
    }
}
