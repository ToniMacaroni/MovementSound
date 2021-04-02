using BepInEx;
using System;
using System.IO;
using UnityEngine;

namespace MovementSound
{
    [BepInPlugin(PluginInfo.PLUGIN_ID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly HarmonyPatcher _harmony = new HarmonyPatcher(PluginInfo.PLUGIN_ID);

        private void Awake()
        {
            Load();
        }

        private async void Load()
        {
            var file = new FileInfo(@"BepInEx\plugins\MovementSound\Sound.wav");

            if (!file.Exists)
            {
                Debug.LogError($"[{nameof(MovementSound)}] Sound.wav was not found");
                return;
            }

            MovementSound.Config.Get();
            HandPlayerPatch.SoundClip = await SoundLoader.Get().LoadClip(file.FullName);
            _harmony.Patch();
        }

        private void OnEnable()
        {
            HandPlayerPatch.Enabled = true;
        }

        private void OnDisable()
        {
            HandPlayerPatch.Enabled = false;
        }
    }
}
