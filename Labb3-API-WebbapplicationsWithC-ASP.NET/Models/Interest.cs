using System.ComponentModel.DataAnnotations;

namespace Labb3_API_WebbapplicationsWithC_ASP.NET.Models
{
    public class Interest
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title requires a minimum of 3 and a maximum of 100 characters")]
        public string Title { get; set; }
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Description requires a minimum of 3 and a maximum of 250 characters")]
        public string Description { get; set; }
        public ICollection<Link> Links { get; set; } = [];
        public ICollection<PersonInterest> PersonInterests { get; set; } = [];
    }
}
