using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class BloodSuger
    {
        public string ScreeningID { get; set; }

        public bool OnMeds { get; set; }

        public string NotOnMedsBSReadings { get; set; }

        public bool ReferToCHOWs { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }
    }
}
