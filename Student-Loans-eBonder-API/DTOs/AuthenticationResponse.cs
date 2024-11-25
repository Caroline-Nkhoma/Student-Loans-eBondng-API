namespace StudentLoanseBonderAPI.DTOs;

public class AuthenticationResponse
{
	public required string AccountId { get; set; }
	public required string Token { get; set; }
	public required DateTime Expiration { get; set; }
}
