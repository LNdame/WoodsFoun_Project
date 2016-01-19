using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUpChildHealth
    {
        public int fuchID { get; set; }

        public int EncounterID { get; set; }

        public DateTime fuchDateOfVisit { get; set; }

        public bool fuchHiEHWentToClinic { get; set; }

        public bool fuchHiEHReReferToClinic { get; set; }

        public string fuchHiEHRefNo { get; set; }

        public bool fuchNewChildWithRTHC { get; set; }

        public bool fuchNewReferToClinic { get; set; }

        public string fuchNewRefNo { get; set; }

        public bool fuchNewMotherHIVPos { get; set; }

        public bool fuchNewChildBreastfed { get; set; }

        public string fuchNewHowLong { get; set; }

        public bool fuchNewChildEverOnNevirapine { get; set; }

        public bool fuchNewReferToClinic2 { get; set; }

        public string fuchNewRefNo2 { get; set; }

        public bool fuchNewHasPCRBeenDone { get; set; }

        public bool fuchNewReferToClinic3 { get; set; }

        public string fuchNewRefNo3 { get; set; }

        public bool fuchNewImmunisationUpToDate { get; set; }

        public bool fuchNewVitAWormMedsGivenEachMonth { get; set; }

        public bool fuchNewReferToClinic4 { get; set; }

        public string fuchNewRefNo4 { get; set; }

        public bool fuchCDevWalkAppropriatelyForAge { get; set; }

        public bool fuchCDevTalkAppropriateForAge { get; set; }

        public bool fuchCDevReferToClinic { get; set; }

        public string fuchCDevRefNo { get; set; }

        public bool fuchSocDevChildAssisted { get; set; }

        public bool fuchSocDevReReferToSD { get; set; }

        public string fuchSocDevRefNo { get; set; }

        public bool fuchCurSocDevReferToClinic { get; set; }

        public bool fuchCurSocDevReferToSD { get; set; }

        public string fuchCurSocDevRefNo { get; set; }
    }
}
