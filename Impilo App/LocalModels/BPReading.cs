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

        public bool OnMedsSytolic { get; set; }

        public bool OnMedsDiastolic { get; set; }

        public string Diastolic { get; set; }

        public string ReferToCHOWs { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }
    }
}
