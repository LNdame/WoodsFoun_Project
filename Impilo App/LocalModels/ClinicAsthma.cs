using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicAsthma
    {
        public int caID { get; set; }

        public int EncounterID { get; set; }

        public bool caDWFReferral { get; set; }

        public string caPeakRespiratoryFlowRate { get; set; }

        public decimal caBPSystolic { get; set; }

        public decimal caBPDiastolic { get; set; }

        public DateTime caDateOfVisit { get; set; }
    }
}
