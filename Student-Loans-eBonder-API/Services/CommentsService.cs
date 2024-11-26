using StudentLoanseBonderAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace StudentLoanseBonderAPI.Services
{
    public class CommentService(ApplicationDbContext dbContext, IMapper mapper, BondingFormService bondingFormService)
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
    }
}
