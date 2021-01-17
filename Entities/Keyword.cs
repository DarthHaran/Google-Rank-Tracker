namespace GRT.Entities
{
    public class Keyword
    {
        public int Id { get; set; }
        public string KeywordName { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
