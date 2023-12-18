using System;
using System.Collections.Generic;
using Base.Tool.GoogleSheet.Example;

namespace Base.Tool.GoogleSheet
{
    [Serializable]
    public class GGSheetData
    {
        public List<ItemInSheet> Item;
        public List<BuffInSheet> Buff;

        // TODO: declare more List<...InSheet> in here, with `field name` is `sheet name`
    }
}