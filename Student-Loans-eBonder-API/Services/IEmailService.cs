namespace StudentLoanseBonderAPI.Services;

public interface IEmailService
{
	Task<bool> SendEmailAsync(string recipients, string subject, string body);
}
