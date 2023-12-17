using UnityEngine;
using System.Collections.Generic;

namespace Base.Tool.Sheet.Example
{
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "Sheet/GameDataSO")]
    public class GameDataSO : ScriptableObject, IFetchDataInSheet
    {
        #region ===== Singleton =====
        private static GameDataSO _instance;

        public static GameDataSO Instance
        {
            get
            {
                if (_instance == null)
                {
                    // find instance in Resources folder
                    _instance = Resources.Load<GameDataSO>(nameof(GameDataSO));
                }

                return _instance;
            }
        }
        #endregion

        public List<ItemData> itemDataSet = new();
        public List<BuffData> buffDataSet = new();

        public void OnDataFetched(SheetData sheetData)
        {
            Debug.Log("Load data into game".Color("yellow"));

            itemDataSet.Clear();
            sheetData.Item.ForEach(itemInSheet =>
                itemDataSet.Add(new ItemData(itemInSheet))
            );

            buffDataSet.Clear();
            sheetData.Buff.ForEach(buffInSheet =>
                buffDataSet.Add(new BuffData(buffInSheet))
            );
        }
    }
}