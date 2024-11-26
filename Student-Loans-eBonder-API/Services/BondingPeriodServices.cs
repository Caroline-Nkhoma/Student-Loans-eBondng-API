using StudentLoanseBonderAPI.DTOs;

namespace StudentLoanseBonderAPI.Services
{
    public class BondingPeriodService
    {
        private BondingPeriodDto _currentBondingPeriod;

        public void SetBondingPeriod(BondingPeriodDto bondingPeriodDto)
        {
            _currentBondingPeriod = bondingPeriodDto;
        }

        public BondingPeriodDto GetBondingPeriod()
        {
            return _currentBondingPeriod;
        }
    }
}
