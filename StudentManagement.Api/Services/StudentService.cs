using StudentManagement.Api.DTOs;
using StudentManagement.Api.Entities;
using StudentManagement.Api.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace StudentManagement.Api.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly IMemoryCache _memoryCache;
    private const string STUDENT_CACHE_KEY = "student_list";

    public StudentService(IStudentRepository repository, IMemoryCache memoryCache)
    {
        _repository = repository;
        _memoryCache = memoryCache;
    }

    // For creating student data using async method
    public async Task AddStudentAsync(StudentDto request,CancellationToken cancellationToken)
    {
        var student = new Student
        {
            Name = request.Name,
            Dob = request.Dob,
            Age = request.Age,
            Gender = request.Gender,
            Phone = request.Phone,
            Semester = request.Semester,
            TotalFees = request.TotalFees,
            FeesPaid = request.FeesPaid,
            FeesRemaining = request.FeesRemaining,
            FeesStatus = request.FeesStatus
        };

        await _repository.AddStudentAsync(student,cancellationToken);
        _memoryCache.Remove(STUDENT_CACHE_KEY);
        await _repository.SaveChangesAsync(cancellationToken);
        Log.Information("Student added successfully: StudentName={@Student}", student.Name);
    }

    public async Task<List<StudentDto>> GetStudentsAsync(CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(STUDENT_CACHE_KEY,out List<StudentDto>? cachedStudents))
        {
            return cachedStudents!;
        }
        var students =await _repository.GetStudentsAsync(cancellationToken);
        var result = students.Select(x => new StudentDto
        {
            Name = x.Name,
            Dob = x.Dob,
            Age = x.Age,
            Gender = x.Gender,
            Phone = x.Phone,
            Semester = x.Semester,
            TotalFees = x.TotalFees,
            FeesPaid = x.FeesPaid,
            FeesRemaining = x.FeesRemaining,
            FeesStatus = x.FeesStatus
        }).ToList();

        var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
        _memoryCache.Set(STUDENT_CACHE_KEY,result,cacheOptions);
        Log.Information("Student data retrieved and cached successfully. Total students: {StudentCount}", result.Count);
        return result;
    }
}