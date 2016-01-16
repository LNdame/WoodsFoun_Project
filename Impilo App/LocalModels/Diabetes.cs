using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Diabetes
    {
        public string ScreeningID { get; set; }

        public string YearOfDiagnosis { get; set; }

        public bool regularlyThirsty { get; set; }

        public bool WeightLoss { get; set; }

        public bool BlurredVision { get; set; }

        public bool UrinatingMore { get; set; }

        public bool NauseaOrVomitting { get; set; }

        public string FootExamResult { get; set; }

        public bool ReferralToClinic { get; set; }

        public string ReferralNo { get; set; }

        public bool FamilyMemberWith { get; set; }
    }
}
