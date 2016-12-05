using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Modules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    class MyBot
    {
        DiscordClient discord;
        public MyBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.MessageReceived += async (s, e) =>
            {
                if (!e.Message.IsAuthor)
                {
                    if(e.Message.Text.ToString() == "~ping")
                    {
                        await e.Channel.SendMessage("pong. I don't understand the point of this exercise.");
                    }
                    else
                        await e.Channel.SendMessage("I seem to be *malfunctioning* again Sir...");
                }
            };

            discord.ExecuteAndWait(async () => {
                await discord.Connect("", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
