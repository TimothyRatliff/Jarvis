using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Audio;
using Discord.Commands;

namespace Jarvis.addons.Logging
{
    class Logger
    {
        private static Logger logger = new Logger();

        private FileStream outstrm;
        private StreamWriter writer;

        public static Logger LoggerInstance
        {
            get { return logger;  }
        }

        public void Log(String command, IGuild guild, IChannel channel)
        {
            Console.WriteLine($"{DateTime.Now.ToString()} | {command.ToString()} | {guild.Name} | {channel.Name}");
            try
            {
                outstrm = new FileStream("./log.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(outstrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open log.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            writer.WriteLine($"{DateTime.Now.ToString()} | {command} | {guild.Name} | {channel.Name}"); ;
            writer.Close();
            outstrm.Close();
        }

        public void LogInfo(String command, IGuild guild, IChannel channel, String info)
        {
            Console.WriteLine($"{DateTime.Now.ToString()} | {command.ToString()} | {guild.Name} | {channel.Name} | {info}");
            try
            {
                outstrm = new FileStream("./log.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(outstrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open log.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            writer.WriteLine($"{DateTime.Now.ToString()} | {command} | {guild.Name} | {channel.Name} | {info}"); ;
            writer.Close();
            outstrm.Close();
        }



    }
}
