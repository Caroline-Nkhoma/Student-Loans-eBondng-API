using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class StudentCreateDTO
{
	public IFormFile? NationalIdScan { get; set; }
	public IFormFile? StudentIdScan { get; set; }
}
