using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagement.Models;
using CompanyManagement.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyManagement.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyModel>> GetAllCompaniesAsync();
        Task<CompanyModel> GetCompanyByIdAsync(string CompanyId);
        Task<IEnumerable<CompanyModel>> GetCompaniesByUserIdAsync(string userId);
        Task AddCompanyAsync(CompanyModel company);
        Task UpdateCompanyAsync(CompanyModel company);
        Task DeleteCompanyAsync(string CompanyId);
    }

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CompanyModel>> GetAllCompaniesAsync() => await _repository.GetAllAsync();

        public async Task<CompanyModel> GetCompanyByIdAsync(string CompanyId) => await _repository.GetByIdAsync(CompanyId);

        public async Task<IEnumerable<CompanyModel>> GetCompaniesByUserIdAsync(string userId) =>
            await _repository.GetByUserIdAsync(userId);
        public async Task AddCompanyAsync(CompanyModel company) => await _repository.AddAsync(company);

        public async Task UpdateCompanyAsync(CompanyModel company) => await _repository.UpdateAsync(company);

        public async Task DeleteCompanyAsync(string CompanyId) => await _repository.DeleteAsync(CompanyId);
    }

}
