using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRT.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public DateTime Date { get; set; }
        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
