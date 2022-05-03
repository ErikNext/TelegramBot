using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal sealed class CreateTestCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Top;
        public string Key { get; } = "Создать тест 📝";
        public Task Invoke(BotCommandContext context)
        {
            var step = new SetNameStep();
            context.User.Step = step;
            return step.InstructionMessage(context);
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
