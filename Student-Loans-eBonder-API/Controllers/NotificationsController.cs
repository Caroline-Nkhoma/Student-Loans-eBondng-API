using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using StudentLoanseBonderAPI.Services;
using StudentLoanseBonderAPI.DTOs;

namespace StudentLoanseBonderAPI.Controllers;

[ApiController]
[Route("api/accounts/{accountId}/notifications")]
public class NotificationController : ControllerBase
{
    private readonly ILogger<NotificationController> _logger;
    private readonly NotificationService _notificationService;

    // Inject the NotificationService
    public NotificationController(ILogger<NotificationController> logger, NotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Get all notifications
    /// </summary>
    /// <returns>List of notifications</returns>
    [HttpGet]
    public async Task<ActionResult> GetNotifications()
    {
        // Fetch notifications from the service
        var notifications = await _notificationService.FindAll();
        return Ok(notifications); // Return a 200 status with the notifications
    }

    /// <summary>
    /// Get a single notification by ID
    /// </summary>
    /// <param name="id">Notification ID</param>
    /// <returns>Notification details</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetNotificationById(int id)
    {
        var notification = await _notificationService.FindOne(id);
        if (notification == null)
            return NotFound(); // Return a 404 if the notification is not found

        return Ok(notification); // Return a 200 with the notification
    }

    /// <summary>
    /// Create a new notification
    /// </summary>
    /// <param name="dto">CreateNotificationDTO object</param>
    /// <returns>Created notification</returns>
    [HttpPost]
    public async Task<ActionResult> CreateNotification([FromBody] NotificationCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); // Return 400 if validation fails

        var created = await _notificationService.Create(dto);

        if (created)
        {
            // Return a 201 status and the created notification
            return Created();
        }
        else
        {
            return Conflict();
        }
    }

    /// <summary>
    /// Update an existing notification
    /// </summary>
    /// <param name="id">Notification ID</param>
    /// <param name="dto">UpdateNotificationDTO object</param>
    /// <returns>No content if successful</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateNotification(int id, [FromBody] NotificationUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); // Return 400 if validation fails

        var success = await _notificationService.UpdateNotificationAsync(id, dto);
        if (!success)
            return NotFound(); // Return 404 if the notification does not exist

        return NoContent(); // Return 204 for a successful update
    }

    /// <summary>
    /// Delete a notification
    /// </summary>
    /// <param name="id">Notification ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNotification(int id)
    {
        var success = await _notificationService.DeleteNotificationAsync(id);
        if (!success)
            return NotFound(); // Return 404 if the notification does not exist

        return NoContent(); // Return 204 for a successful delete
    }
}

