
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class HIV
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> KnownHIVPosStatus { get; set; }
        public Nullable<bool> HIVTestDone { get; set; }
        public string Result { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
