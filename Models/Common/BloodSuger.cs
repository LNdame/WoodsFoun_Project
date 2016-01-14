
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class BloodSuger
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> OnMeds { get; set; }
        public string NotOnMedsBSReadings { get; set; }
        public Nullable<bool> ReferToCHOWs { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    }
}
