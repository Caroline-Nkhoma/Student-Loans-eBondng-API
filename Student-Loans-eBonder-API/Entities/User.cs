using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudentLoanseBonderAPI.Entities;

[Table("user")]
public class User
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Range(1, int.MaxValue)]
	[Column("user_id")]
	public int Id { get; set; }
	[Required]
	public string AccountId { get; set; }
	[Required]
	public IdentityUser Account {get; set;}
	public string FirstName {get; set;} = string.Empty;
	public string Surname {get; set;} = string.Empty;
	public ICollection<string> OtherNames {get; set;} = [];
	public string? Signature { get; set; }
	public string? ProfilePicture { get; set; }
}
