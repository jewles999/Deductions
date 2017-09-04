using DeductionsAPI.Models.ViewModels;
using DeductionsAPI.Models.ViewModels;
using System.Collections.Generic;

namespace DeductionsAPI.Repo
{
    public interface IEnumerable
    {
        bool AddEmployee(EmployeeViewModel vm);
        IEnumerable<SummaryViewModel> GetEmployees();
    }
}
