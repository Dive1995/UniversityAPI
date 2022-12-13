using System;
using AutoMapper;
using StudentAPI.Models;
using StudentAPI.Repository;

namespace StudentAPI.BLL
{
	public class StudentBLL
	{
        private readonly IStudentRepository _studentRepository;
        private readonly IDegreeRepository _degreeRepository;
        private readonly IMapper _mapper;

        public StudentBLL(IStudentRepository studentRepository, IDegreeRepository degreeRepository, IMapper mapper)
		{
			_studentRepository = studentRepository;
			_degreeRepository = degreeRepository;
			_mapper = mapper;
		}

		public async Task<ICollection<StudentReadDto>> GetAllStudents()
		{			
			return _mapper.Map<ICollection<StudentReadDto>>(await _studentRepository.GetAllStudentsAsync()); 
		}

		public async Task<StudentReadDto> GetStudent(string id)
		{
			return _mapper.Map<StudentReadDto>(await _studentRepository.GetStudentAsync(id));
		}

		public async Task<StudentReadDto> AddNewStudent(StudentCreationDto studentCreation)
		{
			var student = _mapper.Map<Student>(studentCreation);
			var degree = await _degreeRepository.GetDegreeByIdAsync(student.DegreeId);
			var lastRunningStudent = await GetRunningIdAsync();

			var registrationId = degree.Code + student.Batch + (lastRunningStudent.Id + 1);

			student.RegistrationId = registrationId;
			var createdStudent = await _studentRepository.AddNewStudentAsync(student);
			return _mapper.Map<StudentReadDto>(createdStudent);
		}

		public async Task<StudentReadDto> UpdateStudent(Student student)
		{
			return _mapper.Map<StudentReadDto>(await _studentRepository.UpdateStudentAsync(student));
		}

		public async Task<Student> GetRunningIdAsync()
		{
			return await _studentRepository.GetRunningIdAsync();
		}

    }
}