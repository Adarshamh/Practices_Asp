using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Interfaces;

public interface IStudentService
{
    Task AddStudentAsync(StudentDto request,CancellationToken cancellationToken);
    Task<List<StudentDto>> GetStudentsAsync(CancellationToken cancellationToken);
}