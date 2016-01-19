using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpOther
    {
        public int fuoID { get; set; }

        public int EncounterID { get; set; }

        public DateTime fuoDateOfVisit { get; set; }

        public bool fuoHiEHWentToClinic { get; set; }

        public bool fuoHiEHReReferToClinic { get; set; }

        public string fuoHiEHRefNo { get; set; }

        public string fuoOCCondition { get; set; }

        public bool fuoOCReferToClinic { get; set; }

        public string fuoOCRefNo { get; set; }
    }
}
