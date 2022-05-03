using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class FindTest : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Top;
        public string Key { get; } = "Пройти тест";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = new FindTestStep();
            return context.User.Step.InstructionMessage(context);
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            if (context.User.Step is null)
                return true;

            return false;
        }
    }
}
