namespace Labb3_API_WebbapplicationsWithC_ASP.NET.Models
{
    public class PersonInterest
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
