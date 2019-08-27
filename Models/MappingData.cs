using System;
using System.Collections.Generic;
using System.Text;

namespace magazzinoApp.Models
{
    class MappingData
    {
        public MappingData(string sourceWorkSheet, string shelfName, int rowBox, int colBox, string startOfRangeBox, string endOfRangeBox)
        {
            SourceWorkSheet = sourceWorkSheet;
            ShelfName = shelfName;
            RowBox = rowBox;
            ColBox = colBox;
            StartOfRangeBox = startOfRangeBox;
            EndOfRangeBox = endOfRangeBox;
        }

        public string SourceWorkSheet { get;  }
        public string ShelfName { get;  }
        public int RowBox { get;  }
        public int ColBox { get;  }
        public string StartOfRangeBox { get;  }
        public string EndOfRangeBox { get;  }

       
    }
}
