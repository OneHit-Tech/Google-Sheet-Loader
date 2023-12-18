#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

namespace Base.Tool.GoogleSheet
{
    public class GGSheetLoader : ScriptableObject
    {
        [ShowInInspector]
        private const string folderPath = "Assets/Resources";

        [Space] public List<GGSheetUrl> sheetUrls;


        [MenuItem("Base/Tool/Google Sheet/Open Config")]
        public static GGSheetLoader GetInstance()
        {
            var instance = Resources.Load<GGSheetLoader>(nameof(GGSheetLoader));
            var assetPath = Path.Combine(folderPath, $"{nameof(GGSheetLoader)}.asset");

            if (instance == null)
            {
                Directory.CreateDirectory(folderPath);
                instance = CreateInstance<GGSheetLoader>();
                AssetDatabase.CreateAsset(instance, assetPath);
                AssetDatabase.SaveAssets();

                Debug.Log("Created " + nameof(GGSheetLoader).Color("yellow") + " in " + folderPath.Color("cyan"));
            }

            Selection.activeObject = instance;
            return instance;
        }


        [MenuItem("Base/Tool/Google Sheet/Fetch Data")]
        public static async void FetchDataFromSheet()
        {
            Debug.Log("Start fetching data ...".Color("orange"));
            var instance = GetInstance();

            foreach (var sheetUrl in instance.sheetUrls)
            {
                // skip fetch sheet
                if (!sheetUrl.enabledFetch)
                {
                    Debug.LogWarning($"Skip fetch sheet {instance.sheetUrls.IndexOf(sheetUrl)}");
                    continue;
                }

                // fetch and calculate time
                var stopwatch = Stopwatch.StartNew();
                var sheetObjectJson = await GetDataAsync(sheetUrl.deployUrl);
                stopwatch.Stop();
                Debug.Log($"Fetched sheet {instance.sheetUrls.IndexOf(sheetUrl)} in {stopwatch.Elapsed.TotalSeconds}s"
                          + "\n" + sheetObjectJson);

                // invoke action load data into game
                var sheetData = JsonConvert.DeserializeObject<GGSheetData>(sheetObjectJson);
                sheetUrl.onFetchedData.Invoke(sheetData);
            }

            Debug.Log("Fetch data completed!".Color("lime"));
        }


        private static async UniTask<string> GetDataAsync(string url)
        {
            var webRequest = UnityWebRequest.Get(url);
            webRequest.timeout = 30; // time out after 30 seconds
            await webRequest.SendWebRequest();
            return webRequest.downloadHandler.text;
        }
    }
}
#endif