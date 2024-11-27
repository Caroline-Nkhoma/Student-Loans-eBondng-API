namespace StudentLoanseBonderAPI.DTOs;

public class UserReadDTO
{
	
	public string FirstName {get; set;}
	public string Surname {get; set;}
	public List<string> OtherNames {get; set;}
	public string FullName
	{
		get => $"{FirstName} {string.Join(' ', [.. OtherNames])} {Surname}";
	}
	public int Id { get; set; }
	public string AccountId { get; set; }
	public string? Signature { get; set; }
	public string? ProfilePicture { get; set; }
}
