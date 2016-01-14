
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Diabetes
    {
        public string ScreeningID { get; set; }
        public string YearOfDiagonosis { get; set; }
        public Nullable<bool> regularlyThirsty { get; set; }
        public Nullable<bool> WeightLoss { get; set; }
        public Nullable<bool> BlurredVision { get; set; }
        public Nullable<bool> UrinatingMore { get; set; }
        public Nullable<bool> NauseaOrVomitting { get; set; }
        public string FootExamResult { get; set; }
        public Nullable<bool> ReferralToClinic { get; set; }
        public string ReferralNo { get; set; }
        public Nullable<bool> FamilyMemberWith { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
