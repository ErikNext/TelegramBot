using Telegram.Bot.Types.Enums;

namespace TelegramBot.BotCore
{
    public class UpdateContext
    {
        public long ChatId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public int MessageId { get; set; }
        public string Name { get; set; }

        public UpdateContext(Update update)
        {
            if (update.Type is UpdateType.CallbackQuery)
            {
                ChatId = update.CallbackQuery.Message.Chat.Id;
                Username = update.CallbackQuery.Message.From.Username;
                Message = update.CallbackQuery.Data;
                MessageId = update.CallbackQuery.Message.MessageId;
                Name = update.CallbackQuery.Message.From.FirstName + " " + update.CallbackQuery.Message.From.LastName;
            }
            else if (update.Type is UpdateType.Message)
            {
                ChatId = update.Message.From.Id;
                Username = update.Message.From.Username;
                Message = update.Message.Text;
                MessageId = update.Message.MessageId;
                Name = update.Message.From.FirstName + " " + update.Message.From.LastName;
            }

        }
    }
}
