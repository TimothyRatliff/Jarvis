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
        public async Task Poll([Summary("The poll options")] String input = null)
        {
            //if(input.Contains(" "))
            //    input
            String[] breaks = { "?", "|"};
            String[] poll = options.Split(breaks, StringSplitOptions.RemoveEmptyEntries);
            String option1 = poll[0];
            String option2 = poll[1];
            


            var context = Context.Channel;
            const int delay = 5000;

            var builder = new EmbedBuilder();
            builder.WithTitle("Poll");
            builder.WithDescription(question);
            builder.AddField("Instructions:", "React with :thumbsup: for: **" + option1 + "**\n\n\nReact with :thumbsdown: for: **" + option2 +"**\n");

            var message = await context.SendMessageAsync("", false, builder);
            await message.AddReactionAsync(new Emoji("U+1F44D"));
            await message.AddReactionAsync(new Emoji("U+1F44E"));
            var m = await context.SendMessageAsync($"Poll started _The poll will end in {delay / 1000} seconds._");
            await Task.Delay(delay);
            await m.DeleteAsync();
            var x = await message.GetReactionUsersAsync("U+1F44D", 1000);
            int xnumber = x.Count();
            var y = await message.GetReactionUsersAsync("U+1F44E", 1000);
            int ynumber = x.Count();
            if (xnumber > ynumber)
            {
                await context.SendMessageAsync(option1 + "wins with" + xnumber + "votes!");
            }
            else if (xnumber < ynumber)
            {
                await context.SendMessageAsync(option2 + "wins with" + ynumber + "votes!");
            }
            else
                await context.SendMessageAsync("It's a tie!");


        }

    }
}
