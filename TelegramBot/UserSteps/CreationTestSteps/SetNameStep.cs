using TelegramBot.DataTypes;

namespace TelegramBot.UserSteps
{
    internal class SetNameStep : IStepUser
    {
        public Task InstructionMessage(BotCommandContext context)
        {
            return context.EditKeyboard("Введите название теста", BotStorage.GetCommandsToDisplay(context));
        }

        public Task Invoke(BotCommandContext context)
        {
            context.User.CurrentTest = new Test(context.Input, context.User.Id);
            context.User.Step = new AddQuestionStep();
            return context.User.Step.InstructionMessage(context);
        }
    }
}
