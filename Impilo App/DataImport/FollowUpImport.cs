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
        public static string GetCellValue(ICell Target)
        {
            string ReturnValue = "";


            try
            {
                ReturnValue = Target.StringCellValue;
            }
            catch
            {
                try
                {
                    ReturnValue = Target.NumericCellValue.ToString();
                }
                catch
                {
                    ReturnValue = Target.DateCellValue.ToString();
                }
            }

            if (ReturnValue == "01-01-0001 12:00:00 AM")
                ReturnValue = "";

            return ReturnValue;
        }
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
                        string BioChowName = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(0));
                        string BioUniqueID = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(1));
                        string BioFirstName = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(2));
                        string BioSecondName = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(3));
                        string BioIDNumber = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(4));
                        string BioGPSLatitude = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(5));
                        string BioGPSLongitude = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(6));
                        string BioArea = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(7));
                        string BioClinic = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(2).GetCell(8));
                        
                        #endregion


                        //The rest of these go into the followup table

                        #region VisitDetails
                        
                        // VD prefix used to identify variables associated with Visit Details tab
                        string VDVisitNum = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(0)); //count generated from counting visits unique to a client ID
                        string VDVisitDate = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(1)); //dateTimeNow, No real need to enter manually
                        string VDNextVisitDate = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(2)); //good
                        string VDOutcome = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(4));//good
                        string VDHPT = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(5));//good
                        string VDDiabetes = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(6));//good
                        string VDEpilepsy = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(7));//good
                        string VDHIV = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(8));//good
                        string VDTB = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(9));//good
                        string VDMatHealth = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(10));//good
                        string VDChildHealth = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(11));//good
                        string VDOther = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(12));//good
                        string VDODoorToDoor = GetCellValue(MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(13));//good
                       
                        #endregion

                        #region HyperTension
                        
                                                   
                        //Hyper prefix used to identify variables associated with Hypertension tab.
                        //HiEHRef = HiEH Referral
                        //ClinicRef = Clinic Referral
                        //AOT = Already On Treatment
                        string HyperDateOfVisit = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(0));
                        
                        string Hyper_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(1)); //good
                        string Hyper_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(2)); //good
                        string Hyper_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(3)); //good
                        string Hyper_HiEHRef_OnMeds = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(4)); //good
                        string Hyper_HiEHRef_StartDate = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(5)); //good
                        //need to separate BP reading to systolic and diastolic for the next two rows
                        string Hyper_HiEHRef_BPReading = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(6));
                        string Hyper_HiEHRef_TodayReadingTop = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(7));
                        string Hyper_HiEHRef_TodayReadingBottom = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(8));//not in form
                        string Hyper_HiEHRef_ReferToClinic = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(9));
                        string Hyper_HiEHRef_RefNum = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(10));
                        
                        string Hyper_ClinicRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(12)); //I think all referrals are to the clinic this may not be needed
                        string Hyper_ClinicRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(13));//I think all referrals are to the clinic - this may not be needed

                        string Hyper_AOT_FollowUpReading = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(15)); //this is the same as the Todays reading
                        
                        string Hyper_DoorToDoorReading_CheckReading = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(17)); //Visit info will already indicate is this is a door to door visit - this may be redundant

                        string Hyper_Medication = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(18)); //good
                        
                        #endregion

                        #region Diabetes
                       
                        //HiEHRef = HiEH Referral
                        //ClinicRef = Clinic Referral
                        //AOT = Already On Treatment
                        string DiabetesDateOfVisit = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(0));

                        string Diabetes_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(1));
                        string Diabetes_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(2));
                        string Diabetes_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(3));
                        string Diabetes_HiEHRef_OnMeds = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(4));
                        string Diabetes_HiEHRef_StartDate = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(5));
                        string Diabetes_HiEHRef_FollowUpTestReading = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(6));
                        string Diabetes_HiEHRef_ReferToClinic = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(7));
                        string Diabetes_HiEHRef_RefNum = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(8));

                        string Diabetes_ClinicRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(10));
                        string Diabetes_ClinicRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(11));

                        string Diabetes_AOT_FollowUpReading = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(13));

                        string Diabetes_DoorToDoorReading_CheckReading = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(15));

                        string Diabetes_Medication = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(16));
                        
                        #endregion

                        #region Epilepsy
                        

                        //HiEHRef = HiEH Referral
                        //CurrentRef = Current Referral
                        //OT = On Treatment
                        string EpilepsyDateOfVisit = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(0));

                        string Epilepsy_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(1));
                        string Epilepsy_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(2));
                        string Epilepsy_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(3));
                       
                        string Epilepsy_CurrentRef_FitInLastMonth = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(5));
                        string Epilepsy_CurrentRef_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(6));
                        string Epilepsy_CurrentRef_RefNum = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(7));

                        string Epilepsy_OT_CurrentlyOnMeds = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(9));
                        string Epilepsy_OT_StartDate = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(10));
                        string Epilepsy_OT_MoreThanThreeFitsInLastMonth = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(11));
                        string Epilepsy_OT_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(12));
                        string Epilepsy_OT_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(13));

                        string Epilepsy_Medication = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(14));
                        
                        #endregion

                        #region Asthma
                        
                        //HiEHRef = HiEH Referral
                        //CurrentRef = Current Referral
                        //OT = On Treatment
                        string AsthmaDateOfVisit = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(0));

                        string Asthma_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(1));
                        string Asthma_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(2));
                        string Asthma_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(3));

                        string Asthma_CurrentRef_DifficultyBreathing = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(5));
                        string Asthma_CurrentRef_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(6));
                        string Asthma_CurrentRef_RefNum = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(7));

                        string Asthma_OT_CurrentlyOnMeds = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(9));
                        string Asthma_OT_StartDate = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(10));
                        string Asthma_OT_IncreaseInAttacks = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(11));
                        string Asthma_OT_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(12));
                        string Asthma_OT_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(13));

                        string Asthma_Medication = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(14));

                        #endregion
                       
                        #region HIV
                        
                        //HiEHRef = HiEH Referral
                        //ClinictRef = Clinic Referral
                        //IP = If Positive
                        //IN = If Negative
                        //IU = If Unknown
                        string HIVDateOfVisit = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(0));

                        string HIV_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(1));
                        string HIV_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(2));
                        string HIV_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(3));

                        string HIV_ClinicRef_RefToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(5));
                        string HIV_ClinicRef_RefNum = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(6));

                        string HIV_HIVStatus = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(8));

                        string HIV_IP_OnARVs = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(10));
                        string HIV_IP_StartDate = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(11));
                        string HIV_IP_AdherenceOK = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(12));
                        string HIV_IP_Concerns = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(13));
                        string HIV_IP_RefToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(14));
                        string HIV_IP_RefNum = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(15));
                        string HIV_IP_ARVsConcern = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(16));
                        string HIV_IP_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(17));
                        string HIV_IP_ReRefNum = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(18));

                        string HIV_IN_CounselingDone = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(20));

                        string HIV_IU_HIVTestDone = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(22));

                        string HIV_HIVTest_HIVTestResults = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(24));
                        string HIV_HIVTest_ReferToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(25));
                        string HIV_HIVTest_RefNum = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(26));

                        string HIV_Medication = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(27));
                       
                        #endregion
            
                        #region TB
                       

                        //HiEHRef = HiEH Referral
                        string TBDateOfVisit = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(0));

                        string TB_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(1));
                        string TB_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(2));
                        string TB_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(3));

                        string TB_SymptonsRefer_WeigthLoss = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(5));
                        string TB_SymptonsRefer_SweatingAtNight = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(6));
                        string TB_SymptonsRefer_FeverOverTwoWeeks = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(7));
                        string TB_SymptonsRefer_CoughMoreTwoWeeks = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(8));
                        string TB_SymptonsRefer_LossOfApetite = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(9));
                        string TB_SymptonsRefer_RefToClinic = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(10));
                        string TB_SymptonsRefer_RefNum = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(11));
                        string TB_SymptonsRefer_Results = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(12));

                        string TB_Treatment_NewlyDiagnosed = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(14));
                        string TB_Treatment_StartDate = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(15));
                        string TB_Treatment_RefContactsToClinic = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(16));
                        string TB_Treatment_PrevOnMeds = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(17));
                        string TB_Treatment_FinishDate = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(18));
                        string TB_Treatment_Concerns = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(19));
                        string TB_Treatment_RefToClinic =GetCellValue( MyWorkbook.GetSheet("TB").GetRow(5).GetCell(20));
                        string TB_Treatment_RefNum = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(21));

                        string TB_Medication = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(22));
                       
                        #endregion

                        #region MaternalHealth
                       

                        //HiEHRef = HiEH Referral
                        //CP = Currently Pregnant
                        //PP = Possible Pregnancy
                        string MatHealthDateOfVisit = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(0));
                       
                        string MatHealth_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(1));
                        string MatHealth_HiEHRef_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(2));
                        string MatHealth_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(3));

                        string MatHealth_CP_DateOfFirstANC = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(5));
                        string MatHealth_CP_DateOfLastANC = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(6));
                        string MatHealth_CP_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(7));
                        string MatHealth_CP_RefNum = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(8));
                        string MatHealth_CP_RegisterForMomConnect = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(9));
                        string MatHealth_CP_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(10));
                        string MatHealth_CP_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(11));
                        string MatHealth_CP_DateOfNextANC = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(12));
                        string MatHealth_CP_DeliveryDate = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(13));
                        string MatHealth_CP_IntendBreastFeed = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(14));
                        string MatHealth_CP_IntendFormula = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(15));

                        string MatHealth_PP_AreYouPregnant = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(17));
                        string MatHealth_PP_DonePregnancyTest = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(18));
                        string MatHealth_PP_Result = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(19));
                        string MatHealth_PP_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(20));
                        string MatHealth_PP_RefNum = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(21));
                        
                        #endregion

                        #region ChildHealth
                       

                        //HiEHRef = HiEH Referral
                        //NB = New Born
                        //CD = Child Development
                        //SDR = Social Development Referral
                        //CSDR = Current Social Development Referral
                        string ChildHealthDateOfVisit = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(0));

                        string ChildHealth_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(1));
                        string ChildHealth_HiEHRef_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(2));
                        string ChildHealth_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(3));

                        string ChildHealth_NB_ChildWithRHC = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(5));
                        string ChildHealth_NB_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(6));
                        string ChildHealth_NB_RefNum = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(7));
                        string ChildHealth_NB_MotherHIVPos = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(8));
                        string ChildHealth_NB_ChildBreastfed = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(9));
                        string ChildHealth_NB_BreastfedHowLong = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(10));
                        string ChildHealth_NB_OnNevirapine = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(11));
                        string ChildHealth_NB_RefToClininc2 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(12));
                        string ChildHealth_NB_RefNum2 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(13));
                        string ChildHealth_NB_PCRDone = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(14));
                        string ChildHealth_NB_RefToClinic3 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(15));
                        string ChildHealth_NB_RefNum3 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(16));
                        string ChildHealth_NB_ImmuneUpToDate = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(17));
                        string ChildHealth_NB_ImmuneOutstanding = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(18));
                        string ChildHealth_NB_VITAandWormsGiven = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(19));
                        string ChildHealth_NB_RefToClinic4 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(20));
                        string ChildHealth_NB_RefNum4 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(21));

                        string ChildHealth_CD_WalkRight = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(23));
                        string ChildHealth_CD_TalkRight = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(24));
                        string ChildHealth_CD_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(25));
                        string ChildHealth_CD_RefNum = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(26));

                        string ChildHealth_SDR_ChildAssisted = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(28));
                        string ChildHealth_SDR_ReRefToSD = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(29));
                        string ChildHealth_SDR_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(30));

                        string ChildHealth_CSDR_ListOfConcerns = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(31));
                        string ChildHealth_CSDR_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(32));
                        string ChildHealth_CSDR_RefToSD = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(33));
                        string ChildHealth_CSDR_RefNum = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(34));
                       
                        #endregion

                        #region Other
                       

                        //HiEHRef = HiEH Referral
                        //OC = Other Condition
                        string OCDateOfVisit = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(0));

                        string OC_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(1));
                        string OC_HiEHRef_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(2));
                        string OC_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(3));

                        string OC_ConditionThat = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(5));
                        string OC_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(6));
                        string OC_RefNum = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(7));
                       
                        #endregion



                        // Queries here
                        string ScreeningID = Utilities.GenerateScreeningID(BioFirstName, BioSecondName);

                        #region Biographical
                        SqlConnection tempConnectionBio = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionBio.Open();
                            SqlCommand tempCommand = new SqlCommand("FollowUpImportInsertBiographical", tempConnectionBio);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
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
