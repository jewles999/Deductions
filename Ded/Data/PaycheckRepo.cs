using DeductionsAPI.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeductionsAPI.Models.ViewModels;
using DeductionsAPI.Models;
using Deductions.Business;

namespace DeductionsAPI.Data
{
    public class PaycheckRepo : IPaycheckRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILetterADiscount _paycheck;
        private const string EmployeeType = "Employee";
        private const string DependentType = "Dependent";


        public PaycheckRepo(ApplicationDbContext dbContext, ILetterADiscount paycheck)
        {
            _dbContext = dbContext;
            _paycheck = paycheck;
        }


        public PaycheckViewModel CalculatePaycheck(int employeeId)
        {
            var people = new List<PaycheckPeopleViewModel>();
            var paycheckView = new PaycheckViewModel();

            var emp = (from e in _dbContext.Employees
                        join d in _dbContext.Dependents on e.EmployeeId equals d.EmployeeId
                        where e.EmployeeId == employeeId
                        select new PaycheckPerson
                        {
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            Salary = e.Salary,
                            DependentFirstName = d.FirstName,
                            RelationshipId = d.Relationship_Id
                        }).ToList();


            var employee = BuildEmployeeModel(emp[0]);
            people.Add(employee);


            foreach (var d in emp)
            {
                var dependent = BuildDependentModel(d);
                people.Add(dependent);
            }

            paycheckView = BuildPaycheckView(people, emp[0].Salary / DeductionContstants.NumberOfPayPeriods);
            paycheckView.People = people;


            return paycheckView;
        }

        private PaycheckViewModel BuildPaycheckView(List<PaycheckPeopleViewModel> people, decimal paycheckAmount)
        {
            var paycheckView = new PaycheckViewModel();

            foreach (var p in people)
            {
                paycheckView.Deductions += p.Deduction;
                paycheckView.Discounts += p.Discount;
            }

            paycheckView.PayPeriodSalary = paycheckAmount;
            paycheckView.TotalDeductions = paycheckView.Deductions - paycheckView.Discounts;
            paycheckView.PaycheckAmount = paycheckAmount - paycheckView.TotalDeductions;

            return paycheckView;
        }

        private PaycheckPeopleViewModel BuildEmployeeModel(PaycheckPerson emp)
        {
            decimal discount = _paycheck.CalculateLetterADiscount(emp.FirstName, emp.LastName, DeductionContstants.Discount);
            decimal discountAmount = _paycheck.CalculateDiscountedDeduction(DeductionContstants.EmployeeDeduction, discount);

            var person = new PaycheckPeopleViewModel
            {
                PersonType = EmployeeType,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Deduction = DeductionContstants.EmployeeDeduction / DeductionContstants.NumberOfPayPeriods,
                Discount = discountAmount / DeductionContstants.NumberOfPayPeriods,
                SubTotal = _paycheck.CalculateSubTotal(DeductionContstants.EmployeeDeduction, discountAmount) / DeductionContstants.NumberOfPayPeriods
            };
            return person;
        }

        private PaycheckPeopleViewModel BuildDependentModel(PaycheckPerson dep)
        {
            decimal discount = _paycheck.CalculateLetterADiscount(dep.DependentFirstName, dep.LastName, DeductionContstants.Discount);
            decimal discountAmount = _paycheck.CalculateDiscountedDeduction(DeductionContstants.DependentDeduction, discount);
            var person = new PaycheckPeopleViewModel
            {
                PersonType = DependentType,
                FirstName = dep.DependentFirstName,
                LastName = dep.LastName,
                Deduction = DeductionContstants.DependentDeduction / DeductionContstants.NumberOfPayPeriods,
                Discount = discountAmount / DeductionContstants.NumberOfPayPeriods,
                SubTotal = _paycheck.CalculateSubTotal(DeductionContstants.DependentDeduction, discountAmount) / DeductionContstants.NumberOfPayPeriods
            };
            return person;
        }
    }
}