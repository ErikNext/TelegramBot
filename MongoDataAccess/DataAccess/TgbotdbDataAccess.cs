using MongoDataAccess.Models;
using MongoDB.Driver;

namespace MongoDataAccess.DataAccess
{
    public class TgbotdbDataAccess
    {
        private const string _connectionString = $"mongodb+srv://Admin:Admin@tgbotdb.lfv2e.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
        private const string _databaseName= "tgbotdb";

        private const string _userCollection = "users";
        private const string _testCollection = "tests";
        private const string _statisticsCollection = "statistics";
        private const string _wordsCollection = "words";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase(_databaseName);
            return db.GetCollection<T>(collection);
        }
        
        public async Task<List<UserModel>> GetAllUsers()
        {
            var userCollection = ConnectToMongo<UserModel>(_userCollection);
            var results = await userCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<List<TestModel>> GetUserTests(long id)
        {
            var testCollection = ConnectToMongo<TestModel>(_testCollection);
            var result = (await testCollection.FindAsync(t => t.Creator.Id == id)).ToList();
            return result;
        }

        public async Task<List<TestPassingStatisticsModel>> TryGetStatistics(Guid testId)
        {
            var testCollection = ConnectToMongo<TestPassingStatisticsModel>(_statisticsCollection);
            var result = (await testCollection.FindAsync(s => s.Test.Id == testId)).ToList();
            return result;
        }

        public async Task<TestModel> GetTest(Guid id)
        {
            var testCollection = ConnectToMongo<TestModel>(_testCollection);
            var result = (await testCollection.FindAsync(t => t.Id == id)).FirstOrDefault();
            return result;
        }

        public Task CreateStatistics(TestPassingStatisticsModel statisticsModel)
        {
            var testCollection = ConnectToMongo<TestPassingStatisticsModel>(_statisticsCollection);
            return testCollection.InsertOneAsync(statisticsModel);
        }

        public Task CreateTest(TestModel test)
        {
            var testCollection = ConnectToMongo<TestModel>(_testCollection);
            return testCollection.InsertOneAsync(test);
        }

        public Task RemoveTest(Guid testId)
        {
            var testCollection = ConnectToMongo<TestModel>(_testCollection);
            var deleteFilter = Builders<TestModel>.Filter.Eq("Id", testId);
            return testCollection.DeleteOneAsync(deleteFilter);
        }

        public Task CreateUser(UserModel user)
        {
            var userCollection = ConnectToMongo<UserModel>(_userCollection);
            return userCollection.InsertOneAsync(user);
        }

        public async Task<UserModel> TryGetUser(long id)
        {
            var userCollection = ConnectToMongo<UserModel>(_userCollection);
            var result = (await userCollection.FindAsync(u => u.Id == id)).FirstOrDefault();
            return result;  
        }
    }
}
