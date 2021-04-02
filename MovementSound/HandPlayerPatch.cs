using HarmonyLib;
using UnityEngine;

namespace MovementSound
{
    [HarmonyPatch(typeof(VRRig), "PlayHandTap")]
    public class HandPlayerPatch
    {
        public static AudioClip SoundClip;
        public static bool Enabled = true;

        private static bool _inited;

        private static void Init(AudioSource leftSource, AudioSource rightSource)
        {
            var config = Config.Get();
            leftSource.pitch = config.LeftPitch.Value;
            rightSource.pitch = config.RightPitch.Value;
            _inited = true;
        }

        public static bool Prefix(bool isLeftHand, float tapVolume, VRRig __instance)
        {
            if (!Enabled) return true;

            if (SoundClip == null)
            {
                return false;
            }

            if (!_inited)
            {
                Init(__instance.leftHandPlayer, __instance.rightHandPlayer);
            }

            if (isLeftHand)
            {
                __instance.leftHandPlayer.volume = tapVolume;
                __instance.leftHandPlayer.pitch = 1.3f;
                __instance.leftHandPlayer.PlayOneShot(SoundClip);
                return false;
            }
            __instance.rightHandPlayer.volume = tapVolume;
            __instance.rightHandPlayer.PlayOneShot(SoundClip);
            return false;
        }
    }
}