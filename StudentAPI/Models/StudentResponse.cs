using System;
namespace StudentAPI.Models
{
	public class StudentResponse
	{
		public int Pages { get; set; }
		public int CurrentPage { get; set; }
		public ICollection<StudentReadDto> Students { get; set; }

	}
}

