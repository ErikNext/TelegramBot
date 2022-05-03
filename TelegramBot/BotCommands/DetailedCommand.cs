using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class DetailedCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Top;
        public string Key { get; } = "Подробнее..";
        public Task Invoke(BotCommandContext context)
        {
            if (context.User.Step is PassingTestStep)
                context.User.Step = new DetailedTestResultStep();
            else if (context.User.Step is CheckTestStep)
                context.User.Step = new DetailedTestInfo();
            return context.User.Step.InstructionMessage(context);
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            var userStep = context.User.Step;
            if (userStep is PassingTestStep
                || userStep is CheckTestStep)
            {
                return true;
            }
            return false;
        }
    }
}
