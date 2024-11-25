using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers
{
    [ApiController]
    [Route("/api/Period")]
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodService _periodService;

        public PeriodController(IPeriodService periodService)
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
