
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBContactTracing
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> HouseHoldOnTBMeds { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    }
}
