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
            if (Target != null)
            {
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
            }
            else
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
                        string VDVisitDate = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(1).DateCellValue.ToString();
                        if (VDVisitDate == "0001/01/01 12:00:00 AM")
                            VDVisitDate = "";
                        string VDNextVisitDate = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(2).DateCellValue.ToString();
                        if (VDNextVisitDate == "0001/01/01 12:00:00 AM")
                            VDNextVisitDate = "";
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
                        string HyperDateOfVisit = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(0).DateCellValue.ToString();
                        if (HyperDateOfVisit == "0001/01/01 12:00:00 AM")
                            HyperDateOfVisit = "";
                        
                        string Hyper_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(1)); //good
                        string Hyper_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(2)); //good
                        string Hyper_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(3)); //good
                        string Hyper_HiEHRef_OnMeds = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(4)); //good
                        string Hyper_HiEHRef_StartDate = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(5).DateCellValue.ToString();
                        if (Hyper_HiEHRef_StartDate == "0001/01/01 12:00:00 AM")
                            Hyper_HiEHRef_StartDate = "";
                        
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
                        string DiabetesDateOfVisit = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(0).DateCellValue.ToString();
                        if (DiabetesDateOfVisit == "0001/01/01 12:00:00 AM")
                            DiabetesDateOfVisit = "";

                        string Diabetes_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(1));
                        string Diabetes_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(2));
                        string Diabetes_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(3));
                        string Diabetes_HiEHRef_OnMeds = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(4));
                        string Diabetes_HiEHRef_StartDate = MyWorkbook.GetSheet("Diabetes").GetRow(5).GetCell(5).DateCellValue.ToString();
                        if (Diabetes_HiEHRef_StartDate == "0001/01/01 12:00:00 AM")
                            Diabetes_HiEHRef_StartDate = "";
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
                        //if (EpilepsyDateOfVisit == "0001/01/01 12:00:00 AM")
                            //EpilepsyDateOfVisit = "";

                        string Epilepsy_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(1));
                        string Epilepsy_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(2));
                        string Epilepsy_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(3));
                       
                        string Epilepsy_CurrentRef_FitInLastMonth = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(5));
                        string Epilepsy_CurrentRef_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(6));
                        string Epilepsy_CurrentRef_RefNum = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(7));

                        string Epilepsy_OT_CurrentlyOnMeds = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(9));
                        string Epilepsy_OT_StartDate = MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(10).DateCellValue.ToString();
                        if (Epilepsy_OT_StartDate == "0001/01/01 12:00:00 AM")
                            Epilepsy_OT_StartDate = "";
                        string Epilepsy_OT_MoreThanThreeFitsInLastMonth = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(11));
                        string Epilepsy_OT_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(12));
                        string Epilepsy_OT_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(13));

                        string Epilepsy_Medication = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(5).GetCell(14));
                        
                        #endregion

                        #region Asthma
                        
                        //HiEHRef = HiEH Referral
                        //CurrentRef = Current Referral
                        //OT = On Treatment
                        //string AsthmaDateOfVisit = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(0).DateCellValue.ToString();
                        //if (AsthmaDateOfVisit == "0001/01/01 12:00:00 AM")
                        //    AsthmaDateOfVisit = "";

                        //string Asthma_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(1));
                        //string Asthma_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(2));
                        //string Asthma_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(3));

                        //string Asthma_CurrentRef_DifficultyBreathing = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(5));
                        //string Asthma_CurrentRef_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(6));
                        //string Asthma_CurrentRef_RefNum = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(7));

                        //string Asthma_OT_CurrentlyOnMeds = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(9));
                        //string Asthma_OT_StartDate = MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(10).DateCellValue.ToString();
                        //if (Asthma_OT_StartDate == "0001/01/01 12:00:00 AM")
                        //    Asthma_OT_StartDate = "";
                        //string Asthma_OT_IncreaseInAttacks = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(11));
                        //string Asthma_OT_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(12));
                        //string Asthma_OT_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(13));

                        //string Asthma_Medication = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(4).GetCell(14));

                        #endregion
                       
                        #region HIV
                        
                        //HiEHRef = HiEH Referral
                        //ClinictRef = Clinic Referral
                        //IP = If Positive
                        //IN = If Negative
                        //IU = If Unknown
                        string HIVDateOfVisit = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(0).DateCellValue.ToString();
                        if (HIVDateOfVisit == "0001/01/01 12:00:00 AM")
                            HIVDateOfVisit = "";

                        string HIV_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(1));
                        string HIV_HiEHRef_ReReferToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(2));
                        string HIV_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(3));

                        string HIV_ClinicRef_RefToClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(5));
                        string HIV_ClinicRef_RefNum = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(6));

                        string HIV_HIVStatus = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(8));

                        string HIV_IP_OnARVs = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(10));
                        string HIV_IP_StartDate = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(11).DateCellValue.ToString();
                        if (HIV_IP_StartDate == "0001/01/01 12:00:00 AM")
                            HIV_IP_StartDate = "";
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
                       // if (TBDateOfVisit == "0001/01/01 12:00:00 AM")
                       //     TBDateOfVisit = "";

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
                        string TB_Treatment_StartDate = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(15).DateCellValue.ToString();
                        if (TB_Treatment_StartDate == "0001/01/01 12:00:00 AM")
                            TB_Treatment_StartDate = "";
                        string TB_Treatment_RefContactsToClinic = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(16));
                        string TB_Treatment_PrevOnMeds = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(17));
                        string TB_Treatment_FinishDate = MyWorkbook.GetSheet("TB").GetRow(5).GetCell(18).DateCellValue.ToString();
                        if (TB_Treatment_FinishDate == "0001/01/01 12:00:00 AM")
                            TB_Treatment_FinishDate = "";
                        string TB_Treatment_Concerns = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(19));
                        string TB_Treatment_RefToClinic =GetCellValue( MyWorkbook.GetSheet("TB").GetRow(5).GetCell(20));
                        string TB_Treatment_RefNum = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(21));

                        string TB_Medication = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(5).GetCell(22));
                       
                        #endregion

                        #region MaternalHealth
                       

                        //HiEHRef = HiEH Referral
                        //CP = Currently Pregnant
                        //PP = Possible Pregnancy
                        string MatHealthDateOfVisit = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(0).DateCellValue.ToString();
                        if (MatHealthDateOfVisit == "0001/01/01 12:00:00 AM")
                            MatHealthDateOfVisit = "";
                       
                        string MatHealth_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(1));
                        string MatHealth_HiEHRef_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(2));
                        string MatHealth_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(3));

                        string MatHealth_CP_DateOfFirstANC = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(5).DateCellValue.ToString();
                        if (MatHealth_CP_DateOfFirstANC == "0001/01/01 12:00:00 AM")
                            MatHealth_CP_DateOfFirstANC = "";
                        string MatHealth_CP_DateOfLastANC = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(6).DateCellValue.ToString();
                        if (MatHealth_CP_DateOfLastANC == "0001/01/01 12:00:00 AM")
                            MatHealth_CP_DateOfLastANC = "";
                        string MatHealth_CP_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(7));
                        string MatHealth_CP_RefNum = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(8));
                        string MatHealth_CP_RegisterForMomConnect = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(9));
                        string MatHealth_CP_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(10));
                        string MatHealth_CP_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(11));
                        string MatHealth_CP_DateOfNextANC = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(12).DateCellValue.ToString();
                        if (MatHealth_CP_DateOfNextANC == "0001/01/01 12:00:00 AM")
                            MatHealth_CP_DateOfNextANC = "";
                        string MatHealth_CP_DeliveryDate = MyWorkbook.GetSheet("Mat Health").GetRow(5).GetCell(13).DateCellValue.ToString();
                        if (MatHealth_CP_DeliveryDate == "0001/01/01 12:00:00 AM")
                            MatHealth_CP_DeliveryDate = "";
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
                        string ChildHealthDateOfVisit = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(0).DateCellValue.ToString();
                        if (ChildHealthDateOfVisit == "0001/01/01 12:00:00 AM")
                            ChildHealthDateOfVisit = "";

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
                        //if (OCDateOfVisit == "0001/01/01 12:00:00 AM")
                         //   OCDateOfVisit = "";

                        string OC_HiEHRef_WentToClinic = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(1));
                        string OC_HiEHRef_ReRefToClinic = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(2));
                        string OC_HiEHRef_ReRefNum = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(3));

                        string OC_ConditionThat = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(5));
                        string OC_RefToClinic = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(6));
                        string OC_RefNum = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(7));
                       
                        #endregion



                        // Queries here
                        string ScreeningID = Utilities.GenerateScreeningID(BioFirstName, BioSecondName);
                        int ClinicID = -1;
                        int EncounterID = -1;
                        int tempIDHyp = -1;
                        int tempIDDia = -1;
                        int tempIDEpi = -1;
                        int tempIDAs = -1;
                        int tempIDHIV = -1;
                        int tempIDTB = -1;

                        #region Find Clinic ID
                        int Errors = 0;
                        SqlConnection tempConnectionFindClinic = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                        try
                        {
                            tempConnectionFindClinic.Open();
                            SqlCommand tempCommand = new SqlCommand("FindClinicID", tempConnectionFindClinic);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ClinicName", BioClinic);

                            ClinicID = (int)tempCommand.ExecuteScalar();
                        }
                        catch (Exception ex) { }
                        finally
                        {
                            tempConnectionFindClinic.Close();
                        }
                        #endregion
                        
                        #region Biographical
                        SqlConnection tempConnectionBio = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionBio.Open();
                            SqlCommand tempCommand = new SqlCommand("AddClient", tempConnectionBio);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ClientID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@HeadOfHousehold", "");
                            tempCommand.Parameters.AddWithValue("@FirstName", BioFirstName);
                            tempCommand.Parameters.AddWithValue("@LastName", BioSecondName);
                            tempCommand.Parameters.AddWithValue("@GPSLatitude", BioGPSLatitude);
                            tempCommand.Parameters.AddWithValue("@GPSLongitude", BioGPSLongitude);
                            tempCommand.Parameters.AddWithValue("@IDNo", BioIDNumber);
                            tempCommand.Parameters.AddWithValue("@ClinicID", ClinicID);
                            tempCommand.Parameters.AddWithValue("@DateOfBirth", "");
                            tempCommand.Parameters.AddWithValue("@Gender", "");
                            tempCommand.Parameters.AddWithValue("@AttendingSchool", "");
                            tempCommand.Parameters.AddWithValue("@Grade", "");
                            tempCommand.Parameters.AddWithValue("@NameofSchool", "");
                            tempCommand.Parameters.AddWithValue("@Area", BioArea);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionBio.Close();
                        }
                        #endregion

                        #region Encounters
                        SqlConnection tempConnectionEncounter = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                        try
                        {
                            tempConnectionEncounter.Open();
                            SqlCommand tempCommand = new SqlCommand("AddEncounters", tempConnectionEncounter);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterDate", "");
                            tempCommand.Parameters.AddWithValue("@ClientID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@EncounterType", 2);
                            tempCommand.Parameters.AddWithValue("@EncounterCapturedBy", BioChowName);

                            EncounterID = (int)((decimal)tempCommand.ExecuteScalar());
                        }
                        catch (Exception ex) { System.Windows.MessageBox.Show(ex.ToString()); }
                        finally
                        {
                            tempConnectionEncounter.Close();
                        }

                        #endregion

                        #region VisitDetails
                        SqlConnection tempConnectionVisit = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionVisit.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpVisitDetails", tempConnectionVisit);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@fuvdID", VDVisitNum);
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@fuvdVisitDate", VDVisitDate);
                            tempCommand.Parameters.AddWithValue("@fuvdNextVisitDate", VDNextVisitDate);
                            tempCommand.Parameters.AddWithValue("@duvdOutcome", VDOutcome);
                            tempCommand.Parameters.AddWithValue("@duvdHypertension", VDHPT == "Yes" || VDHPT == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@duvdDiabetes", VDDiabetes == "Yes" || VDDiabetes == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@duvdEpilepsy", VDEpilepsy == "Yes" || VDEpilepsy == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@duvdHIV", VDHIV == "Yes" || VDHIV == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@duvdTB", VDTB == "Yes" || VDTB == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@duvdMaternalHealth", VDMatHealth == "Yes" || VDMatHealth == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@duvdChildHealth", VDChildHealth == "Yes" || VDChildHealth == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@duvdOther", VDOther == "Yes" || VDOther == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@duvdDoorDoor", VDODoorToDoor == "Yes" || VDODoorToDoor == "1" ? 1 : 0);

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
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpHypertension", tempConnectionHyper);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@fuhDateOfVisit", HyperDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@fuhHiEHWentToClinic", Hyper_HiEHRef_WentToClinic == "Yes" || Hyper_HiEHRef_WentToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhHiEHReReferToClinic", Hyper_HiEHRef_ReReferToClinic == "Yes" || Hyper_HiEHRef_ReReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhHiEHRefNo", Hyper_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fuhHiEHCurrentlyOnMeds", Hyper_HiEHRef_OnMeds == "Yes" || Hyper_HiEHRef_OnMeds == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhHiEHStartDate", Hyper_HiEHRef_StartDate);
                            tempCommand.Parameters.AddWithValue("@fuhHiEHBPScreeningSystolic", Hyper_HiEHRef_BPReading == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Hyper_HiEHRef_BPReading));
                            tempCommand.Parameters.AddWithValue("@fuhHiEHBPScreeningDiastolic", Hyper_HiEHRef_BPReading == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Hyper_HiEHRef_BPReading));
                            tempCommand.Parameters.AddWithValue("@fuhHiEHBPTodaySystolic", Hyper_HiEHRef_TodayReadingTop == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Hyper_HiEHRef_TodayReadingTop));
                            tempCommand.Parameters.AddWithValue("@fuhHiEHBPTodayDiastolic", Hyper_HiEHRef_TodayReadingBottom == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Hyper_HiEHRef_TodayReadingBottom));
                            tempCommand.Parameters.AddWithValue("@fuhHiEHReferToClinic", Hyper_HiEHRef_ReferToClinic == "Yes" || Hyper_HiEHRef_ReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhHiEHRefNo2", Hyper_HiEHRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@fuhCRReReferToClinic", Hyper_ClinicRef_ReReferToClinic == "Yes" || Hyper_ClinicRef_ReReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhCRRefNo", Hyper_ClinicRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fuhAlreadyOnTreatmentFollowUpTestReadingSystolic", Hyper_AOT_FollowUpReading == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Hyper_AOT_FollowUpReading));
                            tempCommand.Parameters.AddWithValue("@fuhAlreadyOnTreatmentFollowUpTestReadingDiastolic", Hyper_AOT_FollowUpReading == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Hyper_AOT_FollowUpReading));
                            tempCommand.Parameters.AddWithValue("@fuhDoorToDoorCheckReadingSystolic", Hyper_DoorToDoorReading_CheckReading == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Hyper_DoorToDoorReading_CheckReading));
                            tempCommand.Parameters.AddWithValue("@fuhDoorToDoorCheckReadingDiastolic", Hyper_DoorToDoorReading_CheckReading == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Hyper_DoorToDoorReading_CheckReading));

                            tempIDHyp = (int)((decimal)(tempCommand.ExecuteScalar()));
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHyper.Close();
                        }
                        #endregion

                        #region HyperTension Meds
                        SqlConnection tempConnectionHyperMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionHyperMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpHypertensionMedication", tempConnectionHyperMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@fuhID", tempIDHyp);
                            tempCommand.Parameters.AddWithValue("@fuhmName", Hyper_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHyperMeds.Close();
                        }
                        #endregion

                        #region Diabetes
                        SqlConnection tempConnectionDia = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionDia.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpDiabetes", tempConnectionDia);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@fudDateOfVisit", DiabetesDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@fudHiEHWentToClinic", Diabetes_HiEHRef_WentToClinic == "Yes" || Diabetes_HiEHRef_WentToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fudHiEHReReferToClinic", Diabetes_HiEHRef_ReReferToClinic == "Yes" || Diabetes_HiEHRef_ReReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fudHiEHRefNo", Diabetes_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fudHiEHCurrentlyOnMeds", Diabetes_HiEHRef_OnMeds == "Yes" || Diabetes_HiEHRef_OnMeds == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fudHiEHStartDate", Diabetes_HiEHRef_StartDate);
                            tempCommand.Parameters.AddWithValue("@fudHiEHFollowUpTestReading", Diabetes_HiEHRef_FollowUpTestReading == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Diabetes_HiEHRef_FollowUpTestReading));
                            tempCommand.Parameters.AddWithValue("@fudHiEHReferToClinic2", Diabetes_HiEHRef_ReferToClinic == "Yes" || Diabetes_HiEHRef_ReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fudHiEHRefNo2", Diabetes_HiEHRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@fudClinicRefReferToClinic", Diabetes_ClinicRef_ReReferToClinic);
                            tempCommand.Parameters.AddWithValue("@fudClinicRefRefNo", Diabetes_ClinicRef_ReRefNum);
                            //tempCommand.Parameters.AddWithValue("@Diabetes_AOT_FollowUpReading", Diabetes_AOT_FollowUpReading);
                            tempCommand.Parameters.AddWithValue("@fudAlreadyOnTreatmentFollowUpTestReading", Diabetes_DoorToDoorReading_CheckReading == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(Diabetes_DoorToDoorReading_CheckReading));
                            tempCommand.Parameters.AddWithValue("@fudDoorDoor", "");
                            
                            tempIDDia = (int)((decimal)(tempCommand.ExecuteScalar()));
                        }
                        catch { }
                        finally
                        {
                            tempConnectionDia.Close();
                        }
                        #endregion

                        #region Diabetes Meds
                        SqlConnection tempConnectionDiaMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionDiaMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpDiabetesMedication", tempConnectionDiaMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@fudID", tempIDDia);
                            tempCommand.Parameters.AddWithValue("@fudmName", Diabetes_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionDiaMeds.Close();
                        }
                        #endregion

                        #region Epilepsy
                        SqlConnection tempConnectionEpi = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionEpi.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpEpilepsy", tempConnectionEpi);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            //tempCommand.Parameters.AddWithValue("@EpilepsyDateOfVisit", EpilepsyDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@fueHiEHWentToClinic", Epilepsy_HiEHRef_WentToClinic == "Yes" || Epilepsy_HiEHRef_WentToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fueHiEHReReferToClinic", Epilepsy_HiEHRef_ReReferToClinic == "Yes" || Epilepsy_HiEHRef_ReReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fueHiEHRefNo", Epilepsy_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fueCRFitInLastMonth", Epilepsy_CurrentRef_FitInLastMonth == "Yes" || Epilepsy_CurrentRef_FitInLastMonth == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fueCRReferToClinic", Epilepsy_CurrentRef_RefToClinic == "Yes" || Epilepsy_CurrentRef_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fueCRRefNo", Epilepsy_CurrentRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@fueOnTreatmentCurrentlyOnMeds", Epilepsy_OT_CurrentlyOnMeds == "Yes" || Epilepsy_OT_CurrentlyOnMeds == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fueOnTreatmentStartDate", Epilepsy_OT_StartDate);
                            tempCommand.Parameters.AddWithValue("@fueOnTreatmentMoreThan3FitsSinceLastMonth", Epilepsy_OT_MoreThanThreeFitsInLastMonth == "Yes" || Epilepsy_OT_MoreThanThreeFitsInLastMonth == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fueOnTreatmentReReferToClinic", Epilepsy_OT_ReRefToClinic == "Yes" || Epilepsy_OT_ReRefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fueOnTreatmentRefNo", Epilepsy_OT_ReRefNum);

                            tempIDEpi = (int)((decimal)(tempCommand.ExecuteScalar()));
                            
                        }
                        catch { }
                        finally
                        {
                            tempConnectionEpi.Close();
                        }
                        #endregion

                        #region Epilepsy Meds
                        SqlConnection tempConnectionEpiMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionEpiMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpEpilepsyMedication", tempConnectionEpiMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@fueID", tempIDEpi);
                            tempCommand.Parameters.AddWithValue("@fuemName", Epilepsy_Medication);
                           
                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionEpiMeds.Close();
                        }
                        #endregion

                        #region Asthma
                        //SqlConnection tempConnectionAst = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        //try
                        //{
                        //    tempConnectionAst.Open();
                        //    SqlCommand tempCommand = new SqlCommand("AddFollowUpAsthma", tempConnectionAst);
                        //    tempCommand.CommandType = CommandType.StoredProcedure;
                        //    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                        //    tempCommand.Parameters.AddWithValue("@fuaDateOfVisit", AsthmaDateOfVisit);
                        //    tempCommand.Parameters.AddWithValue("@fuaHiEHWentToClinic", Asthma_HiEHRef_WentToClinic == "Yes" || Asthma_HiEHRef_WentToClinic == "1" ? 1 : 0);
                        //    tempCommand.Parameters.AddWithValue("@fuaHiEHReReferToClinic", Asthma_HiEHRef_ReReferToClinic == "Yes" || Asthma_HiEHRef_ReReferToClinic == "1" ? 1 : 0);
                        //    tempCommand.Parameters.AddWithValue("@fuaHiHRefNo", Asthma_HiEHRef_ReRefNum);
                        //    tempCommand.Parameters.AddWithValue("@fuaCRDifficultyBreathingAndWheezing", Asthma_CurrentRef_DifficultyBreathing == "Yes" || Asthma_CurrentRef_DifficultyBreathing == "1" ? 1 : 0);
                        //    tempCommand.Parameters.AddWithValue("@fuaCRReferToClinic", Asthma_CurrentRef_RefToClinic == "Yes" || Asthma_CurrentRef_RefToClinic == "1" ? 1 : 0);
                        //    tempCommand.Parameters.AddWithValue("@fuaCRRefNo", Asthma_CurrentRef_RefNum);
                        //    tempCommand.Parameters.AddWithValue("@fuaOTCurrentlyOnMeds", Asthma_OT_CurrentlyOnMeds == "Yes" || Asthma_OT_CurrentlyOnMeds == "1" ? 1 : 0);
                        //    tempCommand.Parameters.AddWithValue("@fuaOTStartDate", Asthma_OT_StartDate);
                        //    tempCommand.Parameters.AddWithValue("@fuaOTIncreasedNoOfAsthmaAttacks", Asthma_OT_IncreaseInAttacks == "Yes" || Asthma_OT_IncreaseInAttacks == "1" ? 1 : 0);
                        //    tempCommand.Parameters.AddWithValue("@fuaOTReReferToClinic", Asthma_OT_ReRefToClinic == "Yes" || Asthma_OT_ReRefToClinic == "1" ? 1 : 0);
                        //    tempCommand.Parameters.AddWithValue("@fuaOTRefNo", Asthma_OT_ReRefNum);

                        //    tempIDAs = (int)(tempCommand.ExecuteScalar());
                        //}
                        //catch { }
                        //finally
                        //{
                        //    tempConnectionAst.Close();
                        //}
                        #endregion

                        #region Asthma Meds
                        //SqlConnection tempConnectionAstMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        //try
                        //{
                        //    tempConnectionAstMeds.Open();
                        //    SqlCommand tempCommand = new SqlCommand("AddFollowUpAsthmaMedication", tempConnectionAstMeds);
                        //    tempCommand.CommandType = CommandType.StoredProcedure;
                        //    tempCommand.Parameters.AddWithValue("@fuaID", tempIDAs);
                        //    tempCommand.Parameters.AddWithValue("@fuamName", Asthma_Medication);

                        //    tempCommand.ExecuteNonQuery();
                        //}
                        //catch { }
                        //finally
                        //{
                        //    tempConnectionAstMeds.Close();
                        //}
                        #endregion

                        #region HIV
                        SqlConnection tempConnectionHIV = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionHIV.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpHIV", tempConnectionHIV);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@fuhivDateOfVisit", HIVDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@fuhivHiEHWentToClinic", HIV_HiEHRef_WentToClinic == "Yes" || HIV_HiEHRef_WentToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivHiEHReReferToClinic", HIV_HiEHRef_ReReferToClinic == "Yes" || HIV_HiEHRef_ReReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivHiEHRefNo", HIV_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fuhivCRReferToClinic", HIV_ClinicRef_RefToClinic == "Yes" || HIV_ClinicRef_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivCRRefNo", HIV_ClinicRef_RefNum);
                            tempCommand.Parameters.AddWithValue("@fuhivHIVStatus", HIV_HIVStatus);
                            tempCommand.Parameters.AddWithValue("@fuhivIPOnARV", HIV_IP_OnARVs == "Yes" || HIV_IP_OnARVs == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivIPStartDate", HIV_IP_StartDate);
                            tempCommand.Parameters.AddWithValue("@fuhivIPAdherenceOK", HIV_IP_AdherenceOK == "Yes" || HIV_IP_AdherenceOK == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivIPConcerns", HIV_IP_Concerns == "Yes" || HIV_IP_Concerns == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivIPReferToClinic", HIV_IP_RefToClinic == "Yes" || HIV_IP_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivIPRefNo", HIV_IP_RefNum);
                            tempCommand.Parameters.AddWithValue("@fuhivIPNotOnARV", HIV_IP_ARVsConcern == "Yes" || HIV_IP_ARVsConcern == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivIPReferToClinic2", HIV_IP_ReRefToClinic == "Yes" || HIV_IP_ReRefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivIPRefNo2", HIV_IP_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fuhivINCounsellingDone", HIV_IN_CounselingDone == "Yes" || HIV_IN_CounselingDone == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivIUHIVTestDone", HIV_IU_HIVTestDone == "Yes" || HIV_IU_HIVTestDone == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivHIVTestResults", HIV_HIVTest_HIVTestResults);
                            tempCommand.Parameters.AddWithValue("@fuhivHIVTestReferToClinic", HIV_HIVTest_ReferToClinic == "Yes" || HIV_HIVTest_ReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuhivHIVRefNo", HIV_HIVTest_RefNum);

                            tempIDHIV = (int)((decimal)(tempCommand.ExecuteScalar()));
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHIV.Close();
                        }
                        #endregion

                        #region HIV Meds
                        SqlConnection tempConnectionHIVMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionHIVMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpHIVMedication", tempConnectionHIVMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@fuhivID", tempIDHIV);
                            tempCommand.Parameters.AddWithValue("@fuhivmName", HIV_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHIVMeds.Close();
                        }
                        #endregion

                        #region TB
                        SqlConnection tempConnectionTB = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionTB.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpTB", tempConnectionTB);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@futbDateOfVisit", TBDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@futbHiEHWentToClinic", TB_HiEHRef_WentToClinic == "Yes" || TB_HiEHRef_WentToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbHiEHReReferToClinic", TB_HiEHRef_ReReferToClinic == "Yes" || TB_HiEHRef_ReReferToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbHiEHRefNo", TB_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@futbTBSRecentUnplannedLoseOfWeight", TB_SymptonsRefer_WeigthLoss == "Yes" || TB_SymptonsRefer_WeigthLoss == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBSExcessiveSweatingAtNight", TB_SymptonsRefer_SweatingAtNight == "Yes" || TB_SymptonsRefer_SweatingAtNight == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBSFeverOver2Weeks", TB_SymptonsRefer_FeverOverTwoWeeks == "Yes" || TB_SymptonsRefer_FeverOverTwoWeeks == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBSCoughMoreThan2Weeks", TB_SymptonsRefer_CoughMoreTwoWeeks == "Yes" || TB_SymptonsRefer_CoughMoreTwoWeeks == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBSLossOfApetite", TB_SymptonsRefer_LossOfApetite == "Yes" || TB_SymptonsRefer_LossOfApetite == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBSReferToClinic", TB_SymptonsRefer_RefToClinic == "Yes" || TB_SymptonsRefer_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBSRefNo", TB_SymptonsRefer_RefNum);
                            tempCommand.Parameters.AddWithValue("@futbTBSResults", TB_SymptonsRefer_Results);
                            tempCommand.Parameters.AddWithValue("@futbTBOTNewlyDiagnosedInLastMonth", TB_Treatment_NewlyDiagnosed == "Yes" || TB_Treatment_NewlyDiagnosed == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBOTStartDate", TB_Treatment_StartDate);
                            tempCommand.Parameters.AddWithValue("@futbTBOTReferTBContactsToClinic", TB_Treatment_RefContactsToClinic == "Yes" || TB_Treatment_RefContactsToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBOTPreviouslyOnMeds", TB_Treatment_PrevOnMeds == "Yes" || TB_Treatment_PrevOnMeds == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBOTFinishDate", TB_Treatment_FinishDate);
                            tempCommand.Parameters.AddWithValue("@futbTBOTConcerns", TB_Treatment_Concerns == "Yes" || TB_Treatment_Concerns == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBOTReferToClinic", TB_Treatment_RefToClinic == "Yes" || TB_Treatment_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@futbTBOTRefNo", TB_Treatment_RefNum);

                            tempIDTB = (int)((decimal)(tempCommand.ExecuteScalar()));

                        }
                        catch { }
                        finally
                        {
                            tempConnectionTB.Close();
                        }
                        #endregion

                        #region TB Meds
                        SqlConnection tempConnectionTBMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionTBMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpTBMedication", tempConnectionTBMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@futbID", tempIDTB);
                            tempCommand.Parameters.AddWithValue("@futbmName", TB_Medication);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionTBMeds.Close();
                        }
                        #endregion

                        #region MaternalHealth
                        SqlConnection tempConnectionMat = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionMat.Open();
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpMaternalHealth", tempConnectionMat);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@fumhDateOfVisit", MatHealthDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@fumhHiEHWentToClinic", MatHealth_HiEHRef_WentToClinic == "Yes" || MatHealth_HiEHRef_WentToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhHiEHReReferToClinic", MatHealth_HiEHRef_ReRefToClinic == "Yes" || MatHealth_HiEHRef_ReRefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhHiEHRefNo", MatHealth_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fumhCPDateOfFirstANC", MatHealth_CP_DateOfFirstANC);
                            tempCommand.Parameters.AddWithValue("@fumhCPDateOfLastANC", MatHealth_CP_DateOfLastANC);
                            tempCommand.Parameters.AddWithValue("@fumhCPReferToClinic", MatHealth_CP_RefToClinic == "Yes" || MatHealth_CP_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhCPRefNo", MatHealth_CP_RefNum == "Yes" || MatHealth_CP_RefNum == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhCPRegisteredForMomConnect", MatHealth_CP_RegisterForMomConnect == "Yes" || MatHealth_CP_RegisterForMomConnect == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhCPReferToClinic2", MatHealth_CP_ReRefToClinic == "Yes" || MatHealth_CP_ReRefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhCPRefNo2", MatHealth_CP_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fumhCPDateOfNextANC", MatHealth_CP_DateOfNextANC);
                            tempCommand.Parameters.AddWithValue("@fumhCPExpectedDateOfDelivery", MatHealth_CP_DeliveryDate);
                            tempCommand.Parameters.AddWithValue("@fumhCPIntendBreastFeed", MatHealth_CP_IntendBreastFeed == "Yes" || MatHealth_CP_IntendBreastFeed == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhCPIntendFormulaFeed", MatHealth_CP_IntendFormula == "Yes" || MatHealth_CP_IntendFormula == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhPPPossiblePregnant", MatHealth_PP_AreYouPregnant == "Yes" || MatHealth_PP_AreYouPregnant == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhPPTestDone", MatHealth_PP_DonePregnancyTest == "Yes" || MatHealth_PP_DonePregnancyTest == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhPPResult", MatHealth_PP_Result);
                            tempCommand.Parameters.AddWithValue("@fumhPPReferToClinic", MatHealth_PP_RefToClinic == "Yes" || MatHealth_PP_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fumhPPRefNo", MatHealth_PP_RefNum);

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
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpChildHealth", tempConnectionChild);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@fuchDateOfVisit", ChildHealthDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@fuchHiEHWentToClinic", ChildHealth_HiEHRef_WentToClinic == "Yes" || ChildHealth_HiEHRef_WentToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchHiEHReReferToClinic", ChildHealth_HiEHRef_ReRefToClinic == "Yes" || ChildHealth_HiEHRef_ReRefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchHiEHRefNo", ChildHealth_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fuchNewChildWithRTHC", ChildHealth_NB_ChildWithRHC == "Yes" || ChildHealth_NB_ChildWithRHC == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewReferToClinic", ChildHealth_NB_RefToClinic == "Yes" || ChildHealth_NB_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewRefNo", ChildHealth_NB_RefNum);
                            tempCommand.Parameters.AddWithValue("@fuchNewMotherHIVPos", ChildHealth_NB_MotherHIVPos == "Yes" || ChildHealth_NB_MotherHIVPos == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewChildBreastfed", ChildHealth_NB_ChildBreastfed == "Yes" || ChildHealth_NB_ChildBreastfed == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewHowLong", ChildHealth_NB_BreastfedHowLong);
                            tempCommand.Parameters.AddWithValue("@fuchNewChildEverOnNevirapine", ChildHealth_NB_OnNevirapine == "Yes" || ChildHealth_NB_OnNevirapine == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewReferToClinic2", ChildHealth_NB_RefToClininc2 == "Yes" || ChildHealth_NB_RefToClininc2 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewRefNo2", ChildHealth_NB_RefNum2);
                            tempCommand.Parameters.AddWithValue("@fuchNewHasPCRBeenDone", ChildHealth_NB_PCRDone == "Yes" || ChildHealth_NB_PCRDone == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewReferToClinic3", ChildHealth_NB_RefToClinic3 == "Yes" || ChildHealth_NB_RefToClinic3 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewRefNo3", ChildHealth_NB_RefNum3);
                            tempCommand.Parameters.AddWithValue("@fuchNewImmunisationUpToDate", ChildHealth_NB_ImmuneUpToDate == "Yes" || ChildHealth_NB_ImmuneUpToDate == "1" ? 1 : 0);
                            //tempCommand.Parameters.AddWithValue("@ChildHealth_NB_ImmuneOutstanding", ChildHealth_NB_ImmuneOutstanding);
                            tempCommand.Parameters.AddWithValue("@fuchNewVitAWormMedsGivenEachMonth", ChildHealth_NB_VITAandWormsGiven == "Yes" || ChildHealth_NB_VITAandWormsGiven == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewReferToClinic4", ChildHealth_NB_RefToClinic4 == "Yes" || ChildHealth_NB_RefToClinic4 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchNewRefNo4", ChildHealth_NB_RefNum4);
                            tempCommand.Parameters.AddWithValue("@fuchCDevWalkAppropriatelyForAge", ChildHealth_CD_WalkRight == "Yes" || ChildHealth_CD_WalkRight == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchCDevTalkAppropriateForAge", ChildHealth_CD_TalkRight == "Yes" || ChildHealth_CD_TalkRight == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchCDevReferToClinic", ChildHealth_CD_RefToClinic == "Yes" || ChildHealth_CD_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchCDevRefNo", ChildHealth_CD_RefNum);
                            tempCommand.Parameters.AddWithValue("@fuchSocDevChildAssisted", ChildHealth_SDR_ChildAssisted == "Yes" || ChildHealth_SDR_ChildAssisted == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchSocDevReReferToSD", ChildHealth_SDR_ReRefToSD == "Yes" || ChildHealth_SDR_ReRefToSD == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchSocDevRefNo", ChildHealth_SDR_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fuchCurSocDevReferToClinic", ChildHealth_CSDR_RefToClinic == "Yes" || ChildHealth_CSDR_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchCurSocDevReferToSD", ChildHealth_CSDR_RefToSD == "Yes" || ChildHealth_CSDR_RefToSD == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuchCurSocDevRefNo", ChildHealth_CSDR_RefNum);


                            //tempCommand.Parameters.AddWithValue("@ChildHealth_CSDR_ListOfConcerns", ChildHealth_CSDR_ListOfConcerns);
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
                            SqlCommand tempCommand = new SqlCommand("AddFollowUpOther", tempConnectionOther);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@fuoDateOfVisit", OCDateOfVisit);
                            tempCommand.Parameters.AddWithValue("@fuoHiEHWentToClinic", OC_HiEHRef_WentToClinic == "Yes" || OC_HiEHRef_WentToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuoHiEHReReferToClinic", OC_HiEHRef_ReRefToClinic == "Yes" || OC_HiEHRef_ReRefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuoHiEHRefNo", OC_HiEHRef_ReRefNum);
                            tempCommand.Parameters.AddWithValue("@fuoOCCondition", OC_ConditionThat);
                            tempCommand.Parameters.AddWithValue("@fuoOCReferToClinic", OC_RefToClinic == "Yes" || OC_RefToClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@fuoOCRefNo", OC_RefNum);

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
