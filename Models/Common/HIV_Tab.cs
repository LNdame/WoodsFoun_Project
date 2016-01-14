namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class HIV_Tab
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> OnMeds { get; set; }
        public string ListMeds { get; set; }
        public Nullable<bool> AdherenceOK { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
        public string ARVFileNo { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
