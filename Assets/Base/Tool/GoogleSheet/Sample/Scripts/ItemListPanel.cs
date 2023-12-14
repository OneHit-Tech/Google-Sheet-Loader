using UnityEngine;

namespace Base.Tool.Sheet.Sample
{
    public class ItemListPanel : MonoBehaviour
    {
        public GameObject uiItemPrefab;
        public Transform content;

        private void Start()
        {
            DestroyCurrentItems();
            ShowAllItems();
        }

        private void DestroyCurrentItems()
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
        }

        private void ShowAllItems()
        {
            var itemDataSet = GameDataSO.Instance.itemDataSet;

            foreach (var itemData in itemDataSet)
            {
                var uiItem = Instantiate(uiItemPrefab, content).GetComponent<ItemUI>();
                uiItem.Setup(itemData);
            }
        }
    }
}