
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class ElderlyCareAssessment
    {
        public string ScreeningID { get; set; }
        public string LegFootArmHanAmputation { get; set; }
        public string PassVisionTest { get; set; }
        public string Bedridden { get; set; }
        public string UseAidToMove { get; set; }
        public string WashYourself { get; set; }
        public string DressYourSelf { get; set; }
        public string ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    }
}
