namespace TelegramBot.UserSteps
{
    internal class GameConnectStep : IStepUser
    {

        public Task InstructionMessage(BotCommandContext context)
        {
            return context.EditKeyboard("Введите ID игры", BotStorage.GetCommandsToDisplay(context));
        }

        public Task Invoke(BotCommandContext context)
        {
            var game = GameStorage.TryGetGame(context.Input);

            if(game != null)
            {
                context.User.Game = game;

                context.User.Step = new GameTicTacToeStep();
                return context.User.Step.Invoke(context);
            }
            return Task.CompletedTask;
        }
    }
}
