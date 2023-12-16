using System;
using System.Collections.Generic;
using Base.Tool.Sheet.Example;

namespace Base.Tool.Sheet
{
    [Serializable]
    public class SheetData
    {
        public List<ItemInSheet> Item;
        public List<BuffInSheet> Buff;

        // TODO: declare more List<...InSheet> in here, with `field name` is `sheet name`
    }
}