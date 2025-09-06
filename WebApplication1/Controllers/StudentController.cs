using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("v1/students")]
public class StudentController : ControllerBase
{
    
    [HttpGet]
    [Route("")]
    public IActionResult GetStudents()
    {
        return Ok(StudentDataAdapter.lstStudent);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetStudentById(int id)
    {
        var student = StudentDataAdapter.lstStudent.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    [Route("")]
    public IActionResult CreateStudent([FromBody] Student student)
    {
        if (student == null || string.IsNullOrWhiteSpace(student.Name))
        {
            return BadRequest("Invalid student data.");
        }
        student.Id = StudentDataAdapter.lstStudent.Max(s => s.Id) + 1;
        StudentDataAdapter.lstStudent.Add(student);
        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public static class StudentDataAdapter
{
    public static readonly List<Student> lstStudent = new List<Student>()
    {
        new Student(){Id=1,Name="John Doe"},
        new Student(){Id=2,Name="Jane Smith"},
        new Student(){Id=3,Name="Sam Brown"}
    };
}