using GoTorz.Models.User;
using GoTorz.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoTorz.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserInput input)
    {
        // You could do additional validation here
        if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password))
            return BadRequest("Email or Password is missing.");

        var (userId, error) = await _userService.CreateUserAsync(input);

        return Ok(new[] { userId, error });
    }

}
