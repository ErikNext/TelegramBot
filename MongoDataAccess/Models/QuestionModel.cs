namespace MongoDataAccess.Models
{
    public class QuestionModel
    {
        public string Question { get; set; }
        public int TrueAnswer { get; set; }
        public List<string> AnswerOptions { get; set; }
    }
}

