namespace GRT.Entities
{
    public class Keyword
    {
        public int Id { get; set; }
        public string KeywordName { get; set; }
        public string GoogleHost { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CityBase64 { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
