using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class RemoveTestCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Middle;
        public string Key { get; } = "Удалить тест";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = new RemoveTestStep();
            return context.User.Step.InstructionMessage(context);
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
