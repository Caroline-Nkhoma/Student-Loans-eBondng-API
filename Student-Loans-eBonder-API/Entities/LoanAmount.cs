using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentLoanseBonderAPI.Entities
{
    [Table("loan_amount")]
    public class LoanAmount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("loan_amount_id")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0.")]
        public int Id { get; set; }

        [Required]
        [Column("loan_amount_identifier")]
        public string LoanAmountIdentifier { get; set; }

        [Required]
        [Column("total_loan_amount")]
        public decimal Amount { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        [Column("user_id")]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
