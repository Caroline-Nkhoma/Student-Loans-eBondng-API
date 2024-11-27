using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

public class GuardianUpdateDTO
{
	public string FirstName {get; set;} = string.Empty;
	public string Surname {get; set;} = string.Empty;
	public List<string> OtherNames {get; set;} = [];
	public string PostalAddress {get; set;} = string.Empty;
	public string PhysicalAddress {get; set;} = string.Empty;
	public string HomeVillage {get; set;} = string.Empty;
	public string TraditionalAuthority {get; set;} = string.Empty;
	public string District {get; set;} = string.Empty;
	[Phone]
	public string? PhoneNumber {get; set;}
	public string Occupation {get; set;} = string.Empty;
}