using System.Threading.Tasks;
using System.Web.Http;
using DeductionsAPI.Repo;

namespace DeductionsAPI.Controllers
{
    [Route("api/Paycheck/{id?}")]
    public class PaycheckController : ApiController
    {
        private readonly IPaycheckRepo _repo;
        public PaycheckController(IPaycheckRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Gets paycheck view for the employee and dependents
        /// </summary>
        /// <param name="id">EmployeeId</param>
        /// <returns>JSON</returns>
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var paycheck = await Task.Run(() => _repo.CalculatePaycheck(id));

            if (paycheck != null)
            {
                return Ok(paycheck);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
