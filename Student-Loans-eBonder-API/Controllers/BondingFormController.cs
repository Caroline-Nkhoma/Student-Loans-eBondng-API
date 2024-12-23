using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/forms")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BondingFormController : ControllerBase
{
    private readonly BondingFormService _bondingFormService;

    public BondingFormController(BondingFormService bondingFormService)
    {
        _bondingFormService = bondingFormService;
	}

	[HttpGet]
	public async Task<ActionResult<List<BondingFormReadDTO>>> GetBondingForms()
	{
		var bondingForms = await _bondingFormService.FindAll();

		return bondingForms;
	}

	[HttpGet("{id}")]
    public async Task<ActionResult<BondingFormReadDTO>> GetBondingForm(string id)
    {
        var bondingForm = await _bondingFormService.FindOne(id);

        if (bondingForm != null)
        {
            return bondingForm;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
	[Authorize(Roles = "Student")]
	public async Task<ActionResult> PostBondingForm([FromForm] BondingFormCreateDTO bondingFormCreateDTO)
    {
        var created = await _bondingFormService.Create(bondingFormCreateDTO);

        if (created)
        {
            return Created();
        }
        else
        {
            return BadRequest();
        }
    }
}
