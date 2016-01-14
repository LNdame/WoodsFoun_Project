
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class MentalHealth
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> PregnantBefore { get; set; }
        public string NoOfPregnancies { get; set; }
        public string HowManySuccessful { get; set; }
        public string WhereDeliveredLasBaby { get; set; }
        public string Caesarian { get; set; }
        public string BabyUnder2_5Kgs { get; set; }
        public string ChildrenDiedUnder1Year { get; set; }
        public string ChildrenDiedBetween1to5Years { get; set; }
        public Nullable<bool> PAPSmearInLast5Years { get; set; }
        public string LastBloodTestResult { get; set; }
        public Nullable<System.DateTime> DateOfFirstANC { get; set; }
        public Nullable<System.DateTime> DateOfLastANC { get; set; }
        public Nullable<bool> ReferredToClinic { get; set; }
        public string ReferralNo { get; set; }
        public Nullable<System.DateTime> DateOfNextANC { get; set; }
        public Nullable<System.DateTime> ExtectedDateOfDelivery { get; set; }
        public Nullable<bool> IntendFormulaFeed { get; set; }
        public Nullable<bool> RegisteredOnMomConnect { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
