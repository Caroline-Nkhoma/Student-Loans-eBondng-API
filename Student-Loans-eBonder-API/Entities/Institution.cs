using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

[Table("instituion")]
[Index(nameof(Name), IsUnique = true)]
public class Institution
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("instituion_id")]
	public string Id { get; set; }
	[Required]
	public string Name { get; set; } = string.Empty;
	[Range(1, int.MaxValue)]
	public int? BondingPeriodId { get; set; }
	public BondingPeriod? BondingPeriod { get; set; }

}
