//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository
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
