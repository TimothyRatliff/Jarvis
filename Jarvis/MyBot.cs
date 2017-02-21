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
                    if(e.Message.Text.ToString() == "~time")
                    {
                        await e.Channel.SendMessage("The current Central time is: " + DateTime.Now.ToString("h:mm:ss tt") + "");
                    }
                    //if (e.Message.Text.ToString() == "~clear 5")
                    //{

                    //}
                    //else
                    //    await e.Channel.SendMessage("I seem to be *malfunctioning* again Sir...");
                }
            };
            discord.ExecuteAndWait(async () => {
                await discord.Connect("MjM2MDEzMTYwMjI4NzE2NTQ0.C44mtw.a6AiYLpmSNY4wj8ALiTRoYDBbes", TokenType.Bot);
                discord.SetGame("mind games");
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
