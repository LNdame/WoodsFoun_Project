using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicMaternalHealth
    {
        public int cmhID { get; set; }

        public int EncounterID { get; set; }

        public bool cmhDWFReferral { get; set; }

        public bool cmhMomConnectRegistered { get; set; }

        public string cmhANCVisitNo { get; set; }

        public string cmhPNC1Week { get; set; }

        public bool cmhPCRDone { get; set; }

        public string cmhPNC6Week { get; set; }

        public DateTime cmhDateOfVisit { get; set; }
    }
}
