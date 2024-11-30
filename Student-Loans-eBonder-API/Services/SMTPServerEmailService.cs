using System.Net.Mail;

namespace StudentLoanseBonderAPI.Services;

public class SmtpServerEmailService : IEmailService
{
	private readonly ILogger<SmtpServerEmailService> _logger;
	private readonly SmtpClient _smtpClient;
	private readonly string _senderEmailAddress;

	public SmtpServerEmailService(ILogger<SmtpServerEmailService> logger, SmtpClient smtpClient, IConfiguration configuration)
	{
		_logger = logger;
		_smtpClient = smtpClient;
		_senderEmailAddress = configuration.GetValue<string>("EmailService:SenderEmailAddress");
	}

	public async Task<bool> SendEmailAsync(string recipients, string subject, string body)
	{
		_logger.LogInformation($"Sending email about '{subject}' to {recipients}");
		MailMessage message = new()
		{
			From = new MailAddress(_senderEmailAddress),
			Subject = subject,
			IsBodyHtml = true,
			Body = body
		};
		message.To.Add(recipients);

		try
		{
			await _smtpClient.SendMailAsync(message);
			_logger.LogInformation($"Successfully sent email about '{subject}' to {recipients}");
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError($"Failed to send email about '{subject}' to {recipients}");
			_logger.LogError(e.Message);
			return false;
		}
	}
}
