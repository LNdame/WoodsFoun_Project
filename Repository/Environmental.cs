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
    
    public partial class Environmental
    {
        public string ScreeningId { get; set; }
        public Nullable<int> NoOfHouseholdCurrent { get; set; }
        public Nullable<int> NoOfHouseholdAway { get; set; }
        public string ListWhere { get; set; }
        public Nullable<System.DateTime> WhenLastClinicVisit { get; set; }
        public Nullable<int> WhichClinic { get; set; }
    }
}
