using System;
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

        public void AddNewStudent(Student student)
        {
            _context.Students.Add(student);

        }

        public ICollection<Student> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Student> GetStudentsByDegree(int degreeId)
        {
            throw new NotImplementedException();
        }

        public Student UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}

