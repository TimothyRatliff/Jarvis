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
        [Command("poll"), Summary("Poll users. Reactions determine winner.")]
        public async Task Poll([Remainder, Summary("The info")] String input = null)
        {
            String poll = input.ToString();
            String[] breaks = {"|"};
            String[] abc = poll.Split(breaks, StringSplitOptions.RemoveEmptyEntries);
            String question = abc[0];
            String option1 = abc[1];
            String option2 = abc[2];
            String time = abc[3];
            


            var context = Context.Channel;

            int delay = Int32.Parse(time) * 1000;
            //const int delay = 5000;

            var builder = new EmbedBuilder();
            builder.WithTitle("Poll");
            builder.WithDescription(question);
            builder.AddField("Instructions:", "React with :thumbsup: for: **" + option1 + "**\n\n\nReact with :thumbsdown: for: **" + option2 +"**\n");

            IUserMessage message = await context.SendMessageAsync("", false, builder);

            await Task.Delay(3000);

            await message.AddReactionAsync(new Emoji("\U0001f44d"));
            await message.AddReactionAsync(new Emoji("\U0001f44e"));

            await Task.Delay(3000);

            var m = await context.SendMessageAsync($"Poll started! _The poll will end in {delay / 1000} seconds._");
            await Task.Delay(delay);
            await m.DeleteAsync();
            var x = await message.GetReactionUsersAsync("\U0001f44d", 1000);
            int xnumber = x.Count() - 1; //subtract one for Jarvis' vote
            var y = await message.GetReactionUsersAsync("\U0001f44e", 1000);
            int ynumber = y.Count() - 1; //subtract one for Jarvis' vote

            await Task.Delay(3000);

            if (xnumber > ynumber)
            {
                await context.SendMessageAsync("**" + option1 + "**" + "wins with **" + xnumber + "** votes!");
            }
            else if (xnumber < ynumber)
            {
                await context.SendMessageAsync("**" + option2 + "**" + "wins with **" + ynumber + "** votes!");
            }
            else
                await context.SendMessageAsync("It's a tie!");

            Logger.LoggerInstance.Log("poll", Context.Guild, Context.Channel);
        }

    }
}
