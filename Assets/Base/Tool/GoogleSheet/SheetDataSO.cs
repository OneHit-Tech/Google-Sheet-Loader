using UnityEngine;
using System.Collections.Generic;
using Base.Tool.Sheet.Sample;

namespace Base.Tool.Sheet
{
    [CreateAssetMenu(fileName = "SheetManagerSO", menuName = "Sheet/SheetManagerSO")]
    public class SheetDataSO : ScriptableObject
    {
        [Space] public GameDataSO gameDataSO;
        [Space] public List<SheetUrl> sheetUrls = new();
    }
}