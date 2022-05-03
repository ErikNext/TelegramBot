using TelegramBot.Game;

namespace TelegramBot.UserSteps
{
    internal class GameTicTacToeStep : IStepUser
    {
        TicTacToePlayer? Player = null;

        public GameTicTacToeStep(BotCommandContext context)
        {
            Player = null;
            context.User.Game = null;
        }

        public GameTicTacToeStep()
        {
        }

        public async Task InstructionMessage(BotCommandContext context)
        {
        }

        public async Task Invoke(BotCommandContext context)
        {
            if (Player == null && context.User.Game != null)
            {
                Player = new TicTacToePlayer(context.User.Id, Mark.Zero, context);
                context.User.Game.AddPlayer(Player);
                await SendFieldToPlayers(context, "Вы подключились к игре", "Пользователь подключился");
            }
            else if (context.User.Game == null || Player == null)
            {
                context.User.Game = new GameTicTacToe();
                Player = new TicTacToePlayer(context.User.Id, Mark.Cross, context);
                context.User.Game.AddPlayer(Player);
                GameStorage.AddGame(context.User.Game);
                await context.EditKeyboard($"Игра началась\n`{context.User.Game.Id}`", Parser.ToKeyboardButtons(context.User.Game.Field), BotStorage.GetCommandsToDisplay(context));
            }
            else
            {
                Player.CommandContext = context;
                Player.Coordinates = Parser.InputToIndexes(context.Input);
                if (context.User.Game.TryMove(Player))
                {
                    await SendFieldToPlayers(context, "Ход противника", "Ваш ход");
                    if (context.User.Game.CheckForVictory(Player))
                    {
                        await SendFieldToPlayers(context, "Вы победили", "Вы проиграли");
                    }
                }
            }
        }


        public async Task SendFieldToPlayers(BotCommandContext context, string messageForPlayer, string messageForOtherPlayers)
        {
            await context.EditKeyboard(messageForPlayer, Parser.ToKeyboardButtons(context.User.Game.Field), BotStorage.GetCommandsToDisplay(context));

            foreach (var player in context.User.Game.Players)
            {
                if (player != Player)
                    await player.CommandContext.EditKeyboard(messageForOtherPlayers, Parser.ToKeyboardButtons(context.User.Game.Field), BotStorage.GetCommandsToDisplay(context));
            }
        }
    }
}