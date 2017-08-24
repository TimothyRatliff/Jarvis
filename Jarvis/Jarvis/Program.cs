using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Jarvis
{
    class Program
    {
		private CommandService commands;
		private DiscordSocketClient _client;
		private IServiceProvider services;

		public static void Main(string[] args)
			=> new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			_client = new DiscordSocketClient();
			commands = new CommandService();

			services = new ServiceCollection()
				.BuildServiceProvider();
			

			_client.Log += Log;
			_client.MessageReceived += MessageReceived;

			string token = "MjM2MDEzMTYwMjI4NzE2NTQ0.DICm4A.h45R_dRSlT-GGI6gFM9ii-3WNgA"; // Remember to keep this private!

			await InstallCommands();
			await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		public async Task InstallCommands()
		{
			// Hook the MessageReceived Event into our Command Handler
			_client.MessageReceived += HandleCommand;
			// Discover all of the commands in this assembly and load them.
			await commands.AddModulesAsync(Assembly.GetEntryAssembly());
		}

		public async Task HandleCommand(SocketMessage messageParam)
		{
			// Don't process the command if it was a System Message
			var message = messageParam as SocketUserMessage;
			if (message == null) return;
			// Create a number to track where the prefix ends and the command begins
			int argPos = 0;
			// Determine if the message is a command, based on if it starts with '!' or a mention prefix
			if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
			// Create a Command Context
			var context = new CommandContext(_client, message);
			// Execute the command. (result does not indicate a return value, 
			// rather an object stating if the command executed successfully)
			var result = await commands.ExecuteAsync(context, argPos, services);
			if (!result.IsSuccess)
				await context.Channel.SendMessageAsync(result.ErrorReason);
		}

	private async Task MessageReceived(SocketMessage message)
		{
			if (message.Content == "!ping")
			{
				await message.Channel.SendMessageAsync("Pong!");
			}
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}
