
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pregnancy
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> CurrentlyPregnant { get; set; }
        public Nullable<bool> PossiblePregnant { get; set; }
        public Nullable<bool> PregnancyTestDone { get; set; }
        public string Results { get; set; }
        public Nullable<long> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
