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
using Jarvis.addons.Settings;

namespace Jarvis.Modules
{
    public class SetupModule : ModuleBase
    {

        //Embed color for SetupModule is builder.WithColor(Color.Black);

        [RequireUserPermission(GuildPermission.ManageGuild)]
        [RequireBotPermission(GuildPermission.ManageGuild)]
        [Command("setup", RunMode = RunMode.Async), Summary("Begin setup of Jarvis")]
        public async Task Setup()
        {
            if(GuildDB.DBInstance.IsSetup(Context.Guild) == true)
            {
                await Context.Channel.SendMessageAsync($"**{Context.Guild.Name}** has already been setup. :white_check_mark:");
                return;
            }
            GuildDB.DBInstance.NewSetup(Context.Guild);

            await Context.Channel.SendMessageAsync($"Created a settings entry for **{Context.Guild.Name}** :white_check_mark:");
            Logger.LoggerInstance.Log("setup", Context.Guild, Context.Channel);
        }

        [RequireUserPermission(GuildPermission.ManageGuild)]
        [RequireBotPermission(GuildPermission.ManageGuild)]
        [Command("delsetup", RunMode = RunMode.Async), Summary("Delete setup entry")]
        public async Task DeleteSetup()
        {
            GuildDB.DBInstance.DeleteSetup(Context.Guild);

            await Context.Channel.SendMessageAsync($"Removed settings entry for **{Context.Guild.Name}** :white_check_mark:");
            Logger.LoggerInstance.Log("delsetup", Context.Guild, Context.Channel);
        }

        [RequireUserPermission(GuildPermission.ManageGuild)]
        [RequireBotPermission(GuildPermission.ManageGuild)]
        [Command("issetup", RunMode = RunMode.Async), Summary("Returns true if server has been setup")]
        public async Task IsSetup()
        {

            if (GuildDB.DBInstance.IsSetup(Context.Guild) == true)
            {
                await Context.Channel.SendMessageAsync($"This server has been setup.");
            }
            else
                await Context.Channel.SendMessageAsync($"This server has not been setup.");

            Logger.LoggerInstance.Log("issetup", Context.Guild, Context.Channel);
        }

        [RequireUserPermission(GuildPermission.ManageGuild)]
        [RequireBotPermission(GuildPermission.ManageGuild)]
        [Command("welcome", RunMode = RunMode.Async), Summary("Toggle welcome message")]
        public async Task ToggleWelcome([Remainder] String input = null)
        {
            
            if (GuildDB.DBInstance.IsSetup(Context.Guild) == false)
            {
                await Context.Channel.SendMessageAsync($"**{Context.Guild.Name}** has not been setup. Please run ~setup.");
                return;
            }

            GuildDB.DBInstance.ToggleWelcome(Context.Guild, Context.Channel);



            await Context.Channel.SendMessageAsync($"Toggled welcome messages for **{Context.Channel.Name}** :white_check_mark:");
            Logger.LoggerInstance.Log("welcome", Context.Guild, Context.Channel);
        }



    }
}
