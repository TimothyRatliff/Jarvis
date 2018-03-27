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

namespace Jarvis.addons.Settings
{
    class Setter
    {
        private static Setter setter = new Setter();

        //private FileStream outstrm;


        public static Setter SetterInstance
        {
            get { return setter; }
        }

        public void NewSetup(IGuild guild)
        {
            using (FileStream fs = File.Open("./serverdb.json", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                //writer.WriteStartObject();
                //writer.WritePropertyName("Guild Name");
                //writer.WriteValue(guild.Name);
                ////writer.WritePropertyName("PSU");
                ////writer.WriteValue("500W");
                ////writer.WritePropertyName("Drives");
                ////writer.WriteStartArray();
                ////writer.WriteValue("DVD read/writer");
                ////writer.WriteComment("(broken)");
                ////writer.WriteValue("500 gigabyte hard drive");
                ////writer.WriteValue("200 gigabype hard drive");
                ////writer.WriteEnd();
                //writer.WriteEndObject();

                //JsonSerializer serializer = new JsonSerializer();
                //serializer.Serialize(writer, guild.Name);
            }

        }

        //public async Boolean IsSetup(IGuild guild)
        //{
        //    using (FileStream fs = File.Open("./serverdb.json", FileMode.Append, FileAccess.Write))
        //    using (StreamReader sr = new StreamReader(fs))
        //    using (JsonReader reader = new JsonTextReader(sr)
        //    {
        //        reader.

        //    }
        //}
    }
}
