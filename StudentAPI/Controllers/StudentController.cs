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
            return Ok(await _studentBLL.GetAllStudents());
        }

        [HttpGet("{registrationId}", Name = "GetStudent")]
        public async Task<ActionResult<StudentReadDto>> GetStudent(string registrationId)
        {
            return Ok(await _studentBLL.GetStudent(registrationId));
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateNewStudent(StudentCreationDto student)
        {
            var result = await _studentBLL.AddNewStudent(student);
            return Created("~/students/" + result.RegistrationId , result);
        }


    }
}

