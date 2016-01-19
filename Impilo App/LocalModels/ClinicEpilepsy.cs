using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicEpilepsy
    {
        public int ceID { get; set; }

        public int EncounterID { get; set; }

        public bool ceDWFReferral { get; set; }

        public int ceNoFitsInLastMonth { get; set; }

        public string ceDrugSideEffects { get; set; }

        public decimal ceBPSystolic { get; set; }

        public decimal ceBPDiastolic { get; set; }

        public DateTime ceDateOfVisit { get; set; }
    }
}
