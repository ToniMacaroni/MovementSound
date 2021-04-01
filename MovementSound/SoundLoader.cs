using System;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace MovementSound
{
    public class SoundLoader : IDisposable
    {
        public static SoundLoader Instance;

        private AssetBundle _assetBundle;

        public static SoundLoader Get()
        {
            if (Instance == null)
            {
                Instance = new SoundLoader();
                //Instance.Load();
            }

            return Instance;
        }

        private SoundLoader()
        {
            
        }

        public void Load()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MovementSound.Resources.Assets");
            var data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            _assetBundle = AssetBundle.LoadFromMemory(data);
        }

        public T LoadAsset<T>(string name) where T : Object
        {
            return _assetBundle.LoadAsset<T>(name);
        }

        public void Dispose()
        {
            Instance = null;
            if (_assetBundle != null)
            {
                _assetBundle.Unload(true);
            }
        }

        public async Task<AudioClip> LoadClip(string path)
        {
            AudioClip clip = null;
            using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
            {
                uwr.SendWebRequest();

                try
                {
                    while (!uwr.isDone) await Task.Delay(5);

                    if (uwr.isNetworkError || uwr.isHttpError) Debug.Log($"{uwr.error}");
                    else
                    {
                        clip = DownloadHandlerAudioClip.GetContent(uwr);
                    }
                }
                catch (Exception err)
                {
                    Debug.LogError($"{err.Message}, {err.StackTrace}");
                }
            }

            return clip;
        }
    }
}