using Moq;
using Microsoft.Extensions.Caching.Memory;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Entities;
using StudentManagement.Api.Interfaces;
using StudentManagement.Api.Services;
namespace StudentManagement.Api.Test.Services;

public class StudentServiceTests
{
    private readonly Mock<IStudentRepository> _repositoryMock;
    private readonly IMemoryCache _memoryCache;
    private readonly StudentService _service;

    public StudentServiceTests()
    {
        _repositoryMock = new Mock<IStudentRepository>();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _service = new StudentService(_repositoryMock.Object, _memoryCache);
    }

    [Fact]
    public async Task GetStudentsAsync_ShouldReturnStudentList()
    {
        // Arrange

        var students = new List<Student>
        {
            new Student
            {
                Name = "Rahul",
                Age = 24,
                Gender = "Male",
                Phone = "9876543210",
                Semester = "6th",
                TotalFees = 50000,
                FeesPaid = 30000,
                FeesRemaining = 20000,
                FeesStatus = "UnPaid"
            }
        };
        _repositoryMock.Setup(x => x.GetStudentsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(students);

        // Act
        var result = await _service.GetStudentsAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Rahul", result[0].Name);
    }
}