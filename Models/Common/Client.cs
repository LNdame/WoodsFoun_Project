
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Screening = new HashSet<Screening>();
        }
    
        public string ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public string Gender { get; set; }
        public Nullable<bool> isHeadOfHousehold { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<bool> isAttendingSchool { get; set; }
        public string Grade { get; set; }
        public Nullable<int> NameOfSchool { get; set; }
        public Nullable<System.DateTime> LastSyncDateFromServer { get; set; }
        public Nullable<System.DateTime> LastSyncDateToServer { get; set; }
        public Nullable<System.DateTime> LastDateUpdated { get; set; }
        public Nullable<bool> isActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Screening> Screening { get; set; }
    }
}
