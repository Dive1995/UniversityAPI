using System;
using StudentAPI.Models;

namespace StudentAPI.Repository
{
	public interface IStudentRepository
	{
		Task<Student> GetStudentAsync(string registrationId);
		Task<ICollection<Student>> GetAllStudentsAsync();
		Task<ICollection<Student>> GetStudentsByDegreeAsync(int degreeId);
		Task<Student> AddNewStudentAsync(Student student);
		//Task<Student> UpdateStudentAsync(Student student);
		Task<Student> GetRunningIdByBatchAsync(int batch);
		Task<ICollection<Student>> GetStudentByNameAsync(string name);
		void SaveChanges();
	}
}

