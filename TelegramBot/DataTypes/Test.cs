namespace TelegramBot.DataTypes
{
    internal class QuestionCollection
    {
        public string Question { get; set; }
        public int TrueAnswer { get; set; }
        public List<string> AnswerOptions { get; set; } = new List<string>();
    }

    internal class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long CreatorId { get; set; }

        public List<QuestionCollection> Questions = new List<QuestionCollection>();

        public Test(string name, long creatorId)
        {
            Name = name;
            CreatorId = creatorId;
            Id = Guid.NewGuid();
        }
    }
}
