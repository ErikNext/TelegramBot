using MongoDataAccess.Models;
using TelegramBot.DataTypes;
using TelegramBot.UserSteps;

namespace TelegramBot.BotCommands
{
    internal class StopAction : IBotCommand
    {
        public MenuPosition Position { get; } = MenuPosition.Top;
        public string Key { get; } = "Стоп";

        public async Task Invoke(BotCommandContext context)
        {
            if(context.User.Step is AnswerOptionsStep)
            {
                context.User.Step = new TrueAnswerStep();
                await context.User.Step.InstructionMessage(context);
            }
            else if (context.User.Step is AddQuestionStep)
            {
                context.User.Step = new CompletionTestCreationStep();
                await context.User.Step.Invoke(context);
            }
        }

        public bool IsNeedToDisplay(BotCommandContext context)
        {
            var userStep = context.User.Step;
            if (userStep is AddQuestionStep
                || userStep is AnswerOptionsStep
                || userStep is TrueAnswerStep)
            {
                return true;
            }
            return false;
        }
    }
}
