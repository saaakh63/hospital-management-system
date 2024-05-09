using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace hospital.models

{
    public class Appointment
    {

        public int Id { get; set; }
        public string Patient_name { get; set; }


        public int? Department_Id { get; set; }
        [ForeignKey(nameof(Department_Id))]
        public Department? Department { get; set; }


        public int? Doctor_ID { get; set; }
        [ForeignKey(nameof(Doctor_ID))]
        public DoctorAccount? Doctor { get; set; }
        public int? Patient_Id { get; set; }
        [ForeignKey(nameof(Patient_Id))]
        public PatientAccount? Patient { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Appointment_Date { get; set; }


        public string? Phonenumber { get; set; }
        public string? Symptoms { get; set; }



    }
}
