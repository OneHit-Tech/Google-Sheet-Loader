using UnityEditor;
using Newtonsoft.Json;
using System.Diagnostics;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using Debug = UnityEngine.Debug;

namespace Base.Editor.GoogleSheet
{
    public static class SheetLoader
    {
        [MenuItem("Base/Editor/Google Sheet/Fetch Data")]
        public static async void FetchDataFromSheet()
        {
            var sheetConfig = SheetConfig.GetInstance();

            Debug.Log("Start fetching data ...".Color("orange"));

            foreach (var sheetUrl in sheetConfig.sheetUrls)
            {
                // skip fetch sheet
                if (!sheetUrl.enabledFetch)
                {
                    Debug.LogWarning($"Skip fetch sheet {sheetConfig.sheetUrls.IndexOf(sheetUrl)}");
                    continue;
                }

                // fetch and calculate time
                var stopwatch = Stopwatch.StartNew();
                var sheetObjectJson = await GetDataAsync(sheetUrl.deployUrl);
                stopwatch.Stop();
                Debug.Log($"Fetched sheet {sheetConfig.sheetUrls.IndexOf(sheetUrl)} in {stopwatch.Elapsed.TotalSeconds}s"
                          + "\n" + sheetObjectJson);

                // invoke action load data into game
                var sheetData = JsonConvert.DeserializeObject<SheetData>(sheetObjectJson);
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