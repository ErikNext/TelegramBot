using MongoDataAccess.DataAccess;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotCore;

namespace TelegramBot.BotCommands
{
    internal class BotCommandContext
    {
        public TgbotdbDataAccess DbAccess { get; set; }
        public ITelegramBotClient BotClient { get; set; }
        public string? Input => UpdateContext.Message; 
        public User User { get; set; }

        private readonly ILogger _logger;
        public UpdateContext UpdateContext;

        public Message LastReceivedMessage { get; set; }

        public BotCommandContext(User user, UpdateContext updateContext, ITelegramBotClient telegramBotClient, ILogger logger, TgbotdbDataAccess dbAccess)
        {
            User = user;
            BotClient = telegramBotClient;
            _logger = logger;
            DbAccess = dbAccess;
            UpdateContext = updateContext;
        }

        public async Task<Message> SendMessage(string message)
        {
            _logger.LogInformation($"{UpdateContext.Username} recieved message: {message}");
            return await BotClient.SendTextMessageAsync(
                    chatId: UpdateContext.ChatId,
                    text: message);
        }

        public Task DeleteMessage(Message message)
        {
            return BotClient.DeleteMessageAsync(UpdateContext.ChatId, message.MessageId);
        }

        public Task EditKeyboard(string message, params string[] textButtons)
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            foreach (var textButton in textButtons)
            {
                List<InlineKeyboardButton> rowButtons = new List<InlineKeyboardButton>();
                InlineKeyboardButton button = InlineKeyboardButton.WithCallbackData(textButton);
                rowButtons.Add(button);
                buttons.Add(rowButtons);
            }
            var inlineKeyboard = new InlineKeyboardMarkup(buttons);
            _logger.LogInformation($"User: {UpdateContext.Username} recieved keyboard: \'{message}\' with buttons:{Environment.NewLine}{string.Join(Environment.NewLine, textButtons)}");

            if (LastReceivedMessage is not null) 
            {
                return BotClient.EditMessageTextAsync(
                        chatId: UpdateContext.ChatId,
                        text: message,
                        messageId: LastReceivedMessage.MessageId,
                        replyMarkup: inlineKeyboard,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                        );
            }
            else
            {
                return SendKeyboard(message, textButtons);
            }
        }

        public async Task SendKeyboard(string message, params string[] textButtons)
        {
            _logger.LogInformation($"User: {UpdateContext.Username} recieved keyboard: \'{message}\' with buttons:{Environment.NewLine}{string.Join(Environment.NewLine, textButtons)}");
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();

            foreach (var textButton in textButtons)
            {
                List<InlineKeyboardButton> rowButtons = new List<InlineKeyboardButton>();
                InlineKeyboardButton button = InlineKeyboardButton.WithCallbackData(textButton);
                rowButtons.Add(button);
                buttons.Add(rowButtons);
            }
            var inlineKeyboard = new InlineKeyboardMarkup(buttons);
            LastReceivedMessage = await BotClient.SendTextMessageAsync(
                chatId: UpdateContext.ChatId,
                text: message,
                replyMarkup: inlineKeyboard
                );
        }

        public Task EditKeyboard(string message, List<List<InlineKeyboardButton>> buttons, params string[] textButtons)
        {
            foreach (var textButton in textButtons)
            {
                List<InlineKeyboardButton> rowButtons = new List<InlineKeyboardButton>();
                InlineKeyboardButton button = InlineKeyboardButton.WithCallbackData(textButton);
                rowButtons.Add(button);
                buttons.Add(rowButtons);
            }
            var inlineKeyboard = new InlineKeyboardMarkup(buttons);
            if (LastReceivedMessage is not null)
            {
                return BotClient.EditMessageTextAsync(
                        chatId: UpdateContext.ChatId,
                        text: message,
                        messageId: LastReceivedMessage.MessageId,
                        replyMarkup: inlineKeyboard,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                        );
            }
            else
            {
                return SendKeyboard(message, buttons);
            }
        }

        public async Task SendKeyboard(string message, List<List<InlineKeyboardButton>> buttons)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(buttons);
            LastReceivedMessage = await BotClient.SendTextMessageAsync(
                        chatId: UpdateContext.ChatId,
                        text: message,
                        replyMarkup: inlineKeyboard,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                        );
        }
    }
}
