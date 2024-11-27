using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/forms/{formId}/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentReadDTO>>> Get(string formId)
    {
        var comments = await _commentService.FindAll(formId);
        return comments;
    }

	[HttpPost]
	public async Task<ActionResult> Post(string formId, [FromBody] CommentCreateDTO commentCreateDTO)
	{
		var created = await _commentService.Create(formId, commentCreateDTO);

        if (created)
        {
            return Created();
        }
        else
        {
            return BadRequest();
        }
	}
}
