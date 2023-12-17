using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Base.Tool.Sheet
{
    [Serializable]
    public class SheetUrl
    {
        [InlineButton(nameof(OpenOriginalSheet), "Open")]
        public string originalUrl;

        [InlineButton(nameof(OpenDeploySheet), "Open")]
        public string deployUrl;

        private void OpenOriginalSheet() => Application.OpenURL(originalUrl);
        private void OpenDeploySheet() => Application.OpenURL(deployUrl);
    }
}