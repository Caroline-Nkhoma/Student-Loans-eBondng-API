namespace StudentLoanseBonderAPI.DTOs;

public class UserReadDTO
{
	public int Id { get; set; }
	public string AccountId { get; set; }
	public string? Signature { get; set; }
	public string? ProfilePicture { get; set; }
}
