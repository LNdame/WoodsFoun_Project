using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class MaternalHealth
    {
        public int EncounterID { get; set; }

        public bool PregnantBefore { get; set; }

        public string NoOfPregnancies { get; set; }

        public string HowManySuccessful { get; set; }

        public string WhereDeliveredLasBaby { get; set; }



        public bool Caesarian { get; set; }

        public bool BabyUnder2_5Kgs { get; set; }

        public bool ChildrenDiedUnder1Year { get; set; }

        public bool ChildrenDiedBetween1to5Years { get; set; }

        public bool PAPSmearInLast5Years { get; set; }

        public string LastBloodTestResult { get; set; }

        public DateTime DateOfLastDelivery { get; set; }

        public string ApgarScore1Min { get; set; }

        public string ApgarScore5Min { get; set; }
        public bool PNC6Days { get; set; }
        public bool PNC6Weeks { get; set; }

        public bool ReferredToClinic1 { get; set; }

        public string ReferralNo1 { get; set; }

        //part two
        public DateTime DateOfFirstANC { get; set; }

        public DateTime DateOfLastANC { get; set; }

        public bool GoneforANC1 { get; set; }
        public bool GoneforANC2 { get; set; }

        public bool ReferredToClinic { get; set; }

        public string ReferralNo { get; set; }

        public DateTime DateOfNextANC { get; set; }

        public DateTime ExpectedDateOfDelivery { get; set; }

        public bool IntendFormulaFeed { get; set; }

        public bool IntendBreastFeed { get; set; }
        public bool RegisteredOnMomConnect { get; set; }

        public string BreastFeedHowLong { get; set; }//not  needed
    }
}
