namespace TelegramBot.DataTypes
{
    internal class AnswerResult
    {
        public string UserAnswer { get; set; }
        public QuestionCollection QuestionCollection { get; set; }
        public string TrueAnswer { get; set; }
        public bool Right { get; set; } = false;

        public AnswerResult(string userAnswer, QuestionCollection questionCollection)
        {
            UserAnswer = userAnswer;
            QuestionCollection = questionCollection;
        }

        public void Review(TestPassingStatistics statistics)
        {
            var answers = QuestionCollection.AnswerOptions;
            var trueAnswerId = QuestionCollection.TrueAnswer;
            TrueAnswer = answers[trueAnswerId];

            if (UserAnswer == answers[trueAnswerId])
            {
                Right = true;
                statistics.CountTrueAnswer++;
            }
        }
    }

    internal class TestPassingStatistics
    {
        public long UserPasserId { get; set; }
        public Test Test { get; set; }
        public int CountQuestion { get; set; }
        public int CountTrueAnswer { get; set; } = 0;

        public TestPassingStatistics(int countQuestion, long userPasserId, Test test)
        {
            CountQuestion = countQuestion;
            UserPasserId = userPasserId;
            Test = test;
        }
    }

    internal class PassingTest
    {
        public Test Test { get; set; }
        public int CurrectQuestionCount { get; private set; } = 0;
        public List<AnswerResult> AnswerResults { get; set; } = new List<AnswerResult>();
        public TestPassingStatistics Statistics { get; set; }

        public PassingTest(Test test, User user)
        {
            Test = test;
            Statistics = new TestPassingStatistics(Test.Questions.Count, user.Id, test);
        }

        public QuestionCollection? GetQuestionCollection()
        {
            if (CurrectQuestionCount < Test.Questions.Count)
                return Test.Questions[CurrectQuestionCount];
            else
                return null;
        }

        public void AddAnswer(string answer)
        {
            var answerResult = new AnswerResult(answer, Test.Questions[CurrectQuestionCount]);
            answerResult.Review(Statistics);
            AnswerResults.Add(answerResult);
            CurrectQuestionCount++;
        }
    }
}
