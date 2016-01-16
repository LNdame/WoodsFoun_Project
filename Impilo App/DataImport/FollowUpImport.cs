using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

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


#region Biographical

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

#region VisitDetails
                        // VD prefix used to identify variables associated with Visit Details tab
                        string VDVisitNum = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(0).StringCellValue;
                        string VDVisitDate = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(1).StringCellValue;
                        string VDNextVisitDate = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(2).StringCellValue;
                        string VDOutcome = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(4).StringCellValue;
                        string VDHPT = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(5).StringCellValue;
                        string VDDiabetes = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(6).StringCellValue;
                        string VDEpilepsy = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(7).StringCellValue;
                        string VDHIV = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(8).StringCellValue;
                        string VDTB = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(9).StringCellValue;
                        string VDMatHealth = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(10).StringCellValue;
                        string VDChildHealth = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(11).StringCellValue;
                        string VDOther = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(12).StringCellValue;
                        string VDODoorToDoor = MyWorkbook.GetSheet("Visit details").GetRow(2).GetCell(13).StringCellValue;
#endregion

#region HyperTension

                        //Hyper prefix used to identify variables associated with Hypertension tab.
                        //HiEHRef = HiEH Referral
                        //ClinicRef = Clinic Referral
                        //AOT = Already On Treatment
                        string HyperDateOfVisit = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(0).StringCellValue;
                        
                        string Hyper_HiEHRef_WentToClinic = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(1).StringCellValue;
                        string Hyper_HiEHRef_ReReferToClinic = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(2).StringCellValue;
                        string Hyper_HiEHRef_ReRefNum = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(3).StringCellValue;
                        string Hyper_HiEHRef_OnMeds = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(4).StringCellValue;
                        string Hyper_HiEHRef_StartDate = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(5).StringCellValue;
                        string Hyper_HiEHRef_BPReading = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(6).StringCellValue;
                        string Hyper_HiEHRef_TodayReadingTop = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(7).StringCellValue;
                        string Hyper_HiEHRef_TodayReadingBottom = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(8).StringCellValue;
                        string Hyper_HiEHRef_ReferToClinic = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(9).StringCellValue;
                        string Hyper_HiEHRef_RefNum = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(10).StringCellValue;
                        
                        string Hyper_ClinicRef_ReReferToClinic = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(12).StringCellValue;
                        string Hyper_ClinicRef_ReRefNum = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(13).StringCellValue;
                        
                        string Hyper_AOT_FollowUpReading = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(15).StringCellValue;
                        
                        string Hyper_DoorToDoorReading_CheckReading = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(17).StringCellValue;

                        string Hyper_Medication = MyWorkbook.GetSheet("Hypertension").GetRow(5).GetCell(18).StringCellValue;
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

#region Diabetes
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
