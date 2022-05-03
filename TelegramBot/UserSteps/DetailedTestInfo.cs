namespace TelegramBot.UserSteps
{
    internal class DetailedTestInfo : IStepUser
    {
        public async Task InstructionMessage(BotCommandContext context)
        {
            var testString = new StringBuilder();
            var userTest = context.User.CurrentTest;

            if (userTest is not null)
            {
                testString.Append($"Название теста: {userTest.Name}\n");
                foreach (var quetsionCollection in userTest.Questions)
                {
                    testString.Append($"Вопрос: " + quetsionCollection.Question + "\n");

                    for (int i = 0; i < quetsionCollection.AnswerOptions.Count; i++)
                    {
                        testString.Append($"Вариант ответа №{i + 1}: " + quetsionCollection.AnswerOptions[i]);
                        if (i == quetsionCollection.TrueAnswer)
                            testString.Append(" (верный)");
                        testString.Append('\n');
                    }
                    testString.Append('\n');
                }
                testString.Append($"Уникальный код: `{userTest.Id}`");
                await context.EditKeyboard(testString.ToString(), BotStorage.GetCommandsToDisplay(context));
            }
            context.User.Step = null;
        }

        public async Task Invoke(BotCommandContext context)
        {
            
        }
    }
}
