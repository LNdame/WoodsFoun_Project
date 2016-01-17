using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class FollowUp
    {
        public int FollowUpID { get; set; }

        public string DateofScreen { get; set; }

        public string VisitNextVisit { get; set; }

        public string VisitOutCome { get; set; }

        public bool VisitHPT { get; set; }

        public bool VisitDiabetes { get; set; }

        public bool VisitEpilepsy { get; set; }

        public bool VisitHIV { get; set; }

        public bool VisitTB { get; set; }

        public bool VisitMatHealth { get; set; }

        public bool VisitChildHealth { get; set; }

        public bool VisitOther { get; set; }

        public bool VisitDooortoDoor { get; set; }

        public bool HyperWentToClinic1 { get; set; }

        public bool HyperReReferToClinic1 { get; set; }

        public string HyperReferralNo1 { get; set; }

        public bool HyperCurrentlyOnMeds { get; set; }

        public string HyperStartDate { get; set; }

        public string HyperScreenBPReadingSystolic { get; set; }

        public string HyperScreenBPReadingDiastolic { get; set; }

        public string HyperTodayTestReadingSystolic { get; set; }

        public string HyperTodayTestReadingDiastolic { get; set; }

        //public string HyperBPReading { get; set; } ..kevin

        // public string HyperTodayTestReading { get; set; } ..kevin

        public bool HyperAlreadyOnTreatment { get; set; }

        public bool HyperReReferToClinic2 { get; set; }

        public string HyperReferralNo2 { get; set; }

        //public string HyperCheckReading { get; set; }

        public string HyperMedication { get; set; }

        //public string HyperReReferToClinic3 { get; set; }

        //public string HyperReferralNo3 { get; set; }

        //public string HyperFollowUpTestReading { get; set; }

        //public string HyperScreenTestReading { get; set; }

        public bool DaiWentToClinic { get; set; }

        public bool DiaReReferToClinic1 { get; set; }

        public string DiaReferralNo1 { get; set; }

        public bool DiaCurrentlyOnMeds { get; set; }

        public string DiaStartDate { get; set; }

        public string DiaScreenTestReading1 { get; set; }

        public string DiaFollowUpTestReading1 { get; set; }

        public bool DiaReferToClinic2 { get; set; }

        public string DiaReferralNo2 { get; set; }

        //public string DiaCheckReading { get; set; }

        public string DiaMedication { get; set; }

        //public string DiaReReferToClinic3 { get; set; }

        //public string DiaReferralNo3 { get; set; }

        //public string DiaFollowUpTestReading3 { get; set; }

        public bool EpiWentToClinic { get; set; }

        public bool EpiReReferToClinic1 { get; set; }

        public string EpiReferralNo1 { get; set; }

        public bool EpiFitInLastMonth { get; set; }

        public bool EpiReferToClinic { get; set; }

        public bool EpiCurrentlyOnMeds { get; set; }

        public string EpiStartDate { get; set; }

        public bool EpiMoreThan3FitsInLastMonth { get; set; }

        public bool EpiReReferToClinic2 { get; set; }

        public string EpiReferralNo2 { get; set; }

        public string EpiMedication { get; set; }

        public string AsDateOfVisit { get; set; }

        public bool AsWentToClinic { get; set; }

        public bool AsReReferToClinic1 { get; set; }

        public string AsReferralNo1 { get; set; }

        //public string AsFitInLastMonth { get; set; }

        public bool AsReferToClinic { get; set; }

        //public string AsReferralNo2 { get; set; }

        public bool AsCurrentlyOnMeds { get; set; }

        public string AsStartDate { get; set; }

        public bool AsIncreasedNoOfAsthmaAttacks { get; set; }

        public bool AsReReferToClinic2 { get; set; }

        public string AsReferralNo2 { get; set; }

        public string AsMedication { get; set; }

        public string TBDateOfVisit { get; set; }

        public string TBARVsConcern { get; set; }

        public bool TBReferToClinic1 { get; set; }

        public string TBReferralNo1 { get; set; }

        public bool TBRecentUnplannedLoseOfWeight { get; set; }

        public bool TBExcessiveSweatingAtNight { get; set; }

        public bool TBFeverOver2Weeks { get; set; }

        public bool TBCoughMoreThan2Week { get; set; }

        public bool TBLossOfApetite { get; set; }

        public bool TBReferredToClinic2 { get; set; }

        public string TBReferralNo2 { get; set; }

        public string TBResult { get; set; }

        public string TBNewlyDiagnosed { get; set; }

        public string TBStartDate { get; set; }

        public string TBReferTBContactsToClinic { get; set; }

        public bool TBPreviouslyOnMeds { get; set; }

        public string TBFinishDate { get; set; }

        public bool TBConcerns { get; set; }

        public string TBReferToClinic3 { get; set; }

        public string TBReferralNo3 { get; set; }

        public string TBMedication { get; set; }

        public string MatDateOfVisit { get; set; }

        public bool MatWentToClinic { get; set; }

        public bool MatReReferToClinic1 { get; set; }

        public string MatReferralNo1 { get; set; }

        public bool MatIsItPosibleYouArePregnent { get; set; }

        public bool MatPregnancyTestDone { get; set; }

        public string MatResult { get; set; }

        public bool MatReferredToClinic2 { get; set; }

        public string MatReferralNo2 { get; set; }

        public string MatDateOf1stANC { get; set; }

        public string MatDateOfLastANC { get; set; }

        public bool MatReferredToClinic3 { get; set; }

        public string MatReferralNo3 { get; set; }

        public bool MatRegisteredForMoMConnect { get; set; }

        public string MatDateOfNextANC { get; set; }

        public bool MatReferToClinic { get; set; }

        public string MatReferralNo4 { get; set; }

        public string MatExpectedDateOfDelivery { get; set; }

        public bool MatIntendBreastfeed { get; set; }

        public bool MatIntendFormulaFeed { get; set; }

        public string ChildDateOfVisit { get; set; }

        public bool ChildARVsConcern { get; set; }

        public bool ChildReferToClinic1 { get; set; }

        public string ChildReferralNo1 { get; set; }

        public bool ChildWalkAppropriateForAge { get; set; }

        public bool ChildTalkAppropriateForAge { get; set; }

        public bool ChildReferToClinic2 { get; set; }

        public string ChildReferralNo2 { get; set; }

        public bool ChildChildAssisted { get; set; }

        public bool ChildReReferToSD { get; set; }

        public string ChildReferralNo3 { get; set; }

        public string ChildListConcernsReChild { get; set; }

        public bool ChildReferToClinic3 { get; set; }

        public bool ChildreferToSD { get; set; }

        public string ChildReferralNo4 { get; set; }

        public bool ChildChildWithRTHC { get; set; }

        public bool ChildReferToClinic4 { get; set; }

        public string ChildReferralNo5 { get; set; }

        public bool ChildMotherTHVPositive { get; set; }

        public bool ChildChildBreastfed { get; set; }

        public string ChildHowLong { get; set; }

        public bool ChildClildEverOnNevirapine { get; set; }

        public bool ChildReferToClinic5 { get; set; }

        public string ChildReferralNo6 { get; set; }

        public bool ChildHowPCRHasDone { get; set; }

        public bool ChildReferToClinic6 { get; set; }

        public string ChildReferralNo7 { get; set; }

        public bool ChildImmunisationUpToDate { get; set; }

        public string ChildWhichImmunisationsOutStanding { get; set; }

        public bool ChildVITAandWormMedsGivenEachMonth { get; set; }

        public bool ChildReferToClinic7 { get; set; }

        public string ChildReferralNo8 { get; set; }

        public string OtherDateOfVisit { get; set; }

        public bool OtherWentToClinic { get; set; }

        public bool OtherReReferToClinic { get; set; }

        public string OtherReferralNo1 { get; set; }

        public string OtherConditionTha { get; set; }

        public bool OtherReferToClinic1 { get; set; }

        public string OtherReferralNo2 { get; set; }

        public string FollowUpIDNumber { get; set; }

        public string HIVDateOfVisit { get; set; }

        public string HIVWentToClinic { get; set; }

        public string HIVRereferToClinic { get; set; }

        public string HIVReferralNo1 { get; set; }

        public bool HIVReferToClinic1 { get; set; }

        public string HIVReferralNo2 { get; set; }

        public string HIVStatus { get; set; }

        public bool HIVOnARVs { get; set; }

        public string HIVStartDate1 { get; set; }

        public bool HIVAdherenceOK { get; set; }

        public bool HIVConcerns { get; set; }

        public bool HIVReferToClinic2 { get; set; }

        public bool HIVReferralNo3 { get; set; }

        public bool HIVARVsConcern { get; set; }

        public string HIVReferToClinic3 { get; set; }

        public string HIVReferralNo4 { get; set; }

        public bool HIVTestingDone { get; set; }

        public bool HIVTestDone { get; set; }

        public bool HIVTestResults { get; set; }

        public bool HIVReferToClinic4 { get; set; }

        public string HIVReferralNo5 { get; set; }

        public string HIVMedication { get; set; }

      


    }

}
