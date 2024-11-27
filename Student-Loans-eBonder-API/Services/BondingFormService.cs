using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Entities;

namespace StudentLoanseBonderAPI.Services
{
    public class BondingFormService
    {
        private readonly ILogger<StudentService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public BondingFormService(ILogger<StudentService> logger, ApplicationDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BondingFormReadDTO?> FindOne(string formId)
        {
		    var form = await _dbContext.BondingForms.FirstOrDefaultAsync(x => x.Id == formId);

            if (form == null)
            {
                return null;
            }

		    return _mapper.Map<BondingFormReadDTO>(form);
        }

        public async Task<bool> Create(BondingFormCreateDTO bondingFormCreateDTO)
        {
            var form = _mapper.Map<BondingForm>(bondingFormCreateDTO);
            _dbContext.BondingForms.Add(form);
		    await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(string formId, BondingFormUpdateDTO bondingFormUpdateDTO)
        {
		    var form = await _dbContext.BondingForms.FirstOrDefaultAsync(x => x.Id == formId);

            if(form == null)
            {
                return false;
            }

            form = _mapper.Map(bondingFormUpdateDTO, form);
            
		    await _dbContext.SaveChangesAsync();
            
            return true;
        }
    }
}
