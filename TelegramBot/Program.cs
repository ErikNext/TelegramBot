global using System.Text;
global using Telegram.Bot;
global using Telegram.Bot.Exceptions;
global using Telegram.Bot.Extensions.Polling;
global using Telegram.Bot.Types;
global using TelegramBot.BotCommands;
global using Microsoft.Extensions.Logging;
using TelegramBot.BotCore;

namespace TelegramBot
{

    public class Program
    {
        internal static BotManager TelegramBot;
        static void Main(string[] args)
        {
            ILogger logger;
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            logger = loggerFactory.CreateLogger<Program>();

            TelegramBot = new BotManager(logger);
            TelegramBot.Start(logger);
            Console.ReadLine();
        }
    }
}