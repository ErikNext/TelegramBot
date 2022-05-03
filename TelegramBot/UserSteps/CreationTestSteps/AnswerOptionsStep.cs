namespace TelegramBot.UserSteps
{
    internal class AnswerOptionsStep : IStepUser
    {
        public Task InstructionMessage(BotCommandContext context)
        {
            return context.EditKeyboard($"Вопрос: {context.User.CurrentQuestionCollection.Question}\n" +
                $"Введите вариант ответа №{context.User.CurrentQuestionCollection.AnswerOptions.Count + 1}", 
                BotStorage.GetCommandsToDisplay(context));
        }

        public Task Invoke(BotCommandContext context)
        {
            context.User.CurrentQuestionCollection.AnswerOptions.Add(context.Input);
            context.User.Step = new AnswerOptionsStep();
            return context.User.Step.InstructionMessage(context);
        }
    }
}
