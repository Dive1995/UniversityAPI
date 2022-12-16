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

		public async Task<StudentResponse> GetAllStudents(int page)
		{
            var pageResults = 3f;
			var studentCount = await _studentRepository.StudentCountAsync();
			var pageCount = Math.Ceiling(studentCount / pageResults);

			var students = _mapper.Map<ICollection<StudentReadDto>>(await _studentRepository.GetAllStudentsAsync(page, (int)pageResults));
			var response = new StudentResponse
			{
				Students = students,
				CurrentPage = page,
				Pages = (int)pageCount
			};

			return response;
		}

		public async Task<StudentReadDto> GetStudent(string id)
		{
			return _mapper.Map<StudentReadDto>(await _studentRepository.GetStudentAsync(id));
		}

		public async Task<StudentReadDto> AddNewStudent(StudentCreationDto studentCreation)
		{
			var student = _mapper.Map<Student>(studentCreation);

			var degree = await _degreeRepository.GetDegreeByIdAsync(student.DegreeId);

			if(degree == null)
			{
				return null; // should throw en error with message, and the global exception handling should handle it
			}

			var lastRunningStudent = await GetRunningIdByBatchAsync(student.Batch);
			int currentStudentRunningId;

            if (lastRunningStudent == null)
			{
                currentStudentRunningId = 1;
			}
			else
			{
				currentStudentRunningId = lastRunningStudent.Id + 1;
			}

			student.Id = currentStudentRunningId;

			var registrationId = degree.Code + student.Batch + currentStudentRunningId.ToString("0000");
			student.RegistrationId = registrationId;

			await _studentRepository.AddNewStudentAsync(student);
			_studentRepository.SaveChanges();

			return _mapper.Map<StudentReadDto>(student);
		}

		public async Task<StudentReadDto> UpdateStudent(StudentUpdateDto studentUpdateDto)
		{
			var student = _mapper.Map<Student>(studentUpdateDto);
			// Check if student exist
			var studentRecordToBeUpdated = await _studentRepository.GetStudentAsync(student.RegistrationId);
			if(studentRecordToBeUpdated == null)
			{
				return null;
			}

			// If batch has changed add new record & delete old record
			if(studentRecordToBeUpdated.Batch != student.Batch)
			{
				return await AddNewStudent(_mapper.Map<StudentCreationDto>(student));
				// delete the old record
			}

            studentRecordToBeUpdated.FirstName = student.FirstName;
            studentRecordToBeUpdated.LastName = student.LastName;
            studentRecordToBeUpdated.Batch = student.Batch;

			_studentRepository.SaveChanges();
            return _mapper.Map<StudentReadDto>(studentRecordToBeUpdated);
		}

		public async Task<Student> GetRunningIdByBatchAsync(int batch)
		{
			return await _studentRepository.GetRunningIdByBatchAsync(batch);
		}

		public async Task<ICollection<StudentReadDto>> SearchStudentByName(string name)
		{
			var searchResult = await _studentRepository.GetStudentByNameAsync(name);
			return _mapper.Map<ICollection<StudentReadDto>>(searchResult);
		}
    }
}