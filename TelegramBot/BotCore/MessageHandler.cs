using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.BotCore
{
    public sealed class MessageHandler
    {
        private Dictionary<string, IBotCommand> _commands;

        private static Dictionary<long, User> _allUsers = new Dictionary<long, User>();
        private TgbotdbDataAccess _dbAccess = new TgbotdbDataAccess();

        private ILogger _logger;

        internal MessageHandler(Dictionary<string, IBotCommand> commands, ILogger logger)
        {
            _commands = commands;
            _logger = logger;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type is not UpdateType.Message 
                && update.Type is not UpdateType.CallbackQuery)
                return;

            var updateContext = new UpdateContext(update);

            try
            {
                if (update.Type is UpdateType.Message)
                    await botClient.DeleteMessageAsync(updateContext.ChatId, updateContext.MessageId);
            }
            catch
            {

            }

            _allUsers.TryGetValue(updateContext.ChatId, out var user);

            if (user is null)
            {
                var commandContext = new BotCommandContext(user,
                    updateContext,
                    botClient,
                    _logger,
                    _dbAccess);
                user = new User(updateContext.ChatId, updateContext.Name, updateContext.Username, _commands, commandContext);
                commandContext.User = user; 

                _allUsers.Add(user.Id, user);

                if (await (_dbAccess.TryGetUser(user.Id)) is null)
                {
                    await _dbAccess.CreateUser(new UserModel(updateContext.ChatId, updateContext.Username, updateContext.Name));
                }
            }
            await user.Invoke(updateContext);
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
