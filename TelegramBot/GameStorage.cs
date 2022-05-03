using TelegramBot.Game;

namespace TelegramBot
{
    internal static class GameStorage
    {
        public static Dictionary<Guid, GameTicTacToe> AllGames = new Dictionary<Guid, GameTicTacToe>();

        public static GameTicTacToe TryGetGame(string stringId)
        {
            GameTicTacToe returnGame = null;
            Guid.TryParse(stringId, out var guidId);
            AllGames.TryGetValue(guidId, out var game);
            if (game != null)
                returnGame = game;
            return game;
        }

        public static void RemoveGame(GameTicTacToe game)
        {
            AllGames.Remove(game.Id);
        }

        public static void AddGame(GameTicTacToe game)
        {
            AllGames.Add(game.Id, game);
        }
    }
}
