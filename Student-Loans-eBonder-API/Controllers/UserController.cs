using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/accounts/{accountId}/users")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
	private readonly ILogger<UserController> _logger;
	private readonly UserService _userService;

	public UserController(ILogger<UserController> logger, UserService userService)
	{
		_logger = logger;
		_userService = userService;
	}

	[HttpGet]
	[Authorize(Roles = "SystemAdmin")]
	public async Task<ActionResult<UserReadDTO>> Get([FromRoute] string accountId)
	{
		var user = await _userService.FindOne(accountId);

		if (user == null)
		{
			return NotFound();
		}
		else
		{
			return user;
		}
	}

	[HttpPut]
	public async Task<ActionResult> Put([FromRoute] string accountId, [FromForm] UserCreateDTO userCreateDTO)
	{
		var hasBeenPut = await _userService.CreateOrUpdate(accountId, userCreateDTO);

		if (hasBeenPut)
		{
			return NoContent();
		}
		else
		{
			return BadRequest();
		}
	}

	[HttpPatch]
	public async Task<ActionResult> Patch([FromRoute] string accountId, [FromForm] UserUpdateDTO userUpdateDTO)
	{
		var updated = await _userService.Update(accountId, userUpdateDTO);

		if (updated)
		{
			return NoContent();
		}
		else
		{
			return NotFound();
		}
	}

	[HttpDelete]
	[Authorize(Roles = "SystemAdmin")]
	public async Task<ActionResult> Delete([FromRoute] string accountId)
	{
		var deleted = await _userService.Delete(accountId);

		if (deleted)
		{
			return NoContent();
		}
		else
		{
			return NotFound();
		}
	}
}
