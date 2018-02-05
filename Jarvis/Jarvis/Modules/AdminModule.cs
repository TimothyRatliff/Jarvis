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
    public class AdminModule : ModuleBase
    {



        //Admin only commands

        [Command("purge", RunMode = RunMode.Async)]
        [Summary("Deletes the specified amount of messages.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task PurgeChat(uint amount)
        {

            var messages = await this.Context.Channel.GetMessagesAsync((int)amount + 1).Flatten();

            await this.Context.Channel.DeleteMessagesAsync(messages);
            const int delay = 5000;
            var m = await this.ReplyAsync($"Purge completed. _This message will be deleted in {delay / 1000} seconds._");
            await Task.Delay(delay);
            await m.DeleteAsync();

            Logger.LoggerInstance.Log("purge", Context.Guild, Context.Channel);
        }

        //[Command("purgeu", RunMode = RunMode.Async)]
        //[Summary("Deletes the specified amount of messages.")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //[RequireBotPermission(ChannelPermission.ManageMessages)]
        //public async Task PurgeUChat(IGuildUser user = null, uint amount)
        //{

        // keep getting messages until we have amount of user messages aquired
        //	var messages = await this.Context.Channel.GetMessagesAsync((int)amount + 1).Flatten();
        //	//if(messages.
        // going to have to use linq to sort through messages here



        //	await this.Context.Channel.DeleteMessagesAsync(messages);
        //	const int delay = 5000;
        //	var m = await this.ReplyAsync($"Purge completed. _This message will be deleted in {delay / 1000} seconds._");
        //	await Task.Delay(delay);
        //	await m.DeleteAsync();

        //	Console.WriteLine(DateTime.Now.ToString() + "	PurgeUChat | Guild: " + Context.Guild.Name + " | Channel: " + Context.Channel.Name + " | Amount: " + amount + "");
        //}

        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [Command("rolecount"), Summary("Displays the amount of users in this role")]
        private async Task RoleCount(SocketRole role)
        {
            var rolecount = role.Members.Count();
            await Context.Channel.SendMessageAsync($"There are currently **{rolecount}** users with the **{role}** role in **{Context.Guild.Name}**!");
            Logger.LoggerInstance.LogInfo("rolecount", Context.Guild, Context.Channel, role.Name);
        }

        [RequireUserPermission(GuildPermission.ManageChannels)]
        [Command("usercount"), Summary("Gets the amount of users in the server")]
        private async Task UserCount()
        {
            var count = await Context.Guild.GetUsersAsync();
            var users = count.Count();
            await Context.Channel.SendMessageAsync($"There are currently **{users}** users in this server!");

            Logger.LoggerInstance.Log("usercount", Context.Guild, Context.Channel);
        }

        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [Command("addrole"), Summary("Creates a new joinable role")]
        private async Task AddRole([Remainder, Summary("Role name")] String input = null)
        {
            if (input.StartsWith("+"))
            {
                await Context.Guild.CreateRoleAsync(input);
                await Context.Channel.SendMessageAsync("Created role: **" + input + "**");
            }
            else
            {
                await Context.Guild.CreateRoleAsync("+" + input);
                await Context.Channel.SendMessageAsync("Created role: **" + input + "**");
            }

            Logger.LoggerInstance.LogInfo("addrole", Context.Guild, Context.Channel, input);
        }

        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        [Command("deleterole"), Summary("Deletes a role")]
        private async Task DeleteRole([Remainder, Summary("Role name")] String input = null)
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == input);
            await role.DeleteAsync();
            await Context.Channel.SendMessageAsync("Deleted role: **" + input + "**");

            Logger.LoggerInstance.LogInfo("deleterole", Context.Guild, Context.Channel, role.Name);
        }











        //Owner only commands

        // ~say hello -> hello
        [RequireOwner]
        [Command("say"), Summary("Echos a message.")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync(echo);

            Logger.LoggerInstance.LogInfo("say", Context.Guild, Context.Channel, echo);
        }

        [RequireOwner]
        [Command("nadeko"), Summary("LUL")]
        private async Task Nadeko()
        {
            await Context.Channel.SendMessageAsync("Nadeko is a weeb bot, Nadeko suxs");
        }

        [RequireOwner]
        [Command("kys"), Summary("keep yourself safe")]
        private async Task KYS()
        {
            await Context.Channel.SendMessageAsync("Keep yourself safe! https://imgur.com/gallery/MZjUO");
        }

    }
}
