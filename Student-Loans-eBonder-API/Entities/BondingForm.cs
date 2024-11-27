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
	[Required]
	public string StudentFullName { get; set; } = string.Empty;
	[Required]
	public string LoansBoardOfficialFullName { get; set; } = string.Empty;
	[Required]
	public string InstitutionAdminFullName { get; set; } = string.Empty;
	[Required]
	public DateOnly StudentDateOfBirth { get; set; }
	[Required]
	public string StudentSex { get; set; } = string.Empty;
	[Required]
	public string StudentPostalAddress { get; set; } = string.Empty;
	[Required]
	public string StudentHomeVillage { get; set; } = string.Empty;
	[Required]
	public string StudentTraditionalAuthority { get; set; } = string.Empty;
	[Required]
	public string StudentDistrict { get; set; } = string.Empty;
	[Required]
	[Phone]
	public string StudentPhoneNumber { get; set; } = string.Empty;
	[Required]
	public string StudentNationalIdNumber { get; set; } = string.Empty;
	[Required]
	public string StudentBankName { get; set; } = string.Empty;
	[Required]
	public string StudentBranchName { get; set; } = string.Empty;
	[Required]
	public string StudentBankAccountName { get; set; } = string.Empty;
	[Required]
	public string StudentBankAccountNumber { get; set; } = string.Empty;
	[Required]
	public string GuardianFullName { get; set; } = string.Empty;
	[Required]
	public string GuardianPostalAddress { get; set; } = string.Empty;
	[Required]
	public string GuardianPhysicalAddress { get; set; } = string.Empty;
	[Required]
	public string GuardianHomeVillage { get; set; } = string.Empty;
	[Required]
	public string GuardianTraditionalAuthority { get; set; } = string.Empty;
	[Required]
	public string GuardianDistrict { get; set; } = string.Empty;
	[Required]
	[Phone]
	public string GuardianPhoneNumber { get; set; } = string.Empty;
	[Required]
	public string GuardianOccupation { get; set; } = string.Empty;
	[Required]
	public string InstitutionName { get; set; } = string.Empty;
	[Required]
	public string StudentProgrammeOfStudy { get; set; } = string.Empty;
	[Required]
	public string StudentRegistrationNumber { get; set; } = string.Empty;
	[Required]
	public string StudentAcademicYear { get; set; } = string.Empty;
	[Required]
	[Range(minimum: 1, maximum: 6)]
	public int StudentYearOfStudy { get; set; }
	[Required]
	public string StudentNationalIdScan { get; set; } = string.Empty;
	[Required]
	public string StudentStudentIdScan { get; set; } = string.Empty;
	[Required]
	public string StudentSignature { get; set; } = string.Empty;
	[Required]
	public string LoansBoardOfficialSignature { get; set; } = string.Empty;
	[Required]
	public string InstitutionAdminSignature { get; set; } = string.Empty;
	[Required]
	[Range(minimum: 0, maximum: (double)decimal.MaxValue)]
	public decimal TuitionLoanAmount { get; set; } = 0;
	[Required]
	[Range(minimum: 0, maximum: (double)decimal.MaxValue)]
	public decimal UpkeepLoanAmount { get; set; } = 0;
	[Required]
	public ICollection<Comment> Comments { get; set; } = [];
}
