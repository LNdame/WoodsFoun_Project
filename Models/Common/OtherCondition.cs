
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class OtherCondition
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> BloodInUrine { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
        public Nullable<bool> Smoking { get; set; }
        public Nullable<bool> DrinkAlchoholUnitsPerWeek { get; set; }
        public Nullable<bool> DiarrhoeaOver3Days { get; set; }
        public Nullable<bool> ReferToClinic2 { get; set; }
        public string ReferralNo2 { get; set; }
        public Nullable<bool> AttendedInitiationSchool { get; set; }
        public Nullable<bool> LagCrampsOver2Weeks { get; set; }
        public Nullable<bool> LagNumbnessOver2Weeks { get; set; }
        public Nullable<bool> FootUlcer { get; set; }
        public Nullable<bool> ReferToClinic3 { get; set; }
        public string ReferalNo3 { get; set; }
        public Nullable<bool> FamilyPlanningAdviceGiven { get; set; }
        public Nullable<bool> Amputation { get; set; }
        public Nullable<bool> PassVisionTest { get; set; }
        public Nullable<bool> Bedridden { get; set; }
        public Nullable<bool> UseAidToMove { get; set; }
        public Nullable<bool> WashYourSelf { get; set; }
        public Nullable<bool> DressYourSelf { get; set; }
        public Nullable<bool> ReferToClinic4 { get; set; }
        public string ReferalNo4 { get; set; }
        public Nullable<bool> FamilyPlanningAdvice { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
