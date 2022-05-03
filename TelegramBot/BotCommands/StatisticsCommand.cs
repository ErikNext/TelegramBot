using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class StatisticsCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Top;
        public string Key { get; } = "Статистика";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = new StatisticsStep();
            return context.User.Step.Invoke(context);
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            var userStep = context.User.Step;
            if (userStep is CheckTestStep)
            {
                return true;
            }
            return false;
        }
    }
}
