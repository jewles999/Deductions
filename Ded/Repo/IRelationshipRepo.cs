using DeductionsAPI.Models;
using System.Collections.Generic;

namespace DeductionsAPI.Repo
{
    public interface IRelationshipRepo
    {
        IEnumerable<Relationship> Relationships { get; }
    }
}
