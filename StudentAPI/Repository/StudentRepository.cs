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
            SaveChanges();
            return student;
        }

        public async Task<ICollection<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.Include(s => s.Degree.Name).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(string registrationId)
        {
            return await _context.Students.Include(s => s.Degree).Where(s => s.RegistrationId == registrationId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Student>> GetStudentsByDegreeAsync(int degreeId)
        {
            return await _context.Students.Include(s => s.Degree).Where(s => s.DegreeId == degreeId).ToListAsync();
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            var studentToBeUpdated = await _context.Students.Include(s => s.Degree).FirstOrDefaultAsync(s => s.RegistrationId == student.RegistrationId);

            if (studentToBeUpdated != null)
            {
                studentToBeUpdated.FirstName = student.FirstName;
                studentToBeUpdated.LastName = student.LastName;
                studentToBeUpdated.Batch = student.Batch;
                SaveChanges();
            }
            return studentToBeUpdated;
        }

        public async Task<Student> GetRunningIdAsync()
        {
            return await _context.Students.OrderByDescending(s => s.Id).FirstOrDefaultAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}

