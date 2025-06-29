namespace API.Models
{
    public class NewInsurer
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? Dob { get; set; }

        public decimal? Mobile { get; set; }
    }
}
