using System;

namespace GRT.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public DateTime Date { get; set; }
        public int KeywordId { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
