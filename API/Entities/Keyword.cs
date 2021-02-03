using System.ComponentModel.DataAnnotations;

namespace GRT.Entities
{
    public class Keyword
    {
        public int Id { get; set; }
        [Required]
        public string KeywordName { get; set; }
        public string GoogleHost { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
