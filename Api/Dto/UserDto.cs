using System.ComponentModel.DataAnnotations;

namespace Api.Dto;

public class UserDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    // public UserDto(string username, string email, string phoneNumber, string password)
    // {
    //     Username = username;
    //     Email = email;
    //     PhoneNumber = phoneNumber;
    //     Password = password;
    // }
}