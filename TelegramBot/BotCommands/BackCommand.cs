using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class BackCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Top;
        public string Key { get; } = "Назад";

        public Task Invoke(BotCommandContext context)
        {
            if (context.User.Step is StatisticsStep)
            {
                context.User.Step = new CheckTestStep();
                return context.User.Step.InstructionMessage(context);
            }
            else
            {
                context.User.Step = null;
                return context.EditKeyboard("Чем займемся? 🕵️", BotStorage.GetCommandsToDisplay(context));
            }
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            var userStep = context.User.Step; 
            if (userStep is FindTestStep
                || userStep is StatisticsStep)
            {
                return true;
            }
            return false;
        }
    } 
}
