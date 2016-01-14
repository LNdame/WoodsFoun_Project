
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChildHealth
    {
        public string ScreeningID { get; set; }
        public string NameOfMother { get; set; }
        public Nullable<bool> ChildWithRTHC { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferalNo { get; set; }
        public string ListConcernsReChild { get; set; }
        public Nullable<bool> ReferToClinic2 { get; set; }
        public string ReferToOVC { get; set; }
        public string ReferralNo2 { get; set; }
        public Nullable<bool> MotherHIVPlus { get; set; }
        public Nullable<bool> ChildBreastFed { get; set; }
        public string Howlong { get; set; }
        public Nullable<bool> ChildEverOnNevirapine { get; set; }
        public Nullable<bool> PCRDone { get; set; }
        public string PCRResults { get; set; }
        public Nullable<bool> ReferToClinic3 { get; set; }
        public string ReferalNo3 { get; set; }
        public Nullable<bool> ImmunisationUpToDate { get; set; }
        public string WhichImmunisatationsOutStanding { get; set; }
        public Nullable<bool> ReferToClinic4 { get; set; }
        public string ReferralNo4 { get; set; }
        public Nullable<bool> VITAandWarmMedsGivenEachMonth { get; set; }
        public Nullable<bool> WalkAppropriateForAge { get; set; }
        public Nullable<bool> TalkAppropriateForAge { get; set; }
    }
}
