using StudentLoanseBonderAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class BondingFormReadDTO
{
	public string Id { get; set; }
	public string StudentFullName { get; set; }
	public string LoansBoardOfficialFullName { get; set; }
	public string InstitutionAdminFullName { get; set; }
	public DateOnly StudentDateOfBirth { get; set; }
	public string StudentSex { get; set; }
	public string StudentPostalAddress { get; set; }
	public string StudentHomeVillage { get; set; }
	public string StudentTraditionalAuthority { get; set; }
	public string StudentDistrict { get; set; }
	[Phone]
	public string StudentPhoneNumber { get; set; }
	public string StudentNationalIdNumber { get; set; }
	public string StudentBankName { get; set; }
	public string StudentBranchName { get; set; }
	public string StudentBankAccountName { get; set; }
	public string StudentBankAccountNumber { get; set; }
	public string GuardianFullName { get; set; }
	public string GuardianPostalAddress { get; set; }
	public string GuardianPhysicalAddress { get; set; }
	public string GuardianHomeVillage { get; set; }
	public string GuardianTraditionalAuthority { get; set; }
	public string GuardianDistrict { get; set; }
	[Phone]
	public string GuardianPhoneNumber { get; set; }
	public string GuardianOccupation { get; set; }
	public string InstitutionName { get; set; }
	public string StudentProgrammeOfStudy { get; set; }
	public string StudentRegistrationNumber { get; set; }
	public string StudentAcademicYear { get; set; }
	[Range(minimum: 1, maximum: 6)]
	public int StudentYearOfStudy { get; set; }
	public string StudentNationalIdScan { get; set; }
	public string StudentStudentIdScan { get; set; }
	public string StudentSignature { get; set; }
	public string LoansBoardOfficialSignature { get; set; }
	public string InstitutionAdminSignature { get; set; }
	[Range(minimum: 0, maximum: (double)decimal.MaxValue)]
	public decimal TuitionLoanAmount { get; set; }
	[Range(minimum: 0, maximum: (double)decimal.MaxValue)]
	public decimal UpkeepLoanAmount { get; set; }
	public List<CommentReadDTO> Comments { get; set; }
}
