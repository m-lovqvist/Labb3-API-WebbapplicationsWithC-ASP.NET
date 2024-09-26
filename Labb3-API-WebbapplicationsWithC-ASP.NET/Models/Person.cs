using System.ComponentModel.DataAnnotations;

namespace Labb3_API_WebbapplicationsWithC_ASP.NET.Models
{
    public class Person
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name requires a minimum of 3 and a maximum of 100 characters")]
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number requires 10 digits")]
        public string? PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int Age { get; set; }

        public ICollection<PersonInterest> PersonInterests { get; set; } = [];
    }
}
