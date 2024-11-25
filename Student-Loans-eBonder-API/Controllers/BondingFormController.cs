using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;
using System.Threading.Tasks;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/forms")]
[ApiController]
public class BondingFormController : ControllerBase
{
    private readonly BondingFormService _bondingFormService;

    public BondingFormController(BondingFormService bondingFormService)
    {
        _bondingFormService = bondingFormService;
    }

    // GET: api/BondingForm/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<BondingFormDTO>> GetBondingForm(string id)
    {
        var bondingForm = await _bondingFormService.GetBondingFormAsync(id);

        if (bondingForm == null)
        {
            return NotFound();
        }

        return bondingForm;
    }

    // POST: api/BondingForm
    [HttpPost]
    public async Task<ActionResult<BondingFormDTO>> PostBondingForm(BondingFormDTO bondingFormDTO)
    {
        var createdForm = await _bondingFormService.CreateBondingFormAsync(bondingFormDTO);

        return CreatedAtAction("GetBondingForm", new { id = createdForm.FormId }, createdForm);
    }

    // PUT: api/BondingForm/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBondingForm(string id, BondingFormDTO bondingFormDTO)
    {
        if (id != bondingFormDTO.FormId)
        {
            return BadRequest();
        }

        var updatedForm = await _bondingFormService.UpdateBondingFormAsync(id, bondingFormDTO);

        if (updatedForm == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
