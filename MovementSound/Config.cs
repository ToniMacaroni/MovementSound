using System.IO;
using BepInEx;
using BepInEx.Configuration;

namespace MovementSound
{
    internal class Config
    {
        private static Config _instance;

        public ConfigEntry<float> LeftPitch;
        public ConfigEntry<float> RightPitch;

        public static Config Get()
        {
            return _instance ??= new Config();
        }

        public Config()
        {
            _instance = this;
            var file = new ConfigFile(Path.Combine(Paths.ConfigPath, "MovementSound.cfg"), true);
            LeftPitch = file.Bind("Sound Options", "LeftPitch", 1f, "Pitch of the left hand");
            RightPitch = file.Bind("Sound Options", "RightPitch", 1f, "Pitch of the right hand");
        }
    }
}