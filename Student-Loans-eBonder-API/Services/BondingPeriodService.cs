using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Entities;

namespace StudentLoanseBonderAPI.Services;

public class BondingPeriodService
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;

	public BondingPeriodService(ApplicationDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<BondingPeriodReadDTO?> FindOneByInstitutionId(string institutionId)
    {
		var institution = await _dbContext.Institutions.FirstOrDefaultAsync(x => x.Id == institutionId);

		if (institution == null)
		{
			return null;
		}

		if (institution.BondingPeriod == null)
		{
			return null;
		}

		return _mapper.Map<BondingPeriodReadDTO>(institution.BondingPeriod);
	}

    public async Task<bool> Create(string institutionId, BondingPeriodCreateDTO bondingPeriodCreateDTO)
	{
		var institution = await _dbContext.Institutions.FirstOrDefaultAsync(x => x.Id == institutionId);

		if (institution == null)
		{
			return false;
		}

		var bondingPeriod = _mapper.Map<BondingPeriod>(bondingPeriodCreateDTO);
		institution.BondingPeriod = bondingPeriod;
		await _dbContext.SaveChangesAsync();

		return true;
    }

	public async Task<bool> Delete(string institutionId)
	{
		var institution = await _dbContext.Institutions.FirstOrDefaultAsync(x => x.Id == institutionId);

		if (institution == null)
		{
			return false;
		}

		institution.BondingPeriod = null;
		await _dbContext.SaveChangesAsync();

		return true;
	}
}
