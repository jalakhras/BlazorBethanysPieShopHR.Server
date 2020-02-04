using BethanysPieShopHR.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
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
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public ICountryDataService _countryDataService { get; set; }
        [Inject]
        public IJobCategoryDataService _jobCategoryDataService { get; set; }
        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategory { get; set; } = new List<JobCategory>();

        protected string CountryId = string.Empty;
        protected string JobCategoryId = string.Empty;

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        protected async override Task OnInitializedAsync()
        {
            Saved = false;
            //Employee = await _employeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            Countries = (await _countryDataService.GetAllCountries()).ToList();
            CountryId = Employee.CountryId.ToString();
            JobCategory = (await _jobCategoryDataService.GetAllJobCategories()).ToList();
            JobCategoryId = Employee.JobCategoryId.ToString();
            int.TryParse(EmployeeId, out var employeeId);

            if (employeeId == 0) //new employee is being created
            {
                //add some defaults
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
            }
            else
            {
                Employee = await _employeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            }

        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            if (Employee.EmployeeId == 0) //new
            {
                var addedEmployee = await _employeeDataService.AddEmployee(Employee);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await _employeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }

        protected async Task DeleteEmployee()
        {
            await _employeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected async Task NavigateToOverview()
        {
            _navigationManager.NavigateTo("/employeeoverview");
        }
    }
}
