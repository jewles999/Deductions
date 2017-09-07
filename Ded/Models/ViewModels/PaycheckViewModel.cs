using System.Collections.Generic;

namespace DeductionsAPI.Models.ViewModels
{
    public class PaycheckPeopleViewModel
    {
        public string PersonType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Deduction { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class PaycheckViewModel
    {
        public IEnumerable<PaycheckPeopleViewModel> People { get; set; }
        public decimal PayPeriodSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal Discounts { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal PaycheckAmount { get; set; }
    }

    public class PaycheckQuery
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string DependentFirstName { get; set; }
    }

    public class PaycheckPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string EmpType { get; set; }
    }
}