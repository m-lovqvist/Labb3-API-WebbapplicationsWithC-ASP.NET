using System.ComponentModel.DataAnnotations;

namespace Labb3_API_WebbapplicationsWithC_ASP.NET.Models
{
    public class Link
    {
        public int Id { get; set; }
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Website requires a minimum of 3 and a maximum of 150 characters")]
        public string Website { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
