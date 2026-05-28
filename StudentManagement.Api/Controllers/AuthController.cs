using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;
namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        await _service.RegisterAsync(request, cancellationToken);
        return Ok(new
        {
            message = "User Registered Successfully"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _service.LoginAsync(request, cancellationToken);
        if (response == null)
        {
            return Unauthorized("Invalid Credentials");
        }
        return Ok(response);
    }
}