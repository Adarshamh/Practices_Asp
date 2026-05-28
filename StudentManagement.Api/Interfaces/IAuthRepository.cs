using StudentManagement.Api.Entities;

namespace StudentManagement.Api.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByEmailAsync(string email,CancellationToken cancellationToken);
    Task RegisterUserAsync(User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}