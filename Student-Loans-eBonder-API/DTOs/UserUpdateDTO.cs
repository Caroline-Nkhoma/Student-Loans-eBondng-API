namespace StudentLoanseBonderAPI.DTOs;

public class UserUpdateDTO
{
	public IFormFile? Signature { get; set; }
	public IFormFile? ProfilePicture { get; set; }
}
