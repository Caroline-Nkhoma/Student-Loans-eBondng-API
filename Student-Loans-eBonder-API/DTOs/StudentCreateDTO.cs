using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class StudentCreateDTO
{
	[Required]
	public string AccountId { get; set; }
	public IFormFile? NationalIdScan { get; set; }
	public IFormFile? StudentIdScan { get; set; }
}
