using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/students")]
[ApiController]
public class StudentController : ControllerBase
{
	private readonly ILogger<StudentController> _logger;
	private readonly StudentService _studentService;

	public StudentController(ILogger<StudentController> logger, StudentService studentService)
	{
		_logger = logger;
		_studentService = studentService;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<StudentReadDTO>> Get(int id)
	{
		var student = await _studentService.FindOne(id);

		if (student == null)
		{
			return NotFound();
		}
		else
		{
			return student;
		}
	}

	[HttpPost]
	public async Task<ActionResult> Post([FromBody] StudentCreateDTO studentCreateDTO)
	{
		var created = await _studentService.Create(studentCreateDTO);
		
		if (created)
		{
			return Created();
		}
		else
		{
			throw new NotImplementedException();
		}
	}

	[HttpPatch("{id}")]
	public async Task<ActionResult> Patch(int id, [FromBody] StudentUpdateDTO studentUpdateDTO)
	{
		var updated = await _studentService.Update(id, studentUpdateDTO);

		if (updated)
		{
			return NoContent();
		}
		else
		{
			return NotFound();
		}
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		var deleted = await _studentService.Delete(id);

		if (deleted)
		{
			return NoContent();
		}
		else
		{
			return NotFound();
		}
	}
}
