using ApiService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApiService.Repository.Interfaces;
using backend.Helpers;
using ApiService.Models;
using System.Data;
using ApiService.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using ApiService.Services;
using XSystem.Security.Cryptography;
using backend.Mapper;

namespace ApiService.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        public UsersRepository(AppDbContext db, IMapper mapper, TokenService tokenService)
        {
            _db = db;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<Users> Register(UserRegisterDto userRegisterDto)
        {

            var passwordHash = GetMd5(userRegisterDto.Password);

            Users user = new Users()
            {
                Username = userRegisterDto.Username,
                Password = passwordHash,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                IsDelete = false                
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {

            var userLoginResponseDto = new UserLoginResponseDto();
            var passwordHash = GetMd5(userLoginDto.Password);

            var result = _db.Users.Where(u => u.Username.ToLower() == userLoginDto.Username.ToLower() && u.Password == passwordHash && u.IsActive == true).FirstOrDefault();

            if (result == null)
            {
                userLoginResponseDto.Id = 0;
                userLoginResponseDto.Token = null;
                userLoginResponseDto.Username = null;
            }
            else {

                // Creo el token
                var accessToken = _tokenService.CreateToken(result);

                userLoginResponseDto.Id = result.Id;
                userLoginResponseDto.Username = result.Username;
                userLoginResponseDto.Token = accessToken;
            }

            return userLoginResponseDto;
        }

		public async Task<UsersResponseListDto> GetUsers()
		{

			var result = _db.Users.Where(u => u.IsActive == true).ToList();
			List<UserResponseDto> listUserResponseDto = _mapper.Map<List<UserResponseDto>>(result);

			UsersResponseListDto usersResponseListDto = new UsersResponseListDto();
			usersResponseListDto.Users = listUserResponseDto;

			return usersResponseListDto;
		}

		public static string GetMd5(string value)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }

    }
}

