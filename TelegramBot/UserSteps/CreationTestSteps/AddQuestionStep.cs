using TelegramBot.DataTypes;

namespace TelegramBot.UserSteps
{
    internal class AddQuestionStep : IStepUser
    {
        public Task InstructionMessage(BotCommandContext context)
        {
            return context.EditKeyboard($"Тест: {context.User.CurrentTest.Name}\n" +
                $"Введите вопрос №{context.User.CurrentTest.Questions.Count + 1}", 
                BotStorage.GetCommandsToDisplay(context));
        }

        public Task Invoke(BotCommandContext context)
        {
            context.User.CurrentQuestionCollection = new QuestionCollection() { Question = context.Input };
            context.User.Step = new AnswerOptionsStep();
            return context.User.Step.InstructionMessage(context);
        }
    }
}
