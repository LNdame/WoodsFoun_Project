using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicChildHealth
    {
        public int cchID { get; set; }

        public int EncounterID { get; set; }

        public bool cchDWFReferral { get; set; }

        public bool cchPCRDone { get; set; }

        public bool cchCurrentRTHC { get; set; }

        public bool cchVaccinationsUpToDate { get; set; }

        public DateTime cchDateOfVisit { get; set; }
    }
}
