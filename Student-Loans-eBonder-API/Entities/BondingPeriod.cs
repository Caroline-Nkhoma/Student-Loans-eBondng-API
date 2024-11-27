using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

[Table("bonding_period")]
public class BondingPeriod
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Range(1, int.MaxValue)]
	[Column("bonding_period_id")]
	public int Id { get; set; }
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndDate { get; set; }
}