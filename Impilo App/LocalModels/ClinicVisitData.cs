using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicVisitData
    {
        public int cvdID { get; set; }

        public int EncounterID { get; set; }

        public decimal cvdHeight { get; set; }

        public decimal cvdWeight { get; set; }

        public decimal cvdBMI { get; set; }

        public DateTime cvdNextVisitDate { get; set; }

        public bool cvdHypertension { get; set; }

        public bool cvdDiabetes { get; set; }

        public bool cvdEpilepsy { get; set; }

        public bool cvdAsthma { get; set; }

        public bool cvdHIV { get; set; }

        public bool cvdTB { get; set; }

        public bool cvdMaternalHealth { get; set; }

        public bool cvdChildHealth { get; set; }

        public bool cvdOther { get; set; }

        public DateTime cvdDateOfVisit { get; set; }
    }
}
