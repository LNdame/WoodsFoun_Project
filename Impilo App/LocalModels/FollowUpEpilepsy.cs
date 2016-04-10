using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpEpilepsy
    {
        public int fueID { get; set; }

        public int EncounterID { get; set; }

        public bool fueHiEHWentToClinic { get; set; }

        public bool fueHiEHReReferToClinic { get; set; }

        public string fueHiEHRefNo { get; set; }

        public bool fueCRFitInLastMonth { get; set; }

        public bool fueCRReferToClinic { get; set; }

        public string fueCRRefNo { get; set; }

        public bool fueOnTreatmentCurrentlyOnMeds { get; set; }

        public DateTime fueOnTreatmentStartDate { get; set; }

        public bool fueOnTreatmentMoreThan3FitsSinceLastMonth { get; set; }

        public bool fueOnTreatmentReReferToClinic { get; set; }

        public string fueOnTreatmentRefNo { get; set; }

        public string fueHiEHRevisit { get; set; }

        public string fueHiEHOutcomes { get; set; }
    }
}
