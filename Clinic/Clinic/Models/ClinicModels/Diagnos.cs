using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.ClinicModels
{
    public class Diagnos : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        List<AppliedDiagnos> AppliedDiagnoses { get; set; }
    }
}
