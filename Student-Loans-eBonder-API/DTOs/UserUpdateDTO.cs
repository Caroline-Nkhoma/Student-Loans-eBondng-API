namespace StudentLoanseBonderAPI.DTOs;

public class UserUpdateDTO
{
	
	public string FirstName {get; set;}
	public string Surname {get; set;}
	public List<string> OtherNames {get; set;}
	public IFormFile? Signature { get; set; }
	public IFormFile? ProfilePicture { get; set; }
}
