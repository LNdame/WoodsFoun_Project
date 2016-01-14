
namespace Models.Common
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
