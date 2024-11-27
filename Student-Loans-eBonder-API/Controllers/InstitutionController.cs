using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/institutions")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class InstitutionController : ControllerBase
{
	private readonly ILogger<InstitutionController> _logger;
	private readonly InstitutionService _institutionService;

	public InstitutionController(ILogger<InstitutionController> logger, InstitutionService institutionService)
	{
		_logger = logger;
		_institutionService = institutionService;
	}

	[HttpGet]
	public async Task<ActionResult<List<InstitutionReadDTO>>> Get()
	{
		var institutions = await _institutionService.FindAll();

		return institutions;
	}

	[HttpGet("{insitutionId}")]
	public async Task<ActionResult<InstitutionReadDTO>> Get(string insitutionId)
	{
		var institution = await _institutionService.FindOne(insitutionId);

		if (institution == null)
		{
			return NotFound();
		}
		else
		{
			return institution;
		}
	}

	[HttpPost]
	public async Task<ActionResult> HttpPost([FromBody] InstitutionCreateDTO institutionCreateDTO)
	{
		var created = await _institutionService.Create(institutionCreateDTO);

		if (created)
		{
			return Created();
		}
		else
		{
			return BadRequest();
		}
	}

	[HttpPatch("{insitutionId}")]
	public async Task<ActionResult> Patch(string insitutionId, [FromBody] InstitutionUpdateDTO institutionUpdateDTO)
	{
		var updated = await _institutionService.Update(insitutionId, institutionUpdateDTO);

		if (updated)
		{
			return NoContent();
		}
		else
		{
			return NotFound();
		}
	}

	[HttpDelete("{insitutionId}")]
	public async Task<ActionResult> Delete(string insitutionId)
	{
		var deleted = await _institutionService.Delete(insitutionId);

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
