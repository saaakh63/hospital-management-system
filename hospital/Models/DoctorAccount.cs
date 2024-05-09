using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital.models
{
    public class DoctorAccount
    {
        [Key]
        public int Doctor_Id { get; set; }

        [Required]
        public string Doctor_Name { get; set; }

        [Required]
        public string Doctor_Gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "")]
        public string Doctor_PhoneNum { get; set; }

       

        [Required]
        public string Doctor_Contact { get; set; }

        // Foreign Key
        [ForeignKey("Department")]
        public int Department_Id { get; set; }

        // Navigation property
        public Department Department { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
