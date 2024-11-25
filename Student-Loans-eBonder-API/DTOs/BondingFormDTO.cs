namespace StudentLoanseBonderAPI.DTOs;

public class BondingFormDTO
{
    public string FormId { get; set; }
    public string StudentName { get; set; }
    public string TotalLoanAmount { get; set; }
    public List<string> Documents { get; set; }
    public List<string> MissingDocuments { get; set; }
}
