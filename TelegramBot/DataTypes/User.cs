using TelegramBot.DataTypes;
using TelegramBot.UserSteps;
using MongoDataAccess.DataAccess;
using TelegramBot.Game;
using TelegramBot.BotCore;

namespace TelegramBot
{
    internal class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public IStepUser? Step { get; set; }
        public BotCommandContext BotCommandContext { get; set; }
        public Dictionary<string, IBotCommand> AllCommands { get; set; }
        public Test? CurrentTest { get; set; }
        public QuestionCollection? CurrentQuestionCollection { get; set; }
        public PassingTest PassingTest { get; set; }
        public GameTicTacToe Game { get; set; }
        public TicTacToePlayer TicTacToePlayer { get; set; }

        public User(long id, string username, string name, Dictionary<string, IBotCommand> allCommands, BotCommandContext botCommandContext)
        {
            Id = id;
            Username = username;
            Name = name;
            AllCommands = allCommands;
            BotCommandContext = botCommandContext;
        }

        public Task Invoke(UpdateContext updateContext)
        {
            BotCommandContext.UpdateContext = updateContext;

            AllCommands.TryGetValue(updateContext.Message, out var command);

            if(command is null)
            {
                if (Step is not null)
                {
                    return Step.Invoke(BotCommandContext);
                }
                else
                {
                    command = new StartCommand();
                }
            }

            return command.Invoke(BotCommandContext);
        }
    }
}
