using Api.Models;

namespace BlazorApp.Services;

public class UserService : IUserService
{
    private readonly HttpClient httpClient;
    
    public UserService(HttpClient _httpClient)
    {
        this.httpClient = _httpClient;
    }
    
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await httpClient.GetFromJsonAsync<User[]>("api/Users");
    }
}