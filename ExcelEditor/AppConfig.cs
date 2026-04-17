using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelEditor
{
    public class AppConfig
    {
        public int MaxGreatestHitsCounter { get; set; } = 500;
        public int ColumnsCounter { get; set; } = 5;
        public bool SaveToExcelInstantly { get; set; }
    }
}
