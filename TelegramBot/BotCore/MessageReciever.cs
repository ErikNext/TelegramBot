using TelegramBot.MyConstans;

namespace TelegramBot.BotCore
{
    public sealed class MessageReciever
    {
        private Func<ITelegramBotClient, Update, CancellationToken, Task> _handleUpdateAsync;
        private Func<ITelegramBotClient, Exception, CancellationToken, Task> _handleErrorAsync;

        public MessageReciever(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync,
            Func<ITelegramBotClient, Exception, CancellationToken, Task> handleErrorAsync)
        {
            _handleUpdateAsync = handleUpdateAsync;
            _handleErrorAsync = handleErrorAsync;
        }

        public void StartRecieve(CancellationTokenSource cancellationTokenSource, ILogger logger)
        {
            var botClient = new TelegramBotClient(Settings.Token);

            var _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            botClient.StartReceiving(_handleUpdateAsync, _handleErrorAsync, _receiverOptions, cancellationToken: cancellationTokenSource.Token);
            logger.LogInformation("Connection to telegram bot");
        }
    }
}
