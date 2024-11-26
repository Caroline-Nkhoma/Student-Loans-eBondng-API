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
    public async Task<ActionResult<BondingFormReadDTO>> GetBondingForm(string id)
    {
        var bondingForm = await _bondingFormService.FindOne(id);

        if (bondingForm == null)
        {
            return NotFound();
        }

        return bondingForm;
    }

    // POST: api/BondingForm
    [HttpPost]
    public async Task<ActionResult> PostBondingForm(BondingFormCreateDTO bondingFormCreateDTO)
    {
        var createdForm = await _bondingFormService.Create(bondingFormCreateDTO);

        return Created();
    }

    // PUT: api/BondingForm/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBondingForm(string id, BondingFormUpdateDTO bondingFormUpdateDTO)
    {
        var updatedForm = await _bondingFormService.Update(id, bondingFormUpdateDTO);

        if (updatedForm == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
