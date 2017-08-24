using System;
using System.Collections.Generic;
using System.Text;
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

		//// ~say info -> displays info
		//[Command("info"), Summary("Displays bot info.")]
		//public async Task Info([Remainder, Summary("The info")] string info)
		//{
		//	// ReplyAsync is a method on ModuleBase
		//	await ReplyAsync(
		//					$"{Format.Bold("Info")}\n" +
		//					$"- Author: Tiiiimster (ID 102092268961296384)\n" +
		//					$"- Library: {DiscordConfig.LibName} ({DiscordConfig.LibVersion})\n" +
		//					$"- Runtime: {GetRuntime()} {GetBitness()}\n" +
		//					$"- Uptime: {GetUptime()}\n\n" +

		//					$"{Format.Bold("Stats")}\n" +
		//					$"- Heap Size: {GetHeapSize()} MB\n" +
		//					$"- Servers: {_client.Servers.Count()}\n" +
		//					$"- Channels: {_client.Servers.Sum(x => x.AllChannels.Count())}\n" +
		//					$"- Users: {_client.Servers.Sum(x => x.Users.Count())}");
		//}

	}
}
