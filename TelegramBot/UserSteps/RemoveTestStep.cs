
namespace TelegramBot.UserSteps
{
    internal class RemoveTestStep : IStepUser
    {
        public Task InstructionMessage(BotCommandContext context)
        {
            Invoke(context);
            return context.EditKeyboard($"Тест: '{context.User.CurrentTest.Name}' был удален!", BotStorage.GetCommandsToDisplay(context));
        }

        public Task Invoke(BotCommandContext context)
        {
            return context.DbAccess.RemoveTest(context.User.CurrentTest.Id);
        }
    }
}
