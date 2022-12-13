using System;
using AutoMapper;
using StudentAPI.Models;

namespace StudentAPI.Profiles
{
	public class StudentMapperProfile : Profile
	{
		public StudentMapperProfile()
		{
			CreateMap<StudentCreationDto, Student>();
			CreateMap<Student, StudentReadDto>();
		}
	}
}

