using StudentManagement.Api.Entities;

namespace StudentManagement.Api.Interfaces;

public interface IStudentRepository
{
    Task AddStudentAsync(Student student,CancellationToken cancellationToken);
    Task<List<Student>> GetStudentsAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}