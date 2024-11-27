using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class BondingFormCreateDTO
{
	[Required]
	public string StudentFullName { get; set; }
	[Required]
	public string LoansBoardOfficialFullName { get; set; }
	[Required]
	public string InstitutionAdminFullName { get; set; }
	[Required]
	public DateOnly StudentDateOfBirth { get; set; }
	[Required]
	public string StudentSex { get; set; }
	[Required]
	public string StudentPostalAddress { get; set; }
	[Required]
	public string StudentHomeVillage { get; set; }
	[Required]
	public string StudentTraditionalAuthority { get; set; }
	[Required]
	public string StudentDistrict { get; set; }
	[Required]
	[Phone]
	public string StudentPhoneNumber { get; set; }
	[Required]
	public string StudentNationalIdNumber { get; set; }
	[Required]
	public string StudentBankName { get; set; }
	[Required]
	public string StudentBranchName { get; set; }
	[Required]
	public string StudentBankAccountName { get; set; }
	[Required]
	public string StudentBankAccountNumber { get; set; }
	[Required]
	public string GuardianFullName { get; set; }
	[Required]
	public string GuardianPostalAddress { get; set; }
	[Required]
	public string GuardianPhysicalAddress { get; set; }
	[Required]
	public string GuardianHomeVillage { get; set; }
	[Required]
	public string GuardianTraditionalAuthority { get; set; }
	[Required]
	public string GuardianDistrict { get; set; }
	[Required]
	[Phone]
	public string GuardianPhoneNumber { get; set; }
	[Required]
	public string GuardianOccupation { get; set; }
	[Required]
	public string InstitutionName { get; set; }
	[Required]
	public string StudentProgrammeOfStudy { get; set; }
	[Required]
	public string StudentRegistrationNumber { get; set; }
	[Required]
	public string StudentAcademicYear { get; set; }
	[Required]
	[Range(minimum: 1, maximum: 6)]
	public int StudentYearOfStudy { get; set; }
	[Required]
	public IFormFile StudentNationalIdScan { get; set; }
	[Required]
	public IFormFile StudentStudentIdScan { get; set; }
	[Required]
	public IFormFile StudentSignature { get; set; }
	[Required]
	public IFormFile LoansBoardOfficialSignature { get; set; }
	[Required]
	public IFormFile InstitutionAdminSignature { get; set; }
	[Required]
	[Range(minimum: 0, maximum: (double)decimal.MaxValue)]
	public decimal TuitionLoanAmount { get; set; } = 0;
	[Required]
	[Range(minimum: 0, maximum: (double)decimal.MaxValue)]
	public decimal UpkeepLoanAmount { get; set; } = 0;
}
