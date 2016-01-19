using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicTB
    {
        public int ctbID { get; set; }

        public int EncounterID { get; set; }

        public bool ctbDWFReferral { get; set; }

        public bool ctbSputumTaken { get; set; }

        public DateTime ctbTestResultsReviewDate { get; set; }

        public string ctbResultsGenexpert { get; set; }

        public string ctbResultsAFB { get; set; }

        public DateTime ctbDateOfVisit { get; set; }
    }
}
