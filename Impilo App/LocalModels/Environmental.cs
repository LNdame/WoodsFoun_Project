using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Environmental
    {
        public int EncounterID { get; set; }

        public int NoOfHouseholdCurrent { get; set; }

        public int NoOfHouseholdAway { get; set; }

        public string ListWhere { get; set; }

        public string WhenLastClinicVisit { get; set; }

        public int WhichClinic { get; set; }
    }
}
