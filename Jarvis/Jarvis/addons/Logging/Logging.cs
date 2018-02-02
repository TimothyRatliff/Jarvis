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
    class Logging
    {
        public void Log(String msg)
        {
            Console.WriteLine($"{DateTime.Now.ToString()} {msg}");
        }
        
    }
}
