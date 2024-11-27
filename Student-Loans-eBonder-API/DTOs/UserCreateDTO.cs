using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class UserCreateDTO
{
	
	public string? FirstName {get; set;}
	public string? Surname {get; set;}
	public List<string>? OtherNames {get; set;}
	public IFormFile? Signature { get; set; }
	public IFormFile? ProfilePicture { get; set; }
}
