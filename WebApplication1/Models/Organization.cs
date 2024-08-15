using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Validations;

namespace WebApplication1.Models
{
    public class Organization
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Organization_EnsureCorrectNumberOfEmployees]
        public int NumberOfEmployees { get; set; }
    }
}
