using System;
namespace StudentAPI.Models
{
	public class StudentReadDto
	{
        public string RegistrationId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string MobileNum { get; set; } = null!;
        public string? LandlineNum { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Batch { get; set; }
        public int DegreeId { get; set; }
        public int Id { get; set; }

        public virtual Degree Degree { get; set; } = null!;
    }
}

