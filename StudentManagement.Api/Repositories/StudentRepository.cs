using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.Entities;
using StudentManagement.Api.Interfaces;
namespace StudentManagement.Api.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddStudentAsync(Student student,CancellationToken cancellationToken)
    {
        await _context.Students.AddAsync(student, cancellationToken);
    }

    public async Task<List<Student>> GetStudentsAsync(CancellationToken cancellationToken)
    {
        return await _context.Students.ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}