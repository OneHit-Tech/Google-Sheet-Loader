using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Tool.Sheet.Sample
{
    public class ItemUI : MonoBehaviour
    {
        [Header("Color")]
        public Image background;
        public Color attackItemColor;
        public Color defenseItemColor;

        [Header("Info")]
        public TextMeshProUGUI idText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI quantityText;
        public TextMeshProUGUI typeText;

        public void Setup(ItemData itemData)
        {
            idText.text = "ID: " + itemData.id;
            nameText.text = "Name: " + itemData.name;
            quantityText.text = "Quantity: " + itemData.quantity;
            typeText.text = "Type: " + itemData.type;

            // set color by EItemType
            background.color = itemData.type == EItemType.Attack
                ? attackItemColor
                : defenseItemColor;
        }
    }
}