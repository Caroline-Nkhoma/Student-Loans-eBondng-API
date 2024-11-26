using StudentLoanseBonderAPI.DTOs;

namespace StudentLoanseBonderAPI.Services
{
    public interface IBondingPeriodService
    {
        void SetBondingPeriod(BondingPeriodDto bondingPeriodDto);
        BondingPeriodDto GetBondingPeriod();
    }

    public class BondingPeriodService : IBondingPeriodService
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
