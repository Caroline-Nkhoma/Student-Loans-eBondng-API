using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/accounts/{accountId}/students")]
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

	[HttpGet]
	[Authorize(Roles = "Student,SystemAdmin")]
	public async Task<ActionResult<StudentReadDTO>> Get([FromRoute] string accountId)
	{
		var student = await _studentService.FindOne(accountId);

		if (student == null)
		{
			return NotFound();
		}
		else
		{
			return student;
		}
	}

	[HttpPut]
	[Authorize(Roles = "Student")]
	public async Task<ActionResult> Put([FromRoute] string accountId, [FromForm] StudentCreateDTO studentCreateDTO)
	{
		var hasBeenPut = await _studentService.CreateOrUpdate(accountId, studentCreateDTO);
		
		if (hasBeenPut)
		{
			return NoContent();
		}
		else
		{
			return BadRequest();
		}
	}

	[HttpPatch]
	[Authorize(Roles = "Student")]
	public async Task<ActionResult> Patch([FromRoute] string accountId, [FromForm] StudentUpdateDTO studentUpdateDTO)
	{
		var updated = await _studentService.Update(accountId, studentUpdateDTO);

		if (updated)
		{
			return NoContent();
		}
		else
		{
			return NotFound();
		}
	}

	[HttpDelete]
	[Authorize(Roles = "Student,SystemAdmin")]
	public async Task<ActionResult> Delete([FromRoute] string accountId)
	{
		var deleted = await _studentService.Delete(accountId);

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
