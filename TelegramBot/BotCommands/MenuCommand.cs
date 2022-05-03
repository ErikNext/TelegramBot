using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class MenuCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Bottom;
        public string Key { get; } = "В меню";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = null;
            return context.EditKeyboard("Чем займемся? 🕵️", BotStorage.GetCommandsToDisplay(context));
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            var userStep = context.User.Step;
            if (userStep is CompletionTestCreationStep
                || userStep is DetailedTestResultStep
                || userStep is CheckTestStep
                || userStep is DetailedTestInfo
                || userStep is RemoveTestStep
                || userStep is PassingTestStep
                || userStep is GameConnectStep)
            {
                return true;
            }
            else if(userStep is GameTicTacToeStep)
            {
                GameStorage.RemoveGame(context.User.Game);
                return true;
            }
            return false;
        }
    }
}
