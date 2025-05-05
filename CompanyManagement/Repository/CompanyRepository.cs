using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagement.data_base_context;
using CompanyManagement.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyManagement.Repository
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyModel>> GetAllAsync();
        Task<CompanyModel> GetByIdAsync(string CompanyId);
        Task<IEnumerable<CompanyModel>> GetByUserIdAsync(string userId);
        Task AddAsync(CompanyModel company);
        Task UpdateAsync(CompanyModel company);
        Task DeleteAsync(string CompanyId);
    }

    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyDbContext _context;

        public CompanyRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyModel>> GetAllAsync() => await _context.Companies.ToListAsync();

        public async Task<CompanyModel> GetByIdAsync(string CompanyId) => await _context.Companies.FindAsync(CompanyId);

        public async Task<IEnumerable<CompanyModel>> GetByUserIdAsync(string userId) =>
            await _context.Companies.Where(c => c.UserId == userId).ToListAsync();

        public async Task AddAsync(CompanyModel company)
        {
            company.CompanyId = Guid.NewGuid().ToString();
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CompanyModel company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string CompanyId)
        {
            var company = await _context.Companies.FindAsync(CompanyId);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }

}
