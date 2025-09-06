using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

[ApiController]
[Route("v1/students")]
public class StudentController : ControllerBase
{

    [HttpGet]
    [Route("")]
    public IActionResult GetStudents()
    {
        return Ok(TeacherDataAdapter.lstStudent);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetStudentById(int id)
    {
        var student = TeacherDataAdapter.lstStudent.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    [Route("")]
    public IActionResult CreateStudent([FromBody] Teacher student)
    {
        if (student == null || string.IsNullOrWhiteSpace(student.Name))
        {
            return BadRequest("Invalid student data.");
        }
        student.Id = TeacherDataAdapter.lstStudent.Max(s => s.Id) + 1;
        TeacherDataAdapter.lstStudent.Add(student);
        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }
}

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public static class TeacherDataAdapter
{
    public static readonly List<Teacher> lstStudent = new List<Teacher>()
    {
        new Teacher(){Id=1,Name="John Doe"},
        new Teacher(){Id=2,Name="Jane Smith"},
        new Teacher(){Id=3,Name="Sam Brown"}
    };
}