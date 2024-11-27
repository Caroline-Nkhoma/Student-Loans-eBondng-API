using StudentLoanseBonderAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StudentLoanseBonderAPI.Entities;

namespace StudentLoanseBonderAPI.Services
{
	public class CommentService(ApplicationDbContext dbContext, IMapper mapper)
	{
		public async Task<List<CommentReadDTO>> FindAll(string formId)
		{
			var form = await dbContext.BondingForms.FirstOrDefaultAsync(x => x.Id == formId);

			if (form == null) {
				return [];
			}

			var comments = form.Comments.ToList();
			
			return mapper.Map<List<CommentReadDTO>>(comments);
		}

		public async Task<bool> Create(string formId, CommentCreateDTO commentCreateDTO)
		{
			var form = await dbContext.BondingForms.FirstOrDefaultAsync(x => x.Id == formId);

			if (form == null)
			{
				return false;
			}

			var comment = mapper.Map<Comment>(commentCreateDTO);
			form.Comments.Add(comment);
			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
