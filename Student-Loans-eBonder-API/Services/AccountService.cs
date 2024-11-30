using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentLoanseBonderAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentLoanseBonderAPI.Services;

public class AccountService
{
	private readonly ILogger<AccountService> _logger;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly IConfiguration _configuration;
	private readonly ApplicationDbContext _dbContext;
	private readonly IEmailService _emailService;

	public AccountService(ILogger<AccountService> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, ApplicationDbContext dbContext, IEmailService emailService)
	{
		_logger = logger;
		_userManager = userManager;
		_signInManager = signInManager;
		_configuration = configuration;
		_dbContext = dbContext;
		_emailService = emailService;
	}

	public async Task<IdentityUser?> FindOneByEmail(string email)
	{
		_logger.LogInformation($"Finding account with email {email}");
		var user = await _userManager.FindByEmailAsync(email);

		return user;
	}

	public async Task<IdentityUser?> FindOneById(string accountId)
	{
		_logger.LogInformation($"Finding account with id {accountId}");
		var user = await _userManager.FindByIdAsync(accountId);

		return user;
	}

	public async Task<IdentityResult> AssignRole(IdentityUser user, string roleName)
	{
		_logger.LogInformation($"Attempting to add account with id {user.Id} to {roleName} role");
		var result = await _userManager.AddToRoleAsync(user, roleName);
		return result;
	}

	public async Task<List<string>> GetRoles(IdentityUser calledOnUser)
	{
		_logger.LogInformation($"Fetching all the roles that the account with id {calledOnUser.Id} belongs to");
		var roles = await _userManager.GetRolesAsync(calledOnUser);
		return [.. roles];
	}

	public async Task<List<string>?> GetProtectedRoles(IdentityUser calledOnUser, IdentityUser callingUser)
	{
		if ((calledOnUser.Id == callingUser.Id) || (await _userManager.IsInRoleAsync(callingUser, "SystemAdmin")))
		{
			return await GetRoles(calledOnUser);
		}
		else
		{
			return null;
		}
	}

	public async Task<IdentityResult> RemoveRole(IdentityUser user, string roleName)
	{
		_logger.LogInformation($"Attempting to remove account with id {user.Id} from {roleName} role");
		var result = await _userManager.RemoveFromRoleAsync(user, roleName);
		return result;
	}

	public async Task<IdentityResult> Register(UserCredentials userCredentials, Func<UserCredentials, Task<bool>> sendEmail)
	{
		_logger.LogInformation($"Attempt to register {userCredentials.Email}");

		var user = new IdentityUser
		{
			UserName = userCredentials.Email,
			Email = userCredentials.Email,
		};

		var sentEmail = await sendEmail(userCredentials);

        if (sentEmail)
        {
			var result = await _userManager.CreateAsync(user, userCredentials.Password);
			return result;
        }
		else
		{
			return IdentityResult.Failed(new IdentityError() { Description = "Failed to send email to given address. Ensure email address is valid." });
		}
	}

	public async Task<IdentityResult> RegisterStudent(UserCredentials userCredentials)
	{
		Func<UserCredentials, Task<bool>> sendEmail = async (_) => await _emailService.SendEmailAsync(
			recipients: userCredentials.Email,
			subject: "Welcome to Students Loans eBonder",
			body: """
			<h1>Welcome to Students Loans eBonder!</h1>
			<p>You have successfully registered as <b><a href="mailto:someone@example.com">someone@example.com</a></b>.</p>
			<p>You are now a user with a <b>Student</b> role.</p>
			<p><b>Thank you for signing up!</b></p>
			<hr />
			<p><b>Students Loans eBonder System.</b></p>
			<p>Please note that this email does not expect any reply and will not respond when replied to.</p>
			""");

		return await Register(userCredentials, sendEmail);
	}

	public async Task<IdentityResult> RegisterLoansBoardOfficial(UserCredentials userCredentials)
	{
		Func<UserCredentials, Task<bool>> sendEmail = async (_) => await _emailService.SendEmailAsync(
			recipients: userCredentials.Email,
			subject: "Welcome to Students Loans eBonder",
			body: """
			<h1>Welcome to Students Loans eBonder!</h1>
			<p>You have successfully registered as <b><a href="mailto:someone@example.com">someone@example.com</a></b>.</p>
			<p>You are now a user with a <b>Loans Board Official</b> role.</p>
			<p><b>Thank you for signing up!</b></p>
			<hr />
			<p><b>Students Loans eBonder System.</b></p>
			<p>Please note that this email does not expect any reply and will not respond when replied to.</p>
			""");

		return await Register(userCredentials, sendEmail);
	}

	public async Task<IdentityResult> RegisterInstitutionAdmin(UserCredentials userCredentials)
	{
		Func<UserCredentials, Task<bool>> sendEmail = async (_) => await _emailService.SendEmailAsync(
			recipients: userCredentials.Email,
			subject: "Welcome to Students Loans eBonder",
			body: """
			<h1>Welcome to Students Loans eBonder!</h1>
			<p>You have successfully registered as <b><a href="mailto:someone@example.com">someone@example.com</a></b>.</p>
			<p>You are now a user with an <b>Institution Administrator</b> role.</p>
			<p><b>Thank you for signing up!</b></p>
			<hr />
			<p><b>Students Loans eBonder System.</b></p>
			<p>Please note that this email does not expect any reply and will not respond when replied to.</p>
			""");

		return await Register(userCredentials, sendEmail);
	}

	public async Task<IdentityResult> RegisterSystemAdmin(UserCredentials userCredentials)
	{
		Func<UserCredentials, Task<bool>> sendEmail = async (_) => await _emailService.SendEmailAsync(
			recipients: userCredentials.Email,
			subject: "Welcome to Students Loans eBonder",
			body: """
			<h1>Welcome to Students Loans eBonder!</h1>
			<p>You have successfully registered as <b><a href="mailto:someone@example.com">someone@example.com</a></b>.</p>
			<p>You are now a user with a <b>System Administrator</b> role.</p>
			<p><b>Thank you for signing up!</b></p>
			<hr />
			<p><b>Students Loans eBonder System.</b></p>
			<p>Please note that this email does not expect any reply and will not respond when replied to.</p>
			""");

		return await Register(userCredentials, sendEmail);
	}

	public async Task<SignInResult> Login(UserCredentials userCredentials)
	{
		_logger.LogInformation($"Attempt to log into {userCredentials.Email}");

		// may want to set lockoutOnFailure to be true in production
		return await _signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, isPersistent: false, lockoutOnFailure: false);
	}

	internal async Task<AuthenticationResponse> BuildToken(UserCredentials userCredentials)
	{
		var claims = new List<Claim>()
		{
			new ("email", userCredentials.Email)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var expiration = DateTime.UtcNow.AddMonths(1);

		var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

		var user = await FindOneByEmail(userCredentials.Email);

		return new AuthenticationResponse
		{
			AccountId = user.Id,
			Token = new JwtSecurityTokenHandler().WriteToken(token),
			Expiration = expiration,
		};
	}
}
