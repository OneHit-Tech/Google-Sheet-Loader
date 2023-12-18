using System.IO;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;

namespace Base.Editor.GoogleSheet
{
    public class SheetConfig : ScriptableObject
    {
        [ShowInInspector]
        private const string folderPath = "Assets/Resources";

        [Space]
        public List<SheetUrl> sheetUrls;


        [MenuItem("Base/Editor/Google Sheet/Open Config")]
        public static SheetConfig GetInstance()
        {
            var instance = Resources.Load<SheetConfig>(nameof(SheetConfig));
            var assetPath = Path.Combine(folderPath, $"{nameof(SheetConfig)}.asset");

            if (instance == null)
            {
                Directory.CreateDirectory(folderPath);
                instance = CreateInstance<SheetConfig>();
                AssetDatabase.CreateAsset(instance, assetPath);
                AssetDatabase.SaveAssets();

                Debug.Log("Created " + nameof(SheetConfig).Color("yellow") + " in " + folderPath.Color("cyan"));
            }

            Selection.activeObject = instance;
            return instance;
        }
    }
}