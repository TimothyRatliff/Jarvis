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
    public class PublicModule
    {
		private async Task JoinedGuild(SocketGuild context)
		{
			await context.CurrentUser.Guild.GetTextChannel(context.TextChannels.FirstOrDefault().Id)
				.SendMessageAsync("Welcome " + context.CurrentUser.Mention + " to ***!");
		}
	}
}
