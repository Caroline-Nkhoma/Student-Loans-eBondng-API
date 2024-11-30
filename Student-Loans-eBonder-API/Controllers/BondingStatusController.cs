using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers
{
    [ApiController]
    [Route("api/bonding-status")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class BondingStatusController : ControllerBase
    {
        private readonly BondingStatusService _bondingStatusService;

        public BondingStatusController(BondingStatusService bondingStatusService)
        {
            _bondingStatusService = bondingStatusService;
        }

        [HttpGet]
		public ActionResult<BondingStatusDto> GetBondingStatus()
        {
            var status = _bondingStatusService.GetBondingStatus();
            return Ok(status);
        }
    }
}
