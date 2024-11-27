using System.ComponentModel.DataAnnotations;

namespace StudentLoanseBonderAPI.DTOs;

public class InstitutionCreateDTO
{
	[Required]
	public string Name { get; set; }
}
