namespace StudentLoanseBonderAPI.DTOs;

public class BondingFormCreateDTO
{
    public string StudentName { get; set; }
    public string TotalLoanAmount { get; set; }
	public string NationalIdScan { get; set; }
	public string StudentIdScan { get; set; }
	public string Signature { get; set; }
}
