using System.ComponentModel.DataAnnotations;

namespace ApiService.Models.Dtos
{
    public class UsersResponseListDto
	{       
        public List<UserResponseDto> Users { get; set; }

    }
}
