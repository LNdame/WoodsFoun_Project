using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicOther
    {
        public int coID { get; set; }

        public int EncounterID { get; set; }

        public bool coDWFReferral { get; set; }

        public string coCondition { get; set; }

        public string coOutcome { get; set; }

        public decimal coBPSystolic { get; set; }

        public decimal coBPDiastolic { get; set; }

        public DateTime coDateOfVisit { get; set; }
    }
}
