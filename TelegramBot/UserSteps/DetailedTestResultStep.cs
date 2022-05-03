namespace TelegramBot.UserSteps
{
    internal class DetailedTestResultStep : IStepUser
    {
        public Task InstructionMessage(BotCommandContext context)
        {
            var stringTestResult = new StringBuilder();
            var passingTest = context.User.PassingTest;

            stringTestResult.Append($"Результаты прохождения теста: {passingTest.Test.Name}\n\n");
            foreach (var answerResult in passingTest.AnswerResults)
            {
                stringTestResult.Append($"Вопрос: {answerResult.QuestionCollection.Question}\n");
                stringTestResult.Append($"Правильный ответ: {answerResult.TrueAnswer}\n");
                stringTestResult.Append($"Ваш ответ: {answerResult.UserAnswer}\n\n");
            }

            stringTestResult.Append($"Верных ответов: {passingTest.Statistics.CountTrueAnswer} из {passingTest.Statistics.CountQuestion}");
            return context.EditKeyboard(stringTestResult.ToString(), BotStorage.GetCommandsToDisplay(context));
        }

        public Task Invoke(BotCommandContext context)
        {
            return Task.CompletedTask;
        }
    }
}
