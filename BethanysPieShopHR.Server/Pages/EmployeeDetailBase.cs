using BethanysPieShopHR.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShopHR.Server.Pages
{
    public class EmployeeDetailBase : ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; } = new Employee();
        [Inject]
        public IEmployeeDataService _employeeDataService { get; set; }
        protected async override Task OnInitializedAsync()
        {

            Employee = await _employeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));

        }

    }
}
