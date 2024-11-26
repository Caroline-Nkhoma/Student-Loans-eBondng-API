using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class UserCreateDTO
{
	
	public string FirstName {get; set;} = string.Empty;
	public string Surname {get; set;} = string.Empty;
	public List<string> OtherNames {get; set;} = [];
	public string FullName
	{
		get => $"{FirstName} {string.Join(' ', [.. OtherNames])} {Surname}";
	}
	
	public IFormFile? Signature { get; set; }
	public IFormFile? ProfilePicture { get; set; }
}
