namespace TelegramBot.BotCore
{
    internal sealed class BotManager
    {
        private MessageHandler _messageHandler;
        private MessageReciever _messageReciever;

        public BotManager(ILogger logger)
        {
            _messageHandler = new MessageHandler(BotStorage.AllCommands, logger);
            _messageReciever = new MessageReciever(_messageHandler.HandleUpdateAsync, _messageHandler.HandleErrorAsync);
        }

        public void Start(ILogger logger)
        {
            _messageReciever.StartRecieve(new CancellationTokenSource(), logger);
        }
    }
}
