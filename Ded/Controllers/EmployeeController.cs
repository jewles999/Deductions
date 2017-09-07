using DeductionsAPI.Repo;
using System.Threading.Tasks;
using System.Web.Http;
using DeductionsAPI.Models.ViewModels;

namespace DeductionsAPI.Controllers
{
    /// <summary>
    /// Employee API Controller
    /// </summary>
    [Route("api/Employee/{id?}")]
    public class EmployeeController : ApiController
    {
        private readonly IEnumerable _repo;
        public EmployeeController(IEnumerable repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Route: GET api/Employee
        /// </summary>
        /// <returns>
        /// JSON of Employees and Count of Dependents:
        /// {FirstName, LastName, Salary, DependentCount}
        /// </returns>
        public async Task<IHttpActionResult> GetAsync()
        {
            var emps = await Task.Run(() => _repo.GetEmployees());

            if (emps != null)
            {
                return Ok(emps);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Route: POST api/Employee
        /// Inserts new Employee and a list of Dependendents into the database
        /// </summary>
        /// <param name="vm">EmployeeViewModel from json</param>
        /// <returns>
        /// JSON of Employees and Count of Dependents:
        /// {FirstName, LastName, Salary, DependentCount}
        /// </returns>
        public async Task<IHttpActionResult> PostAsync(EmployeeViewModel vm)
        {
            var add = await Task.Run(() => _repo.AddEmployee(vm));
            if(add)
            {
                var emps = await Task.Run(() => _repo.GetEmployees());
                if (emps != null)
                {
                    return Ok(emps);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
    }
}
