using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpMaternalHealth
    {
        public int fumhID { get; set; }

        public int EncounterID { get; set; }

        public DateTime fumhDateOfVisit { get; set; }

        public bool fumhHiEHWentToClinic { get; set; }

        public bool fumhHiEHReReferToClinic { get; set; }

        public string fumhHiEHRefNo { get; set; }

        public DateTime fumhCPDateOfFirstANC { get; set; }

        public DateTime fumhCPDateOfLastANC { get; set; }

        public bool fumhCPReferToClinic { get; set; }

        public string fumhCPRefNo { get; set; }

        public bool fumhCPRegisteredForMomConnect { get; set; }

        public bool fumhCPReferToClinic2 { get; set; }

        public string fumhCPRefNo2 { get; set; }

        public DateTime fumhCPDateOfNextANC { get; set; }

        public DateTime fumhCPExpectedDateOfDelivery { get; set; }

        public bool fumhCPIntendBreastFeed { get; set; }

        public bool fumhCPIntendFormulaFeed { get; set; }

        public bool fumhPPPossiblePregnant { get; set; }

        public bool fumhPPTestDone { get; set; }

        public string fumhPPResult { get; set; }

        public bool fumhPPReferToClinic { get; set; }

        public string fumhPPRefNo { get; set; }
    }
}
