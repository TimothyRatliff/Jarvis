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
using Jarvis.addons.Logging;

namespace Jarvis
{
    public class InfoModule : ModuleBase
    {

		[Command("help"), Summary("Displays a list of commands.")]
		public async Task Help([Remainder, Summary("The list of commands")] String help = null)
		{
			await Context.Channel.SendMessageAsync(
							$"{Format.Bold("Commands")}\n\n" +
							$"{ Format.Bold("Info Commands")}\n" +
							$"- ~help - Displays a list of commands \n" +
							$"- ~users  - Displays the amount of users connected to this server \n" +
							$"- ~say x - Repeats x message \n" +
							$"- ~square x - Squares x number \n" +
							$"- ~userinfo x- Displays user name with Discord tag number \n" +
							$"- ~invlink - Displays the link to invite Jarvis to a server \n" +
							$"- ~info  - Displays info about Jarvis \n" +
							$"- ~ping  - Replies if Jarvis is online \n" +
							$"- ~wave  - :wave: :wink: \n" +

                            $"{ Format.Bold("Public Commands")}\n" +
                            $"- ~poll x | y | z - Creates a poll with x question and y/z as the options in the poll \n" +


                            $"{ Format.Bold("Admin Commands")}\n" +
							$"- ~purge x - Deletes x number of messages from the text channel \n" +
							$"- ~rolecount x - Displays the amount of users in this (x) role \n" 

							);
            Logger.LoggerInstance.Log("help", Context.Guild, Context.Channel);
		}

		// ~sample square 20 -> 400
		[Command("square"), Summary("Squares a number.")]
		public async Task Square([Summary("The number to square.")] int num)
		{
			// We can also access the channel from the Command Context.
			await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");

            Logger.LoggerInstance.LogInfo("square", Context.Guild, Context.Channel, num.ToString());
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

            Logger.LoggerInstance.LogInfo("userinfo", Context.Guild, Context.Channel, user.ToString());
		}

		private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
		private static string GetBitness() => $"{IntPtr.Size * 8}-bit";
		private static string GetUptime() => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        // ~say info -> displays info
        [Command("info"), Summary("Displays bot info.")]
        public async Task Info([Remainder, Summary("The info")] String info = null)
        {
            var guilds = await Context.Client.GetGuildsAsync();
            var guildcount = guilds.Count();
            var channels = await Context.Guild.GetChannelsAsync();
            var channelscount = channels.Count();
            var users = await Context.Guild.GetUsersAsync();
            var userscount = users.Count();

            var builder = new EmbedBuilder();
            //builder.WithTitle("Info");
            //builder.WithDescription("Here is some info about [me](http://www.jarvisbot.online)!");
            //builder.WithUrl("(http://www.jarvisbot.online");
            //builder.WithColor(new Color(0x6D8D9F));
            //builder.WithTimestamp(DateTimeOffset.FromUnixTimeSeconds(1508182618044));
            //builder.WithFooter(footer => {
            //    footer
            //            .WithText("footer text")
            //            .WithIconUrl("https://cdn.discordapp.com/embed/avatars/0.png");
            //    });
            //builder.WithThumbnailUrl("https://cdn.discordapp.com/embed/avatars/0.png");
            //builder.WithImageUrl("https://cdn.discordapp.com/embed/avatars/0.png");
            //builder.WithAuthor(author =>
            //{
            //    author
            //        .WithName("author name")
            //        .WithUrl("https://discordapp.com")
            //        .WithIconUrl("https://cdn.discordapp.com/embed/avatars/0.png");
            //});
            //builder.AddField("🤔Author:", "@Tiiiimster#0946");
            //builder.AddField("😱Library:", "Discord.Net Core (6)");
            //builder.AddField("🙄Runtime:", ".NETCore v2.0 64-bit");
            //builder.AddInlineField("Uptime:", "uptime"); 
            builder.WithTitle("Info");
            builder.WithDescription("Here is some info about [me](http://www.jarvisbot.online)!");
            builder.WithUrl("http://www.jarvisbot.online");
            builder.WithAuthor(author =>
            {
                author
                    .WithName("Jarvis")
                    .WithUrl("http://www.jarvisbot.online")
                    .WithIconUrl("https://imgur.com/a/GdzzY");
            });
            builder.WithFooter(footer => {
                footer
                    .WithText("Jarvis")
                    .WithIconUrl("https://cdn.discordapp.com/embed/avatars/0.png");
            });
            builder.AddInlineField("Author:", "@Tiiiimster#0946");
            builder.AddInlineField("Version:", "Jarvis v0.1.12");
            builder.AddField("Library:", "Discord.Net Core (v6)");
            builder.AddField("Runtime:", ".NETCore v2.0 64-bit");
            builder.AddField("Website:", "http://www.jarvisbot.online"); 
            builder.AddField("Uptime:", GetUptime());

            builder.AddInlineField("Heap Size:", "" + GetHeapSize() + " mb");
            builder.AddInlineField("Servers:", guildcount);

            await Context.Channel.SendMessageAsync("", false, builder);

            //await Context.Channel.SendMessageAsync("" +
            //				$"{ Format.Bold("Info")}\n" +
            //				$"- Author: @Tiiiimster#0946 \n" +
            //				$"- Library: {"Discord.Net Core"} ({DiscordConfig.APIVersion})\n" +
            //				$"- Runtime: {".NETCore v2.0"} {GetBitness()}\n" +
            //				$"- Uptime: {GetUptime()}\n\n" +


            //				$"{ Format.Bold("Stats")}\n" +
            //				$"- Heap Size: {GetHeapSize()} mb\n" +
            //				$"- Servers: {guildcount}\n" +
            //				$"- Channels: {channelscount} (in this guild) \n" +
            //				$"- Users: {userscount} (in this guild) \n" 
            //				);
            Logger.LoggerInstance.Log("info", Context.Guild, Context.Channel);
		}

		[Command("ping"), Summary("Replies to prove Jarvis is online")]
		private async Task Ping()
		{
            var bd = new EmbedBuilder();
            bd.AddField("Pong!", " ");
            bd.WithThumbnailUrl("https://i.imgur.com/NB96RHF.gif");
            bd.WithImageUrl("https://cdn.discordapp.com/embed/avatars/0.png");
            await Context.Channel.SendMessageAsync("", false, bd);
            //await Context.Channel.SendMessageAsync("Pong!");
            Logger.LoggerInstance.Log("ping", Context.Guild, Context.Channel);
		}

		[Command("wave"), Summary("Replies with a wave ;)")]
		private async Task Wave()
		{
			await Context.Channel.SendMessageAsync(":wave: https://i.imgur.com/APigjvz.gifv :wave:");
            Logger.LoggerInstance.Log("wave", Context.Guild, Context.Channel);
		}

		[Command("nice"), Summary("Replies with niceme.me")]
		private async Task Nice()
		{

			await Context.Channel.SendMessageAsync("http://niceme.me");
            Logger.LoggerInstance.Log("nice", Context.Guild, Context.Channel);
		}

		[Command("invlink"), Summary("Displays the link to invite Jarvis to a server")]
		private async Task InvLink()
		{
			await Context.Channel.SendMessageAsync($"Jarvis invite link: <https://discordapp.com/oauth2/authorize?client_id=236013160228716544&scope=bot&permissions=506985687>");
            Logger.LoggerInstance.Log("invlink", Context.Guild, Context.Channel);
        }


    }
}
