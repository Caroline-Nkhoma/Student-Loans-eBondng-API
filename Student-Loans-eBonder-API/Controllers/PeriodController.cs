using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers
{
    [ApiController]
    [Route("/api/Period")]
    public class PeriodController : ControllerBase
    {
        private readonly PeriodService _periodService;

        public PeriodController(PeriodService periodService)
        {
            _periodService = periodService;
        }

        [HttpGet]
        public ActionResult<PeriodDto> GetPeriod()
        {
            var period = _periodService.GetPeriod();
            return Ok(period);
        }
    }
}
