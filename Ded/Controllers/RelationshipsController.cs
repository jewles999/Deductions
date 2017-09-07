using DeductionsAPI.Repo;
using System.Threading.Tasks;
using System.Web.Http;

namespace DeductionsAPI.Controllers
{
    /// <summary>
    /// Relationships API Controller
    /// Retrieves a list of Dependent Relationship types
    /// to populate the drop down on the client
    /// </summary>
    [Route("api/Relationships")]
    public class RelationshipsController : ApiController
    {
        private readonly IRelationshipRepo _repo;

        public RelationshipsController(IRelationshipRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Route: GET api/Relationships 
        /// </summary>
        /// <returns>
        /// JSON of Relationships
        /// [{"Id":1,"Description":"Spouse"},{},...]
        /// </returns>
        public async Task<IHttpActionResult> GetAsync()
        {
            var rels = await Task.Run(() => _repo.Relationships);

            if (rels != null)
            {
                return Ok(rels);
            }
            else
            {
                return NotFound();
            }
        }
       
    }
}
