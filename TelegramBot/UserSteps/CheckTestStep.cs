using TelegramBot.DataTypes;

namespace TelegramBot.UserSteps
{
    internal class CheckTestStep : IStepUser
    {
        public async Task InstructionMessage(BotCommandContext context)
        {
            var printButtons = new List<string>();
            var tests = await context.DbAccess.GetUserTests(context.User.Id);
            foreach (var test in tests)
            {
                printButtons.Add(test.Name);
            }
            printButtons.Add(new BackCommand().Key);
            await context.EditKeyboard("Твои тесты", printButtons.ToArray());
        }

        public async Task Invoke(BotCommandContext context)
        { 
            var userTests = await context.DbAccess.GetUserTests(context.User.Id);
            StringBuilder testString = new StringBuilder();
            Test userTest = null;

            foreach (var test in userTests)
            {
                if (test.Name == context.Input)
                {
                    userTest = test.ToTest();
                    break;
                }
            }

            if (userTest is not null)
            {
                context.User.CurrentTest = userTest;
                testString.Append($"Название теста: {userTest.Name}\n");
                testString.Append($"Дата создания: processing..\n");
                testString.Append($"Колличество вопросов: {userTest.Questions.Count}\n");
                testString.Append($"Уникальный код: `{userTest.Id}`\n");
                await context.EditKeyboard(testString.ToString(), BotStorage.GetCommandsToDisplay(context));
            }
        }
    }
}
