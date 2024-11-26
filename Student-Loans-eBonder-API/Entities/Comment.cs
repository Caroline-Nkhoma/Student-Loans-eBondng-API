using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

[Table("comment")]
public class Comment
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Range(1, int.MaxValue)]
	[Column("comment_id")]
	public int Id { get; set; }
    public string Text { get; set; }
	[Required]
    public string FormId { get; set; }
	[Required]
    public BondingForm Form { get; set; }
}