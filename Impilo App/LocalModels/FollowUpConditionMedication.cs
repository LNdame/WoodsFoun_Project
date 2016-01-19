using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpConditionMedication
    {
        public int fucmID { get; set; }

        public string fucmName { get; set; }

        public int mcID { get; set; }

        public int EncounterID { get; set; }
    }
}
