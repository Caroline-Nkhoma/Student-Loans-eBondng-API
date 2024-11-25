using AutoMapper;
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
	private readonly string _containerName = "student-documents";

	public StudentService(ILogger<StudentService> logger, ApplicationDbContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
	{
		_logger = logger;
		_dbContext = dbContext;
		_mapper = mapper;
		_fileStorageService = fileStorageService;
	}

	public async Task<StudentReadDTO?> FindOne(string accountId)
	{
		_logger.LogInformation($"Finding student belogning to account with id {accountId}");
		var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.AccountId == accountId);

		if (student == null)
		{
			_logger.LogInformation($"Could not find student belonging to account with id {accountId}");
			return null;
		}

		_logger.LogInformation($"Found student belonging to account with id {accountId}");
		_logger.LogDebug($"Converting Student into StudentReadDTO");
		return _mapper.Map<StudentReadDTO>(student);
	}

	public async Task<bool> CreateOrUpdate(string accountId, StudentCreateDTO studentCreateDTO)
	{
		_logger.LogInformation("Checking if account already has a student");
		var existingStudent = await _dbContext.Students.FirstOrDefaultAsync(x => x.AccountId == accountId);

		if (existingStudent == null)
		{
			_logger.LogInformation($"Account does not have student");

			_logger.LogInformation("Creating new student");
			_logger.LogDebug($"Converting StudentCreateDTO into Student");
			var student = _mapper.Map<Student>(studentCreateDTO);

			student.AccountId = accountId;

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
		else
		{
			_logger.LogInformation("Updating existing student");
			_logger.LogDebug($"Converting StudentCreateDTO into Student");
			existingStudent = _mapper.Map(studentCreateDTO, existingStudent);

			if (studentCreateDTO.NationalIdScan != null)
			{
				_logger.LogInformation("Saving uploaded national id scan");
				existingStudent.NationalIdScan = await _fileStorageService.EditFile(_containerName, existingStudent.NationalIdScan, studentCreateDTO.NationalIdScan);
			}

			if (studentCreateDTO.StudentIdScan != null)
			{
				_logger.LogInformation("Saving uploaded student id scan");
				existingStudent.StudentIdScan = await _fileStorageService.EditFile(_containerName, existingStudent.StudentIdScan, studentCreateDTO.StudentIdScan);
			}

			_logger.LogInformation($"Updating existing student belogning to account with id {accountId}");
			await _dbContext.SaveChangesAsync();

			return true;
		}
	}

	public async Task<bool> Update(string accountId, StudentUpdateDTO studentUpdateDTO)
	{
		_logger.LogInformation($"Attempting to updating student belonging to account with id {accountId}");
		var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.AccountId == accountId);

		if (student == null)
		{
			_logger.LogInformation($"Could not find student belonging to account with id {accountId}");
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

		_logger.LogInformation($"Updating existing student belonging to account with id {accountId}");
		await _dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<bool> Delete(string accountId)
	{
		_logger.LogInformation($"Attempting to delete student belonging to account with id {accountId}");
		var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.AccountId == accountId);

		if (student == null)
		{
			_logger.LogInformation($"Could not find student belonging to account with id {accountId}");
			return false;
		}

		_logger.LogInformation($"Deleting student belonging to account with id {accountId}");
		_dbContext.Students.Remove(student);
		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Deleting uploaded national id scan");
		await _fileStorageService.DeleteFile(containerName: _containerName, filePath: student.NationalIdScan);
		_logger.LogInformation("Deleting uploaded student id scan");
		await _fileStorageService.DeleteFile(containerName: _containerName, filePath: student.StudentIdScan);

		return true;
	}
}
