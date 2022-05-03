using MongoDataAccess.Models;

namespace TelegramBot.UserSteps
{
    internal class CompletionTestCreationStep : IStepUser
    {
        public async Task InstructionMessage(BotCommandContext context)
        {
        }

        public async Task Invoke(BotCommandContext context)
        {
            if (context.User.CurrentTest.Questions.Count == 0)
            {
                await context.EditKeyboard("В тесте должен быть хотя бы 1 вопрос", BotStorage.GetCommandsToDisplay(context));
            }
            else
            {
                TestModel testModel = await context.User.CurrentTest.ToModel();
                await context.DbAccess.CreateTest(testModel);
                context.User.Step = null;

                await context.EditKeyboard($"Тест \'{context.User.CurrentTest.Name}\' успешно создан!\n`{context.User.CurrentTest.Id}`", BotStorage.GetCommandsToDisplay(context));
            }
        }
    }
}
