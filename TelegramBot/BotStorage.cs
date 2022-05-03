
using TelegramBot.Game;

namespace TelegramBot.BotCommands
{
    internal static class BotStorage
    {
        public static Dictionary<string, IBotCommand> AllCommands = new Dictionary<string, IBotCommand>();
        static BotStorage()
        {
            var typeIBotCommand = typeof(IBotCommand);
            var typesOfCommands = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && typeIBotCommand.IsAssignableFrom(p));

            foreach(var typeOfCommand in typesOfCommands)
            {
                IBotCommand command = (IBotCommand)Activator.CreateInstance(typeOfCommand);
                if (AllCommands.ContainsKey(command.Key))
                {
                    Console.WriteLine($"Ключ для команды {command.Key} имеет дубликат");
                }
                else
                {
                    AllCommands.Add(command.Key, command);
                }
            }
            AllCommands = AllCommands.OrderBy(pair => pair.Value.Position).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static string[] GetCommandsToDisplay(BotCommandContext context)
        {
            var commands = new List<string>();
            foreach (var command in AllCommands)
            {
                if (command.Value.IsNeedToDisplay(context) == true)
                {
                    commands.Add(command.Key);
                }
            }
            return commands.ToArray();
        }
    }
}
