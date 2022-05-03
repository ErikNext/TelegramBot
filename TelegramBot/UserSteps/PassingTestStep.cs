namespace TelegramBot.UserSteps
{
    internal class PassingTestStep : IStepUser
    {
        public async Task InstructionMessage(BotCommandContext context)
        {
            var questionCollection = context.User.PassingTest.GetQuestionCollection();
            if (questionCollection is not null)
            {
                var question = questionCollection.Question;
                var answers = questionCollection.AnswerOptions;

                var Print = $"Вопрос №{context.User.PassingTest.CurrectQuestionCount+1} / {context.User.PassingTest.Test.Questions.Count}\n{question}";

                await context.EditKeyboard(Print, answers.ToArray());
            }
            else
            {
                
                var testStatistics = context.User.PassingTest.Statistics;
                var testStatisticsModel = await testStatistics.ToModel();
                await context.DbAccess.CreateStatistics(testStatisticsModel);
                await context.EditKeyboard($"Верных ответов: {testStatistics.CountTrueAnswer} из {testStatistics.CountQuestion}", BotStorage.GetCommandsToDisplay(context));
            }
        }

        public Task Invoke(BotCommandContext context)
        {
            context.User.PassingTest.AddAnswer(context.Input);
            return InstructionMessage(context);
        }
    }
}
