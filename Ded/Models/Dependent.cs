using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeductionsAPI.Models
{
    public class Dependent
    {
        [Key]
        public int DependentId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public int Relationship_Id { get; set; }
    }
}