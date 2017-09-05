using DeductionsAPI.Repo;
using System.Collections.Generic;
using System.Linq;
using DeductionsAPI.Models;
using DeductionsAPI.Models.ViewModels;

namespace DeductionsAPI.Data
{
    /// <summary>
    /// Employee data functionality:
    /// AddEmployee 
    /// GetEmployees
    /// </summary>
    public class EmployeeRepo : IEnumerable
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public bool AddEmployee(EmployeeViewModel vm)
        {
            Employee emp = new Employee{ FirstName = vm.FirstName, LastName = vm.LastName, Salary = vm.Salary };
            _dbContext.Employees.Add(emp);

            var dependents = new List<Dependent>();
            foreach (var d in vm.Dependents)
            {
                dependents.Add(new Dependent { DependentId = d.DependentId, FirstName = d.FirstName,
                    Employee = emp, Relationship_Id = d.Relationship_Id });

            }
            emp.Dependents = dependents;
            return _dbContext.SaveChanges() > 0;
        }



        public IEnumerable<SummaryViewModel> GetEmployees()
        {
            var query = (IEnumerable<SummaryViewModel>)(
                    _dbContext.Employees
                       .GroupJoin(
                          _dbContext.Dependents,
                          e => e.EmployeeId,
                          d => d.EmployeeId,
                          (e, dependents) =>
                             new SummaryViewModel
                             {
                                 Id = e.EmployeeId,
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 Salary = e.Salary,
                                 DependentCount = dependents.Count()
                             }
                       )
                );
            return query.ToList();
        }
    }
}