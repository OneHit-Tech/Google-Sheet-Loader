using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Base.Tool.Sheet.Sample
{
    public class Panel : MonoBehaviour
    {
        [Title("Item")]
        public GameObject uiItemPrefab;
        public Transform itemContent;
        public GameObject itemScrollView;
        public Button itemTabButton;

        [Title("Buff")]
        public GameObject uiBuffPrefab;
        public Transform buffContent;
        public GameObject buffScrollView;
        public Button buffTabButton;

        private void Start()
        {
            PrepareItems();
            PrepareBuffs();
            OpenItemScrollView();
        }

        private void PrepareItems()
        {
            // clear item in scene
            foreach (Transform child in itemContent)
                Destroy(child.gameObject);

            // instantiate new item
            var itemDataSet = GameDataSO.Instance.itemDataSet;
            foreach (var itemData in itemDataSet)
            {
                var uiItem = Instantiate(uiItemPrefab, itemContent).GetComponent<ItemUI>();
                uiItem.Setup(itemData);
            }
        }

        private void PrepareBuffs()
        {
            foreach (Transform child in buffContent)
                Destroy(child.gameObject);

            var buffDataSet = GameDataSO.Instance.buffDataSet;
            foreach (var buffData in buffDataSet)
            {
                var uiBuff = Instantiate(uiBuffPrefab, buffContent).GetComponent<BuffUI>();
                uiBuff.Setup(buffData);
            }
        }

        public void OpenItemScrollView()
        {
            // show item scroll view & highlight item tab button
            itemScrollView.SetActive(true);
            itemTabButton.image.color = Color.white;

            // hide buff scroll view & unhighlight buff tab button
            buffScrollView.SetActive(false);
            buffTabButton.image.color = new Color(0.8f, 0.8f, 0.8f);
        }

        public void OpenBuffScrollView()
        {
            itemScrollView.SetActive(false);
            itemTabButton.image.color = new Color(0.8f, 0.8f, 0.8f);

            buffScrollView.SetActive(true);
            buffTabButton.image.color = Color.white;
        }
    }
}