﻿//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Discord;
//using Discord.Modules;
//using Discord.Commands;
//using Discord.WebSocket;
//using Jarvis.addons.Logging;
//using Jarvis.addons.Settings;

//namespace Jarvis.Modules
//{
//    public class SetupModule : ModuleBase
//    {

//        //Embed color for SetupModule is builder.WithColor(Color.Black);

//        [RequireUserPermission(GuildPermission.ManageRoles)]
//        [RequireBotPermission(GuildPermission.ManageRoles)]
//        [Command("setup", RunMode = RunMode.Async), Summary("Begin setup of Jarvis")]
//        public async Task Setup()
//        {
//            Setter.SetterInstance.NewSetup(Context.Guild);

//            await Context.Channel.SendMessageAsync($"Created a settings entry for **{Context.Guild.Name}** :white_check_mark:");
//            Logger.LoggerInstance.Log("setup", Context.Guild, Context.Channel);
//        }

//        [RequireUserPermission(GuildPermission.ManageRoles)]
//        [RequireBotPermission(GuildPermission.ManageRoles)]
//        [Command("issetup", RunMode = RunMode.Async), Summary("Begin setup of Jarvis")]
//        public async Task IsSetup()
//        {

//            if (Setter.SetterInstance.IsSetup(Context.Guild) == true)
//            {
//                await Context.Channel.SendMessageAsync($"This server has been setup.");
//            }
//            else
//                await Context.Channel.SendMessageAsync($"This server has not been setup.");

//            Logger.LoggerInstance.Log("issetup", Context.Guild, Context.Channel);
//        }


//    }
//}