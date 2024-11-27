using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Entities;

namespace StudentLoanseBonderAPI.Services
{
    public class BondingFormService
    {
        private readonly ILogger<BondingFormService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
		private readonly IFileStorageService _fileStorageService;
		private readonly string _containerName = "bonding-form-documents";

		public BondingFormService(ILogger<BondingFormService> logger, ApplicationDbContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
		{
			_logger = logger;
			_dbContext = dbContext;
			_mapper = mapper;
			_fileStorageService = fileStorageService;
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

		public async Task<List<BondingFormReadDTO>> FindAll()
		{
			var forms = await _dbContext.BondingForms.ToListAsync();

			return _mapper.Map<List<BondingFormReadDTO>>(forms);
		}

		public async Task<bool> Create(BondingFormCreateDTO bondingFormCreateDTO)
        {
            var form = _mapper.Map<BondingForm>(bondingFormCreateDTO);
            await _dbContext.BondingForms.AddAsync(form);

			if (bondingFormCreateDTO.StudentNationalIdScan != null)
			{
				_logger.LogInformation("Saving uploaded national id scan");
				form.StudentNationalIdScan = await _fileStorageService.SaveFile(_containerName, bondingFormCreateDTO.StudentNationalIdScan);
			}

			if (bondingFormCreateDTO.StudentStudentIdScan != null)
			{
				_logger.LogInformation("Saving uploaded student id scan");
				form.StudentStudentIdScan = await _fileStorageService.SaveFile(_containerName, bondingFormCreateDTO.StudentStudentIdScan);
			}

			if (bondingFormCreateDTO.StudentSignature != null)
			{
				_logger.LogInformation("Saving uploaded student signature");
				form.StudentSignature = await _fileStorageService.SaveFile(_containerName, bondingFormCreateDTO.StudentSignature);
			}

			if (bondingFormCreateDTO.LoansBoardOfficialSignature != null)
			{
				_logger.LogInformation("Saving uploaded loans board official signature");
				form.LoansBoardOfficialSignature = await _fileStorageService.SaveFile(_containerName, bondingFormCreateDTO.LoansBoardOfficialSignature);
			}

			if (bondingFormCreateDTO.InstitutionAdminSignature != null)
			{
				_logger.LogInformation("Saving uploaded institution administrator signature");
				form.InstitutionAdminSignature = await _fileStorageService.SaveFile(_containerName, bondingFormCreateDTO.InstitutionAdminSignature);
			}

			await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
