//
namespace Models.Common
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
