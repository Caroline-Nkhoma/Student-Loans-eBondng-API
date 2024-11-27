using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Entities;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers
{
    [ApiController]
    [Route("api/institutions/{institutionId}/bonding-periods")]
    public class BondingPeriodController : ControllerBase
    {
        private readonly BondingPeriodService _bondingPeriodService;

        public BondingPeriodController(BondingPeriodService bondingPeriodService)
        {
            _bondingPeriodService = bondingPeriodService;
		}

		[HttpGet]
		public async Task<ActionResult<BondingPeriodReadDTO>> GetBondingPeriod([FromRoute] string institutionId)
		{
			var bondingPeriod = await _bondingPeriodService.FindOneByInstitutionId(institutionId);

			if (bondingPeriod != null)
			{
				return bondingPeriod;
			}
			else
			{
				return NotFound();
			}
		}

		[HttpPost]
        public async Task<ActionResult> SetBondingPeriod([FromRoute] string institutionId, [FromBody] BondingPeriodCreateDTO bondingPeriodCreateDTO)
        {
            var created = await _bondingPeriodService.Create(institutionId, bondingPeriodCreateDTO);

			if (created)
			{
				return Created();
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteBondingPeriod([FromRoute] string institutionId)
		{
			var deleted = await _bondingPeriodService.Delete(institutionId);

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
}
