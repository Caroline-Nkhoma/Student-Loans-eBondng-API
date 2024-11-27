using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

[Table("form")]
public class BondingForm
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("form_id")]
	public string Id { get; set; }
    public string StudentName { get; set; }
    public string TotalLoanAmount { get; set; }
	public string? NationalIdScan { get; set; }
	public string? StudentIdScan { get; set; }
	public string? Signature { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
