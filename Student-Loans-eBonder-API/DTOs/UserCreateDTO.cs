using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class UserCreateDTO
{
	public IFormFile? Signature { get; set; }
	public IFormFile? ProfilePicture { get; set; }
}
