using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicHypertension
    {
        public int chID { get; set; }

        public int EncounterID { get; set; }

        public bool chDWFReferral { get; set; }

        public decimal chDiagAndTreatSystolic { get; set; }

        public decimal chDiagAndTreatDiastolic { get; set; }

        public decimal chNotOnMedsSystolic { get; set; }

        public decimal chNotOnMedsDiastolic { get; set; }

        public DateTime chNextReviewDate { get; set; }

        public decimal chOnMedsSystolic { get; set; }

        public decimal chOnMedsDiastolic { get; set; }

        public string chBloodSugarLevel { get; set; }

        public string chCreatinine { get; set; }

        public string chCholesterol { get; set; }

        public DateTime chDateOfVisit { get; set; }
    }
}
