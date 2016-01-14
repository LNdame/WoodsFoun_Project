using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class CurrentMedications
    {
        public string ScreeningID { get; set; }

        public int DiseaseID { get; set; }

        public DateTime StartDate { get; set; }

        public bool Defaulting { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferralID { get; set; }
    }
}
