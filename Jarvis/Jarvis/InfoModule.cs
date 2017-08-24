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
