﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

	public async Task<UserReadDTO?> FindOne(int id)
	{
		_logger.LogInformation($"Finding user with id {id}");
		var user = await _dbContext.AccountUsers.FirstOrDefaultAsync(x => x.Id == id);

		if (user == null)
		{
			_logger.LogInformation($"Could not find user with id {id}");
			return null;
		}

		_logger.LogInformation($"Found user with id {id}");
		_logger.LogDebug($"Converting User into UserReadDTO");
		return _mapper.Map<UserReadDTO>(user);
	}

	public async Task<bool> Create(UserCreateDTO userCreateDTO)
	{
		_logger.LogInformation("Creating new user");
		_logger.LogDebug($"Converting UserCreateDTO into User");
		var user = _mapper.Map<User>(userCreateDTO);

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

	public async Task<bool> Update(int id, UserUpdateDTO userUpdateDTO)
	{
		_logger.LogInformation($"Attempting to updating user with id {id}");
		var user = await _dbContext.AccountUsers.FirstOrDefaultAsync(x => x.Id == id);

		if (user == null)
		{
			_logger.LogInformation($"Could not find user with id {id}");
			return false;
		}

		_logger.LogDebug($"Converting UserUpdateDTO into user");
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

		_logger.LogInformation($"Updating existing user with id {id}");
		await _dbContext.SaveChangesAsync();
		return true;
	}

	[HttpDelete("{id}")]
	public async Task<bool> Delete(int id)
	{
		_logger.LogInformation($"Attempting to delete user with id {id}");
		var user = await _dbContext.AccountUsers.FirstAsync(x => x.Id == id);

		if (user == null)
		{
			_logger.LogInformation($"Could not find user with id {id}");
			return false;
		}

		_logger.LogInformation($"Deleting user with id {id}");
		_dbContext.Remove(user);
		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Deleting uploaded signature");
		await _fileStorageService.DeleteFile(user.Signature, _containerName);
		_logger.LogInformation("Deleting uploaded profile picture");
		await _fileStorageService.DeleteFile(user.ProfilePicture, _containerName);

		return true;
	}
}
