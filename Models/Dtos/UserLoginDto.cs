using System.ComponentModel.DataAnnotations;

namespace ApiService.Models.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "El campo Username es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo Password es obligatorio")]
        public string Password { get; set; }

    }
}
