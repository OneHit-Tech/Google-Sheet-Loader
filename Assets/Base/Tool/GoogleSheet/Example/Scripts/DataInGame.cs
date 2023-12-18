using System;

namespace Base.Tool.GoogleSheet.Example
{
    public enum EItemType
    {
        Attack = 1,
        Defense = 2,
    }

    [Serializable]
    public class ItemData
    {
        public int id;
        public string name;
        public int quantity;
        public EItemType type;

        public ItemData(ItemInSheet itemInSheet)
        {
            id = itemInSheet.ID;
            name = itemInSheet.Name;
            quantity = itemInSheet.Quantity;
            type = (EItemType)itemInSheet.Type;
        }
    }

    [Serializable]
    public class BuffData
    {
        public int id;
        public string name;
        public float value;
        public int quantity;

        public BuffData(BuffInSheet buffInSheet)
        {
            id = buffInSheet.ID;
            name = buffInSheet.Name;
            value = buffInSheet.Value;
            quantity = buffInSheet.Quantity;
        }
    }
}