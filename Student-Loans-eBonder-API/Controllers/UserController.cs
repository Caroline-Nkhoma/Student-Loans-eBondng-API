using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/users")]
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

	[HttpGet("self")]
	public async Task<ActionResult<UserReadDTO>> Get()
	{
		var email = HttpContext.User.Claims.First(x => x.Type == "email").Value;
		var user = await _userService.FindOne(email);

		if (user == null)
		{
			return NotFound();
		}
		else
		{
			return user;
		}
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<UserReadDTO>> Get(int id)
	{
		var user = await _userService.FindOne(id);

		if (user == null)
		{
			return NotFound();
		}
		else
		{
			return user;
		}
	}

	[HttpPost]
	public async Task<ActionResult> Post([FromForm] UserCreateDTO userCreateDTO)
	{
		var created = await _userService.Create(userCreateDTO);

		if (created)
		{
			return Created();
		}
		else
		{
			throw new NotImplementedException();
		}
	}

	[HttpPatch("{id}")]
	public async Task<ActionResult> Patch(int id, [FromForm] UserUpdateDTO userUpdateDTO)
	{
		var updated = await _userService.Update(id, userUpdateDTO);

		if (updated)
		{
			return NoContent();
		}
		else
		{
			return NotFound();
		}
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		var deleted = await _userService.Delete(id);

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
