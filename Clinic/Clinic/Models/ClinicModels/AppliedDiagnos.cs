using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.ClinicModels
{
    public class AppliedDiagnos : BaseEntity
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public int DiagnosId { get; set; }
        public Diagnos Diagnos { get; set; }

        public AppliedDiagnos()
        {

        }

        public AppliedDiagnos(int AppointmentId, int DiagnosId)
        {
            this.AppointmentId = AppointmentId;
            this.DiagnosId = DiagnosId;
        }
    }
}
