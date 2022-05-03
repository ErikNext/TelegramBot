using MongoDB.Bson.Serialization.Attributes;

namespace MongoDataAccess.Models
{
    public class TestPassingStatisticsModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public TestModel Test { get; set; }
        public UserModel UserPasser { get; set; }
        public int CountTrueAnswer { get; set; }
        public DateTime Date { get; set; }

        public TestPassingStatisticsModel(TestModel test, UserModel user, int countTrueAnswer)
        {
            Test = test;
            UserPasser = user;
            CountTrueAnswer = countTrueAnswer;
            Date = DateTime.Now.AddHours(3);
        }
    }
}
