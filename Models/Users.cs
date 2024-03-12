namespace ApiService.Models;

using System.ComponentModel.DataAnnotations;

public class Users
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }

}