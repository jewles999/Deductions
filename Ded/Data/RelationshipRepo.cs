using System.Collections.Generic;
using System.Linq;
using DeductionsAPI.Repo;
using DeductionsAPI.Models;

namespace DeductionsAPI.Data
{
    /// <summary>
    /// List of Relationships
    /// </summary>
    public class RelationshipRepo : IRelationshipRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public RelationshipRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Relationship> Relationships => _dbContext.Relationships.OrderBy(i => i.Id).ToList();
    }
}