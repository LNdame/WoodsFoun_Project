
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class General
    {
        public string ScreeningID { get; set; }
        public Nullable<int> Weight { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> BMI { get; set; }
    }
}
