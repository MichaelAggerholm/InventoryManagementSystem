using Api.Data;
using Api.Dto;
using AutoMapper;
using Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController: ControllerBase
{
    private readonly UserDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public UsersController(UserDbContext dbContext, IMapper mapper)
    { 
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        // Hent alle brugerer fra databasen til listen contacts
        var users = await _dbContext.Users.ToListAsync();
        // Konverter listen users til en liste af UserDto
        var usersDto = _mapper.Map<List<UserDto>>(users);
        // Returner listen usersDto
        return Ok(usersDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUser(UserDto userDto)
    {
        // Konverter userDto til en User
        var user = _mapper.Map<User>(userDto);
        // Sæt Id til en ny Guid
        user.Id = Guid.NewGuid();
        // Tilføj user i databasen
        await _dbContext.Users.AddAsync(user);
        // Gem ændringerne i databasen
        await _dbContext.SaveChangesAsync();

        // Returner user hvis alt gik godt
        return Ok(user);
    }
}