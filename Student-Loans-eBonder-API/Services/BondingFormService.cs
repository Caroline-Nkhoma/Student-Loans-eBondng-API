using StudentLoanseBonderAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentLoanseBonderAPI.Services
{
    public class BondingFormService
    {
        private readonly List<BondingFormDTO> _bondingForms = new List<BondingFormDTO>();

        public async Task<BondingFormDTO> GetBondingFormAsync(string id)
        {
            var form = _bondingForms.Find(b => b.FormId == id);
            return await Task.FromResult(form);
        }

        public async Task<BondingFormDTO> CreateBondingFormAsync(BondingFormDTO bondingFormDTO)
        {
            _bondingForms.Add(bondingFormDTO);
            return await Task.FromResult(bondingFormDTO);
        }

        public async Task<BondingFormDTO> UpdateBondingFormAsync(string id, BondingFormDTO bondingFormDTO)
        {
            var form = _bondingForms.Find(b => b.FormId == id);
            if (form != null)
            {
                form.StudentName = bondingFormDTO.StudentName;
                form.TotalLoanAmount = bondingFormDTO.TotalLoanAmount;
                form.Documents = bondingFormDTO.Documents;
                form.MissingDocuments = bondingFormDTO.MissingDocuments;
            }
            return await Task.FromResult(form);
        }
    }
}
