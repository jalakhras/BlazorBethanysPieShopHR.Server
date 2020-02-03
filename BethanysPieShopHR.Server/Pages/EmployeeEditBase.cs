using BethanysPieShopHR.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BethanysPieShopHR.Server.Pages
{
    public class EmployeeEditBase : ComponentBase
    {
        [Inject]
        public IEmployeeDataService _employeeDataService { get; set; }
        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        protected async override Task OnInitializedAsync()
        {
            Employee = await _employeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
        }
    }
}
