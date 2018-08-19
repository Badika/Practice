using Clinic.Models.ClinicModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.DbModels
{
    public class ClinicContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Diagnos> Diagnoses { get; set; }
        public DbSet<AppliedDiagnos> AppliedDiagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
        }
    }
}
