using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class UserCreateDTO
{
	[Required]
	public string AccountId { get; set; }
	public IFormFile? Signature { get; set; }
	public IFormFile? ProfilePicture { get; set; }
}
