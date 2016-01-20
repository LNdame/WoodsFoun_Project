using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicDiabetes
    {
        public int cdID { get; set; }

        public int EncounterID { get; set; }

        public bool cdDWFReferral { get; set; }

        public string cdNotOnMedsBSLevel { get; set; }

        public DateTime cdNextReviewDate { get; set; }

        public string cdOnMedsBSLevel { get; set; }

        public string cdHbA1C { get; set; }

        public string cdCreatinine { get; set; }

        public string cdCholesterol { get; set; }

        public string cdFootExam { get; set; }

        public string cdEyeTest { get; set; }

        public bool cdReferToClinic { get; set; }

        public int cdReferralNo { get; set; }

        public decimal cdBPDiastolic { get; set; }

        public decimal cdBPSystolic { get; set; }

        public DateTime cdDateOfVisit { get; set; }

        public bool cdDiagnosedAndGivenTreatment { get; set; }
    }
}
