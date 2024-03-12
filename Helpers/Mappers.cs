using ApiService.Models;
using ApiService.Models.Dtos;
using AutoMapper;

namespace backend.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Users, UserRegisterResponseDto>();
			CreateMap<Users, UserResponseDto>().ReverseMap();
			//CreateMap<Users, UsersResponseListDto>();
			//CreateMap<List<Users>, List<UserResponseDto>>();


		}
    }
}

