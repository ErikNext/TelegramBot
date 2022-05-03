using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDataAccess.Models
{
    public class TestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserModel Creator { get; set; }
        public List<QuestionModel> Questions { get; set; }
        public TestModel(Guid id, string name, UserModel creator, List<QuestionModel> questions)
        {
            Id = id;
            Name = name;
            Creator = creator;
            Questions = questions;
        }
    }
}

