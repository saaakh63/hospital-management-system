//using Microsoft.AspNet.Identity;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Primitives;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace hospital.models
//{
//    public class PatientAccount
//    {
//        internal StringValues selectedGender;

//        public string Name { get; set; }
//        [Key]
//        public string Email { get; set; }

//        [Required]
//        [StringLength(100, MinimumLength = 8)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Password")]

//        public string Password { get; set; }


//          public string PhoneNumber { get; set; }
//        public string? gender { get; set; }



//    }
//}
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital.models
{
    public class PatientAccount
    {
        internal StringValues selectedGender;
        [Key]
        public int Patient_ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "")]
        public string Email { get; set; }
        [NotMapped]
        //   public IFormFile? ProfilePicture { get; set; }
        [Required]
        public string? ProfilePicture { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string Password { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "")]
        public string PhoneNumber { get; set; }
        public string? Gender { get; set; }

        public ICollection<PatientReport> PatientReports { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}

