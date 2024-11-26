using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers
{
    [ApiController]
    [Route("/api/BondingStatus")]
    public class BondingStatusController : ControllerBase
    {
        private readonly IBondingStatusService _bondingStatusService;

        public BondingStatusController(IBondingStatusService bondingStatusService)
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
