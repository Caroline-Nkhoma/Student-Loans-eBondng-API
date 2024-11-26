using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class StudentCreateDTO
{
	public DateOnly? DateOfBirth {get; set;}
	public string? Sex {get; set;} = string.Empty;
	public string? PostalAddress {get; set;} = string.Empty;
	public string? HomeVillage {get; set;} = string.Empty;
	public string? TraditionalAuthority {get; set;} = string.Empty;
	public string? District {get; set;} = string.Empty;
	[Length(minimumLength: 6, maximumLength: 16)]
	public string? PhoneNumber {get; set;}
	public string? NationalIdNumber {get; set;}
	public string? BankName {get; set;} = string.Empty;
	public string? BranchName {get; set;} = string.Empty;
	public string? BankAccountName {get; set;} = string.Empty;
	public string? BankAccountNumber {get; set;}
	public string? InstitutionName {get; set;} = string.Empty;
	public string? ProgrammeOfStudy {get; set;} = string.Empty;
	public string? RegistrationNumber {get; set;}
	public string? AcademicYear {get; set;} = string.Empty;
	[Range(minimum: 1, maximum: 6)]
	public int? YearOfStudy {get; set;}
	public IFormFile? NationalIdScan { get; set; }
	public IFormFile? StudentIdScan { get; set; }
}
