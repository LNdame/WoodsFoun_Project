using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ChildHealth
    {
        public int EncounterID { get; set; }

        public string NameOfMother { get; set; }

        public bool ChildWithRTHC { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferalNo { get; set; }

        public string ListConcernsReChild { get; set; }

        public bool ReferToClinic2 { get; set; }

        public bool ReferToOVC { get; set; }

        public string ReferralNo2 { get; set; }

        public bool MotherHIVPlus { get; set; }

        public bool ChildBreastFed { get; set; }

        public string Howlong { get; set; }

        public bool ChildEverOnNevirapine { get; set; }

        public bool PCRDone { get; set; }

        public string PCRResults { get; set; }

        public bool ReferToClinic3 { get; set; }

        public string ReferalNo3 { get; set; }

        public bool ImmunisationUpToDate { get; set; }

        public string WhichImmunisatationsOutStanding { get; set; }

        public bool ReferToClinic4 { get; set; }

        public string ReferralNo4 { get; set; }

        public bool VITAandWarmMedsGivenEachMonth { get; set; }

        public bool WalkAppropriateForAge { get; set; }

        public bool TalkAppropriateForAge { get; set; }
    }
}
