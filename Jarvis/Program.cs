using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Modules;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    public class Program
    {
        static void Main(string[] args) => new Program().Start(args);

        MyBot bot = new MyBot();

        private const String AppName = "Jarvis";

        private const String AppLink = "https://github.com/TimothyRatliff/Jarvis";

        private void Start(string[] args)
        {
#if !DNXCORE50
            Console.Title = $"{AppName} (Discord.Net v{DiscordConfig.LibVersion})";
#endif
        }
    }
}
