using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.UserSteps
{

    

    

    

    

    

    //internal class GameTicTacToe
    //{
    //    public int NumberColumns { get; set; } = 3;
    //    public WinnerType Winner { get; set; } = WinnerType.Empty;
    //    public int NumberRows { get; set; } = 3;
    //    public EnemyType Enemy { get; set; } 
    //    public List<List<InlineKeyboardButton>> Field { get; set; } = new List<List<InlineKeyboardButton>>();

    //    public GameTicTacToe(EnemyType enemy)
    //    {
    //        Enemy = enemy;
    //    }

    //    public void FieldDrawing()
    //    {
    //        for (int i = 0; i < NumberRows; i++)
    //        {
    //            List<InlineKeyboardButton> columns = new List<InlineKeyboardButton>();
    //            for (int j = 0; j < NumberColumns; j++)
    //            {
    //                var inlineKeyboardButton = new InlineKeyboardButton(_emptyCell) { CallbackData = $"{i} {j}" };
    //                columns.Add(inlineKeyboardButton);
    //            }
    //            Field.Add(columns);
    //        }
    //    }

    //    public void UserMove(string cellNumber)
    //    {
    //        string[] index = cellNumber.Split(' ');

    //        if (index.Length == 2)
    //        {
    //            int.TryParse(index[0], out int row);
    //            int.TryParse(index[1], out int column);
    //            if (Field[row][column].Text == _emptyCell)
    //                Field[row][column].Text = _crossCell;
    //            else
    //                return;
    //        }

    //        if(CheckForResultGame() != WinnerType.Empty)
    //        {
    //            Winner = CheckForResultGame();
    //            return;
    //        }

    //        if(Enemy == EnemyType.Bot)
    //        {
    //            BotMove();
    //        }
    //    }

    //    public WinnerType CheckForResultGame()
    //    {
    //        WinnerType winner = WinnerType.Empty;

    //        for (int i = 0; i < NumberRows; i++)
    //        {
    //            for (int j = 0; j < NumberColumns; j++)
    //            {
    //                if (Field[i][j].Text == _crossCell || Field[j][i].Text == _crossCell)
    //                    winner = WinnerType.UserWin;
    //                else
    //                {
    //                    winner = WinnerType.Empty;
    //                    break;
    //                }
    //            }
    //            if (winner != WinnerType.Empty)
    //                return winner;
    //        }

    //        //for (int i = 0; i < NumberRows; i++)
    //        //{
    //        //    if (Field[i][i].Text == _crossCell)
    //        //        victory = true;
    //        //    else
    //        //    {
    //        //        victory = false;
    //        //        break;
    //        //    }
    //        //} 

    //        //if (victory == true)
    //        //    return victory;

    //        //for (int i = 0, j = NumberColumns - 1; i < NumberRows; i++, j--)
    //        //{
    //        //    if (Field[i][j].Text == _crossCell)
    //        //        victory = true;
    //        //    else
    //        //    {
    //        //        victory = false;
    //        //        break;
    //        //    }
    //        //}
    //        return winner;
    //    }

    //    public void BotMove()
    //    {
    //        var rnd = new Random();
    //        int row = rnd.Next(0, 3);
    //        int column = rnd.Next(0, 3);
    //        if (Field[row][column].Text == _emptyCell)
    //            Field[row][column].Text = _zeroCell;
    //        else
    //            BotMove();
    //    }

    //    public async Task PrintField(BotCommandContext context)
    //    {
    //        InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(Field);

    //        if (context.User.LastReceivedMessage is not null)
    //        {
    //            await context.BotClient.EditMessageTextAsync(
    //                    chatId: context.UpdateContext.ChatId,
    //                    text: Winner.ToString(),
    //                    messageId: context.User.LastReceivedMessage.MessageId,
    //                    replyMarkup: inlineKeyboardMarkup
    //                    );
    //        }
    //    }
    //}
    
    
}
    