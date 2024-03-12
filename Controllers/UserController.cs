using ApiService.Models;
using ApiService.Models.Dtos;
using ApiService.Repository;
using ApiService.Repository.Interfaces;
using AutoMapper;
using backend.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {

        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        protected ApiResponse _apiResponse;
        public UserController(IUsersRepository usersRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            this._apiResponse = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var result = await _usersRepository.Login(userLoginDto);

                _apiResponse.Result = result;
                _apiResponse.Messages.Add("Login correcto del usuario");
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.ErrorMessages.Add("Error haciendo login del usuario");
                _apiResponse.ErrorMessages.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }

        }


        [HttpPost("registeruser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                var result = await _usersRepository.Register(userRegisterDto);

                _apiResponse.Result = result;
                _apiResponse.Messages.Add("Registro correcto del usuario");
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {                
                _apiResponse.ErrorMessages.Add("Error registrando el usuario");
                _apiResponse.ErrorMessages.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }
            
        }

        [CustomAuthorization]
        [HttpGet("users")]
        public async Task<IActionResult> getUsers()
        {
            try
            {                

                var result = await _usersRepository.GetUsers();

                _apiResponse.Result = result;
                _apiResponse.Messages.Add("Consulta de Usuarios Registrados");
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.ErrorMessages.Add("Error consultando los usuarios");
                _apiResponse.ErrorMessages.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }

        }
    }
}