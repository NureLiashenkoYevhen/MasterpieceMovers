using API.Attributes;
using BLL.Jwt;
using BLL.Users;
using Core.DTO.Users;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;

    public UserController(IUserService userService, IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginUserModel loginModel)
    {
        var user = await _userService.LoginAsync(loginModel.Email, loginModel.Password);

        if (user is null)
        {
            return BadRequest("Invalid email or password");
        }
        
        var token = _jwtTokenService.GenerateToken(user.Id, user.Role);
        
        return Ok(token);
    }
    
    [HttpPost("SignUp")]
    public async Task<ActionResult> SignUp([FromBody] SignUpUserModel signUpModel)
    {
        try
        {
            var result = await _userService.SignUpAsync(signUpModel);
            
            return result ? Ok("User registration successful")
                          : BadRequest("User with the same email already exists");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
    
    [AdminAuth]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [AdminAuth]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }

    [AdminAuth]
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserModel user)
    {
        try
        {
            await _userService.CreateUserAsync(user);
            
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest("User already exists");
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [AdminAuth]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserModel user)
    {
        try
        {
            await _userService.UpdateUserAsync(id, user);
            return NoContent();
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [AdminAuth]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}