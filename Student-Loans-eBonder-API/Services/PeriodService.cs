using StudentLoanseBonderAPI.DTOs;

namespace StudentLoanseBonderAPI.Services
{
    public interface IPeriodService
    {
        PeriodDto GetPeriod();
    }

    public class PeriodService : IPeriodService
    {
        private readonly PeriodDto _period = new() { StartDate = "2022-01-01", EndDate = "2022-12-31" };

        public PeriodDto GetPeriod()
        {
            // Simulate fetching period data
            return _period;
        }
    }
}
