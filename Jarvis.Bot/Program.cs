using Jarvis.Bot.Configuration;
using System;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.WebSocket;

namespace Jarvis.Bot
{
    public class Program
    {
        public DiscordSocketClient _client;
        private static AppSettings _settings = AppSettings.Initialize();


        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Console.Title = "Jarvis";
            _client = new DiscordSocketClient();
            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, _settings.token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }



        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }
}
