using UnityEngine;
using Newtonsoft.Json;
using System.Diagnostics;
using UnityEngine.Events;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using Debug = UnityEngine.Debug;
using Sirenix.OdinInspector;

namespace Base.Tool.Sheet
{
    [CreateAssetMenu(fileName = "SheetLoader", menuName = "Sheet/SheetLoader")]
    public class SheetLoader : ScriptableObject
    {
        [Space] public SheetUrl sheetUrl;
        [Space] public UnityEvent<SheetData> onFetchedData;


        [PropertySpace]
        [GUIColor(0f, 0.8f, 0.4f)]
        [Button(ButtonSizes.Large)]
        private async void FetchDataFromSheet()
        {
            Debug.Log("Start fetching data ...".Color("orange"));

            var sheetObjectJson = await GetDataAsync(sheetUrl.deployUrl);
            var sheetData = JsonConvert.DeserializeObject<SheetData>(sheetObjectJson);
            onFetchedData.Invoke(sheetData);
        }


        private static async UniTask<string> GetDataAsync(string url)
        {
            var webRequest = UnityWebRequest.Get(url);
            webRequest.timeout = 30; // time out after 30 seconds

            // await fetch & calculate time
            var stopwatch = Stopwatch.StartNew();
            await webRequest.SendWebRequest();
            stopwatch.Stop();

            Debug.Log($"Fetch completed in {stopwatch.Elapsed.TotalSeconds}s".Color("lime")
                      + "\n" + webRequest.downloadHandler.text);

            return webRequest.downloadHandler.text;
        }
    }
}