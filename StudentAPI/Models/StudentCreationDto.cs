using System;
using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models
{
	public class StudentCreationDto
	{
        [Required]
        [MinLength(3)]
        public string? FirstName { get; set; }
        [Required]
        [MinLength(3)]
        public string? LastName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? MobileNum { get; set; }
        public string? LandlineNum { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Batch { get; set; }
        [Required]
        public int DegreeId { get; set; }
    }
}

