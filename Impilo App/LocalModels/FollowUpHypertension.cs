using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpHypertension
    {
        public int fuhID { get; set; }

        public int EncounterID { get; set; }

        public DateTime fuhDateOfVisit { get; set; }

        public bool fuhHiEHWentToClinic { get; set; }

        public bool fuhHiEHReReferToClinic { get; set; }

        public string fuhHiEHRefNo { get; set; }

        public bool fuhHiEHCurrentlyOnMeds { get; set; }

        public DateTime fuhHiEHStartDate { get; set; }

        public string fuhHiEHBPScreeningSystolic { get; set; }

        public string fuhHiEHBPScreeningDiastolic { get; set; }

        public decimal fuhHiEHBPTodaySystolic { get; set; }

        public decimal fuhHiEHBPTodayDiastolic { get; set; }

        public bool fuhHiEHReferToClinic { get; set; }

        public string fuhHiEHRefNo2 { get; set; }

        public bool fuhCRReReferToClinic { get; set; }

        public string fuhCRRefNo { get; set; }

        public string fuhAlreadyOnTreatmentFollowUpTestReading { get; set; }

        public string fuhDoorToDoorCheckReading { get; set; }

        public string fuhMedication { get; set; }
    }
}
