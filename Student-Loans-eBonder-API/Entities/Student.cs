using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

[Table("student")]
public class Student
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Range(1, int.MaxValue)]
	[Column("student_id")]
	public int Id { get; set; }
	[Required]
	public string AccountId { get; set; }
	[Required]
	public IdentityUser Account { get; set; }
	public string? NationalIdScan { get; set; }
	public string? StudentIdScan { get; set; }
}
