using StudentLoanseBonderAPI.DTOs;

namespace StudentLoanseBonderAPI.Services
{
    public class BondingStatusService
    {
        public BondingStatusDto GetBondingStatus()
        {
            // Simulate fetching bonding status
            return new BondingStatusDto { Status = "Unsubmitted" };
        }
    }
}
