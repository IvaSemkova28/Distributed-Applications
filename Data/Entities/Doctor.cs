using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Doctor : BaseEntity
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public char Genger { get; set; }
        [Required]
        public string? Specialization { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public DateTime DateOfEmployment { get; set; }
    }
}
