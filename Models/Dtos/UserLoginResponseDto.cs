using System.ComponentModel.DataAnnotations;

namespace ApiService.Models.Dtos
{
    public class UserLoginResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }        
        public string Token { get; set; }

    }
}
