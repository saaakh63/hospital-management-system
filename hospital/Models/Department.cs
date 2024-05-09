using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace hospital.models

{
    public class Department
    {

        [Key]
        public int Department_Id { get; set; }

        public string Department_Name { get; set; }
        public ICollection<DoctorAccount> Doctors { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}
