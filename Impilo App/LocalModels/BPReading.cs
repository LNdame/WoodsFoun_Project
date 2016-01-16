using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class BPReading
    {
        public string ScreeningID { get; set; }

        public string Systolic { get; set; }

        public bool OnMeds { get; set; }

        public string Diastolic { get; set; }

        public bool ReferToCHOWs { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }
    }
}
