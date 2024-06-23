using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Appointment : BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        public virtual Patient? Patient { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }
        [Required]
        public DateTime AppoinmentDate { get; set; }
        [Required]
        public string? Symptoms { get; set; }
        [Required]
        public string? Diagnosis { get; set; }
        [Required]
        public string? Treatment { get; set; }
        [Required]
        public double Cost { get; set; }

    }
}
