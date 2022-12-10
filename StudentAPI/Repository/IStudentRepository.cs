using System;
using StudentAPI.Models;

namespace StudentAPI.Repository
{
	public interface IStudentRepository
	{
		Student GetStudent(string registrationId);
		ICollection<Student> GetAllStudents();
		ICollection<Student> GetStudentsByDegree(int degreeId);
		void AddNewStudent(Student student);
		Student UpdateStudent(Student student);
		void SaveChanges();
	}
}

