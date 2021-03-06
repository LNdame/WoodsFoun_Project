namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hut
    {
        public int HutId { get; set; }
        public string ClientId { get; set; }
        public string HutStracture { get; set; }
        public string TypeOfRoof { get; set; }
        public string Ventilation { get; set; }
        public Nullable<int> TotalNoOfRooms { get; set; }
        public Nullable<bool> isMainHut { get; set; }
        public Nullable<System.DateTime> DateLastSnycFromServer { get; set; }
        public Nullable<System.DateTime> DateLastSnycToServer { get; set; }
        public Nullable<System.DateTime> LastDateUpdated { get; set; }
        public Nullable<bool> isActive { get; set; }
    }
}
