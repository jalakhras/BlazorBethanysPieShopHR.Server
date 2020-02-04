using BethanysPieShopHR.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHR.Server.Pages
{
    public class EmployeeEditBase : ComponentBase
    {
        [Inject]
        public IEmployeeDataService _employeeDataService { get; set; }  
        [Inject]
        public ICountryDataService _countryDataService { get; set; }
        [Inject]
        public IJobCategoryDataService _jobCategoryDataService { get; set; }
        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategory { get; set; } = new List<JobCategory>();

        public string CountryId = string.Empty;
        public string JobCategoryId = string.Empty;
        protected async override Task OnInitializedAsync()
        {
            Employee = await _employeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            Countries = (await _countryDataService.GetAllCountries()).ToList();
            CountryId = Employee.CountryId.ToString();
            JobCategory = (await _jobCategoryDataService.GetAllJobCategories()).ToList();
            JobCategoryId = Employee.JobCategoryId.ToString();
        }
    }
}
