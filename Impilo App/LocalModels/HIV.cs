using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class HIV
    {
        public string ScreeningID { get; set; }

        public bool KnownHIVPosStatus { get; set; }

        public bool HIVTestDone { get; set; }

        public string Result { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }
    }
}
