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

        public char LegFootArmHanAmputation { get; set; }

        public char PassVisionTest { get; set; }

        public char Bedridden { get; set; }

        public char UseAidToMove { get; set; }

        public char WashYourself { get; set; }

        public char DressYourSelf { get; set; }

        public char ReferToClinic { get; set; }

        public char ReferralNo { get; set; }
    }
}
