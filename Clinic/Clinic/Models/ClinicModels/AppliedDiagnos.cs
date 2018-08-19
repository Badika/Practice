using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.ClinicModels
{
    public class AppliedDiagnos : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int DiagnosId { get; set; }
    }
}
