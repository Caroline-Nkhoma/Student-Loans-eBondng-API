using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers
{
    [ApiController]
    [Route("/api/bonding period")]
    public class BondingPeriodController : ControllerBase
    {
        private readonly BondingPeriodService _bondingPeriodService;

        public BondingPeriodController(BondingPeriodService bondingPeriodService)
        {
            _bondingPeriodService = bondingPeriodService;
        }

        [HttpPost]
        public IActionResult SetBondingPeriod([FromBody] BondingPeriodDto bondingPeriodDto)
        {
            _bondingPeriodService.SetBondingPeriod(bondingPeriodDto);
            return Ok(new { message = "Bonding period set successfully" });
        }

        [HttpGet]
        public ActionResult<BondingPeriodDto> GetBondingPeriod()
        {
            var bondingPeriod = _bondingPeriodService.GetBondingPeriod();
            return Ok(bondingPeriod);
        }
    }
}
