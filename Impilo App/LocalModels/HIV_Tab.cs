using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class HIV_Tab
    {
        public string ScreeningID { get; set; }

        public string YearOfDiagnosis { get; set; }
        public bool OnMeds { get; set; }

        public string ListMeds { get; set; }

        public bool AdherenceOK { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralNo { get; set; }

        public string ARVFileNo { get; set; }
    }
}
