using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.Entities;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanAmountController : ControllerBase
{
    private readonly LoanAmountService _loanAmountService;

    public LoanAmountController(LoanAmountService loanAmountService)
    {
        _loanAmountService = loanAmountService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoanAmount([FromBody] LoanAmount loanAmount)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdLoanAmount = await _loanAmountService.CreateLoanAmountAsync(loanAmount);

        // Replace "GetLoanAmountById" with the actual method or endpoint name for fetching loan details.
        return CreatedAtAction(nameof(GetLoanAmountById), new { id = createdLoanAmount.Id }, createdLoanAmount);
    }

    private object GetLoanAmountById()
    {
        throw new NotImplementedException();
    }
}
