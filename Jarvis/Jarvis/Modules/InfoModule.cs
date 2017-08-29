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

namespace Jarvis
{
    public class InfoModule : ModuleBase
    {
		// ~say hello -> hello
		[Command("say"), Summary("Echos a message.")]
		public async Task Say([Remainder, Summary("The text to echo")] string echo)
		{
			// ReplyAsync is a method on ModuleBase
			await ReplyAsync(echo);
		}

		[Command("help"), Summary("Displays a list of commands.")]
		public async Task Help([Remainder, Summary("The list of commands")] string help)
		{
			await ReplyAsync(help);
		}

		// ~sample square 20 -> 400
		[Command("square"), Summary("Squares a number.")]
		public async Task Square([Summary("The number to square.")] int num)
		{
			// We can also access the channel from the Command Context.
			await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
		}

		// ~sample userinfo --> foxbot#0282
		// ~sample userinfo @Khionu --> Khionu#8708
		// ~sample userinfo Khionu#8708 --> Khionu#8708
		// ~sample userinfo Khionu --> Khionu#8708
		// ~sample userinfo 96642168176807936 --> Khionu#8708
		// ~sample whois 96642168176807936 --> Khionu#8708
		[Command("userinfo"), Summary("Returns info about the current user, or the user parameter, if one passed.")]
		[Alias("user", "whois")]
		public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
		{
			var userInfo = user ?? Context.Client.CurrentUser;
			await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
		}

		[Command("purge", RunMode = RunMode.Async)]
		[Summary("Deletes the specified amount of messages.")]
		[RequireUserPermission(GuildPermission.Administrator)]
		[RequireBotPermission(ChannelPermission.ManageMessages)]
		public async Task PurgeChat(uint amount)
		{
			var messages = await this.Context.Channel.GetMessagesAsync((int)amount + 1).Flatten();

			await this.Context.Channel.DeleteMessagesAsync(messages);
			const int delay = 5000;
			var m = await this.ReplyAsync($"Purge completed. _This message will be deleted in {delay / 1000} seconds._");
			await Task.Delay(delay);
			await m.DeleteAsync();
		}


		// ~say info -> displays info
		[Command("info"), Summary("Displays bot info.")]
		public async Task Info([Remainder, Summary("The info")] string info)
		{
			info = "Author: Tiiiimster (ID 102092268961296384)\n";
				//$"{Format.Bold("Info")}\n" +
				//			$"- Author: Tiiiimster (ID 102092268961296384)\n" +
				//			$"- Library: \n" +
				//			$"- Runtime: \n" +
				//			$"- Uptime: \n\n" +

				//			$"{Format.Bold("Stats")}\n" +
				//			$"- Heap Size: MB\n" +
				//			$"- Servers: \n" +
				//			$"- Channels: \n" +
				//			$"- Users: ";
			// ReplyAsync is a method on ModuleBase
			await ReplyAsync("Author: Tiiiimster (ID 102092268961296384)\n");
		}

	}
}
