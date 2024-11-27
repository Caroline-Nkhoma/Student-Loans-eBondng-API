using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Entities;

namespace StudentLoanseBonderAPI.Services;

public class InstitutionService
{
	private readonly ILogger<InstitutionService> _logger;
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;

	public InstitutionService(ILogger<InstitutionService> logger, ApplicationDbContext dbContext, IMapper mapper)
	{
		_logger = logger;
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<InstitutionReadDTO?> FindOne(string insitutionId)
	{
		_logger.LogInformation($"Finding insitution with id {insitutionId}");
		var institution = await _dbContext.Institutions.FirstOrDefaultAsync(x => x.Id == insitutionId);

		if (institution == null)
		{
			_logger.LogInformation($"Could not find institution with id {insitutionId}");
			return null;
		}

		_logger.LogInformation($"Found institution with id {insitutionId}");
		_logger.LogDebug($"Converting Institution into InstitutionReadDTIO");
		return _mapper.Map<InstitutionReadDTO>(institution);
	}

	public async Task<List<InstitutionReadDTO>> FindAll()
	{
		var institutions = await _dbContext.Institutions.ToListAsync();
		return _mapper.Map<List<InstitutionReadDTO>>(institutions);
	}

	public async Task<bool> Create(InstitutionCreateDTO institutionCreateDTO)
	{
		var possibleDuplicate = await _dbContext.Institutions.FirstOrDefaultAsync(x => x.Name == institutionCreateDTO.Name);

		if (possibleDuplicate != null)
		{
			return false;
		}

		var institution = _mapper.Map<Institution>(institutionCreateDTO);

		_dbContext.Institutions.Add(institution);
		await _dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<bool> Update(string insitutionId, InstitutionUpdateDTO institutionUpdateDTO)
	{
		_logger.LogInformation($"Attempting to updating insitution with id {insitutionId}");
		var insitution = await _dbContext.Institutions.FirstOrDefaultAsync(x => x.Id == insitutionId);

		if (insitution == null)
		{
			_logger.LogInformation($"Could not find insitution with id {insitutionId}");
			return false;
		}

		var possibleDuplicate = _dbContext.Institutions.FirstOrDefaultAsync(x => x.Name == institutionUpdateDTO.Name);

		if (possibleDuplicate != null)
		{
			return false;
		}

		insitution = _mapper.Map(institutionUpdateDTO, insitution);

		_logger.LogInformation($"Updating existing insitution with id {insitutionId}");
		await _dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<bool> Delete(string insitutionId)
	{
		var institution = await _dbContext.Institutions.FirstOrDefaultAsync(x => x.Id == insitutionId);

		if (institution == null)
		{
			_logger.LogInformation($"Could not find insitution with id {insitutionId}");
			return false;
		}

		_logger.LogInformation($"Deleting insitution with id {insitutionId}");
		_dbContext.Institutions.Remove(institution);
		await _dbContext.SaveChangesAsync();

		return true;
	}
}
