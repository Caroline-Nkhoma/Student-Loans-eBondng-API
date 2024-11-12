namespace StudentLoanseBonderAPI.DTOs;

public class StudentReadDTO
{
	public int Id { get; set; }
	public string AccountId { get; set; }
	public string? NationalIdScan { get; set; }
	public string? StudentIdScan { get; set; }
}
