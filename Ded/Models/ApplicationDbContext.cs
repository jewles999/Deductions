using System.Data.Entity;

namespace DeductionsAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Deduction> Deductions { get; set; }

        public ApplicationDbContext()
            : base("makerDb")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}