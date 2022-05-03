using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Game
{
    internal class TicTacToePlayer
    {
        public long PlayerId { get; set; }
        public string Mark { get; set; }
        public BotCommandContext CommandContext { get; set; }  
        public Coordinates Coordinates { get; set; }

        public TicTacToePlayer(long id, string mark, BotCommandContext commandContext)
        {
            PlayerId = id;
            Mark = mark;
            CommandContext = commandContext;
        }
    }
}
