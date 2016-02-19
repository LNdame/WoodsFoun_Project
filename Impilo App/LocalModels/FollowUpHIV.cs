using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpHIV
    {
        public int fuhivID { get; set; }

        public int EncounterID { get; set; }

        public DateTime fuhivDateOfVisit { get; set; }

        public bool fuhivHiEHWentToClinic { get; set; }

        public string fuhHIVStatus { get; set; }

        public bool fuhivHiEHReReferToClinic { get; set; }

        public string fuhivHiEHRefNo { get; set; }

        public bool fuhivCRReferToClinic { get; set; }

        public string fuhivCRRefNo { get; set; }

        public string fuhivHIVStatus { get; set; }

        public bool fuhivIPOnARV { get; set; }

        public DateTime fuhivIPStartDate { get; set; }

        public bool fuhivIPAdherenceOK { get; set; }

        public bool fuhivIPConcerns { get; set; }

        public bool fuhivIPReferToClinic { get; set; }

        public string fuhivIPRefNo { get; set; }

        public bool fuhivIPNotOnARV { get; set; }

        public bool fuhivIPReferToClinic2 { get; set; }

        public string fuhivIPRefNo2 { get; set; }

        public bool fuhivINCounsellingDone { get; set; }

        public bool fuhivIUHIVTestDone { get; set; }

        public string fuhivHIVTestResults { get; set; }

        public bool fuhivHIVTestReferToClinic { get; set; }

        public string fuhivHIVRefNo { get; set; }
    }
}
