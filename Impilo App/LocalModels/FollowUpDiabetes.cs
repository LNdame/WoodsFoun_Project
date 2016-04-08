using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpDiabetes
    {
        public int fudID { get; set; }

        public int EncounterID { get; set; }

        public DateTime fudDateOfVisit { get; set; }

        public bool fudHiEHWentToClinic { get; set; }

        public bool fudHiEHReferToClinic { get; set; }

        public string fudHiEHRefNo { get; set; }

        public bool fudHiEHCurrentlyOnMeds { get; set; }

        public DateTime fudHiEHStartDate { get; set; }

        public decimal fudHiEHFollowUpTestReading { get; set; }

        //public bool fudHiEHReferToClinic2 { get; set; }

        //public string fudHiEHRefNo2 { get; set; }

        public bool fudHiEHReReferToClinic { get; set; }

        public string fudHiEHReRefNo { get; set; }

        //public decimal fudAlreadyOnTreatmentFollowUpTestReading { get; set; }

        //public string fudDoorDoor { get; set; }

        //public string fudMedication { get; set; }

        public string fudHiEHNextVisit { get; set; }

        public string fudHiEHOutcomes { get; set; }
    }
}
