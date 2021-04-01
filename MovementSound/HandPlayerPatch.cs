using HarmonyLib;
using UnityEngine;

namespace MovementSound
{
    [HarmonyPatch(typeof(VRRig), "PlayHandTap")]
    public class HandPlayerPatch
    {
        public static AudioClip SoundClip;
        public static bool Enabled = true;

        public static bool Prefix(bool isLeftHand, float tapVolume, VRRig __instance)
        {
            if (!Enabled) return true;

            if (SoundClip == null)
            {
                Debug.LogError("Sound is null");
                return false;
            }

            if (isLeftHand)
            {
                __instance.leftHandPlayer.volume = tapVolume;
                __instance.leftHandPlayer.PlayOneShot(SoundClip);
                return false;
            }
            __instance.rightHandPlayer.volume = tapVolume;
            __instance.rightHandPlayer.PlayOneShot(SoundClip);
            return false;
        }
    }
}