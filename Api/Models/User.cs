using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
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
    
    // public User(Guid id, string username, string email, string phoneNumber, string password)
    // {
    //     Id = id;
    //     Username = username;
    //     Email = email;
    //     PhoneNumber = phoneNumber;
    //     Password = password;
    // }
}