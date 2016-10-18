using Discord;
using Discord.Commands;

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
                    await e.Channel.SendMessage("Hello Sir, glad to be here.");
            };

            discord.ExecuteAndWait(async () => {
                await discord.Connect("MjM2MDEzMTYwMjI4NzE2NTQ0.CuYCvw.mFXFZGJm8QdXlayFtcN6WiBqQLw", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
