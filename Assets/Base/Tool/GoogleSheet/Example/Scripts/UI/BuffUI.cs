using TMPro;
using UnityEngine;

namespace Base.Tool.GoogleSheet.Example
{
    public class BuffUI : MonoBehaviour
    {
        public TextMeshProUGUI idText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI valueText;
        public TextMeshProUGUI quantityText;

        public void Setup(BuffData itemData)
        {
            idText.text = "ID: " + itemData.id;
            nameText.text = "Name: " + itemData.name;
            valueText.text = "Value: " + itemData.value;
            quantityText.text = "Quantity: " + itemData.quantity;
        }
    }
}