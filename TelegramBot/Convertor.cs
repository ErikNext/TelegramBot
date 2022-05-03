using MongoDataAccess.Models;
using TelegramBot.DataTypes;
using MongoDataAccess.DataAccess;

namespace TelegramBot
{
    internal static class Convertor
    {
        private static TgbotdbDataAccess _dbAccess { get; set; }

        public static async Task<TestModel> ToModel(this Test test)
        {
            _dbAccess = new TgbotdbDataAccess();
            var user = await _dbAccess.TryGetUser(test.CreatorId);
            List<QuestionModel> questionModels = test.Questions
                .Select(x => new QuestionModel()
                {
                    Question = x.Question,
                    AnswerOptions = x.AnswerOptions,
                    TrueAnswer = x.TrueAnswer
                }).ToList();
            return new TestModel(test.Id, test.Name, user, questionModels);
        }

        public static Test ToTest(this TestModel testModel)
        {
            List<QuestionCollection> questionCollection = testModel.Questions
                .Select(x => new QuestionCollection()
                {
                    Question = x.Question,
                    AnswerOptions = x.AnswerOptions,
                    TrueAnswer = x.TrueAnswer
                }).ToList();
            Test test = new Test(testModel.Name, testModel.Creator.Id);
            test.Questions = questionCollection;
            test.Id = testModel.Id;
            return test;
        }

        public static async Task<TestPassingStatisticsModel> ToModel(this TestPassingStatistics statistics)
        {
            _dbAccess = new TgbotdbDataAccess();
            var userPasser = await _dbAccess.TryGetUser(statistics.UserPasserId);
            var testModel = await statistics.Test.ToModel();
            return new TestPassingStatisticsModel(testModel, userPasser, statistics.CountTrueAnswer);
        }

        public static async Task<TestPassingStatistics> ToStatistics(this TestPassingStatisticsModel statisticsModel)
        {
            _dbAccess = new TgbotdbDataAccess();
            var testModel = await _dbAccess.GetTest(statisticsModel.Test.Id);
            var test = testModel.ToTest();
            return new TestPassingStatistics(statisticsModel.Test.Questions.Count, statisticsModel.UserPasser.Id, test);
        }
    }
}
