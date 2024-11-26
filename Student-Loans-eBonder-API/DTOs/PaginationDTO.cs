namespace StudentLoanseBonderAPI.DTOs;

public class PaginationDTO
{
    public int Page { get; set; } = 1;
    private readonly int _maxRecordsPerPage = 50;

	private int _recordsPerPage;

	public PaginationDTO()
	{
		_recordsPerPage = _maxRecordsPerPage;
	}

	public int RecordsPerPage
	{
		get => _recordsPerPage;
		set
		{
			_recordsPerPage = value > _maxRecordsPerPage ? _maxRecordsPerPage : value;
		}
	}

}
