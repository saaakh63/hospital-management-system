using hospital.models;
using Microsoft.EntityFrameworkCore;

namespace hospital.connect
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions options) : base(options) { }

        public static string WebRootPath { get; internal set; }

        //public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DoctorAccount> Doctors { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ListDoctors=doctors.db");
        //}
        public DbSet<PatientReport> PatientReports { get; set; }

        public DbSet<PatientAccount> Accounts { get;internal set; }
        public object DoctorAccount { get; internal set; }
    }

}

