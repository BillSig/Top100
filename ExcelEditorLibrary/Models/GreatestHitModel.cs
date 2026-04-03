using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelEditorLibrary.Models
{
    public class GreatestHitModel
    {
        public int Position { get; set; }
        public string BandName { get; set; }
        public string SongTitle { get; set; }
        public string VideoLink { get; set; }
        public bool IsViewed { get; set; }
    }
}
