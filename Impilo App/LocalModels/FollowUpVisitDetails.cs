using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpVisitDetails
    {
        public int fuvdID { get; set; }

        public int EncounterID { get; set; }

        public DateTime fuvdVisitDate { get; set; }

        public DateTime fuvdNextVisitDate { get; set; }

        public string duvdOutcome { get; set; }

        public bool duvdHypertension { get; set; }

        public bool duvdDiabetes { get; set; }

        public bool duvdEpilepsy { get; set; }

        public bool duvdHIV { get; set; }

        public bool duvdTB { get; set; }

        public bool duvdMaternalHealth { get; set; }

        public bool duvdChildHealth { get; set; }

        public string duvdOther { get; set; }

        public bool duvdDoorDoor { get; set; }
    }
}
