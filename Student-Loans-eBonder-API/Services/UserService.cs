using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Entities;

namespace StudentLoanseBonderAPI.Services;

public class UserService
{
	private readonly ILogger<UserService> _logger;
	private readonly ApplicationDbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IFileStorageService _fileStorageService;
	private readonly string _containerName = "user-documents";

	public UserService(ILogger<UserService> logger, ApplicationDbContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
	{
		_logger = logger;
		_dbContext = dbContext;
		_mapper = mapper;
		_fileStorageService = fileStorageService;
	}

	public async Task<UserReadDTO?> FindOne(string accountId)
	{
		_logger.LogInformation($"Finding user belogning to account with id {accountId}");
		var user = await _dbContext.AccountUsers.FirstOrDefaultAsync(x => x.AccountId == accountId);

		if (user == null)
		{
			_logger.LogInformation($"Could not find user belogning to account with id {accountId}");
			return null;
		}

		_logger.LogInformation($"Found user belogning to account with id {accountId}");
		_logger.LogDebug($"Converting User into UserReadDTO");
		return _mapper.Map<UserReadDTO>(user);
	}

	public async Task<bool> CreateOrUpdate(string accountId, UserCreateDTO userCreateDTO)
	{
		_logger.LogInformation("Checking if account already has a user");
		var existingUser = await _dbContext.AccountUsers.FirstOrDefaultAsync(x => x.AccountId == accountId);

		if (existingUser == null)
		{
			_logger.LogInformation($"Account does not have user");

			_logger.LogInformation("Creating new user");
			_logger.LogDebug($"Converting UserCreateDTO into User");
			var user = _mapper.Map<User>(userCreateDTO);

			user.AccountId = accountId;

			if (userCreateDTO.Signature != null)
			{
				_logger.LogInformation("Saving uploaded signature");
				user.Signature = await _fileStorageService.SaveFile(_containerName, userCreateDTO.Signature);
			}

			if (userCreateDTO.ProfilePicture != null)
			{
				_logger.LogInformation("Saving uploaded profile picture");
				user.ProfilePicture = await _fileStorageService.SaveFile(_containerName, userCreateDTO.ProfilePicture);
			}

			_logger.LogInformation("Adding new user");
			_dbContext.AccountUsers.Add(user);
			await _dbContext.SaveChangesAsync();

			return true;
		}
		else
		{
			_logger.LogInformation("Updating existing user");
			_logger.LogDebug($"Converting UserCreateDTO into User");
			existingUser = _mapper.Map(userCreateDTO, existingUser);

			if (userCreateDTO.Signature != null)
			{
				_logger.LogInformation("Saving uploaded signature");
				existingUser.Signature = await _fileStorageService.EditFile(_containerName, existingUser.Signature, userCreateDTO.Signature);
			}

			if (userCreateDTO.ProfilePicture != null)
			{
				_logger.LogInformation("Saving uploaded profile picture");
				existingUser.ProfilePicture = await _fileStorageService.EditFile(_containerName, existingUser.ProfilePicture, userCreateDTO.ProfilePicture);
			}

			_logger.LogInformation($"Updating existing user belogning to account with id {accountId}");
			await _dbContext.SaveChangesAsync();

			return true;
		}
	}

	public async Task<bool> Update(string accountId, UserUpdateDTO userUpdateDTO)
	{
		_logger.LogInformation($"Attempting to updating user belonging to account with id {accountId}");
		var user = await _dbContext.AccountUsers.FirstOrDefaultAsync(x => x.AccountId == accountId);

		if (user == null)
		{
			_logger.LogInformation($"Could not find user belonging to account with id {accountId}");
			return false;
		}

		_logger.LogDebug($"Converting UserUpdateDTO into User");
		user = _mapper.Map(userUpdateDTO, user);

		if (userUpdateDTO.Signature != null)
		{
			_logger.LogInformation("Saving uploaded signature");
			user.Signature = await _fileStorageService.EditFile(_containerName, user.Signature, userUpdateDTO.Signature);
		}

		if (userUpdateDTO.ProfilePicture != null)
		{
			_logger.LogInformation("Saving uploaded profile picture");
			user.ProfilePicture = await _fileStorageService.EditFile(_containerName, user.ProfilePicture, userUpdateDTO.ProfilePicture);
		}

		_logger.LogInformation($"Updating existing user belonging to account with id {accountId}");
		await _dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<bool> Delete(string accountId)
	{
		_logger.LogInformation($"Attempting to delete user belonging to account with id {accountId}");
		var user = await _dbContext.AccountUsers.FirstOrDefaultAsync(x => x.AccountId == accountId);

		if (user == null)
		{
			_logger.LogInformation($"Could not find user belonging to account with id {accountId}");
			return false;
		}

		_logger.LogInformation($"Deleting user belonging to account with id {accountId}");
		_dbContext.Remove(user);
		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Deleting uploaded signature");
		await _fileStorageService.DeleteFile(containerName: _containerName, filePath: user.Signature);
		_logger.LogInformation("Deleting uploaded profile picture");
		await _fileStorageService.DeleteFile(containerName: _containerName, filePath: user.ProfilePicture);

		return true;
	}
}
