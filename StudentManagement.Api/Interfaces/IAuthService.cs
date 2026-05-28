using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequestDto request,CancellationToken cancellationToken);
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request,CancellationToken cancellationToken);
}