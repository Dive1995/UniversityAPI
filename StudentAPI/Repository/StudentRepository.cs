using System;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Repository
{
	public class StudentRepository : IStudentRepository
	{
        private readonly UniversityContext _context;

        public StudentRepository(UniversityContext context)
		{
            _context = context;
		}

        public async Task<Student> AddNewStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            return student;
        }

        public async Task<ICollection<Student>> GetAllStudentsAsync(int page, int pageResult)
        {
            return await _context.Students.Skip((page - 1)*pageResult).Take(pageResult).Include(s => s.Degree).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(string registrationId)
        {
            return await _context.Students.Include(s => s.Degree).Where(s => s.RegistrationId == registrationId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Student>> GetStudentsByDegreeAsync(int degreeId)
        {
            return await _context.Students.Include(s => s.Degree).Where(s => s.DegreeId == degreeId).ToListAsync();
        }       

        public async Task<Student> GetRunningIdByBatchAsync(int batch)
        {
            return await _context.Students.OrderByDescending(s => s.Id).Where(s => s.Batch == batch).FirstOrDefaultAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<ICollection<Student>> GetStudentByNameAsync(string name)
        {
            return await _context.Students.Where(s => s.FirstName.Contains(name) || s.LastName.Contains(name)).ToListAsync();
        }

        public async Task<int> StudentCountAsync()
        {
            return await _context.Students.CountAsync();
        }
    }
}

