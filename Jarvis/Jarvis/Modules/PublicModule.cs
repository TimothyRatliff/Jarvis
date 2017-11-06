using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Modules;
using Discord.Commands;
using Discord.WebSocket;

namespace Jarvis.Modules
{
    public class PublicModule : InfoModule
    {
		//private DiscordSocketClient client;
        

		//public async Task UserJoined()
		//{
		//	client.UserJoined += WelcomeUser;
		//}
		//public async Task WelcomeUser(SocketGuildUser user)
		//{
		//	await Context.Channel.SendMessageAsync("Welcome" + user.Mention + "to the server!");
		//	Console.WriteLine(DateTime.Now.ToString() + "	Welcome | Guild: " + Context.Guild.Name + " | Channel: " + Context.Channel.Name + " | User " + Context.User.Username + "");
		//}
	}
}
