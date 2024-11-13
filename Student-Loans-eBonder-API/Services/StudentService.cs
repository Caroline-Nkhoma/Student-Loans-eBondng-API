using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Entities;

namespace StudentLoanseBonderAPI.Services;

public class StudentService
{
	private readonly ILogger<StudentService> _logger;
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IFileStorageService _fileStorageService;
	private readonly AccountService _accountService;
	private readonly string _containerName = "student-documents";

	public StudentService(ILogger<StudentService> logger, ApplicationDbContext dbContext, IMapper mapper, IFileStorageService fileStorageService, AccountService accountService)
	{
		_logger = logger;
		_dbContext = dbContext;
		_mapper = mapper;
		_fileStorageService = fileStorageService;
		_accountService = accountService;
	}

	public async Task<StudentReadDTO?> FindOne(int id)
	{
		_logger.LogInformation($"Finding student with id {id}");
		var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

		if (student == null)
		{
			_logger.LogInformation($"Could not find student with id {id}");
			return null;
		}

		_logger.LogInformation($"Found student with id {id}");
		_logger.LogDebug($"Converting Student into StudentReadDTO");
		return _mapper.Map<StudentReadDTO>(student);
	}

	public async Task<StudentReadDTO?> FindOne(string email)
	{
		_logger.LogInformation($"Finding student with email {email}");
		var account = await _accountService.FindOne(email);

		if (account == null)
		{
			_logger.LogInformation($"Could not find student with email {email}");
			return null;
		}

		var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.AccountId == account.Id);

		if (student == null)
		{
			_logger.LogInformation($"Could not find student with email {email}");
			return null;
		}

		_logger.LogInformation($"Found student with email {email}");
		_logger.LogDebug($"Converting Student into StudentReadDTO");
		return _mapper.Map<StudentReadDTO>(student);
	}

	public async Task<bool> Create(StudentCreateDTO studentCreateDTO)
	{
		_logger.LogInformation("Creating new student");
		_logger.LogDebug($"Converting StudentCreateDTO into Student");
		var student = _mapper.Map<Student>(studentCreateDTO);

		if (studentCreateDTO.NationalIdScan != null)
		{
			_logger.LogInformation("Saving uploaded national id scan");
			student.NationalIdScan = await _fileStorageService.SaveFile(_containerName, studentCreateDTO.NationalIdScan);
		}

		if (studentCreateDTO.StudentIdScan != null)
		{
			_logger.LogInformation("Saving uploaded student id scan");
			student.StudentIdScan = await _fileStorageService.SaveFile(_containerName, studentCreateDTO.StudentIdScan);
		}

		_logger.LogInformation("Adding new student");
		_dbContext.Students.Add(student);
		await _dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<bool> Update(int id, StudentUpdateDTO studentUpdateDTO)
	{
		_logger.LogInformation($"Attempting to updating student with id {id}");
		var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

		if (student == null)
		{
			_logger.LogInformation($"Could not find student with id {id}");
			return false;
		}

		_logger.LogDebug($"Converting StudentUpdateDTO into Student");
		student = _mapper.Map(studentUpdateDTO, student);

		if (studentUpdateDTO.NationalIdScan != null)
		{
			_logger.LogInformation("Saving uploaded national id scan");
			student.NationalIdScan = await _fileStorageService.EditFile(_containerName, student.NationalIdScan, studentUpdateDTO.NationalIdScan);
		}

		if (studentUpdateDTO.StudentIdScan != null)
		{
			_logger.LogInformation("Saving uploaded student id scan");
			student.StudentIdScan = await _fileStorageService.EditFile(_containerName, student.NationalIdScan, studentUpdateDTO.StudentIdScan);
		}

		_logger.LogInformation($"Updating existing student with id {id}");
		await _dbContext.SaveChangesAsync();

		return true;
	}

	[HttpDelete("{id}")]
	public async Task<bool> Delete(int id)
	{
		_logger.LogInformation($"Attempting to delete student with id {id}");
		var student = await _dbContext.Students.FirstAsync(x => x.Id == id);

		if (student == null)
		{
			_logger.LogInformation($"Could not find student with id {id}");
			return false;
		}

		_logger.LogInformation($"Deleting student with id {id}");
		_dbContext.Remove(student);
		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Deleting uploaded national id scan");
		await _fileStorageService.DeleteFile(student.NationalIdScan, _containerName);
		_logger.LogInformation("Deleting uploaded student id scan");
		await _fileStorageService.DeleteFile(student.StudentIdScan, _containerName);

		return true;
	}
}
