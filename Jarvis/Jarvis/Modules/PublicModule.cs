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
        //Embed color for InfoModule is builder.WithColor(Color.Blue);

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
        [Command("poll", RunMode = RunMode.Async), Summary("Poll users. Reactions determine winner.")]
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
            //if time of poll is longer than 30 seconds
            if(delay > 30000)
            {
                await context.SendMessageAsync("Tracked polls can not currently run longer than *30 seconds*. Use **~poll** with a time of *0* for an untracked poll.");
                return;
            }

            var builder = new EmbedBuilder();
            builder.WithTitle("Poll");
            builder.WithColor(Color.Blue);
            builder.WithDescription(question);
            builder.AddField("Instructions:", "React with :thumbsup: for: **" + option1 + "**\n\n\nReact with :thumbsdown: for: **" + option2 +"**\n");

            if (delay == 0)
            {
                builder.WithFooter(footer => {
                    footer
                            .WithText("Note: This is an untracked poll, vote at your leisure.");
                });

            }

            IUserMessage message = await context.SendMessageAsync("", false, builder);

            await message.AddReactionAsync(new Emoji("\U0001f44d"));
            await message.AddReactionAsync(new Emoji("\U0001f44e"));

            //await Task.Delay(3000);

            if(delay == 0)
            {
                Logger.LoggerInstance.Log("poll", Context.Guild, Context.Channel);
                return;
            }

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

        [Command("remind", RunMode = RunMode.Async), Summary("Remind message will be sent in this channel at specified time.")]
        public async Task Remind([Remainder, Summary("Time to reminded")] String input = null)
        {
            String reminder = input.ToString();
            String[] breaks = { "|" };
            String[] abc = reminder.Split(breaks, StringSplitOptions.RemoveEmptyEntries);
            String remindmsg = abc[0];
            String time = abc[1];
            String timef = abc[2];
            DateTime rtime = DateTime.Now;

            int delay = Int32.Parse(time);

            //if(timef == " s" || )
            //{
            //    await Context.Channel.SendMessageAsync("Woops! I can't understand spaces in the time format (yet). You'll need to ask me again without spaces.");
            //    return;
            //}

            if (timef == "d" || timef == " d")
            {
                await Context.Channel.SendMessageAsync($"Reminder **{remindmsg}** set for **{delay}** days. :white_check_mark: " +
                    $"*Warning: reminders set for multiple days currently have a chance of not going through due to limited thread capacity.*");
                double days = Convert.ToDouble(delay);
                TimeSpan milidays = TimeSpan.FromDays(days);
                await Task.Delay((int) milidays.TotalMilliseconds);
            }

            if (timef == "m" || timef == " m")
            {
                await Context.Channel.SendMessageAsync($"Reminder **{remindmsg}** set for **{delay}** minutes. :white_check_mark:");
                double minutes = Convert.ToDouble(delay);
                TimeSpan milimintues = TimeSpan.FromMinutes(minutes);
                await Task.Delay((int) milimintues.TotalMilliseconds);
            }

            if(timef == "s" || timef == " s")
            {
                await Context.Channel.SendMessageAsync($"Reminder **{remindmsg}** set for **{delay}** seconds. :white_check_mark:");
                double seconds = Convert.ToDouble(delay);
                TimeSpan milis = TimeSpan.FromSeconds(seconds);
                await Task.Delay((int) milis.TotalMilliseconds);
            }

            var builder = new EmbedBuilder();
            builder.WithTitle("Reminder");
            builder.WithColor(Color.Blue);
            builder.WithDescription(remindmsg);
            builder.WithFooter($"Set at: {rtime.ToString()}");

            await Context.Channel.SendMessageAsync($"{Context.User.Mention} has asked me to remind you:");
            IUserMessage message = await Context.Channel.SendMessageAsync("", false, builder);

            Logger.LoggerInstance.Log("remind", Context.Guild, Context.Channel);
        }

        //[RequireBotPermission(GuildPermission.ManageRoles)]
        //[Command("join"), Summary("Joins a joinable role")]
        //private async Task Join([Remainder, Summary("Role name")] String input = null)
        //{
        //    if (input.StartsWith("+"))
        //    {
        //        var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == input);

        //        if (role != null)
        //        {
        //            SocketGuildUser user = Context.User as SocketGuildUser;

        //            if (user.Roles.Contains(role))
        //            {
        //                await Context.Channel.SendMessageAsync(user.Mention + " already has the role **" + role.ToString() + "**!");
        //            }
        //            else
        //            {
        //                await user.AddRoleAsync(role);
        //                await Context.Channel.SendMessageAsync("Successfully gave " + user.Mention + " **" + role.ToString() + "**");
        //                Logger.LoggerInstance.LogInfo("join", Context.Guild, Context.Channel, role.Name);
        //            }
        //        }
        //    }
        //    else
        //        await Context.Channel.SendMessageAsync("Unable to find joinable role: **" + input + "**");
        //}

        //[RequireBotPermission(GuildPermission.ManageRoles)]
        //[Command("listroles"), Summary("Lists joinable roles")]
        //private async Task ListRole()
        //{
        //    await Context.Channel.SendMessageAsync("Joinable roles: ");
        //    Logger.LoggerInstance.Log("listroles", Context.Guild, Context.Channel);
        //}
    }
}
