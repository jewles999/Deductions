using System.Collections.Generic;
using System.Linq;
using DeductionsAPI.Repo;
using DeductionsAPI.Models.ViewModels;
using DeductionsAPI.Models;
using Deductions.Business;
using Ded.Settings;
using System;

namespace DeductionsAPI.Data
{
    /// <summary>
    /// Creates a detailed paycheck view per employeeId
    /// </summary>
    public class PaycheckRepo : IPaycheckRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPaycheck _paycheck;

        public PaycheckRepo(ApplicationDbContext dbContext, IPaycheck paycheck)
        {
            _dbContext = dbContext;
            _paycheck = paycheck;
        }

        /// <summary>
        /// Gets Employee and Dependent data from the db
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public PaycheckViewModel CalculatePaycheck(int employeeId)
        {
            var emp = (from e in _dbContext.Employees
                       join d in _dbContext.Dependents on e.EmployeeId equals d.EmployeeId into subQ
                       from deps in subQ.DefaultIfEmpty()
                       where e.EmployeeId == employeeId
                       select new PaycheckQuery
                       {
                           FirstName = e.FirstName,
                           LastName = e.LastName,
                           Salary = e.Salary,
                           DependentFirstName = deps == null ? String.Empty : (deps.FirstName)
                       }).ToList();
            return BuildPaycheck(emp);
        }

        /// <summary>
        /// Iterates through the db result set and builds a paycheck view
        /// </summary>
        /// <param name="emp">List query result</param>
        /// <returns></returns>
        private PaycheckViewModel BuildPaycheck(List<PaycheckQuery> emp)
        {
            var people = new List<PaycheckPeopleViewModel>();
            var paycheckView = new PaycheckViewModel();

            try
            {
                //if any exceptions occur, they will result from settings not being pulled from Deductions table correctly, or are missing
                var person = new PaycheckPerson { FirstName = emp[0].FirstName, LastName = emp[0].LastName, Salary = emp[0].Salary, EmpType = Constants.EmployeeType };

                var employee = BuildPaycheckModel(person);
                people.Add(employee);

                if (emp.Count > 1)
                {
                    foreach (var d in emp)
                    {
                        person = new PaycheckPerson { FirstName = d.DependentFirstName, LastName = d.LastName, Salary = 0, EmpType = Constants.DependentType };
                        var dependent = BuildPaycheckModel(person);
                        people.Add(dependent);
                    }
                }
                paycheckView = BuildPaycheckView(people, _paycheck.CalculatePayPeriodValue(emp[0].Salary, DeductionContstants.NumberOfPayPeriods));
                paycheckView.People = people;
            }
            catch(Exception)
            {
                //LOG to the database or other means
                throw;
            }
            return paycheckView;
        }

        /// <summary>
        /// Builds summary money values of the paycheck
        /// </summary>
        /// <param name="people"></param>
        /// <param name="paycheckAmount"></param>
        /// <returns></returns>
        private PaycheckViewModel BuildPaycheckView(List<PaycheckPeopleViewModel> people, decimal paycheckAmount)
        {
            var paycheckView = new PaycheckViewModel();

            foreach (var p in people)
            {
                paycheckView.Deductions += p.Deduction;
                paycheckView.Discounts += p.Discount;
            }

            paycheckView.PayPeriodSalary = _paycheck.Round(paycheckAmount);
            paycheckView.TotalDeductions = _paycheck.Round(paycheckView.Deductions - paycheckView.Discounts);
            paycheckView.PaycheckAmount = _paycheck.Round(paycheckAmount - paycheckView.TotalDeductions);

            return paycheckView;
        }

        /// <summary>
        /// Builds line by line money values of the paycheck
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        private PaycheckPeopleViewModel BuildPaycheckModel(PaycheckPerson person)
        {
            var discount = _paycheck.Round(_paycheck.CalculateLetterADiscount(person.FirstName, person.LastName, DeductionContstants.Discount));
            var benefitCost = person.EmpType == Constants.EmployeeType ? DeductionContstants.EmployeeDeduction : DeductionContstants.DependentDeduction;
            var discountAmount = _paycheck.Round(_paycheck.CalculateDiscountedDeduction(benefitCost, discount));

            var vm = new PaycheckPeopleViewModel
            {
                PersonType = person.EmpType,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Deduction = _paycheck.Round(_paycheck.CalculatePayPeriodValue(benefitCost, DeductionContstants.NumberOfPayPeriods)),
                Discount = _paycheck.Round(_paycheck.CalculatePayPeriodValue(discountAmount, DeductionContstants.NumberOfPayPeriods)),
                SubTotal = _paycheck.Round(_paycheck.CalculatePayPeriodValue(_paycheck.CalculateSubTotal(benefitCost, discountAmount),
                                                                    DeductionContstants.NumberOfPayPeriods))
            };
            return vm;
        }
    }
}