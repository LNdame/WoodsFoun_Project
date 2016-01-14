using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Tubercolosis
    {
        public string ScreeningID { get; set; }

        public bool HaveTubercolosis { get; set; }

        public string WhatMedsAreYouOn { get; set; }

        public bool Defaulting { get; set; }

        public bool LossWeight { get; set; }

        public bool SweatingAtNight { get; set; }

        public bool FeverOver2Weeks { get; set; }

        public bool CoughMoreThan2Weeks { get; set; }

        public bool LossOfApetite { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }
    }
}
