namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tubercolosis
    {
        public string ScreeningID { get; set; }
        public Nullable<bool> HaveTubercolosis { get; set; }
        public string WhatMedsAreYouOn { get; set; }
        public Nullable<bool> Defaulting { get; set; }
        public Nullable<bool> LossWeight { get; set; }
        public Nullable<bool> SweatingAtNight { get; set; }
        public Nullable<bool> FeverOver2Weeks { get; set; }
        public Nullable<bool> CoughMoreThan2Weeks { get; set; }
        public Nullable<bool> LossOfApetite { get; set; }
        public Nullable<bool> ReferToClinic { get; set; }
        public string ReferralNo { get; set; }
    
        public virtual Screening Screening { get; set; }
    }
}
