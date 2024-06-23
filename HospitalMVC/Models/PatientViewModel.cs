using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HospitalMVC.Models
{
    public class PatientViewModel : BaseEntity
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }
}
