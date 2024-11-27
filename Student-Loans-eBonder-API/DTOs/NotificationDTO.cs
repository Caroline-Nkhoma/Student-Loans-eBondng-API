namespace StudentLoanseBonderAPI.DTOs;
public class NotificationDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
}
