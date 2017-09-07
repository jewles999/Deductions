using System.Collections.Generic;

namespace DeductionsAPI.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public List<Dependent> Dependents { get; set; }
    }
}