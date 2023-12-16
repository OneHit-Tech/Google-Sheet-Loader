#if UNITY_EDITOR
using UnityEngine;
using Newtonsoft.Json;
using System.Diagnostics;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Base.Tool.Sheet
{
    [CreateAssetMenu(fileName = "SheetManagerSO", menuName = "Sheet/SheetManagerSO")]
    public class SheetDataSO : ScriptableObject
    {
        public List<SheetUrl> sheetUrls = new();

        [PropertySpace]
        [GUIColor(0f, 0.8f, 0.4f)]
        [Button(ButtonSizes.Large)]
        private async void FetchDataFromSheet()
        {
            Debug.Log("Start fetching data ...".Color("orange"));
            foreach (var sheetUrl in sheetUrls)
            {
                var sheetObjectJson = await GetDataAsync(sheetUrl.deployUrl);
                var sheetData = JsonConvert.DeserializeObject<SheetData>(sheetObjectJson);
                sheetUrl.onFetchedData.Invoke(sheetData);
            }

            Debug.Log("Fetch data complete!".Color("lime"));
        }


        private static async UniTask<string> GetDataAsync(string url)
        {
            var webRequest = UnityWebRequest.Get(url);
            webRequest.timeout = 30; // time out after 30 seconds

            // await fetch & calculate time
            var stopwatch = Stopwatch.StartNew();
            await webRequest.SendWebRequest();
            stopwatch.Stop();

            Debug.Log($"End request after {stopwatch.Elapsed.TotalSeconds}s".Color("cyan")
                      + "\n" + webRequest.downloadHandler.text);

            return webRequest.downloadHandler.text;
        }
    }
}
#endif