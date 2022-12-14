using System;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.BLL;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
	[ApiController]
	[Route("api/student")]
	public class StudentController : ControllerBase
	{
        private readonly StudentBLL _studentBLL;

        public StudentController(StudentBLL studentBLL)
		{
			_studentBLL = studentBLL;
		}

        [HttpGet]
        public async Task<ActionResult<ICollection<StudentReadDto>>> GetAllStudents()
        {
            var allStudents = await _studentBLL.GetAllStudents();

            if (allStudents == null)
            {
                return BadRequest();
            }

            return Ok(allStudents);
        }

        [HttpGet("{registrationId}", Name = "GetStudent")]
        public async Task<ActionResult<StudentReadDto>> GetStudent(string registrationId)
        {
            var student = await _studentBLL.GetStudent(registrationId);

            if(student == null)
            {
                return BadRequest();
            }

            return Ok(student);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateNewStudent(StudentCreationDto student)
        {
            var result = await _studentBLL.AddNewStudent(student);

            if(result == null)
            {
                return BadRequest(new { message = "One or more invalid fields" });
            }

            return Created("~/students/" + result.RegistrationId , result);
        }

        [HttpPost("search")]
        public async Task<ActionResult<ICollection<StudentReadDto>>> SearchStudentByName([FromBody] string name)
        {
            var result = await _studentBLL.SearchStudentByName(name);

            return Ok(result);
        }


    }
}

