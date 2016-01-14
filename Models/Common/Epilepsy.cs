
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Epilepsy
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> FitsInLastMonth { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    }
}
