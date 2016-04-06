using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ChildHealth5_12
    {
        public int EncounterID { get; set; }

        public string NameOfMother { get; set; }

        public bool ChildWithRTHC { get; set; }

        public bool ReferToClinic { get; set; }

        public string ReferalNo { get; set; }
        //--

        public string ListConcernsReChild { get; set; }

        public bool ReferToClinic2 { get; set; }

        public bool ReferToOVC { get; set; }

        public string ReferralNo2 { get; set; }
        public string ReferralNoOVC { get; set; }
        //--

        public string MotherHIVPlus { get; set; }

        public string ChildBreastFed { get; set; }

        public string Howlong { get; set; }

        public string ChildEverOnNevirapine { get; set; }

        public string PCRDone { get; set; }

        public string PCRResults { get; set; }

        public bool ReferToClinic3 { get; set; }

        public string ReferalNo3 { get; set; }
        //--



        public string ImmunisationUpToDate { get; set; }

        public List<string> WhichImmunisatationsOutStanding { get; set; }

        public bool ReferToClinic4 { get; set; }

        public string ReferralNo4 { get; set; }

        public string VITAandWarmMedsGivenEachMonth { get; set; }

        public string WalkAppropriateForAge { get; set; }

        public string TalkAppropriateForAge { get; set; }

        public bool ReferToClinic5 { get; set; }

        public string ReferralNo5 { get; set; }
        //--
        public string MUACColour { get; set; }

        public bool ReferToClinic6 { get; set; }

        public string ReferralNo6 { get; set; }
    }
}
