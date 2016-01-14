
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class CurrentMedications
    {
        public string ScreeningID { get; set; }
        public int DiseaseID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<bool> Defaulting { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralID { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
