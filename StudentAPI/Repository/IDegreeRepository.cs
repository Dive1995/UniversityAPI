using System;
using StudentAPI.Models;

namespace StudentAPI.Repository
{
	public interface IDegreeRepository
    {
		Task<ICollection<Degree>> GetAllDegreeAsync();
		Task<Degree> GetDegreeByIdAsync(int id);
		Task<Degree> CreateNewDegreeAsync(Degree degree);
		void SaveChanges();
	}
}

