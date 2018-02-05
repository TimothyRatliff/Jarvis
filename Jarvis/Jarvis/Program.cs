using System;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Jarvis.addons.Logging;

namespace Jarvis
{
    class Program
    {
		private CommandService commands;
		public DiscordSocketClient _client;
		private IServiceProvider services;

		public static void Main(string[] args)
			=> new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			string Jarvis = "Jarvis";
			Console.Title = Jarvis;
			_client = new DiscordSocketClient();
			commands = new CommandService();

			services = new ServiceCollection()
				.BuildServiceProvider();

            //_client.UserJoined += WelcomeUser;
            _client.Log += Log;

			string token = File.ReadAllText("token.txt");

			await InstallCommands();
			await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();
			await _client.SetGameAsync("jarvisbot.online");


            // Block this task until the program is closed.
            await Task.Delay(-1);
		}

        //public async Task UserJoined()
        //{
        //    _client.UserJoined += WelcomeUser;
        //}
        public async Task WelcomeUser(SocketGuildUser user)
        {
            //need to find channel to send message in
            var guild = user.Guild as IGuild;
            var channels = await guild.GetTextChannelsAsync();
            var thechannel = channels.FirstOrDefault();
            //while(channels != null)
            //{
            //    var channel = channels.FirstOrDefault();
            //    if (channel.Name == "general" || channel.Name == "welcome")
            //    {
            //        thechannel = channel;
            //        channels = null;
            //        await channel.SendMessageAsync("Welcome " + user.Mention + " to the server!"); //welcomes the new user

            //    }
            //    else
            //    {
            //        channels.Skip(1);
            //        Console.WriteLine("Unable to find general or welcome channel, only found: " + channel.Name + "");
            //    }

            //}
            await thechannel.SendMessageAsync("Welcome " + user.Mention + " to " + user.Guild.Name + "!"); //welcomes the new user
            Console.WriteLine(DateTime.Now.ToString() + "	Welcome | Guild: " + user.Guild.Name + " | Channel: " + thechannel.Name + " | User " + user.Username + "");

            //Console.WriteLine("Channel Name: " + channel.Name + "");
            //await channel.SendMessageAsync("Welcome " + user.Mention + " to the server!"); //Welcomes the new user
            //await Context.Channel.SendMessageAsync("Welcome" + user.Mention + "to the server!");
            //Console.WriteLine(DateTime.Now.ToString() + "	Welcome | Guild: " + Context.Guild.Name + " | Channel: " + Context.Channel.Name + " | User " + Context.User.Username + "");
        }

        public async Task InstallCommands()
		{
			// Hook the MessageReceived Event into our Command Handler
			_client.MessageReceived += HandleCommand;
			// Discover all of the commands in this assembly and load them
			await commands.AddModulesAsync(Assembly.GetEntryAssembly());
		}

		public async Task HandleCommand(SocketMessage messageParam)
		{
			// Don't process the command if it was a System Message
			var message = messageParam as SocketUserMessage;
			if (message == null) return;
			// Create a number to track where the prefix ends and the command begins
			int argPos = 0;
			// Determine if the message is a command, based on if it starts with '~' or a mention prefix
			if (!(message.HasCharPrefix('~', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
			// Create a Command Context
			var context = new CommandContext(_client, message);
			// Execute the command. (result does not indicate a return value, 
			// rather an object stating if the command executed successfully)
			var result = await commands.ExecuteAsync(context, argPos, services);
            if (!result.IsSuccess)
                Logger.LoggerInstance.Error(message.ToString(), context.Guild, context.Channel, result.ErrorReason);
				//await context.Channel.SendMessageAsync(result.ErrorReason);
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}
