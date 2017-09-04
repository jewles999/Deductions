using System.ComponentModel.DataAnnotations;

namespace DeductionsAPI.Models
{
    public class Relationship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Description { get; set; }
    }
}