using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicHIV
    {
        public int chivID { get; set; }

        public int EncounterID { get; set; }

        public bool chivDWFReferral { get; set; }

        public decimal chivCD4 { get; set; }

        public decimal chivViralLoad { get; set; }

        public decimal chivBPSystolic { get; set; }

        public decimal chivBPDiastolic { get; set; }

        public DateTime chivDateOfVisit { get; set; }
    }
}
