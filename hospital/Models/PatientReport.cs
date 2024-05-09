using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital.models
{
    public class PatientReport
    {
        [Key]
        public int PatientReport_Id { get; set; }
        public string Report_name { get; set; }
        public string Report_MedicationName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Report_Date { get; set; }
        public double cost { get; set; }
        public string prescription { get; set; }
        public int? Patient_ID { get; set; }
        [ForeignKey(nameof(Patient_ID))]
        public PatientAccount Patient { get; set; }
        public int? Doctor_ID { get; set; }
        [ForeignKey(nameof(Doctor_ID))]
        public DoctorAccount Doctor { get; set; }

    }
}
