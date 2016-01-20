using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicConditionTreatment
    {
        public int cctID { get; set; }

        public int mcID { get; set; }

        public int EncounterID { get; set; }

        public string cctName { get; set; }
    }
}
