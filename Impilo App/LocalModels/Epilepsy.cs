using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Epilepsy
    {
        public string ScreeningID { get; set; }

        public bool FitsInLastMonth { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }
    }
}
