using TelegramBot.DataTypes;

namespace TelegramBot.UserSteps
{
    internal class FindTestStep : IStepUser
    {
        public Task InstructionMessage(BotCommandContext context)
        {
            return context.EditKeyboard("Введите уникальный код", BotStorage.GetCommandsToDisplay(context));
        }

        public async Task Invoke(BotCommandContext context)
        {
            Guid.TryParse(context.Input, out Guid id);

            var test = (await context.DbAccess.GetTest(id))?.ToTest();

            if (test is null)
                return;
            else
            {
                context.User.PassingTest = new PassingTest(test, context.User);
                context.User.Step = new PassingTestStep();
                await context.User.Step.InstructionMessage(context);
            }
        }
    }
}
