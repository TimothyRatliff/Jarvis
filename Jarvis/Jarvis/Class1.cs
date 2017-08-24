using Discord;
using System;

namespace Jarvis
{
	public class Class1
	{
		static void Main(string[] args) => new Class1().Start(args);
		Program jarvis = new Program();

		private const String AppName = "Jarvis";

		private const String AppLink = "https://github.com/TimothyRatliff/Jarvis";

		private void Start(string[] args)
		{
#if !DNXCORE50
			Console.Title = $"{AppName} (Discord.Net v{DiscordConfig.APIVersion})";
#endif
		}
	}
}
