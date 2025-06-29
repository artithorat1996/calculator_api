namespace API.Models
{
    public class UpdateInsurerDetails
    {
        public decimal Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? Dob { get; set; }

        public decimal? Mobile { get; set; }
    }
}
