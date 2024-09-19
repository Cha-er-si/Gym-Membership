namespace GymMembership.Models
{
    public class Members
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMember { get; set; }
        public bool IsMonthly { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
