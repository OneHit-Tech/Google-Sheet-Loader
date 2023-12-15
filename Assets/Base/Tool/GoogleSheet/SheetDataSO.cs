#if UNITY_EDITOR
using UnityEngine;
using Newtonsoft.Json;
using System.Diagnostics;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using Base.Tool.Sheet.Sample;
using Sirenix.OdinInspector;

namespace Base.Tool.Sheet
{
    [CreateAssetMenu(fileName = "SheetManagerSO", menuName = "Sheet/SheetManagerSO")]
    public class SheetDataSO : ScriptableObject
    {
        [Space] public GameDataSO gameDataSO;
        [Space] public List<SheetUrl> sheetUrls = new();


        [PropertySpace]
        [GUIColor(0f, 0.8f, 0.4f)]
        [Button(ButtonSizes.Large)]
        private async void FetchData()
        {
            var sheetDataList = new List<SheetData>();

            Debug.Log($"Fetching data ...".Color("orange"));
            foreach (var sheetUrl in sheetUrls)
            {
                var sheetObjectJson = await GetDataAsync(sheetUrl.deployUrl);
                var sheetData = JsonConvert.DeserializeObject<SheetData>(sheetObjectJson);
                sheetDataList.Add(sheetData);
            }

            Debug.Log("Fetch data complete!".Color("lime"));


            // * EXAMPLE: load data to game
            var dataGameInSheet = sheetDataList[0];
            gameDataSO.LoadDataFromSheet(dataGameInSheet);
        }


        private static async UniTask<string> GetDataAsync(string url)
        {
            var webRequest = UnityWebRequest.Get(url);
            webRequest.timeout = 30; // time out after 30 seconds

            // await fetch & calculate time
            var stopwatch = Stopwatch.StartNew();
            await webRequest.SendWebRequest();
            stopwatch.Stop();

            Debug.Log($"Finish in {stopwatch.Elapsed.TotalSeconds}s".Color("cyan")
                      + "\n" + webRequest.downloadHandler.text);

            return webRequest.downloadHandler.text;
        }
    }
}
#endif