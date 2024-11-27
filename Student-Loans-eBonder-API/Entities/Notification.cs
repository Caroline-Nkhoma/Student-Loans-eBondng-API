using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities;

[Table("notification")]
public class Notification
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Range(1, int.MaxValue)]
	[Column("notification_id")]
	public int Id { get; set; }

	[Required]
	[Column("title")]
	public string Title { get; set; }

	[Required]
	[Column("description")]
	public string Description { get; set; }

	[Required]
	public bool IsRead { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	[Column("Account_id")]
	public string? AccountId { get; set; } // Optional FK to link to a user account
}
