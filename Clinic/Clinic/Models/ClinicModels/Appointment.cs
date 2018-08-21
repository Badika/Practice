using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.ClinicModels
{
    public class Appointment : BaseEntity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime Date { get; set; }

        public List<AppliedDiagnos> AppliedDiagnoses { get; set; }

        public Appointment()
        {

        }
        public Appointment(int id)
        {
            this.Id = id;
        }
    }
}
