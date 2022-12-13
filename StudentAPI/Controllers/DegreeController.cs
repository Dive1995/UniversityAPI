using System;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Repository;

namespace StudentAPI.Controllers
{
	[ApiController]
	[Route("api/degree")]
	public class DegreeController : ControllerBase
	{
        private readonly IDegreeRepository _repository;

        public DegreeController(IDegreeRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<ActionResult<ICollection<Degree>>> GetAllDegreesAsync()
		{

			return Ok(await _repository.GetAllDegreeAsync());
			//using(IDegreeRepository repository = new DegreeRepository())
			//{
			//}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Degree>> GetDegreeById(int id)
		{
			return Ok(await _repository.GetDegreeByIdAsync(id));
		}

		[HttpPost("create")]
		public async Task<ActionResult<Degree>> AddNewDegree(Degree degree)
		{
            return await _repository.CreateNewDegreeAsync(degree);
        }
	}
}

