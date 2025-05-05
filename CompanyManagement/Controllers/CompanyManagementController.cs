using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagement.Models;
using CompanyManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyManagement.Controllers
{
    public class CompanyManagementController
    {

        private readonly ICompanyService _service;

        public CompanyManagementController(ICompanyService service)
        {
            _service = service;
        }

        [Function("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "companies")] HttpRequest req)
        {
            var companies = await _service.GetAllCompaniesAsync();
            return new OkObjectResult(companies);
        }

        [Function("GetCompanyById")]
        public async Task<IActionResult> GetCompanyById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "companies/{CompanyId}")] HttpRequest req, string CompanyId)
        {
            var company = await _service.GetCompanyByIdAsync(CompanyId);
            return company != null ? new OkObjectResult(company) : new NotFoundResult();
        }

        [Function("GetCompaniesByUser")]
        public async Task<IActionResult> GetCompaniesByUser(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "companies/user/{userId}")] HttpRequest req, string userId)
        {
            var companies = await _service.GetCompaniesByUserIdAsync(userId);
            return new OkObjectResult(companies);
        }

        [Function("CreateCompany")]
        public async Task<IActionResult> CreateCompany(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "companies")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var company = JsonConvert.DeserializeObject<CompanyModel>(requestBody);

            await _service.AddCompanyAsync(company);
            return new OkObjectResult(company);
        }

        [Function("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "companies/{CompanyId}")] HttpRequest req, string CompanyId)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedCompany = JsonConvert.DeserializeObject<CompanyModel>(requestBody);
            updatedCompany.CompanyId = CompanyId;

            await _service.UpdateCompanyAsync(updatedCompany);
            return new OkObjectResult(updatedCompany);
        }

        [Function("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "companies/{CompanyId}")] HttpRequest req, string CompanyId)
        {
            await _service.DeleteCompanyAsync(CompanyId);
            return new OkResult();
        }

    }
}