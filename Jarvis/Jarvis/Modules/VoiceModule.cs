//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Discord;
//using Discord.Modules;
//using Discord.Commands;
//using Discord.WebSocket;
//using Discord.Audio;

//namespace Jarvis.Modules
//{
//	public class VoiceModule : ModuleBase
//	{
//		private IAudioClient _aclient;
//		[Command("join", RunMode = RunMode.Async)]
//		public async Task JoinChannel(IVoiceChannel channel = null)
//		{
//			// Get the audio channel
//			channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
//			if (channel == null) { await Context.Message.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

//			// For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
//			var audioClient = await channel.ConnectAsync();
//			Console.WriteLine(DateTime.Now.ToString() + "	JoinChannel | Guild: " +channel.Guild.Name+" | Channel: " + channel.Name + "");
//		}

//		[Command("leave")]
//		public async Task LeaveChannel(IVoiceChannel channel = null)
//		{
//			// Get the audio channel
//			channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
//			if (channel == null) { await Context.Message.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }
//			_aclient = await channel.ConnectAsync();
//			await _aclient.StopAsync();

//			Console.WriteLine(DateTime.Now.ToString() + "	LeaveChannel | Guild: " + channel.Guild.Name + " | Channel: " + channel.Name + "");
//		}


//	}
//}
