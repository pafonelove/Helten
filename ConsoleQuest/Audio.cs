using GameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleQuest
{
    internal class Audio
    {
        // Create an object of the SoundPlayer class responsible for loading music in .wav format (requires System.Windows.Extensions 7.0.0 to be included.)
        public static SoundPlayer player = new SoundPlayer();

        // Play music method.
        public static void PlayMusic([CallerMemberName] string? callerMemberName = null) // callerMemberName is using for saving method name, which called PlayMusic method
        {
            string? location = callerMemberName; // save location name, wherein need to play music
            string folder = "audio/";
            if (location == "BattleZone")
            {
                player.SoundLocation = folder + @"battle.wav";   // path to .wav file
                player.PlayLooping();   // loop music playing
            }
            else if (location == "ShowLore")
            {
                player.SoundLocation = folder + @"lore.wav";
                player.PlayLooping();
            }
            else if (location == "Market")
            {
                player.SoundLocation = folder + @"market.wav";
                player.PlayLooping();
            }
            else if (location == "Doctor")
            {
                player.SoundLocation = folder + @"doctor.wav";
                player.PlayLooping();
            }
            else
            {
                player.SoundLocation = folder + @"main.wav";
                player.PlayLooping();
            }
        }

        public static void PlaySFX(string sfx)
        {
            string folder = "audio/";
            if (sfx.ToLower() == "victory")
            {
                player.SoundLocation = folder + @"victory.wav";   // path to .wav file
                player.PlayLooping();   // loop music playing
            }
            else
            {
                player.SoundLocation = folder + @"defeat.wav";
                player.PlayLooping();
            }
        }

        // Stop music method.
        public static void Stop(string? location)
        {
            string folder = "audio/";
            player.Stop();
            if (location != "BattleZone")
            {
                player.SoundLocation = folder + @"main.wav";
                player.PlayLooping();
            }
        }
    }
}
