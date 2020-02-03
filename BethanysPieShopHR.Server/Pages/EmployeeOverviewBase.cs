using BethanysPieShopHR.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHR.Server.Pages
{
    public class EmployeeOverviewBase : ComponentBase
    {
        [Inject]
        public IEmployeeDataService _employeeDataService { get; set; } 
        public IEnumerable<Employee> Employees { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employees = (await _employeeDataService.GetAllEmployees()).ToList();
        }
    }
}


