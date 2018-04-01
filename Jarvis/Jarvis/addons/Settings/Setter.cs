//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using System.IO;
//using Discord;
//using Discord.Modules;
//using Discord.Commands;
//using Discord.WebSocket;
//using Jarvis.addons.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace Jarvis.addons.Settings
//{
//    class Setter
//    {
//        private static Setter setter = new Setter();

//        //private FileStream outstrm;


//        public static Setter SetterInstance
//        {
//            get { return setter; }
//        }


//        public class GuildSetting
//        {
//            public string name;
//            public string isSetup;
//        }


//        public void NewSetup(IGuild guild)
//        {
//            using (FileStream fs = File.Open("./serverdb.json", FileMode.Append, FileAccess.Write))
//            using (StreamWriter sw = new StreamWriter(fs))
//            using (JsonWriter writer = new JsonTextWriter(sw))
//            {
//                writer.Formatting = Formatting.Indented;

//                writer.WriteStartObject();
//                writer.WritePropertyName("Guild Name");
//                writer.WriteValue(guild.Name);
//                writer.WritePropertyName("isSetup");
//                writer.WriteValue(true);
//                ////writer.WritePropertyName("PSU");
//                ////writer.WriteValue("500W");
//                ////writer.WritePropertyName("Drives");
//                ////writer.WriteStartArray();
//                ////writer.WriteValue("DVD read/writer");
//                ////writer.WriteComment("(broken)");
//                ////writer.WriteValue("500 gigabyte hard drive");
//                ////writer.WriteValue("200 gigabype hard drive");
//                ////writer.WriteEnd();
//                writer.WriteEndObject();


//                //JsonSerializer serializer = new JsonSerializer();
//                //serializer.Serialize(writer, guild.Name);
//            }

//        }


        

//        public Boolean IsSetup(IGuild guild)
//        {
//            using (StreamReader file = new StreamReader("./serverdb.json"))
//            //using (JsonTextReader reader = new JsonTextReader(file))
//            {
//                //JObject serverdb = (JObject)JToken.ReadFrom(reader);

//                string json = file.ReadToEnd();
//                Console.WriteLine(json);
//                var serverdb = JsonConvert.DeserializeObject<List<JObject>>(json);
//                Console.WriteLine(serverdb.First().GetValue("Guild Name").ToString());
//                //string guildname = (string)serverdb["Guild Name"][guild.Name];
//                //Console.WriteLine(serverdb.ToString());
//                //if (serverdb.GetValue("Guild Name").ToString() == guild.Name)
//                //    return true;
//                //return false;

//                foreach (JObject obj in serverdb)
//                {
//                    if (obj.GetValue("Guild Name").ToString() == guild.Name)
//                        return true;
//                }
//                return false;

//            }

//        }
//    }
//}
