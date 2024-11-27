using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

public class GuardianCreateDTO
{
	public string? FirstName { get; set; }
	public string? Surname {get; set;}
	public List<string>? OtherNames {get; set;}
	public string? PostalAddress {get; set;}
	public string? PhysicalAddress {get; set;}
	public string? HomeVillage {get; set;}
	public string? TraditionalAuthority {get; set;}
	public string? District {get; set;}
	[Phone]
	public string? PhoneNumber {get; set;}
	public string? Occupation {get; set;}
}