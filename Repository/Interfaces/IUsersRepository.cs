using ApiService;
using ApiService.Models;
using ApiService.Models.Dtos;

namespace ApiService.Repository.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users> Register(UserRegisterDto userRegisterDto);
        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);

		Task<UsersResponseListDto> GetUsers();
	}
}
