using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRT.Entities
{
    public class ReportRow
    {
        public string KeywordName { get; set; }
        public int LastPosition { get; set; }
        public int LastMonthsPosition { get; set; }
        public int Difference { get => LastMonthsPosition - LastPosition; }
    }
}
