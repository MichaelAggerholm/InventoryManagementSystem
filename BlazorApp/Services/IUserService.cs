using Api.Models;

namespace BlazorApp.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();
}