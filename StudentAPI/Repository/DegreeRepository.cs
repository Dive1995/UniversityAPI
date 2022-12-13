using System;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Repository
{
	public class DegreeRepository : IDegreeRepository
	{
		public DegreeRepository()
		{
		}

        public async Task<Degree> CreateNewDegreeAsync(Degree degree)
        {
            using (UniversityContext context = new UniversityContext())
            {
                await context.Degrees.AddAsync(degree);
                context.SaveChanges();
                return degree;
            }
        }

        public async Task<ICollection<Degree>> GetAllDegreeAsync()
        {
            UniversityContext? context = null;

            try {
                context = new UniversityContext();
                return await context.Degrees.ToListAsync();
            }
            finally {
                context?.Dispose();
            }
        }

        public async Task<Degree> GetDegreeByIdAsync(int id)
        {
            using UniversityContext context = new UniversityContext();
            
            return await context.Degrees.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public void SaveChanges()
        {
            UniversityContext? context = null;

            try
            {
                context = new UniversityContext();
                context.SaveChanges();
            }
            finally
            {
                context?.Dispose();
            }

        }
    }
}

