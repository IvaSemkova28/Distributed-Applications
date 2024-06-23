using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Data.Entities;

namespace HospitalMVC.Models
{
    public class DoctorViewModel : BaseEntity
    {   
        [Required]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        [Required]
        [DisplayName("Gender")]
        public char Genger { get; set; }
        [Required]
        [DisplayName("Specialization")]
        public string? Specialization { get; set; }
        [Required]
        [DisplayName("Phone")]
        public string? Phone { get; set; }
        [Required]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("DateOfEmployment")]
        public DateTime DateOfEmployment { get; set; }
    }
}
