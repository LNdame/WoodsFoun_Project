using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Pregnancy
    {
        public string ScreeningID { get; set; }

        public bool CurrentlyPregnant { get; set; }

        public bool PossiblePregnant { get; set; }

        public bool PregnancyTestDone { get; set; }

        public string Results { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }
    }
}
