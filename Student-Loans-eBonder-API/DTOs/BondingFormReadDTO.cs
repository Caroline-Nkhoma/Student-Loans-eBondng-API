namespace StudentLoanseBonderAPI.DTOs;

public class BondingFormReadDTO
{
	public string Id { get; set; }
    public string StudentName { get; set; }
    public string TotalLoanAmount { get; set; }
	public string? NationalIdScan { get; set; }
	public string? StudentIdScan { get; set; }
	public string? Signature { get; set; }
    public List<CommentReadDTO> Comments { get; set; }
}
