using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace MyBot
{
	public class Program
	{
		public static void Main(string[] args)
			=> new Program().MainAsync().GetAwaiter().GetResult();

		private DiscordSocketClient _client;
		public async Task MainAsync()
		{
			_client = new DiscordSocketClient();

			_client.Log += Log;

			_client.MessageReceived += MessageReceived;

			string token = "MjM2MDEzMTYwMjI4NzE2NTQ0.DH_QMw.W4gM0UytYbYh8HCCZTdG5w917ZE"; // (fake token)
			await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		private async Task MessageReceived(SocketMessage message)
		{
			if (message.Content == "say hello")
			{
				await message.Channel.SendMessageAsync("Hello!");
			}
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}

