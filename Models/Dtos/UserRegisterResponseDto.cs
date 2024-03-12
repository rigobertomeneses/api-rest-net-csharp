using System.ComponentModel.DataAnnotations;

namespace ApiService.Models.Dtos
{
    public class UserRegisterResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }

    }
}
