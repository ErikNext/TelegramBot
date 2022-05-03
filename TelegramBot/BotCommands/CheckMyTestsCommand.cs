using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class CheckMyTestsCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Top;
        public string Key { get; } = "Мои тесты 📋";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = new CheckTestStep();
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
