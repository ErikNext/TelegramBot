using TelegramBot.UserSteps;

namespace TelegramBot.Game
{
    internal class GameTicTacToe
    {
        public bool Active { get; set; }
        public TicTacToePlayer QueueMove { get; set; }
        public Guid Id { get; set; }
        public string[,] Field { get; set; }

        public int FieldRows = 3;
        public int FieldColumns = 3;
        public List<TicTacToePlayer> Players = new List<TicTacToePlayer>();

        public GameTicTacToe()
        {
            Active = true;
            Id = Guid.NewGuid();
            Field = new string[FieldRows, FieldColumns];
            InitializeField();
        }

        public void ChangeQueueMove(TicTacToePlayer retiredPlayer)
        {
            foreach(var player in Players)
            {
                if(player != retiredPlayer)
                {
                    QueueMove = player;
                    break;
                }
            }
        }

        public TicTacToePlayer GetPlayer(TicTacToePlayer desiredPlayer)
        {
            foreach(var player in Players)
                if(player == desiredPlayer)
                    return player;
            return null;
        }

        public bool TryMove(TicTacToePlayer player)
        {
            if (player.Coordinates.Row < FieldRows && player.Coordinates.Column < FieldColumns
                && player.Coordinates.Row >= 0 && player.Coordinates.Column >= 0 && QueueMove == player && Active)
            {
                if (Field[player.Coordinates.Row, player.Coordinates.Column] == Mark.Empty)
                {
                    ChangeQueueMove(player);
                    Field[player.Coordinates.Row, player.Coordinates.Column] = player.Mark;
                    return true;
                }
            }
            return false;
        }

        public bool AddPlayer(TicTacToePlayer player)
        {
            if (QueueMove is null)
                QueueMove = player;

            if (Players.Count < 2)
            {
                Players.Add(player);
                return true;
            }
            return false;
        }

        public bool CheckForVictory(TicTacToePlayer player)
        {
            if(CheckRowsAndColumns(player.Mark) || CheckMainDiagonal(player.Mark)
                || CheckSideDiagonal(player.Mark))
            {
                Active = false;
                return true;
            }
            return false;
        }

        public bool CheckRowsAndColumns(string mark)
        {
            bool victory = false;
            for (int i = 0; i < FieldRows; i++)
            {
                for (int j = 0; j < FieldColumns; j++)
                {
                    if (Field[i, j] == mark || Field[j, i] == mark)
                        victory = true;
                    else
                    {
                        victory = false;
                        break;
                    }
                }
                if (victory == true)
                    return victory;
            }
            return victory;
        }

        public bool CheckMainDiagonal(string mark)
        {
            bool victory = false;
            for (int i = 0; i < FieldRows; i++)
            {
                if (Field[i, i] == mark)
                    victory = true;
                else
                {
                    victory = false;
                    break;
                }
            }
            return victory;
        }

        public bool CheckSideDiagonal(string mark)
        {
            bool victory = false;
            for (int i = 0, j = FieldColumns - 1; i < FieldRows; i++, j--)
            {
                if (Field[i, j] == mark)
                    victory = true;
                else
                {
                    victory = false;
                    break;
                }
            }
            return victory;
        }

        public bool CheckFreeCells()
        {
            for (int i = 0; i < FieldRows; i++)
            {
                for (int j = 0; j < FieldColumns; j++)
                {
                    if (Field[i, j] == Mark.Empty)
                        return true;
                }
            }
            return false;
        }

        public void InitializeField()
        {
            for (int i = 0; i < FieldRows; i++)
            {
                for (int j = 0; j < FieldColumns; j++)
                {
                    Field[i, j] = Mark.Empty;
                }
            }
        }
    }
}
