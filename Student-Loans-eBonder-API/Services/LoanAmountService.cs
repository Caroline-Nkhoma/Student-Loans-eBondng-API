using StudentLoanseBonderAPI;
using StudentLoanseBonderAPI.Entities;
namespace StudentLoanseBonderAPI.Services
{
    public class LoanAmountService
    {
        private readonly ApplicationDbContext _context;

        public LoanAmountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoanAmount> CreateLoanAmountAsync(LoanAmount loanAmount)
        {
            _context.Add(loanAmount);
            await _context.SaveChangesAsync();
            return loanAmount;
        }
    }
}
