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
    
    public partial class Clinic
    {
        public int ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string Logitude { get; set; }
        public string Latitude { get; set; }
        public Nullable<System.DateTime> DateLastSnycFromServer { get; set; }
        public Nullable<System.DateTime> DateLastSnycToServer { get; set; }
        public Nullable<System.DateTime> LastDayUpdated { get; set; }
    }
}
