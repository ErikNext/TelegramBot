namespace TelegramBot.UserSteps
{
    internal class StatisticsStep : IStepUser
    {
        public async Task InstructionMessage(BotCommandContext context)
        { 
        }

        public async Task Invoke(BotCommandContext context)
        {
            if (context.User.CurrentTest != null)
            {
                var statisticsModels = await context.DbAccess.TryGetStatistics(context.User.CurrentTest.Id);

                var stringStatistics = new StringBuilder();
                foreach (var statistics in statisticsModels)
                {
                    stringStatistics.Append($"Имя пользователя: {statistics.UserPasser.Username}\n");
                    stringStatistics.Append($"Дата прохождения: {statistics.Date:g}\n");
                    stringStatistics.Append($"Верных ответов: {statistics.CountTrueAnswer} / {statistics.Test.Questions.Count}\n\n");
                }

                if (stringStatistics.ToString() == "")
                    stringStatistics.Append("Не найдено результатов");
                await context.EditKeyboard(stringStatistics.ToString(), BotStorage.GetCommandsToDisplay(context));
            }   
        }
    }
}
