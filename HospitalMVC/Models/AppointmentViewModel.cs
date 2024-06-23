using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Data.Entities;

namespace HospitalMVC.Models
{
    public class AppointmentViewModel : BaseEntity
    {
        
        [Required]
        [DisplayName("ID of the patient")]
        public int PatientId { get; set; }
        public virtual Patient? Patient { get; set; }

        [Required]
        [DisplayName("ID of the doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }
        [Required]
        [DisplayName("Date of the appointment")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [DisplayName("Symptoms")]
        public string? Symptoms { get; set; }
        [Required]
        [DisplayName("Diagnosis")]
        public string? Diagnosis { get; set; }
        [Required]
        [DisplayName("Treatment")]
        public string? Treatment { get; set; }
        [Required]
        [DisplayName("Cost")]
        public double Cost { get; set; }
    }
}
