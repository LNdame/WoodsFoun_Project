using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Hypertension
    {
        public string ScreeningID { get; set; }

        public string YearOfDiagnosis { get; set; }

        public bool Headache { get; set; }

        public bool BlurredVision { get; set; }

        public bool ChestPain { get; set; }

        public bool ReferralToClinic { get; set; }

        public string ReferalNo { get; set; }

        public bool EverHadStroke { get; set; }

        public string YearOfStroke { get; set; }
        public int HowManyInFamilyOnMedsForHypertension { get; set; }
        public bool AnyOneInFamilyHadStroke { get; set; }
    }
}
