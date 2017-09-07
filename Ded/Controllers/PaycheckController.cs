using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
