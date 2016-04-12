using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpTB
    {
        public int futID { get; set; }

        public int EncounterID { get; set; }

        public DateTime futbDateOfVisit { get; set; }

        public bool futbHiEHWentToClinic { get; set; }

        public bool futbHiEHReReferToClinic { get; set; }

        public string futbHiEHRefNo { get; set; }

        public bool futbTBSRecentUnplannedLoseOfWeight { get; set; }

        public bool futbTBSExcessiveSweatingAtNight { get; set; }

        public bool futbTBSFeverOver2Weeks { get; set; }

        public bool futbTBSCoughMoreThan2Weeks { get; set; }

        public bool futbTBSLossOfApetite { get; set; }

        public bool futbTBSReferToClinic { get; set; }

        public string futbTBSRefNo { get; set; }

        public string futbTBSResults { get; set; }

        public bool futbTBOTNewlyDiagnosedInLastMonth { get; set; }

        public DateTime futbTBOTStartDate { get; set; }

        public bool futbTBOTReferTBContactsToClinic { get; set; }

        public bool futbTBOTPreviouslyOnMeds { get; set; }

        public DateTime futbTBOTFinishDate { get; set; }

        public bool futbTBOTConcerns { get; set; }

        public bool futbTBOTReferToClinic { get; set; }

        public string futbTBOTRefNo { get; set; }

        public string futbHiEHOutcomes { get; set; }
    }
}
