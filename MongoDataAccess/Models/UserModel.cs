namespace MongoDataAccess.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }

        public UserModel(long id, string username, string name)
        {
            Id = id;
            Username = username;
            Name = name;
        }  
    }
}
