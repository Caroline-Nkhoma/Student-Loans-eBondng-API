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


	[HttpPost("register")]
	public async Task<ActionResult<AuthenticationResponse>> Register([FromBody] UserCredentials userCredentials)
	{
		var result = await _accountService.Register(userCredentials);

		if (result.Succeeded)
		{
			_logger.LogInformation($"{userCredentials.Email}, has registered an account");
			return _accountService.BuildToken(userCredentials);
		}
		else
		{
			_logger.LogInformation($"Attempt to register {userCredentials.Email} failed");
			return BadRequest(result.Errors);
		}
	}

	[HttpPost("login")]
	public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials userCredentials)
	{
		var result = await _accountService.Login(userCredentials);

		if (result.Succeeded)
		{
			_logger.LogInformation($"{userCredentials.Email}, has logged into their account");
			return _accountService.BuildToken(userCredentials);
		}
		else
		{
			_logger.LogInformation($"Attempt to log into {userCredentials.Email} failed");
			return BadRequest("Incorrect login");
		}
	}
}
