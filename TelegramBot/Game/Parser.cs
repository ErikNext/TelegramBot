using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Game
{
    internal static class Parser
    {
        public static Coordinates? InputToIndexes(string text)
        {
            var indexes = text.Split("-");
            if (indexes.Length == 2)
            {
                int.TryParse(indexes[0], out int row);
                int.TryParse(indexes[1], out int column);
                return new Coordinates(row, column);
            }
            return null;
        }

        public static List<List<InlineKeyboardButton>> ToKeyboardButtons(string[,] field)
        {
            var rows = new List<List<InlineKeyboardButton>>();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                List<InlineKeyboardButton> columns = new List<InlineKeyboardButton>();
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    var buttonColumn = new InlineKeyboardButton(field[i, j]) { CallbackData = $"{i}-{j}" };
                    columns.Add(buttonColumn);
                }
                rows.Add(columns);
            }
            return rows;
        }
    }
}
