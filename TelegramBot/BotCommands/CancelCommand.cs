using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class CancelCommand : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Bottom;
        public string Key { get; } = "Отменить";

        public Task Invoke(BotCommandContext context)
        {
            context.User.Step = null;
            context.User.CurrentQuestionCollection = null;
            context.User.CurrentTest = null;
            return context.EditKeyboard("Создание теста отменено", BotStorage.GetCommandsToDisplay(context));
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            var userStep = context.User.Step;
            if (userStep is AddQuestionStep 
                || userStep is AnswerOptionsStep 
                || userStep is SetNameStep 
                || userStep is TrueAnswerStep
                || userStep is CompletionTestCreationStep)
            {
                return true;
            }
            return false;
        }
    } 
}
