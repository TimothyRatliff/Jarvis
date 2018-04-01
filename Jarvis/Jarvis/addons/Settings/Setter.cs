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


//        public class serverDB
//        {
//            [JsonProperty("DBEntries")]
//            public Dictionary<string, GuildSetting> GuildSettings { get; set; }
//        }

//        public class GuildSetting
//        {
//            [JsonProperty("guildName")]
//            public string guildName { get; set; }
//            [JsonProperty("isSetup")]
//            public Boolean isSetup { get; set; }
//        }


//        public void NewSetup(IGuild guild)
//        {
//            using (FileStream fs = File.Open("./serverdb.json", FileMode.Append, FileAccess.Write))
//            using (StreamWriter sw = new StreamWriter(fs))
//            using (JsonWriter writer = new JsonTextWriter(sw))
//            {

//                writer.Formatting = Formatting.Indented;

//                writer.WriteStartObject();
//                writer.WritePropertyName("guildName");
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
//            {
//                //JObject serverdb = (JObject)JToken.ReadFrom(reader);

//                string json = file.ReadToEnd();
//                Console.WriteLine(json);
                

//                var serverdb = JsonConvert.DeserializeObject<GuildSetting>(json);
//                //var serverDBList = serverdb.GuildSettingEntries.ToList();

//                //var serverdb = JsonConvert.DeserializeObject<List<GuildSetting>>(json);
//                //Console.WriteLine("Trying to print the first guild name");
//                //Console.WriteLine($"name: {serverdb.guildName} issetup:{serverdb.isSetup.ToString()}");

//                //Console.WriteLine(serverdb.First().GetValue("Guild Name").ToString());

//                //string guildname = (string)serverdb["Guild Name"][guild.Name];
//                //Console.WriteLine(serverdb.ToString());
//                //if (serverdb.GetValue("Guild Name").ToString() == guild.Name)
//                //    return true;
//                //return false;

//                Console.WriteLine($"Printing serverdb: {serverdb.ToString()}");
//                return false;

//                //foreach (GuildSetting obj in serverDBList)
//                //{
//                //    if (obj.guildName == guild.Name)
//                //        return true;
//                //}
//                //return false;
//                //if (serverdb.guildName == guild.Name)
//                //    return true;
//                //else
//                //    return false;

//            }

//        }
//    }
//}
