using StudentManagement.Api.DTOs;
using StudentManagement.Api.Entities;
using StudentManagement.Api.Helpers;
using StudentManagement.Api.Interfaces;
namespace StudentManagement.Api.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;

    private readonly JwtHelper _jwtHelper;

    public AuthService(IAuthRepository repository,JwtHelper jwtHelper)
    {
        _repository = repository;
        _jwtHelper = jwtHelper;
    }

    public async Task RegisterAsync(RegisterRequestDto request,CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetUserByEmailAsync(request.Email,cancellationToken);
        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
        await _repository.RegisterUserAsync(user,cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request,CancellationToken cancellationToken)
    {
        var user =await _repository.GetUserByEmailAsync(request.Email,cancellationToken);

        if (user == null)
        {
            return null;
        }

        if (user.Password != request.Password)
        {
            return null;
        }

        var token = _jwtHelper.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token
        };
    }
}