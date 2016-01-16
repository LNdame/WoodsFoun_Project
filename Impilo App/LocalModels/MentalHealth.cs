using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class MentalHealth
    {
        public string ScreeningID { get; set; }

        public bool PregnantBefore { get; set; }

        public string NoOfPregnancies { get; set; }

        public string HowManySuccessful { get; set; }

        public string WhereDeliveredLasBaby { get; set; }

        public string Caesarian { get; set; }

        public string BabyUnder2_5Kgs { get; set; }

        public string ChildrenDiedUnder1Year { get; set; }

        public string ChildrenDiedBetween1to5Years { get; set; }

        public bool PAPSmearInLast5Years { get; set; }

        public string LastBloodTestResult { get; set; }

        public DateTime DateOfFirstANC { get; set; }

        public DateTime DateOfLastANC { get; set; }

        public bool ReferredToClinic { get; set; }

        public string ReferralNo { get; set; }

        public DateTime DateOfNextANC { get; set; }

        public DateTime ExpectedDateOfDelivery { get; set; }

        public bool IntendFormulaFeed { get; set; }

        public bool IntendBreastFeed { get; set; }
        public bool RegisteredOnMomConnect { get; set; }
    }
}
