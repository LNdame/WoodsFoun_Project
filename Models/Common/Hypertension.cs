
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hypertension
    {
        public string ScreeningID { get; set; }
        public string YearOfDiagnosis { get; set; }
        public Nullable<bool> Headache { get; set; }
        public Nullable<bool> BlurredVision { get; set; }
        public Nullable<bool> ChestPain { get; set; }
        public Nullable<bool> ReferralToClinic { get; set; }
        public string ReferalNo { get; set; }
        public Nullable<bool> EverHadStroke { get; set; }
        public string YearOfStroke { get; set; }
        public Nullable<bool> AnyOneInFamilyHadStroke { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
