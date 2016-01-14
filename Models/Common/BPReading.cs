
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class BPReading
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> OnMedsSytolic { get; set; }
        public Nullable<bool> OnMedsDiastolic { get; set; }
        public string Diastolic { get; set; }
        public string ReferToCHOWs { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    }
}
