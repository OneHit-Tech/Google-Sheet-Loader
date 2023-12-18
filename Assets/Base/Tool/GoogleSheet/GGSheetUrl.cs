using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Base.Tool.GoogleSheet
{
    [Serializable]
    public class GGSheetUrl
    {
        [InfoBox("<size=12>This sheet will not be fetched</size>", InfoMessageType.Warning, "@!enabledFetch")]
        public bool enabledFetch = true;

        [HideIf("@!enabledFetch")]
        [InlineButton(nameof(OpenOriginalSheet), "Open")]
        public string originalUrl;

        [HideIf("@!enabledFetch")]
        [InlineButton(nameof(OpenDeploySheet), "Open")]
        public string deployUrl;

        [HideIf("@!enabledFetch")]
        public UnityEvent<GGSheetData> onFetchedData;

        private void OpenOriginalSheet() => Application.OpenURL(originalUrl);
        private void OpenDeploySheet() => Application.OpenURL(deployUrl);
    }
}