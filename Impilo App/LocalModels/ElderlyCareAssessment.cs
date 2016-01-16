using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ElderlyCareAssessment
    {
        public string ScreeningID { get; set; }

        public bool LegFootArmHanAmputation { get; set; }

        public bool PassVisionTest { get; set; }

        public bool Bedridden { get; set; }

        public bool UseAidToMove { get; set; }

        public bool WashYourself { get; set; }

        public bool DressYourSelf { get; set; }

        public bool FeedYourSelf { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }
    }
}
