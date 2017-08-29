﻿using System;
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
    class AdminModule : ModuleBase
    {
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
	}
}