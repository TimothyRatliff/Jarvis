using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.Modules;
using Discord.Commands;
using Discord.WebSocket;
using Jarvis.addons.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LiteDB;

namespace Jarvis.addons.Settings
{
    class GuildDB
    {
        private static GuildDB DB = new GuildDB();

        public static GuildDB DBInstance
        {
            get { return DB; }
        }

        public class GuildSetting
        {
            public int Id { get; set; }
            public string guildName { get; set; }
            public Boolean isSetup { get; set; }
            public Boolean welcomeEnabled { get; set; }
            public ulong welcomeChannelID { get; set; }
        }

        public void NewSetup(IGuild guild)
        {
            using (var db = new LiteDatabase("./guilds.db"))
            {
                var serverdb = db.GetCollection<GuildSetting>("guildsettings");
                var guildentry = new GuildSetting
                {
                    guildName = guild.Name,
                    isSetup = true
                };

                serverdb.Insert(guildentry);
                serverdb.EnsureIndex(x => x.guildName);
            }
            
        }

        public void DeleteSetup(IGuild guild)
        {
            using (var db = new LiteDatabase("./guilds.db"))
            {
                var serverdb = db.GetCollection<GuildSetting>("guildsettings");
                serverdb.Delete(Query.EQ("guildName", guild.Name));
            }

        }

        public Boolean IsSetup(IGuild guild)
        {
            using (var db = new LiteDatabase("./guilds.db"))
            {
                var serverdb = db.GetCollection<GuildSetting>("guildsettings");
                try
                {
                    var result = (serverdb.Find(Query.EQ("guildName", guild.Name))).Single();
                }
                catch (System.InvalidOperationException)
                {
                    return false;   //if we can't call Single() on result, the entry does not exist
                }
                return true;
            }
        }

        public void ToggleWelcome(IGuild guild, IMessageChannel channel)
        {
            using (var db = new LiteDatabase("./guilds.db"))
            {
                var serverdb = db.GetCollection<GuildSetting>("guildsettings");
                var result = (serverdb.Find(Query.EQ("guildName", guild.Name))).Single();
                if (result.welcomeEnabled == true)
                    result.welcomeEnabled = false;
                if (result.welcomeEnabled == false)
                    result.welcomeEnabled = true;
                result.welcomeChannelID = channel.Id;
                serverdb.Update(result);
            }

        }


    }
}

