namespace TelegramBot.UserSteps
{
    internal class TrueAnswerStep : IStepUser
    {
        public Task InstructionMessage(BotCommandContext context)
        {
            var message = new StringBuilder();
            int i = 1;

            message.Append("Введите номер правильного ответа\n");
            message.Append($"Вопрос: {context.User.CurrentQuestionCollection.Question}\n");
            foreach (var answer in context.User.CurrentQuestionCollection.AnswerOptions)
            {
                message.Append($"№{i} {answer}\n");
                i++;
            }

            return context.EditKeyboard(message.ToString(), BotStorage.GetCommandsToDisplay(context));
        }

        public async Task Invoke(BotCommandContext context)
        {
            bool success = int.TryParse(context.Input, out int correctAnswer);

            if(success is true && correctAnswer <= context.User.CurrentQuestionCollection.AnswerOptions.Count && correctAnswer > 0)
            {
                context.User.CurrentQuestionCollection.TrueAnswer = correctAnswer - 1; // because index starts from zero
                context.User.CurrentTest.Questions.Add(context.User.CurrentQuestionCollection);
                context.User.Step = new AddQuestionStep();
                await context.User.Step.InstructionMessage(context);
            }      
        }
    }
}
