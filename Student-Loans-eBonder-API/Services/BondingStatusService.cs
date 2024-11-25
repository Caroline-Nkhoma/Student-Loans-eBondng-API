using StudentLoanseBonderAPI.DTOs;

namespace StudentLoanseBonderAPI.Services
{
    public interface IBondingStatusService
    {
        BondingStatusDto GetBondingStatus();
    }

    public class BondingStatusService : IBondingStatusService
    {
        public BondingStatusDto GetBondingStatus()
        {
            // Simulate fetching bonding status
            return new BondingStatusDto { Status = "Unsubmitted" };
        }
    }
}
