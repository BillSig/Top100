using ExcelEditorLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelEditorLibrary.Models
{
    public class GreatestHitModel : IHasSearchableFilePath
    {
        public int Position { get; set; }
        public string BandName { get; set; }
        public string SongTitle { get; set; }
        public string VideoLink { get; set; }
        public string FilePath { get; set; }
        public bool IsLocked { get; set; }

        public IEnumerable<string> GetSearchTokens()
        {
            return new[] { BandName, SongTitle };
        }

        public void OnFilePathMatched()
        {
            IsLocked = true;
        }
    }
}
