using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/students")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class StudentController : ControllerBase
{
	private readonly ILogger<StudentController> _logger;
	private readonly StudentService _studentService;

	public StudentController(ILogger<StudentController> logger, StudentService studentService)
	{
		_logger = logger;
		_studentService = studentService;
	}

	[HttpGet("self")]
	public async Task<ActionResult<StudentReadDTO>> Get()
	{
		var email = HttpContext.User.Claims.First(x => x.Type == "email").Value;
		var student = await _studentService.FindOne(email);

		if (student == null)
		{
			return NotFound();
		}
		else
		{
			return student;
		}
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
	public async Task<ActionResult> Post([FromForm] StudentCreateDTO studentCreateDTO)
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
	public async Task<ActionResult> Patch(int id, [FromForm] StudentUpdateDTO studentUpdateDTO)
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
