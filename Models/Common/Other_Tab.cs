
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Other_Tab
    {
        public string ScreeningID { get; set; }
        public string OtherID { get; set; }
        public string OtherConditionFound { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
