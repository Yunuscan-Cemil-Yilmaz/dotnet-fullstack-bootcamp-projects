using Microsoft.AspNetCore.Mvc;
using StudentManagerApi.Domain.Entities;
using StudentManagerApi.Application.Services;

namespace StudentManagerApi.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;
    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        return student == null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        var created = await _studentService.CreateAsync(student);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Student student)
    {
        var result = await _studentService.UpdateAsync(id, student);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _studentService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}