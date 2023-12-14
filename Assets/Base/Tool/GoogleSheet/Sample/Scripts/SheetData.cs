﻿using System;
using System.Collections.Generic;

namespace Base.Tool.Sheet.Sample
{
    // * Tên trường phải trùng với tên cột trong sheet
    // * Thứ tự các trường phải trùng với thứ tự các cột trong sheet

    [Serializable]
    public class ItemInSheet
    {
        public int ID;
        public string Name;
        public int Quantity;
        public int Type; // * 1: Attack, 2: Defense 
    }

    [Serializable]
    public class BuffInSheet
    {
        public int ID;
        public string Name;
        public float Value;
        public int Quantity;
    }

    [Serializable]
    public class SheetData
    {
        public List<ItemInSheet> Item;
        public List<BuffInSheet> Buff;
    }
}