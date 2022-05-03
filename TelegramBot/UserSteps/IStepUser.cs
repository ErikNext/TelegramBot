namespace TelegramBot.UserSteps
{
    internal interface IStepUser
    {
        Task Invoke(BotCommandContext context);
        Task InstructionMessage(BotCommandContext context);
    }
}
