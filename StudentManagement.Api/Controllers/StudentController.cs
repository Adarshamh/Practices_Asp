using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Interfaces;
namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;
    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent(StudentDto request, CancellationToken cancellationToken)
    {
        await _service.AddStudentAsync(request, cancellationToken);
        return Ok(new{message = "Student Saved Successfully"});
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents(CancellationToken cancellationToken)
    {
        var students = await _service.GetStudentsAsync(cancellationToken);
        return Ok(students);
    }
}