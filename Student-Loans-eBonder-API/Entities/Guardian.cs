using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

[Table("guardian")]
public class Guardian
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Range(1, int.MaxValue)]
	[Column("guardian_id")]
	public int Id { get; set; }
	public string FirstName {get; set;} = string.Empty;
	public string Surname {get; set;} = string.Empty;
	public List<string> OtherNames {get; set;} = [];
	public string FullName
	{
		get => $"{FirstName} {string.Join(' ', [.. OtherNames])} {Surname}";
	}
	public string PostalAddress {get; set;} = string.Empty;
	public string PhysicalAddress {get; set;} = string.Empty;
	public string HomeVillage {get; set;} = string.Empty;
	public string TraditionalAuthority {get; set;} = string.Empty;
	public string District {get; set;} = string.Empty;
	[Length(minimumLength: 6, maximumLength: 16)]
	public string? PhoneNumber {get; set;}
	public string Occupation {get; set;} = string.Empty;
}