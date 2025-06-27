using Microsoft.EntityFrameworkCore;
using StudentManagerApi.Domain.Entities;
using StudentManagerApi.Infrastructure.Config;

namespace StudentManagerApi.Application.Services;

public class StudentService
{
    private readonly StudentDbContext _context;

    public StudentService(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students
            .Include(s => s.Lessons)
            .Include(s => s.ExamResults)
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Lessons)
            .Include(s => s.ExamResults)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student> CreateAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<bool> UpdateAsync(int id, Student updateStudent)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return false;
        try
        {
            student.FullName = updateStudent.FullName;
            student.Lessons = updateStudent.Lessons;
            student.ExamResults = updateStudent.ExamResults;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle concurrency exception
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return false;

        try
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            // Handle foreign key constraint violation
            return false;
        }
    }
}   