using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly ILogger<AccountController> _logger;
	private readonly AccountService _accountService;

	public AccountController(ILogger<AccountController> logger, AccountService accountService)
	{
		_logger = logger;
		_accountService = accountService;
	}

	private async Task<ActionResult<AuthenticationResponse>> Register(UserCredentials userCredentials, Func<UserCredentials, Task<IdentityResult>> registerUser, string roleToAdd)
	{
		var result = await registerUser(userCredentials);

		if (result.Succeeded)
		{
			_logger.LogInformation($"{userCredentials.Email}, has registered an account");
			
			var user = await _accountService.FindOneByEmail(userCredentials.Email);
			var addToUserRoleResult = await _accountService.AssignRole(user, "User");

			if (addToUserRoleResult.Succeeded)
			{
				_logger.LogInformation($"Successfully added account with email {userCredentials.Email} to User role");
			}
			else
			{
				_logger.LogError($"Failed to add account with email {userCredentials.Email} to User role");
			}

			var addToRoleResult = await _accountService.AssignRole(user, roleToAdd);

			if (addToUserRoleResult.Succeeded)
			{
				_logger.LogInformation($"Successfully added account with email {userCredentials.Email} to {roleToAdd} role");
			}
			else
			{
				_logger.LogError($"Failed to add account with email {userCredentials.Email} to {roleToAdd} role");
			}

			return await _accountService.BuildToken(userCredentials);
		}
		else
		{
			_logger.LogInformation($"Attempt to register {userCredentials.Email} failed");
			return BadRequest(result.Errors);
		}
	}

	[HttpGet("register/students")]
	private async Task<ActionResult<AuthenticationResponse>> RegisterStudent([FromBody] UserCredentials userCredentials)
	{
		Func<UserCredentials, Task<IdentityResult>> registerUser = async (_) => await _accountService.RegisterStudent(userCredentials);

		return await Register(userCredentials, registerUser, "Student");
	}

	[HttpGet("register/loans-board-officials")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "LoansBoardOfficial,SystemAdmin")]
	private async Task<ActionResult<AuthenticationResponse>> RegisterLoansBoardOfficial([FromBody] UserCredentials userCredentials)
	{
		Func<UserCredentials, Task<IdentityResult>> registerUser = async (_) => await _accountService.RegisterLoansBoardOfficial(userCredentials);

		return await Register(userCredentials, registerUser, "LoansBoardOfficial");
	}

	[HttpGet("register/institution-admins")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "InstitutionAdmin,LoansBoardOfficial,SystemAdmin")]
	private async Task<ActionResult<AuthenticationResponse>> RegisterInstitutionAdmin([FromBody] UserCredentials userCredentials)
	{
		Func<UserCredentials, Task<IdentityResult>> registerUser = async (_) => await _accountService.RegisterInstitutionAdmin(userCredentials);

		return await Register(userCredentials, registerUser, "InstitutionAdmin");
	}

	[HttpGet("register/system-admins")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SystemAdmin")]
	private async Task<ActionResult<AuthenticationResponse>> RegisterSystemAdmin([FromBody] UserCredentials userCredentials)
	{
		Func<UserCredentials, Task<IdentityResult>> registerUser = async (_) => await _accountService.RegisterSystemAdmin(userCredentials);

		return await Register(userCredentials, registerUser, "SystemAdmin");
	}

	[HttpPost("login")]
	public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials userCredentials)
	{
		var result = await _accountService.Login(userCredentials);

		if (result.Succeeded)
		{
			_logger.LogInformation($"{userCredentials.Email}, has logged into their account");
			return await _accountService.BuildToken(userCredentials);
		}
		else
		{
			_logger.LogInformation($"Attempt to log into {userCredentials.Email} failed");
			return BadRequest("Incorrect login");
		}
	}

	[HttpPost("{accountId}/roles")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SystemAdmin")]
	public async Task<ActionResult> AssignRole(string accountId, [FromBody] string role)
	{
		var user = await _accountService.FindOneById(accountId);

		if (user == null)
		{
			_logger.LogInformation($"Could not find account with id {accountId}");
			return NotFound();
		}

		var result = await _accountService.AssignRole(user, role);

		if (result.Succeeded)
		{
			_logger.LogInformation($"Successfully assigned account with id {accountId} to User role");
			return NoContent();
		}
		else
		{
			_logger.LogError($"Failed to assigned account with id {accountId} to User role");
			return BadRequest(result.Errors);
		}
	}

	[HttpGet("{accountId}/roles")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public async Task<ActionResult<List<string>>> GetRoles(string accountId)
	{
		var user = await _accountService.FindOneById(accountId);

		if (user == null)
		{
			_logger.LogInformation($"Could not find account with id {accountId}");
			return NotFound();
		}

		return await _accountService.GetRoles(user);
	}
}
