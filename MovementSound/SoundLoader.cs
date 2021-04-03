using System;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace MovementSound
{
    public class SoundLoader
    {
        public static async Task<AudioClip> LoadClip(string path)
        {
            using UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);

            var tcs = new TaskCompletionSource<bool>();
            var op = request.SendWebRequest();
            op.completed += ao => { tcs.SetResult(true); };

            try
            {
                await tcs.Task;

                if (request.isNetworkError || request.isHttpError) Debug.LogError($"{request.error}");
                else
                {
                    return DownloadHandlerAudioClip.GetContent(request);
                }
            }
            catch (Exception err)
            {
                Debug.LogError($"{err.Message}, {err.StackTrace}");
            }

            return null;
        }
    }
}