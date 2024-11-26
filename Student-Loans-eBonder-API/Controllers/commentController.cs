using Microsoft.AspNetCore.Mvc;
using StudentLoanseBonderAPI.DTOs;
using StudentLoanseBonderAPI.Services;
using System.Threading.Tasks;

namespace StudentLoanseBonderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService)
    {
        _commentService = commentService;
    }

    // GET: api/BondingFormVerification/{formId}
    [HttpGet("{formId}")]
    public async Task<ActionResult<List<CommentReadDTO>>> Get(string formId)
    {
        var comments = await _commentService.FindAll(formId);
        return comments;
    }

   
}
