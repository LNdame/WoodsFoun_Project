using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpHIV
    {
        public int fuhiv { get; set; }

        public int EncounterID { get; set; }

        public DateTime fuhivDateOfVisit { get; set; }

        public bool fuhivHiEHWentToClinic { get; set; }

        public bool fuhivHiEHReReferToClinic { get; set; }

        public string fuhHiEHRefNo { get; set; }

        public bool fuhCRReferToClinic { get; set; }

        public string fuhCRRefNo { get; set; }

        public string fuhHIVStatus { get; set; }

        public bool fuhIPOnARV { get; set; }

        public DateTime fuhIPStartDate { get; set; }

        public bool fuhIPAdherenceOK { get; set; }

        public bool fuhIPConcerns { get; set; }

        public bool fuhIPReferToClinic { get; set; }

        public string fuhIPRefNo { get; set; }

        public bool fuhIPNotOnARV { get; set; }

        public bool fuhIPReferToClinic2 { get; set; }

        public string fuhIPRefNo2 { get; set; }

        public bool fuhINCounsellingDone { get; set; }

        public bool fuhIUHIVTestDone { get; set; }

        public string fuhHIVTestResults { get; set; }

        public bool fuhHIVTestReferToClinic { get; set; }

        public string fuhHIVRefNo { get; set; }
    }
}
