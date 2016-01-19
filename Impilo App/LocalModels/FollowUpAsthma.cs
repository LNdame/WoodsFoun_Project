using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpAsthma
    {
        public int fuaID { get; set; }

        public int EncounterID { get; set; }

        public DateTime fuaDateOfVisit { get; set; }

        public bool fuaHiEHWentToClinic { get; set; }

        public bool fuaHiEHReReferToClinic { get; set; }

        public string fuaHiHRefNo { get; set; }

        public bool fuaCRDifficultyBreathingAndWheezing { get; set; }

        public bool fuaCRReferToClinic { get; set; }

        public string fuaCRRefNo { get; set; }

        public bool fuaOTCurrentlyOnMeds { get; set; }

        public DateTime fuaOTStartDate { get; set; }

        public bool fuaOTIncreasedNoOfAsthmaAttacks { get; set; }

        public bool fuaOTReReferToClinic { get; set; }

        public string fuaOTRefNo { get; set; }
    }
}
