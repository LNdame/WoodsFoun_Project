using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Impilo_App.DataImport
{
    class FollowUpImport
    {
        public static bool Import(string TargetFolder)
        {
            bool Success = true;

            string[] Files = Directory.GetFiles(TargetFolder);
            List<string> ErrorList = new List<string>();

            foreach (string CurrentFile in Files)
            {
                try
                {
                    XSSFWorkbook MyWorkbook = null;

                    using (FileStream file = new FileStream(CurrentFile, FileMode.Open, FileAccess.Read))
                    {
                        MyWorkbook = new XSSFWorkbook(file);

                        // Read file values here

                        // Biographical Data goes into client table
                        #region Biographical
                        // dbo.client table
                        string BioChowName = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(0).StringCellValue;
                        string BioUniqueID = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(1).StringCellValue;
                        string BioFirstName = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(2).StringCellValue;
                        string BioSecondName = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(3).StringCellValue;
                        string BioIDNumber = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(4).StringCellValue;
                        string BioGPSLatitude = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(5).StringCellValue;
                        string BioGPSLongitude = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(6).StringCellValue;
                        string BioArea = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(7).StringCellValue;
                        string BioClinic = MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(8).StringCellValue;
                        #endregion


                        //The rest of these go into the followup table

                        #region VisitDetails
                        // VD prefix used to identify variables associated with Visit Details tab
                        string VDVisitNum = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(0).StringCellValue; //count generated from counting visits unique to a client ID
                        string VDVisitDate = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(1).StringCellValue; //dateTimeNow, No real need to enter manually
                        string VDNextVisitDate = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(2).StringCellValue; //good
                        string VDOutcome = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(4).StringCellValue;//good
                        string VDHPT = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(5).StringCellValue;//good
                        string VDDiabetes = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(6).StringCellValue;//good
                        string VDEpilepsy = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(7).StringCellValue;//good
                        string VDHIV = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(8).StringCellValue;//good
                        string VDTB = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(9).StringCellValue;//good
                        string VDMatHealth = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(10).StringCellValue;//good
                        string VDChildHealth = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(11).StringCellValue;//good
                        string VDOther = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(12).StringCellValue;//good
                        string VDODoorToDoor = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(13).StringCellValue;//good
                        #endregion

                        #region HyperTension

                        //Hyper prefix used to identify variables associated with Hypertension tab.
                        //HiEHRef = HiEH Referral
                        //ClinicRef = Clinic Referral
                        //AOT = Already On Treatment
                        string HyperDateOfVisit = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(0).StringCellValue;
                        
                        string Hyper_HiEHRef_WentToClinic = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(1).StringCellValue; //good
                        string Hyper_HiEHRef_ReReferToClinic = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(2).StringCellValue; //good
                        string Hyper_HiEHRef_ReRefNum = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(3).StringCellValue; //good
                        string Hyper_HiEHRef_OnMeds = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(4).StringCellValue; //good
                        string Hyper_HiEHRef_StartDate = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(5).StringCellValue; //good
                        //need to separate BP reading to systolic and diastolic for the next two rows
                        string Hyper_HiEHRef_BPReading = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(6).StringCellValue;
                        string Hyper_HiEHRef_TodayReadingTop = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(7).StringCellValue;
                        string Hyper_HiEHRef_TodayReadingBottom = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(8).StringCellValue;//not in form
                        string Hyper_HiEHRef_ReferToClinic = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(9).StringCellValue;
                        string Hyper_HiEHRef_RefNum = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(10).StringCellValue;
                        
                        string Hyper_ClinicRef_ReReferToClinic = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(12).StringCellValue; //I think all referrals are to the clinic this may not be needed
                        string Hyper_ClinicRef_ReRefNum = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(13).StringCellValue;//I think all referrals are to the clinic - this may not be needed

                        string Hyper_AOT_FollowUpReading = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(15).StringCellValue; //this is the same as the Todays reading
                        
                        string Hyper_DoorToDoorReading_CheckReading = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(17).StringCellValue; //Visit info will already indicate is this is a door to door visit - this may be redundant

                        string Hyper_Medication = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(18).StringCellValue; //good
                        #endregion

                        #region Diabetes
                        //HiEHRef = HiEH Referral
                        //ClinicRef = Clinic Referral
                        //AOT = Already On Treatment
                        string DiabetesDateOfVisit = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(0).StringCellValue;

                        string Diabetes_HiEHRef_WentToClinic = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(1).StringCellValue;
                        string Diabetes_HiEHRef_ReReferToClinic = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(2).StringCellValue;
                        string Diabetes_HiEHRef_ReRefNum = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(3).StringCellValue;
                        string Diabetes_HiEHRef_OnMeds = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(4).StringCellValue;
                        string Diabetes_HiEHRef_StartDate = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(5).StringCellValue;
                        string Diabetes_HiEHRef_FollowUpTestReading = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(6).StringCellValue;
                        string Diabetes_HiEHRef_ReferToClinic = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(7).StringCellValue;
                        string Diabetes_HiEHRef_RefNum = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(8).StringCellValue;

                        string Diabetes_ClinicRef_ReReferToClinic = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(10).StringCellValue;
                        string Diabetes_ClinicRef_ReRefNum = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(11).StringCellValue;

                        string Diabetes_AOT_FollowUpReading = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(13).StringCellValue;

                        string Diabetes_DoorToDoorReading_CheckReading = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(15).StringCellValue;

                        string Diabetes_Medication = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(16).StringCellValue;
              
                        #endregion

                        #region Epilepsy
                        //HiEHRef = HiEH Referral
                        //CurrentRef = Current Referral
                        //OT = On Treatment
                        string EpilepsyDateOfVisit = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(0).StringCellValue;

                        string Epilepsy_HiEHRef_WentToClinic = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(1).StringCellValue;
                        string Epilepsy_HiEHRef_ReReferToClinic = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(2).StringCellValue;
                        string Epilepsy_HiEHRef_ReRefNum = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(3).StringCellValue;
                       
                        string Epilepsy_CurrentRef_FitInLastMonth = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(5).StringCellValue;
                        string Epilepsy_CurrentRef_RefToClinic = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(6).StringCellValue;
                        string Epilepsy_CurrentRef_RefNum = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(7).StringCellValue;

                        string Epilepsy_OT_CurrentlyOnMeds = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(9).StringCellValue;
                        string Epilepsy_OT_StartDate = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(10).StringCellValue;
                        string Epilepsy_OT_MoreThanThreeFitsInLastMonth = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(11).StringCellValue;
                        string Epilepsy_OT_ReRefToClinic = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(12).StringCellValue;
                        string Epilepsy_OT_ReRefNum = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(13).StringCellValue;

                        string Epilepsy_Medication = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(14).StringCellValue;

                        #endregion

                        #region Asthma
                        //HiEHRef = HiEH Referral
                        //CurrentRef = Current Referral
                        //OT = On Treatment
                        string AsthmaDateOfVisit = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(0).StringCellValue;

                        string Asthma_HiEHRef_WentToClinic = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(1).StringCellValue;
                        string Asthma_HiEHRef_ReReferToClinic = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(2).StringCellValue;
                        string Asthma_HiEHRef_ReRefNum = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(3).StringCellValue;

                        string Asthma_CurrentRef_DifficultyBreathing = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(5).StringCellValue;
                        string Asthma_CurrentRef_RefToClinic = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(6).StringCellValue;
                        string Asthma_CurrentRef_RefNum = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(7).StringCellValue;

                        string Asthma_OT_CurrentlyOnMeds = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(9).StringCellValue;
                        string Asthma_OT_StartDate = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(10).StringCellValue;
                        string Asthma_OT_IncreaseInAttacks = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(11).StringCellValue;
                        string Asthma_OT_ReRefToClinic = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(12).StringCellValue;
                        string Asthma_OT_ReRefNum = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(13).StringCellValue;

                        string Asthma_Medication = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(14).StringCellValue;

                        #endregion

                        #region HIV

                        //HiEHRef = HiEH Referral
                        //ClinictRef = Clinic Referral
                        //IP = If Positive
                        //IN = If Negative
                        //IU = If Unknown
                        string HIVDateOfVisit = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(0).StringCellValue;

                        string HIV_HiEHRef_WentToClinic = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(1).StringCellValue;
                        string HIV_HiEHRef_ReReferToClinic = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(2).StringCellValue;
                        string HIV_HiEHRef_ReRefNum = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(3).StringCellValue;

                        string HIV_ClinicRef_RefToClinic = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(5).StringCellValue;
                        string HIV_ClinicRef_RefNum = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(6).StringCellValue;

                        string HIV_HIVStatus = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(8).StringCellValue;

                        string HIV_IP_OnARVs = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(10).StringCellValue;
                        string HIV_IP_StartDate = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(11).StringCellValue;
                        string HIV_IP_AdherenceOK = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(12).StringCellValue;
                        string HIV_IP_Concerns = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(13).StringCellValue;
                        string HIV_IP_RefToClinic = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(14).StringCellValue;
                        string HIV_IP_RefNum = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(15).StringCellValue;
                        string HIV_IP_ARVsConcern = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(16).StringCellValue;
                        string HIV_IP_ReRefToClinic = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(17).StringCellValue;
                        string HIV_IP_ReRefNum = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(18).StringCellValue;

                        string HIV_IN_CounselingDone = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(20).StringCellValue;

                        string HIV_IU_HIVTestDone = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(22).StringCellValue;

                        string HIV_HIVTest_HIVTestResults = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(24).StringCellValue;
                        string HIV_HIVTest_ReferToClinic = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(25).StringCellValue;
                        string HIV_HIVTest_RefNum = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(26).StringCellValue;

                        string HIV_Medication = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(27).StringCellValue;

                        #endregion
            
                        #region TB

                        //HiEHRef = HiEH Referral
                        string TBDateOfVisit = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(0).StringCellValue;

                        string TB_HiEHRef_WentToClinic = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(1).StringCellValue;
                        string TB_HiEHRef_ReReferToClinic = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(2).StringCellValue;
                        string TB_HiEHRef_ReRefNum = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(3).StringCellValue;

                        string TB_SymptonsRefer_WeigthLoss = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(5).StringCellValue;
                        string TB_SymptonsRefer_SweatingAtNight = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(6).StringCellValue;
                        string TB_SymptonsRefer_FeverOverTwoWeeks = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(7).StringCellValue;
                        string TB_SymptonsRefer_CoughMoreTwoWeeks = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(8).StringCellValue;
                        string TB_SymptonsRefer_LossOfApetite = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(9).StringCellValue;
                        string TB_SymptonsRefer_RefToClinic = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(10).StringCellValue;
                        string TB_SymptonsRefer_RefNum = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(11).StringCellValue;
                        string TB_SymptonsRefer_Results = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(12).StringCellValue;

                        string TB_Treatment_NewlyDiagnosed = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(14).StringCellValue;
                        string TB_Treatment_StartDate = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(15).StringCellValue;
                        string TB_Treatment_RefContactsToClinic = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(16).StringCellValue;
                        string TB_Treatment_PrevOnMeds = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(17).StringCellValue;
                        string TB_Treatment_FinishDate = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(18).StringCellValue;
                        string TB_Treatment_Concerns = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(19).StringCellValue;
                        string TB_Treatment_RefToClinic = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(20).StringCellValue;
                        string TB_Treatment_RefNum = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(21).StringCellValue;

                        string TB_Medication = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(22).StringCellValue;
                        
                        #endregion

                        #region MaternalHealth

                        //HiEHRef = HiEH Referral
                        //CP = Currently Pregnant
                        //PP = Possible Pregnancy
                        string MatHealthDateOfVisit = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(0).StringCellValue;
                       
                        string MatHealth_HiEHRef_WentToClinic = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(1).StringCellValue;
                        string MatHealth_HiEHRef_ReRefToClinic = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(2).StringCellValue;
                        string MatHealth_HiEHRef_ReRefNum = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(3).StringCellValue;

                        string MatHealth_CP_DateOfFirstANC = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(5).StringCellValue;
                        string MatHealth_CP_DateOfLastANC = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(6).StringCellValue;
                        string MatHealth_CP_RefToClinic = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(7).StringCellValue;
                        string MatHealth_CP_RefNum = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(8).StringCellValue;
                        string MatHealth_CP_RegisterForMomConnect = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(9).StringCellValue;
                        string MatHealth_CP_ReRefToClinic = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(10).StringCellValue;
                        string MatHealth_CP_ReRefNum = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(11).StringCellValue;
                        string MatHealth_CP_DateOfNextANC = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(12).StringCellValue;
                        string MatHealth_CP_DeliveryDate = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(13).StringCellValue;
                        string MatHealth_CP_IntendBreastFeed = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(14).StringCellValue;
                        string MatHealth_CP_IntendFormula = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(15).StringCellValue;

                        string MatHealth_PP_AreYouPregnant = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(17).StringCellValue;
                        string MatHealth_PP_DonePregnancyTest = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(18).StringCellValue;
                        string MatHealth_PP_Result = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(19).StringCellValue;
                        string MatHealth_PP_RefToClinic = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(20).StringCellValue;
                        string MatHealth_PP_RefNum = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(21).StringCellValue;                

                        #endregion

                        #region ChildHealth
                        //HiEHRef = HiEH Referral
                        //NB = New Born
                        //CD = Child Development
                        //SDR = Social Development Referral
                        //CSDR = Current Social Development Referral
                        string ChildHealthDateOfVisit = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(0).StringCellValue;

                        string ChildHealth_HiEHRef_WentToClinic = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(1).StringCellValue;
                        string ChildHealth_HiEHRef_ReRefToClinic = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(2).StringCellValue;
                        string ChildHealth_HiEHRef_ReRefNum = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(3).StringCellValue;

                        string ChildHealth_NB_ChildWithRHC = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(5).StringCellValue;
                        string ChildHealth_NB_RefToClinic = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(6).StringCellValue;
                        string ChildHealth_NB_RefNum = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(7).StringCellValue;
                        string ChildHealth_NB_MotherHIVPos = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(8).StringCellValue;
                        string ChildHealth_NB_ChildBreastfed = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(9).StringCellValue;
                        string ChildHealth_NB_BreastfedHowLong = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(10).StringCellValue;
                        string ChildHealth_NB_OnNevirapine = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(11).StringCellValue;
                        string ChildHealth_NB_RefToClininc2 = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(12).StringCellValue;
                        string ChildHealth_NB_RefNum2 = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(13).StringCellValue;
                        string ChildHealth_NB_PCRDone = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(14).StringCellValue;
                        string ChildHealth_NB_RefToClinic3 = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(15).StringCellValue;
                        string ChildHealth_NB_RefNum3 = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(16).StringCellValue;
                        string ChildHealth_NB_ImmuneUpToDate = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(17).StringCellValue;
                        string ChildHealth_NB_ImmuneOutstanding = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(18).StringCellValue;
                        string ChildHealth_NB_VITAandWormsGiven = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(19).StringCellValue;
                        string ChildHealth_NB_RefToClinic4 = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(20).StringCellValue;
                        string ChildHealth_NB_RefNum4 = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(21).StringCellValue;

                        string ChildHealth_CD_WalkRight = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(23).StringCellValue;
                        string ChildHealth_CD_TalkRight = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(24).StringCellValue;
                        string ChildHealth_CD_RefToClinic = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(25).StringCellValue;
                        string ChildHealth_CD_RefNum = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(26).StringCellValue;

                        string ChildHealth_SDR_ChildAssisted = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(28).StringCellValue;
                        string ChildHealth_SDR_ReRefToSD = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(29).StringCellValue;
                        string ChildHealth_SDR_ReRefNum = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(30).StringCellValue;

                        string ChildHealth_CSDR_ListOfConcerns = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(31).StringCellValue;
                        string ChildHealth_CSDR_RefToClinic = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(32).StringCellValue;
                        string ChildHealth_CSDR_RefToSD = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(33).StringCellValue;
                        string ChildHealth_CSDR_RefNum = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(34).StringCellValue;

                        #endregion

                        #region Other
                        //HiEHRef = HiEH Referral
                        //OC = Other Condition
                        string OCDateOfVisit = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(0).StringCellValue;

                        string OC_HiEHRef_WentToClinic = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(1).StringCellValue;
                        string OC_HiEHRef_ReRefToClinic = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(2).StringCellValue;
                        string OC_HiEHRef_ReRefNum = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(3).StringCellValue;

                        string OC_ConditionThat = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(5).StringCellValue;
                        string OC_RefToClinic = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(6).StringCellValue;
                        string OC_RefNum = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(7).StringCellValue;

                        #endregion



                        // Queries here
                        #region Biographical
                        SqlConnection tempConnectionBio = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionBio.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportInsertBiographical", tempConnectionBio);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@BioChowName", BioChowName);
                            tempCommand.Parameters.AddWithValue("@BioUniqueID", BioUniqueID);
                            tempCommand.Parameters.AddWithValue("@BioFirstName", BioFirstName);
                            tempCommand.Parameters.AddWithValue("@BioSecondName", BioSecondName);
                            tempCommand.Parameters.AddWithValue("@BioIDNumber", BioIDNumber);
                            tempCommand.Parameters.AddWithValue("@BioGPSLatitude", BioGPSLatitude);
                            tempCommand.Parameters.AddWithValue("@BioGPSLongitude", BioGPSLongitude);
                            tempCommand.Parameters.AddWithValue("@BioArea", BioArea);
                            tempCommand.Parameters.AddWithValue("@BioClinic", BioClinic);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionBio.Close();
                        }
                        #endregion

                        #region VisitDetails
                        SqlConnection tempConnectionVisit = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionVisit.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportInsertVisitDetails", tempConnectionVisit);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@VDVisitNum", VDVisitNum);
                            tempCommand.Parameters.AddWithValue("@VDVisitDate", VDVisitDate);
                            tempCommand.Parameters.AddWithValue("@VDNextVisitDate", VDNextVisitDate);
                            tempCommand.Parameters.AddWithValue("@VDOutcome", VDOutcome);
                            tempCommand.Parameters.AddWithValue("@VDHPT", VDHPT);
                            tempCommand.Parameters.AddWithValue("@VDDiabetes", VDDiabetes);
                            tempCommand.Parameters.AddWithValue("@VDEpilepsy", VDEpilepsy);
                            tempCommand.Parameters.AddWithValue("@VDHIV", VDHIV);
                            tempCommand.Parameters.AddWithValue("@VDTB", VDTB);
                            tempCommand.Parameters.AddWithValue("@VDMatHealth", VDMatHealth);
                            tempCommand.Parameters.AddWithValue("@VDChildHealth", VDChildHealth);
                            tempCommand.Parameters.AddWithValue("@VDOther", VDOther);
                            tempCommand.Parameters.AddWithValue("@VDODoorToDoor", VDODoorToDoor);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionVisit.Close();
                        }
                        #endregion

                        #region HyperTension
                        SqlConnection tempConnectionHyper = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionHyper.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportHypertention", tempConnectionHyper);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@HyperDateOfVisit", HyperDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_WentToClinic", Hyper_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_ReReferToClinic", Hyper_HiEHRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_ReRefNum", Hyper_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_OnMeds", Hyper_HiEHRef_OnMeds);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_StartDate", Hyper_HiEHRef_StartDate);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_BPReading", Hyper_HiEHRef_BPReading);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_TodayReadingTop", Hyper_HiEHRef_TodayReadingTop);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_TodayReadingBottom", Hyper_HiEHRef_TodayReadingBottom);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_ReferToClinic", Hyper_HiEHRef_ReferToClinic);
                            tempCommand.Parameters.AddWithValue("@Hyper_HiEHRef_RefNum", Hyper_HiEHRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@Hyper_ClinicRef_ReReferToClinic", Hyper_ClinicRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@Hyper_ClinicRef_ReRefNum", Hyper_ClinicRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@Hyper_AOT_FollowUpReading", Hyper_AOT_FollowUpReading);
                            tempCommand.Parameters.AddWithValue("@Hyper_DoorToDoorReading_CheckReading", Hyper_DoorToDoorReading_CheckReading);
                            tempCommand.Parameters.AddWithValue("@Hyper_Medication", Hyper_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHyper.Close();
                        }
                        #endregion

                        #region Diabetes
                        SqlConnection tempConnectionDia = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionDia.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportDiabetes", tempConnectionDia);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@DiabetesDateOfVisit", DiabetesDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@Diabetes_HiEHRef_WentToClinic", Diabetes_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@Diabetes_HiEHRef_ReReferToClinic", Diabetes_HiEHRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@Diabetes_HiEHRef_ReRefNum", Diabetes_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@Diabetes_HiEHRef_OnMeds", Diabetes_HiEHRef_OnMeds);
                            tempCommand.Parameters.AddWithValue("@Diabetes_HiEHRef_StartDate", Diabetes_HiEHRef_StartDate);
                            tempCommand.Parameters.AddWithValue("@Diabetes_HiEHRef_FollowUpTestReading", Diabetes_HiEHRef_FollowUpTestReading);
                            tempCommand.Parameters.AddWithValue("@Diabetes_HiEHRef_ReferToClinic", Diabetes_HiEHRef_ReferToClinic);
                            tempCommand.Parameters.AddWithValue("@Diabetes_HiEHRef_RefNum", Diabetes_HiEHRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@Diabetes_ClinicRef_ReReferToClinic", Diabetes_ClinicRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@Diabetes_ClinicRef_ReRefNum", Diabetes_ClinicRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@Diabetes_AOT_FollowUpReading", Diabetes_AOT_FollowUpReading);
                            tempCommand.Parameters.AddWithValue("@Diabetes_DoorToDoorReading_CheckReading", Diabetes_DoorToDoorReading_CheckReading);
                            tempCommand.Parameters.AddWithValue("@Diabetes_Medication", Diabetes_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionDia.Close();
                        }
                        #endregion

                        #region Epilepsy
                        SqlConnection tempConnectionEpi = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionEpi.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportEpilepsy", tempConnectionEpi);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EpilepsyDateOfVisit", EpilepsyDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_HiEHRef_WentToClinic", Epilepsy_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_HiEHRef_ReReferToClinic", Epilepsy_HiEHRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_HiEHRef_ReRefNum", Epilepsy_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_CurrentRef_FitInLastMonth", Epilepsy_CurrentRef_FitInLastMonth);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_CurrentRef_RefToClinic", Epilepsy_CurrentRef_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_CurrentRef_RefNum", Epilepsy_CurrentRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_OT_CurrentlyOnMeds", Epilepsy_OT_CurrentlyOnMeds);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_OT_StartDate", Epilepsy_OT_StartDate);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_OT_MoreThanThreeFitsInLastMonth", Epilepsy_OT_MoreThanThreeFitsInLastMonth);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_OT_ReRefToClinic", Epilepsy_OT_ReRefToClinic);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_OT_ReRefNum", Epilepsy_OT_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@Epilepsy_Medication", Epilepsy_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionEpi.Close();
                        }
                        #endregion

                        #region Asthma
                        SqlConnection tempConnectionAst = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionAst.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportAsthma", tempConnectionAst);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@AsthmaDateOfVisit", AsthmaDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@Asthma_HiEHRef_WentToClinic", Asthma_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@Asthma_HiEHRef_ReReferToClinic", Asthma_HiEHRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@Asthma_HiEHRef_ReRefNum", Asthma_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@Asthma_CurrentRef_DifficultyBreathing", Asthma_CurrentRef_DifficultyBreathing);
                            tempCommand.Parameters.AddWithValue("@Asthma_CurrentRef_RefToClinic", Asthma_CurrentRef_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@Asthma_CurrentRef_RefNum", Asthma_CurrentRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@Asthma_OT_CurrentlyOnMeds", Asthma_OT_CurrentlyOnMeds);
                            tempCommand.Parameters.AddWithValue("@Asthma_OT_StartDate", Asthma_OT_StartDate);
                            tempCommand.Parameters.AddWithValue("@Asthma_OT_IncreaseInAttacks", Asthma_OT_IncreaseInAttacks);
                            tempCommand.Parameters.AddWithValue("@Asthma_OT_ReRefToClinic", Asthma_OT_ReRefToClinic);
                            tempCommand.Parameters.AddWithValue("@Asthma_OT_ReRefNum", Asthma_OT_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@Asthma_Medication", Asthma_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionAst.Close();
                        }
                        #endregion

                        #region HIV
                        SqlConnection tempConnectionHIV = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionHIV.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportHIV", tempConnectionHIV);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@HIVDateOfVisit", HIVDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@HIV_HiEHRef_WentToClinic", HIV_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@HIV_HiEHRef_ReReferToClinic", HIV_HiEHRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@HIV_HiEHRef_ReRefNum", HIV_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@HIV_ClinicRef_RefToClinic", HIV_ClinicRef_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@HIV_ClinicRef_RefNum", HIV_ClinicRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@HIV_HIVStatus", HIV_HIVStatus);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_OnARVs", HIV_IP_OnARVs);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_StartDate", HIV_IP_StartDate);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_AdherenceOK", HIV_IP_AdherenceOK);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_Concerns", HIV_IP_Concerns);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_RefToClinic", HIV_IP_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_RefNum", HIV_IP_RefNum);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_ARVsConcern", HIV_IP_ARVsConcern);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_ReRefToClinic", HIV_IP_ReRefToClinic);
                            tempCommand.Parameters.AddWithValue("@HIV_IP_ReRefNum", HIV_IP_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@HIV_IN_CounselingDone", HIV_IN_CounselingDone);
                            tempCommand.Parameters.AddWithValue("@HIV_IU_HIVTestDone", HIV_IU_HIVTestDone);
                            tempCommand.Parameters.AddWithValue("@HIV_HIVTest_HIVTestResults", HIV_HIVTest_HIVTestResults);
                            tempCommand.Parameters.AddWithValue("@HIV_HIVTest_ReferToClinic", HIV_HIVTest_ReferToClinic);
                            tempCommand.Parameters.AddWithValue("@HIV_HIVTest_RefNum", HIV_HIVTest_RefNum);
                            tempCommand.Parameters.AddWithValue("@HIV_Medication", HIV_Medication);
                            
                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHIV.Close();
                        }
                        #endregion

                        #region TB
                        SqlConnection tempConnectionTB = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionTB.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportTB", tempConnectionTB);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@TBDateOfVisit", TBDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@TB_HiEHRef_WentToClinic", TB_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@TB_HiEHRef_ReReferToClinic", TB_HiEHRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@TB_HiEHRef_ReRefNum", TB_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@TB_SymptonsRefer_WeigthLoss", TB_SymptonsRefer_WeigthLoss);
                            tempCommand.Parameters.AddWithValue("@TB_SymptonsRefer_SweatingAtNight", TB_SymptonsRefer_SweatingAtNight);
                            tempCommand.Parameters.AddWithValue("@TB_SymptonsRefer_FeverOverTwoWeeks", TB_SymptonsRefer_FeverOverTwoWeeks);
                            tempCommand.Parameters.AddWithValue("@TB_SymptonsRefer_CoughMoreTwoWeeks", TB_SymptonsRefer_CoughMoreTwoWeeks);
                            tempCommand.Parameters.AddWithValue("@TB_SymptonsRefer_LossOfApetite", TB_SymptonsRefer_LossOfApetite);
                            tempCommand.Parameters.AddWithValue("@TB_SymptonsRefer_RefToClinic", TB_SymptonsRefer_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@TB_SymptonsRefer_RefNum", TB_SymptonsRefer_RefNum);
                            tempCommand.Parameters.AddWithValue("@TB_SymptonsRefer_Results", TB_SymptonsRefer_Results);
                            tempCommand.Parameters.AddWithValue("@TB_Treatment_NewlyDiagnosed", TB_Treatment_NewlyDiagnosed);
                            tempCommand.Parameters.AddWithValue("@TB_Treatment_StartDate", TB_Treatment_StartDate);
                            tempCommand.Parameters.AddWithValue("@TB_Treatment_RefContactsToClinic", TB_Treatment_RefContactsToClinic);
                            tempCommand.Parameters.AddWithValue("@TB_Treatment_PrevOnMeds", TB_Treatment_PrevOnMeds);
                            tempCommand.Parameters.AddWithValue("@TB_Treatment_FinishDate", TB_Treatment_FinishDate);
                            tempCommand.Parameters.AddWithValue("@TB_Treatment_Concerns", TB_Treatment_Concerns);
                            tempCommand.Parameters.AddWithValue("@TB_Treatment_RefToClinic", TB_Treatment_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@TB_Treatment_RefNum", TB_Treatment_RefNum);
                            tempCommand.Parameters.AddWithValue("@TB_Medication", TB_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionTB.Close();
                        }
                        #endregion

                        #region MaternalHealth
                        SqlConnection tempConnectionMat = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionMat.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportMaternal", tempConnectionMat);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@MatHealthDateOfVisit", MatHealthDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@MatHealth_HiEHRef_WentToClinic", MatHealth_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@MatHealth_HiEHRef_ReRefToClinic", MatHealth_HiEHRef_ReRefToClinic);
                            tempCommand.Parameters.AddWithValue("@MatHealth_HiEHRef_ReRefNum", MatHealth_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_DateOfFirstANC", MatHealth_CP_DateOfFirstANC);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_DateOfLastANC", MatHealth_CP_DateOfLastANC);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_RefToClinic", MatHealth_CP_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_RefNum", MatHealth_CP_RefNum);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_RegisterForMomConnect", MatHealth_CP_RegisterForMomConnect);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_ReRefToClinic", MatHealth_CP_ReRefToClinic);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_ReRefNum", MatHealth_CP_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_DateOfNextANC", MatHealth_CP_DateOfNextANC);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_DeliveryDate", MatHealth_CP_DeliveryDate);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_IntendBreastFeed", MatHealth_CP_IntendBreastFeed);
                            tempCommand.Parameters.AddWithValue("@MatHealth_CP_IntendFormula", MatHealth_CP_IntendFormula);
                            tempCommand.Parameters.AddWithValue("@MatHealth_PP_AreYouPregnant", MatHealth_PP_AreYouPregnant);
                            tempCommand.Parameters.AddWithValue("@MatHealth_PP_DonePregnancyTest", MatHealth_PP_DonePregnancyTest);
                            tempCommand.Parameters.AddWithValue("@MatHealth_PP_Result", MatHealth_PP_Result);
                            tempCommand.Parameters.AddWithValue("@MatHealth_PP_RefToClinic", MatHealth_PP_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@MatHealth_PP_RefNum", MatHealth_PP_RefNum);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionMat.Close();
                        }
                        #endregion

                        #region ChildHealth
                        SqlConnection tempConnectionChild = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionChild.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportChild", tempConnectionChild);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ChildHealthDateOfVisit", ChildHealthDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_HiEHRef_WentToClinic", ChildHealth_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_HiEHRef_ReRefToClinic", ChildHealth_HiEHRef_ReRefToClinic);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_HiEHRef_ReRefNum", ChildHealth_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_ChildWithRHC", ChildHealth_NB_ChildWithRHC);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_RefToClinic", ChildHealth_NB_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_RefNum", ChildHealth_NB_RefNum);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_MotherHIVPos", ChildHealth_NB_MotherHIVPos);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_ChildBreastfed", ChildHealth_NB_ChildBreastfed);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_BreastfedHowLong", ChildHealth_NB_BreastfedHowLong);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_OnNevirapine", ChildHealth_NB_OnNevirapine);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_RefToClininc2", ChildHealth_NB_RefToClininc2);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_RefNum2", ChildHealth_NB_RefNum2);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_PCRDone", ChildHealth_NB_PCRDone);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_RefToClinic3", ChildHealth_NB_RefToClinic3);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_RefNum3", ChildHealth_NB_RefNum3);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_ImmuneUpToDate", ChildHealth_NB_ImmuneUpToDate);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_ImmuneOutstanding", ChildHealth_NB_ImmuneOutstanding);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_VITAandWormsGiven", ChildHealth_NB_VITAandWormsGiven);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_RefToClinic4", ChildHealth_NB_RefToClinic4);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_NB_RefNum4", ChildHealth_NB_RefNum4);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_CD_WalkRight", ChildHealth_CD_WalkRight);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_CD_TalkRight", ChildHealth_CD_TalkRight);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_CD_RefToClinic", ChildHealth_CD_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_CD_RefNum", ChildHealth_CD_RefNum);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_SDR_ChildAssisted", ChildHealth_SDR_ChildAssisted);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_SDR_ReRefToSD", ChildHealth_SDR_ReRefToSD);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_SDR_ReRefNum", ChildHealth_SDR_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_CSDR_ListOfConcerns", ChildHealth_CSDR_ListOfConcerns);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_CSDR_RefToClinic", ChildHealth_CSDR_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_CSDR_RefToSD", ChildHealth_CSDR_RefToSD);
                            tempCommand.Parameters.AddWithValue("@ChildHealth_CSDR_RefNum", ChildHealth_CSDR_RefNum);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionChild.Close();
                        }
                        #endregion

                        #region Other
                        SqlConnection tempConnectionOther = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionOther.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportOther", tempConnectionOther);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@OCDateOfVisit", OCDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@OC_HiEHRef_WentToClinic", OC_HiEHRef_WentToClinic);
                            tempCommand.Parameters.AddWithValue("@OC_HiEHRef_ReRefToClinic", OC_HiEHRef_ReRefToClinic);
                            tempCommand.Parameters.AddWithValue("@OC_HiEHRef_ReRefNum", OC_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@OC_ConditionThat", OC_ConditionThat);
                            tempCommand.Parameters.AddWithValue("@OC_RefToClinic", OC_RefToClinic);
                            tempCommand.Parameters.AddWithValue("@OC_RefNum", OC_RefNum);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionOther.Close();
                        }
                        #endregion


                    }
                }
                catch (Exception ex)
                {
                    Success = false;
                    ErrorList.Add(ex.Message);
                }
            }

            if (Success == false)
            {
                System.Windows.MessageBox.Show("Some errors occurred.  Please check the log file for a list of errors.", "Notification", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

                File.WriteAllLines(Directory.GetCurrentDirectory() + "ImportErrorLog.txt", ErrorList);
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "ImportErrorLog.txt");
            }
            else
                System.Windows.MessageBox.Show(Files.Length.ToString() + " file(s) have been successfully imported.", "Notification", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            return Success;
        }
    }
}
