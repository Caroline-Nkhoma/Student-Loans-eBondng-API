using StudentLoanseBonderAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class StudentCreateDTO
{
	public DateOnly? DateOfBirth {get; set;}
	public string? Sex {get; set;}
	public string? PostalAddress {get; set;}
	public string? HomeVillage {get; set;}
	public string? TraditionalAuthority {get; set;}
	public string? District {get; set;}
	[Phone]
	public string? PhoneNumber {get; set;}
	public string? NationalIdNumber {get; set;}
	public string? BankName {get; set;}
	public string? BranchName {get; set;}
	public string? BankAccountName {get; set;}
	public string? BankAccountNumber {get; set;}
	public GuardianCreateDTO? Guardian { get; set;}
	public string? InstitutionId {get; set;}
	public string? ProgrammeOfStudy {get; set;}
	public string? RegistrationNumber {get; set;}
	public string? AcademicYear {get; set;}
	[Range(minimum: 1, maximum: 6)]
	public int? YearOfStudy {get; set;}
	public IFormFile? NationalIdScan { get; set; }
	public IFormFile? StudentIdScan { get; set; }
}
