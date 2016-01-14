
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Screening
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Screening()
        {
            this.CurrentMedications = new HashSet<CurrentMedications>();
            this.Other_Tab = new HashSet<Other_Tab>();
        }
    
        public string ScreeningID { get; set; }
        public Nullable<System.DateTime> ScreeningDate { get; set; }
        public string ClientId { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrentMedications> CurrentMedications { get; set; }
        public virtual Diabetes Diabetes { get; set; }
        public virtual HIV HIV { get; set; }
        public virtual HIV_Tab HIV_Tab { get; set; }
        public virtual Hypertension Hypertension { get; set; }
        public virtual MentalHealth MentalHealth { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Other_Tab> Other_Tab { get; set; }
        public virtual OtherCondition OtherCondition { get; set; }
        public virtual Pregnancy Pregnancy { get; set; }
        public virtual Tubercolosis Tubercolosis { get; set; }
    }
}
