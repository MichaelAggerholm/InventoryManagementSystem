using Api.Data;
using Api.Dto;
using AutoMapper;
using Api.Models;
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
        var users = await _dbContext.Users.ToListAsync();
        // Konverter listen users til en liste af UserDto
        var usersDto = _mapper.Map<List<UserDto>>(users);
        
        return Ok(usersDto);
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        // Konverter user til UserDto
        var userDto = _mapper.Map<UserDto>(user);
        if (userDto == null)
        {
            return NotFound();
        }
        
        return Ok(userDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
    {
        // Konverter userDto til en User
        var user = _mapper.Map<User>(userDto);
        
        // Sæt Id til en ny Guid
        user.Id = Guid.NewGuid();
        // Hash password med BCrypt
        user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        
        // Tilføj user i databasen og gem
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return Ok(user);
    }
    
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserDto userDto)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        
        // Konverter userDto til en User
        _mapper.Map(userDto, user);
        
        // Hash password med BCrypt
        user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        
        // Gem ændringer i databasen
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return Ok(user);
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}