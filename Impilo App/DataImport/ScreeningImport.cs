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
    class ScreeningImport
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

                        #region Biographical
                        //Notes:  Did not make provisions for multiple Clinics, Schools or Grades - Client must confirm if it is necessary
                        string BioChowName = GetCellValue (MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(0));
                        string BioUniqueID = GetCellValue (MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(1));
                        string BioDateOfScreen = GetCellValue (MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(2));
                        string BioHeadOfHousehold = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(3));
                        string BioName = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(4));
                        string BioSurname = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(5));
                        string BioGPSLat = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(6));
                        string BioGPSLong = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(7));
                        string BioIDNum = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(8));
                        string BioClinicUsed = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(9));
                        string BioDateOfBirth = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(10));
                        string BioMale = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(11));
                        string BioFemale = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(12));
                        string BioAttendingSchool = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(13));
                        string BioSchoolName = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(14));
                        string BioGrade = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(15));
                        #endregion

                        #region Environmental
                        //Notes:  Did not make provisions for multiple Clinics,  - Client must confirm if it is necessary
                        //Notes"  Made provision for 5 huts in total
                        //Notes"  Made provision for 2 water Supplies
                        string EnNoOfPeople = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(1));
                        string EnNoLiveAway = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(2));
                        string EnListWhere0 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(3));
                        string EnListWhere1 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(3));
                        string EnListWhere2 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(6).GetCell(3));
                        string EnListWhere3 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(7).GetCell(3));
                        string EnListWhere4 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(8).GetCell(3));
                        string EnFamilyLastVisit0 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(4));
                        string EnFamilyLastVisit1 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(4));
                        string EnFamilyLastVisit2 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(6).GetCell(4));
                        string EnFamilyLastVisit3 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(7).GetCell(4));
                        string EnFamilyLastVisit4 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(8).GetCell(4));
                        string EnWhichClinic = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(5));
                        string EnMainHutStructure = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(6));
                        string EnMainHutTypeRoof = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(7));
                        string EnMainHutVentilation = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(8));
                        string EnMainHutTotalRooms = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(9));
                        string EnHut2Structure = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(9).GetCell(6));
                        string EnHut2TypeRoof = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(9).GetCell(7));
                        string EnHut2Ventilation = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(9).GetCell(8));
                        string EnHut2TotalRooms = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(9).GetCell(9));
                        string EnHut3Structure = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(13).GetCell(6));
                        string EnHut3TypeRoof = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(13).GetCell(7));
                        string EnHut3Ventilation = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(13).GetCell(8));
                        string EnHut3TotalRooms = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(13).GetCell(9));
                        string EnHut4Structure = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(18).GetCell(6));
                        string EnHut4TypeRoof = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(18).GetCell(7));
                        string EnHut4Ventilation = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(18).GetCell(8));
                        string EnHut4TotalRooms = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(18).GetCell(9));
                        string EnHut5Structure = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(22).GetCell(6));
                        string EnHut5TypeRoof = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(22).GetCell(7));
                        string EnHut5Ventilation = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(22).GetCell(8));
                        string EnHut5TotalRooms = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(22).GetCell(9));
                        string EnNoSleepingInOneRoom = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(10));
                        string EnNoOfStructures = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(11)); 
                        string EnRainWaterCollection = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(12));
                        string EnWaterSupply = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(13));
                        string EnWaterSupply1 = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(13));
                        string EnWalkingDistanceWater = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(14));
                        string EnTreatWater = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(15));
                        string EnHutElectricity = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(16));
                        string EnFridge = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(17));
                        string EnUseForCooking = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(18));
                        string EnUseForCooking1 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(18));
                        string EnUseForCooking2 = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(6).GetCell(18));
                        string EnTypeToilet = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(19));
                        string EnDisposeWaste = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(20));
                        string EnDisposeWaste1 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(20));
                        string EnSourceIncomeHousehold = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(21));
                        string EnFoodParcel = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(22));
                        #endregion

                        #region General
                        string GenWeight = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(1));
                        string GenHeight = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(2));
                        string GenBMI = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(3));
                        string GenCurrentOnMeds = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(5));
                        string GenCurrentNotOnMeds = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(6));
                        string GenCurrentHPT = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(8));
                        string GenCurrentHPTListMeds1 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(9));
                        string GenCurrentHPTListMeds2 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(5).GetCell(9));
                        string GenCurrentHPTListMeds3 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(6).GetCell(9));
                        string GenCurrentHPTListMeds4 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(7).GetCell(9));
                        string GenCurrentHPTListMeds5 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(8).GetCell(9));
                        string GenCurrentHPTStartDate = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(10));
                        string GenCurrentHPTDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(11));
                        string GenCurrentHPTReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(12));
                        string GenCurrentHPTReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(13));
                        string GenCurrentDiabetes = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(15));
                        string GenCurrentDiabetesListMeds1 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(16));
                        string GenCurrentDiabetesListMeds2 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(5).GetCell(16));
                        string GenCurrentDiabetesListMeds3 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(6).GetCell(16));
                        string GenCurrentDiabetesListMeds4 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(7).GetCell(16));
                        string GenCurrentDiabetesListMeds5 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(8).GetCell(16));
                        string GenCurrentDiabetesStartDate = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(17));
                        string GenCurrentDiabetesDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(18));
                        string GenCurrentDiabetesReferClinic = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(19));
                        string GenCurrentDiabetesReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(20));
                        string GenCurrentEpilepsy = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(22));
                        string GenCurrentEpilepsyListMeds1 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(23));
                        string GenCurrentEpilepsyListMeds2 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(5).GetCell(23));
                        string GenCurrentEpilepsyListMeds3 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(6).GetCell(23));
                        string GenCurrentEpilepsyListMeds4 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(7).GetCell(23));
                        string GenCurrentEpilepsyListMeds5 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(8).GetCell(23));
                        string GenCurrentEpilepsyStartDate = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(24));
                        string GenCurrentEpilepsyDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(25));
                        string GenCurrentEpilepsyReferClinic = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(26));
                        string GenCurrentEpilepsyReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(27));
                        string GenCurrentAsthma = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(29));
                        string GenCurrentAsthmaListMeds1 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(30));
                        string GenCurrentAsthmaListMeds2 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(5).GetCell(30));
                        string GenCurrentAsthmaListMeds3 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(6).GetCell(30));
                        string GenCurrentAsthmaListMeds4 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(7).GetCell(30));
                        string GenCurrentAsthmaListMeds5 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(8).GetCell(30));
                        string GenCurrentAsthmaStartDate = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(31));
                        string GenCurrentAsthmaDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(32));
                        string GenCurrentAsthmaReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(33));
                        string GenCurrentAsthmaReferNo = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(34));
                        string GenCurrentOther = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(36));
                        string GenCurrentOtherListMeds1 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(37));
                        string GenCurrentOtherListMeds2 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(5).GetCell(37));
                        string GenCurrentOtherListMeds3 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(6).GetCell(37));
                        string GenCurrentOtherListMeds4 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(7).GetCell(37));
                        string GenCurrentOtherListMeds5 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(8).GetCell(37));
                        string GenCurrentOtherStartDate = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(38));
                        string GenCurrentOtherDefaulting = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(39));
                        string GenCurrentOtherReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(40));
                        string GenCurrentOtherReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(41));
                        
                        string GenBPOnMedsSystolic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(43));
                        string GenBPOnMedsDiatolic = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(44));
                        string GenBPNotOnMedsSystolic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(45));
                        string GenBPNotOnMedsDiatolic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(46));
                        string GenBPReferCHOW = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(47));
                        string GenBPReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(48));
                        string GenBPReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(49));

                        string GenBSOnMeds = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(51));
                        string GenBSNotOnMeds = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(52));
                        string GenBSReferChow = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(53));
                        string GenBSReferClinic = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(54));
                        string GenBSReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(55));

                        string GenEPFitsMonth = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(57));
                        string GenEPReferClinic = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(58));
                        string GenEPReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(59));

                        string GenHIVPosStatus = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(61));
                        string GenHIVNegStatus = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(62));
                        string GenHIVTestDone = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(63));
                        string GenHIVResult = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(64));
                        string GenHIVReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(65));
                        string GenHIVReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(66));

                        string GenPregCurrently = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(68));
                        string GenPregPossible = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(69));
                        string GenPregTestDate = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(70));
                        string GenPregResult = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(71));
                        string GenPregReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(72));
                        string GenPregReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(73));

                        string GenTBCurrentHave = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(75));
                        string GenTBCurrentMeds1 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(76));
                        string GenTBCurrentMeds2 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(5).GetCell(76));
                        string GenTBCurrentMeds3 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(6).GetCell(76));
                        string GenTBCurrentMeds4 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(7).GetCell(76));
                        string GenTBCurrentMeds5 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(8).GetCell(76));
                        string GenTBCurrentDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(77));
                        string GenTBSymtomWeightLoss = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(78));
                        string GenTBSymtomSweat = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(79));
                        string GenTBSymtomFeaver = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(80));
                        string GenTBSymtomCough = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(81));
                        string GenTBSymtomApetite = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(82));
                        string GenTBSymtomReferClininc = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(83));
                        string GenTBSymtomReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(84));
                        string GenTBTraceHousholdOnMeds = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(85));
                        string GenTBTraceReferClininc = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(86));
                        string GenTBTraceReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(87));

                        string GenOtherBloodUrine = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(89));
                        string GenOtherReferClinic1 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(90));
                        string GenOtherReferNo1 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(91));
                        string GenOtherSmoking = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(93));
                        string GenOtherAlcohol = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(94));
                        string GenOtherDiarrhoea = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(96));
                        string GenOtherReferClinic2 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(97));
                        string GenOtherReferNo2 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(98));
                        string GenOtherInitiationSchool = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(100));
                        string GenOtherLegCramps = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(102));
                        string GenOtherLegNumb = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(103));
                        string GenOtherFootUlcer = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(104));
                        string GenOtherReferClinic3 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(105));
                        string GenOtherReferNo3 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(106));

                        string GenElderAmputation = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(108));
                        string GenElderVision = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(109));
                        string GenElderBedridden = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(110));
                        string GenElderMovingAid = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(111));
                        string GenElderWash = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(112));
                        string GenElderFeed = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(113));
                        string GenElderDress = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(114));
                        string GenElderReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(115));
                        string GenElderReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(116));

                        string GenFamilyPlan = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(118));

                        #endregion

                        #region Hypertention
                        string HypYear = GetCellValue(MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(1));
                        string HypHeadache = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(3));
                        string HypVision = GetCellValue(MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(4));
                        string HypShortBreath = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(5));
                        string HypConfusion = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(6));
                        string HypChestPain = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(7));
                        string HypReferClinic = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(8));
                        string HypReferNo = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(9));
                        string HypHadStroke = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(11));
                        string HypHadStrokeYear = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(12));
                        string HypFamilyOnMeds = GetCellValue(MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(13));
                        string HypFamilyStroke = GetCellValue (MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(14));
                        #endregion

                        #region Diabetes
                        string DYear = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(1));
                        string DThirsty = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(3));
                        string DWeightloss = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(4));
                        string DVision = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(5));
                        string DUrinate = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(6));
                        string DNausea = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(7));
                        string DFoot = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(8));
                        string DReferClinic = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(9));
                        string DReferNo = GetCellValue (MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(10));
                        string DFamilyMember = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(12));
                        #endregion
                        
                        #region HIV
                        string HIVYear = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(1));
                        string HIVOnMeds = GetCellValue (MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(2));
                        string HIVListMeds1 = GetCellValue (MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(3));
                        string HIVListMeds2 = GetCellValue (MyWorkbook.GetSheet("HIV").GetRow(3).GetCell(3));
                        string HIVListMeds3 = GetCellValue (MyWorkbook.GetSheet("HIV").GetRow(4).GetCell(3));
                        string HIVListMeds4 = GetCellValue (MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(3));
                        string HIVListMeds5 = GetCellValue (MyWorkbook.GetSheet("HIV").GetRow(6).GetCell(3));
                        string HIVAdherence = GetCellValue (MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(4));
                        string HIVReferClinic = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(5));
                        string HIVReferNo = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(6));
                        string HIVARVNo = GetCellValue (MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(7));
                        #endregion

                        #region Maternal Health
                        string MHPregnantBefore = GetCellValue(MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(1));
                        string MHNoPregnant = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(2));
                        string MHNOSuccessful = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(3));
                        string MHWhereDeliveredLast = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(4));
                        string MHCaesarian = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(5));
                        string MHBabyUnder25 = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(6));
                        string MHChildrenDied1 = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(8));
                        string MHChildrenDied15 = GetCellValue(MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(9));
                        string MHPAPSmear = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(10));
                        string MHBloodResult = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(11));
                        string MHFirstANCDate = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(13));
                        string MHLastANCDate = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(14));
                        string MHReferClinic = GetCellValue(MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(15));
                        string MHReferNo = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(16));
                        string MHNextANCDate = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(17));
                        string MHExpectedDeliverDate = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(18));
                        string MHBreastfeed = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(19));
                        string MHFormula = GetCellValue (MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(20));
                        string MHRegisteredMomConnect = GetCellValue(MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(21));
                        #endregion

                        #region Child Health
                        string CHNameMother = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(1));
                        string CHChildRTHC = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(3));
                        string CHReferClinic1 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(4));
                        string CHReferNo1 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(5));
                        string CHMotherHIV = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(7));
                        string CHChildBreastfeed = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(8));
                        string CHHowLong = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(9));
                        string CHChildOnNevirapine = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(10));
                        string CHPCR = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(11));
                        string CHPCRResult = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(12));
                        string CHReferClininc2 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(13));
                        string CHReferNo2 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(14));
                        string CHImmunisationUpToDate = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(15));
                        string CHImmunisationOutstanding1 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(16));
                        string CHImmunisationOutstanding2 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(3).GetCell(16));
                        string CHImmunisationOutstanding3 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(4).GetCell(16));
                        string CHImmunisationOutstanding4 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(16));
                        string CHImmunisationOutstanding5 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(6).GetCell(16));
                        string CHReferClinic3 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(17));
                        string CHReferNo3 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(18));
                        string CHMedsGiven = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(19));
                        string CHWalkAppropriate = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(21));
                        string CHTalkAppropriate = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(22));
                        string CHChildConcerns1 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(24));
                        string CHChildConcerns2 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(3).GetCell(24));
                        string CHChildConcerns3 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(4).GetCell(24));
                        string CHChildConcerns4 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(24));
                        string CHChildConcerns5 = GetCellValue(MyWorkbook.GetSheet("Child health").GetRow(6).GetCell(24));
                        string CHReferClinic4 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(25));
                        string CHReferOVC = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(26));
                        string CHReferNo4 = GetCellValue (MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(27));
                        #endregion

                        #region Other
                        string OCondition1 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(2).GetCell(1));
                        string OReferClinic1 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(2).GetCell(2));
                        string OReferNo1 = GetCellValue (MyWorkbook.GetSheet("Other").GetRow(2).GetCell(2));
                        string OCondition2 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(3).GetCell(1));
                        string OReferClinic2 = GetCellValue (MyWorkbook.GetSheet("Other").GetRow(3).GetCell(2));
                        string OReferNo2 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(3).GetCell(2));
                        string OCondition3 = GetCellValue (MyWorkbook.GetSheet("Other").GetRow(4).GetCell(1));
                        string OReferClinic3 = GetCellValue (MyWorkbook.GetSheet("Other").GetRow(4).GetCell(2));
                        string OReferNo3 = GetCellValue (MyWorkbook.GetSheet("Other").GetRow(4).GetCell(2));
                        string OCondition4 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(1));
                        string OReferClinic4 = GetCellValue (MyWorkbook.GetSheet("Other").GetRow(5).GetCell(2));
                        string OReferNo4 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(5).GetCell(2));
                        string OCondition5 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(6).GetCell(1));
                        string OReferClinic5 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(6).GetCell(2));
                        string OReferNo5 = GetCellValue(MyWorkbook.GetSheet("Other").GetRow(6).GetCell(2));
                        #endregion


                        // Queries here
                        string ScreeningID = Utilities.GenerateScreeningID(BioName, BioSurname);

                        #region Biographical
                        SqlConnection tempConnectionBio = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionBio.Open();
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertBiographical", tempConnectionBio);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@BioChowName", BioChowName);
                            tempCommand.Parameters.AddWithValue("@BioUniqueID", BioUniqueID);
                            tempCommand.Parameters.AddWithValue("@BioDateOfScreen", BioDateOfScreen);
                            tempCommand.Parameters.AddWithValue("@BioHeadOfHousehold", BioHeadOfHousehold);
                            tempCommand.Parameters.AddWithValue("@BioName", BioName);
                            tempCommand.Parameters.AddWithValue("@BioSurname", BioSurname);
                            tempCommand.Parameters.AddWithValue("@BioGPSLat", BioGPSLat);
                            tempCommand.Parameters.AddWithValue("@BioGPSLong", BioGPSLong);
                            tempCommand.Parameters.AddWithValue("@BioIDNum", BioIDNum);
                            tempCommand.Parameters.AddWithValue("@BioClinicUsed", BioClinicUsed);
                            tempCommand.Parameters.AddWithValue("@BioDateOfBirth", BioDateOfBirth);
                            tempCommand.Parameters.AddWithValue("@BioMale", BioMale);
                            tempCommand.Parameters.AddWithValue("@BioFemale", BioFemale);
                            tempCommand.Parameters.AddWithValue("@BioAttendingSchool", BioAttendingSchool);
                            tempCommand.Parameters.AddWithValue("@BioSchoolName", BioSchoolName);
                            tempCommand.Parameters.AddWithValue("@BioGrade", BioGrade);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionBio.Close();
                        }
                        #endregion

                        #region Environmental
                        SqlConnection tempConnectionEnv = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionEnv.Open();
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertEnvironmental", tempConnectionEnv);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@EnNoOfPeople", EnNoOfPeople);
                            tempCommand.Parameters.AddWithValue("@EnNoLiveAway", EnNoLiveAway);
                            tempCommand.Parameters.AddWithValue("@EnListWhere0", EnListWhere0);
                            tempCommand.Parameters.AddWithValue("@EnListWhere1", EnListWhere1);
                            tempCommand.Parameters.AddWithValue("@EnListWhere2", EnListWhere2);
                            tempCommand.Parameters.AddWithValue("@EnListWhere3", EnListWhere3);
                            tempCommand.Parameters.AddWithValue("@EnListWhere4", EnListWhere4);
                            tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit0", EnFamilyLastVisit0);
                            tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit1", EnFamilyLastVisit1);
                            tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit2", EnFamilyLastVisit2);
                            tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit3", EnFamilyLastVisit3);
                            tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit4", EnFamilyLastVisit4);
                            tempCommand.Parameters.AddWithValue("@EnWhichClinic", EnWhichClinic);
                            tempCommand.Parameters.AddWithValue("@EnMainHutStructure", EnMainHutStructure);
                            tempCommand.Parameters.AddWithValue("@EnMainHutTypeRoof", EnMainHutTypeRoof);
                            tempCommand.Parameters.AddWithValue("@EnMainHutVentilation", EnMainHutVentilation);
                            tempCommand.Parameters.AddWithValue("@EnMainHutTotalRooms", EnMainHutTotalRooms);
                            tempCommand.Parameters.AddWithValue("@EnHut2Structure", EnHut2Structure);
                            tempCommand.Parameters.AddWithValue("@EnHut2TypeRoof", EnHut2TypeRoof);
                            tempCommand.Parameters.AddWithValue("@EnHut2Ventilation", EnHut2Ventilation);
                            tempCommand.Parameters.AddWithValue("@EnHut2TotalRooms", EnHut2TotalRooms);
                            tempCommand.Parameters.AddWithValue("@EnHut3Structure", EnHut3Structure);
                            tempCommand.Parameters.AddWithValue("@EnHut3TypeRoof", EnHut3TypeRoof);
                            tempCommand.Parameters.AddWithValue("@EnHut3Ventilation", EnHut3Ventilation);
                            tempCommand.Parameters.AddWithValue("@EnHut3TotalRooms", EnHut3TotalRooms);
                            tempCommand.Parameters.AddWithValue("@EnHut4Structure", EnHut4Structure);
                            tempCommand.Parameters.AddWithValue("@EnHut4TypeRoof", EnHut4TypeRoof);
                            tempCommand.Parameters.AddWithValue("@EnHut4Ventilation", EnHut4Ventilation);
                            tempCommand.Parameters.AddWithValue("@EnHut4TotalRooms", EnHut4TotalRooms);
                            tempCommand.Parameters.AddWithValue("@EnHut5Structure", EnHut5Structure);
                            tempCommand.Parameters.AddWithValue("@EnHut5TypeRoof", EnHut5TypeRoof);
                            tempCommand.Parameters.AddWithValue("@EnHut5Ventilation", EnHut5Ventilation);
                            tempCommand.Parameters.AddWithValue("@EnHut5TotalRooms", EnHut5TotalRooms);
                            tempCommand.Parameters.AddWithValue("@EnNoSleepingInOneRoom", EnNoSleepingInOneRoom);
                            tempCommand.Parameters.AddWithValue("@EnNoOfStructures", EnNoOfStructures);
                            tempCommand.Parameters.AddWithValue("@EnRainWaterCollection", EnRainWaterCollection);
                            tempCommand.Parameters.AddWithValue("@EnWaterSupply", EnWaterSupply);
                            tempCommand.Parameters.AddWithValue("@EnWaterSupply1", EnWaterSupply1);
                            tempCommand.Parameters.AddWithValue("@EnWalkingDistanceWater", EnWalkingDistanceWater);
                            tempCommand.Parameters.AddWithValue("@EnTreatWater", EnTreatWater);
                            tempCommand.Parameters.AddWithValue("@EnHutElectricity", EnHutElectricity);
                            tempCommand.Parameters.AddWithValue("@EnFridge", EnFridge);
                            tempCommand.Parameters.AddWithValue("@EnUseForCooking", EnUseForCooking);
                            tempCommand.Parameters.AddWithValue("@EnUseForCooking1", EnUseForCooking1);
                            tempCommand.Parameters.AddWithValue("@EnUseForCooking2", EnUseForCooking2);
                            tempCommand.Parameters.AddWithValue("@EnTypeToilet", EnTypeToilet);
                            tempCommand.Parameters.AddWithValue("@EnDisposeWaste", EnDisposeWaste);
                            tempCommand.Parameters.AddWithValue("@EnDisposeWaste1", EnDisposeWaste1);
                            tempCommand.Parameters.AddWithValue("@EnSourceIncomeHousehold", EnSourceIncomeHousehold);
                            tempCommand.Parameters.AddWithValue("@EnFoodParcel", EnFoodParcel);

                            tempCommand.ExecuteScalar();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionEnv.Close();
                        }
                        #endregion

                        #region General
                        SqlConnection tempConnectionGen = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionGen.Open();
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertGeneral", tempConnectionGen);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@GenWeight", GenWeight);
                            tempCommand.Parameters.AddWithValue("@GenHeight", GenHeight);
                            tempCommand.Parameters.AddWithValue("@GenBMI", GenBMI);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOnMeds", GenCurrentOnMeds);
                            tempCommand.Parameters.AddWithValue("@GenCurrentNotOnMeds", GenCurrentNotOnMeds);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPT", GenCurrentHPT);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTListMeds1", GenCurrentHPTListMeds1);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTListMeds2", GenCurrentHPTListMeds2);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTListMeds3", GenCurrentHPTListMeds3);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTListMeds4", GenCurrentHPTListMeds4);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTListMeds5", GenCurrentHPTListMeds5);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTStartDate", GenCurrentHPTStartDate);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTDefaulting", GenCurrentHPTDefaulting);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTReferClinic", GenCurrentHPTReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenCurrentHPTReferNo", GenCurrentHPTReferNo);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetes", GenCurrentDiabetes);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesListMeds1", GenCurrentDiabetesListMeds1);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesListMeds2", GenCurrentDiabetesListMeds2);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesListMeds3", GenCurrentDiabetesListMeds3);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesListMeds4", GenCurrentDiabetesListMeds4);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesListMeds5", GenCurrentDiabetesListMeds5);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesStartDate", GenCurrentDiabetesStartDate);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesDefaulting", GenCurrentDiabetesDefaulting);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesReferClinic", GenCurrentDiabetesReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenCurrentDiabetesReferNo", GenCurrentDiabetesReferNo);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsy", GenCurrentEpilepsy);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyListMeds1", GenCurrentEpilepsyListMeds1);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyListMeds2", GenCurrentEpilepsyListMeds2);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyListMeds3", GenCurrentEpilepsyListMeds3);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyListMeds4", GenCurrentEpilepsyListMeds4);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyListMeds5", GenCurrentEpilepsyListMeds5);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyStartDate", GenCurrentEpilepsyStartDate);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyDefaulting", GenCurrentEpilepsyDefaulting);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyReferClinic", GenCurrentEpilepsyReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenCurrentEpilepsyReferNo", GenCurrentEpilepsyReferNo);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthma", GenCurrentAsthma);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaListMeds1", GenCurrentAsthmaListMeds1);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaListMeds2", GenCurrentAsthmaListMeds2);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaListMeds3", GenCurrentAsthmaListMeds3);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaListMeds4", GenCurrentAsthmaListMeds4);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaListMeds5", GenCurrentAsthmaListMeds5);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaStartDate", GenCurrentAsthmaStartDate);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaDefaulting", GenCurrentAsthmaDefaulting);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaReferClinic", GenCurrentAsthmaReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenCurrentAsthmaReferNo", GenCurrentAsthmaReferNo);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOther", GenCurrentOther);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherListMeds1", GenCurrentOtherListMeds1);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherListMeds2", GenCurrentOtherListMeds2);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherListMeds3", GenCurrentOtherListMeds3);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherListMeds4", GenCurrentOtherListMeds4);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherListMeds5", GenCurrentOtherListMeds5);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherStartDate", GenCurrentOtherStartDate);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherDefaulting", GenCurrentOtherDefaulting);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherReferClinic", GenCurrentOtherReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenCurrentOtherReferNo", GenCurrentOtherReferNo);

                            tempCommand.Parameters.AddWithValue("@GenBPOnMedsSystolic", GenBPOnMedsSystolic);
                            tempCommand.Parameters.AddWithValue("@GenBPOnMedsDiatolic", GenBPOnMedsDiatolic);
                            tempCommand.Parameters.AddWithValue("@GenBPNotOnMedsSystolic", GenBPNotOnMedsSystolic);
                            tempCommand.Parameters.AddWithValue("@GenBPNotOnMedsDiatolic", GenBPNotOnMedsDiatolic);
                            tempCommand.Parameters.AddWithValue("@GenBPReferCHOW", GenBPReferCHOW);
                            tempCommand.Parameters.AddWithValue("@GenBPReferClinic", GenBPReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenBPReferNo", GenBPReferNo);

                            tempCommand.Parameters.AddWithValue("@GenBSOnMeds", GenBSOnMeds);
                            tempCommand.Parameters.AddWithValue("@GenBSNotOnMeds", GenBSNotOnMeds);
                            tempCommand.Parameters.AddWithValue("@GenBSReferChow", GenBSReferChow);
                            tempCommand.Parameters.AddWithValue("@GenBSReferClinic", GenBSReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenBSReferNo", GenBSReferNo);

                            tempCommand.Parameters.AddWithValue("@GenEPFitsMonth", GenEPFitsMonth);
                            tempCommand.Parameters.AddWithValue("@GenEPReferClinic", GenEPReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenEPReferNo", GenEPReferNo);

                            tempCommand.Parameters.AddWithValue("@GenHIVPosStatus", GenHIVPosStatus);
                            tempCommand.Parameters.AddWithValue("@GenHIVNegStatus", GenHIVNegStatus);
                            tempCommand.Parameters.AddWithValue("@GenHIVTestDone", GenHIVTestDone);
                            tempCommand.Parameters.AddWithValue("@GenHIVResult", GenHIVResult);
                            tempCommand.Parameters.AddWithValue("@GenHIVReferClinic", GenHIVReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenHIVReferNo", GenHIVReferNo);

                            tempCommand.Parameters.AddWithValue("@GenPregCurrently", GenPregCurrently);
                            tempCommand.Parameters.AddWithValue("@GenPregPossible", GenPregPossible);
                            tempCommand.Parameters.AddWithValue("@GenPregTestDate", GenPregTestDate);
                            tempCommand.Parameters.AddWithValue("@GenPregResult", GenPregResult);
                            tempCommand.Parameters.AddWithValue("@GenPregReferClinic", GenPregReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenPregReferNo", GenPregReferNo);

                            tempCommand.Parameters.AddWithValue("@GenTBCurrentHave", GenTBCurrentHave);
                            tempCommand.Parameters.AddWithValue("@GenTBCurrentMeds1", GenTBCurrentMeds1);
                            tempCommand.Parameters.AddWithValue("@GenTBCurrentMeds2", GenTBCurrentMeds2);
                            tempCommand.Parameters.AddWithValue("@GenTBCurrentMeds3", GenTBCurrentMeds3);
                            tempCommand.Parameters.AddWithValue("@GenTBCurrentMeds4", GenTBCurrentMeds4);
                            tempCommand.Parameters.AddWithValue("@GenTBCurrentMeds5", GenTBCurrentMeds5);
                            tempCommand.Parameters.AddWithValue("@GenTBCurrentDefaulting", GenTBCurrentDefaulting);
                            tempCommand.Parameters.AddWithValue("@GenTBSymtomWeightLoss", GenTBSymtomWeightLoss);
                            tempCommand.Parameters.AddWithValue("@GenTBSymtomSweat", GenTBSymtomSweat);
                            tempCommand.Parameters.AddWithValue("@GenTBSymtomFeaver", GenTBSymtomFeaver);
                            tempCommand.Parameters.AddWithValue("@GenTBSymtomCough", GenTBSymtomCough);
                            tempCommand.Parameters.AddWithValue("@GenTBSymtomApetite", GenTBSymtomApetite);
                            tempCommand.Parameters.AddWithValue("@GenTBSymtomReferClininc", GenTBSymtomReferClininc);
                            tempCommand.Parameters.AddWithValue("@GenTBSymtomReferNo", GenTBSymtomReferNo);
                            tempCommand.Parameters.AddWithValue("@GenTBTraceHousholdOnMeds", GenTBTraceHousholdOnMeds);
                            tempCommand.Parameters.AddWithValue("@GenTBTraceReferClininc", GenTBTraceReferClininc);
                            tempCommand.Parameters.AddWithValue("@GenTBTraceReferNo", GenTBTraceReferNo);

                            tempCommand.Parameters.AddWithValue("@GenOtherBloodUrine", GenOtherBloodUrine);
                            tempCommand.Parameters.AddWithValue("@GenOtherReferClinic1", GenOtherReferClinic1);
                            tempCommand.Parameters.AddWithValue("@GenOtherReferNo1", GenOtherReferNo1);
                            tempCommand.Parameters.AddWithValue("@GenOtherSmoking", GenOtherSmoking);
                            tempCommand.Parameters.AddWithValue("@GenOtherAlcohol", GenOtherAlcohol);
                            tempCommand.Parameters.AddWithValue("@GenOtherDiarrhoea", GenOtherDiarrhoea);
                            tempCommand.Parameters.AddWithValue("@GenOtherReferClinic2", GenOtherReferClinic2);
                            tempCommand.Parameters.AddWithValue("@GenOtherReferNo2", GenOtherReferNo2);
                            tempCommand.Parameters.AddWithValue("@GenOtherInitiationSchool", GenOtherInitiationSchool);
                            tempCommand.Parameters.AddWithValue("@GenOtherLegCramps", GenOtherLegCramps);
                            tempCommand.Parameters.AddWithValue("@GenOtherLegNumb", GenOtherLegNumb);
                            tempCommand.Parameters.AddWithValue("@GenOtherFootUlcer", GenOtherFootUlcer);
                            tempCommand.Parameters.AddWithValue("@GenOtherReferClinic3", GenOtherReferClinic3);
                            tempCommand.Parameters.AddWithValue("@GenOtherReferNo3", GenOtherReferNo3);

                            tempCommand.Parameters.AddWithValue("@GenElderAmputation", GenElderAmputation);
                            tempCommand.Parameters.AddWithValue("@GenElderVision", GenElderVision);
                            tempCommand.Parameters.AddWithValue("@GenElderBedridden", GenElderBedridden);
                            tempCommand.Parameters.AddWithValue("@GenElderMovingAid", GenElderMovingAid);
                            tempCommand.Parameters.AddWithValue("@GenElderWash", GenElderWash);
                            tempCommand.Parameters.AddWithValue("@GenElderFeed", GenElderFeed);
                            tempCommand.Parameters.AddWithValue("@GenElderDress", GenElderDress);
                            tempCommand.Parameters.AddWithValue("@GenElderReferClinic", GenElderReferClinic);
                            tempCommand.Parameters.AddWithValue("@GenElderReferNo", GenElderReferNo);

                            tempCommand.Parameters.AddWithValue("@GenFamilyPlan", GenFamilyPlan);
                            
                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionGen.Close();
                        }
                        #endregion

                        #region Hypertention
                        SqlConnection tempConnectionHyp = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionHyp.Open();
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertHypertention", tempConnectionHyp);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@HypYear", HypYear);
                            tempCommand.Parameters.AddWithValue("@HypHeadache", HypHeadache);
                            tempCommand.Parameters.AddWithValue("@HypVision", HypVision);
                            tempCommand.Parameters.AddWithValue("@HypShortBreath", HypShortBreath);
                            tempCommand.Parameters.AddWithValue("@HypConfusion", HypConfusion);
                            tempCommand.Parameters.AddWithValue("@HypChestPain", HypChestPain);
                            tempCommand.Parameters.AddWithValue("@HypReferClinic", HypReferClinic);
                            tempCommand.Parameters.AddWithValue("@HypReferNo", HypReferNo);
                            tempCommand.Parameters.AddWithValue("@HypHadStroke", HypHadStroke);
                            tempCommand.Parameters.AddWithValue("@HypHadStrokeYear", HypHadStrokeYear);
                            tempCommand.Parameters.AddWithValue("@HypFamilyOnMeds", HypFamilyOnMeds);
                            tempCommand.Parameters.AddWithValue("@HypFamilyStroke", HypFamilyStroke);        

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHyp.Close();
                        }
                        #endregion

                        #region Diabetes
                        SqlConnection tempConnectionDia = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionDia.Open();
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertDiabetes", tempConnectionDia);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@DYear", DYear);
                            tempCommand.Parameters.AddWithValue("@DThirsty", DThirsty);
                            tempCommand.Parameters.AddWithValue("@DWeightloss", DWeightloss);
                            tempCommand.Parameters.AddWithValue("@DVision", DVision);
                            tempCommand.Parameters.AddWithValue("@DUrinate", DUrinate);
                            tempCommand.Parameters.AddWithValue("@DNausea", DNausea);
                            tempCommand.Parameters.AddWithValue("@DFoot", DFoot);
                            tempCommand.Parameters.AddWithValue("@DReferClinic", DReferClinic);
                            tempCommand.Parameters.AddWithValue("@DReferNo", DReferNo);
                            tempCommand.Parameters.AddWithValue("@DFamilyMember", DFamilyMember);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionDia.Close();
                        }
                        #endregion

                        #region HIV
                        SqlConnection tempConnectionHIV = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionHIV.Open();
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertHIV", tempConnectionHIV);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@HIVYear", HIVYear);
                            tempCommand.Parameters.AddWithValue("@HIVOnMeds", HIVOnMeds);
                            tempCommand.Parameters.AddWithValue("@HIVListMeds1", HIVListMeds1);
                            tempCommand.Parameters.AddWithValue("@HIVListMeds2", HIVListMeds2);
                            tempCommand.Parameters.AddWithValue("@HIVListMeds3", HIVListMeds3);
                            tempCommand.Parameters.AddWithValue("@HIVListMeds4", HIVListMeds4);
                            tempCommand.Parameters.AddWithValue("@HIVListMeds5", HIVListMeds5);
                            tempCommand.Parameters.AddWithValue("@HIVAdherence", HIVAdherence);
                            tempCommand.Parameters.AddWithValue("@HIVReferClinic", HIVReferClinic);
                            tempCommand.Parameters.AddWithValue("@HIVReferNo", HIVReferNo);
                            tempCommand.Parameters.AddWithValue("@HIVARVNo", HIVARVNo);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHIV.Close();
                        }
                        #endregion

                        #region Maternal Health
                        SqlConnection tempConnectionMat = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionMat.Open();
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertMaternalHealth", tempConnectionMat);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@MHPregnantBefore", MHPregnantBefore);
                            tempCommand.Parameters.AddWithValue("@MHNoPregnant", MHNoPregnant);
                            tempCommand.Parameters.AddWithValue("@MHNOSuccessful", MHNOSuccessful);
                            tempCommand.Parameters.AddWithValue("@MHWhereDeliveredLast", MHWhereDeliveredLast);
                            tempCommand.Parameters.AddWithValue("@MHCaesarian", MHCaesarian);
                            tempCommand.Parameters.AddWithValue("@MHBabyUnder25", MHBabyUnder25);
                            tempCommand.Parameters.AddWithValue("@MHChildrenDied1", MHChildrenDied1);
                            tempCommand.Parameters.AddWithValue("@MHChildrenDied15", MHChildrenDied15);
                            tempCommand.Parameters.AddWithValue("@MHPAPSmear", MHPAPSmear);
                            tempCommand.Parameters.AddWithValue("@MHBloodResult", MHBloodResult);
                            tempCommand.Parameters.AddWithValue("@MHFirstANCDate", MHFirstANCDate);
                            tempCommand.Parameters.AddWithValue("@MHLastANCDate", MHLastANCDate);
                            tempCommand.Parameters.AddWithValue("@MHReferClinic", MHReferClinic);
                            tempCommand.Parameters.AddWithValue("@MHReferNo", MHReferNo);
                            tempCommand.Parameters.AddWithValue("@MHNextANCDate", MHNextANCDate);
                            tempCommand.Parameters.AddWithValue("@MHExpectedDeliverDate", MHExpectedDeliverDate);
                            tempCommand.Parameters.AddWithValue("@MHBreastfeed", MHBreastfeed);
                            tempCommand.Parameters.AddWithValue("@MHFormula", MHFormula);
                            tempCommand.Parameters.AddWithValue("@MHRegisteredMomConnect", MHRegisteredMomConnect);

                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnectionMat.Close();
                        }
                        #endregion

                        #region Child Health
                        SqlConnection tempConnectionChild = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionChild.Open();
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertChildHealth", tempConnectionChild);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@CHNameMother", CHNameMother);
                            tempCommand.Parameters.AddWithValue("@CHChildRTHC", CHChildRTHC);
                            tempCommand.Parameters.AddWithValue("@CHReferClinic1", CHReferClinic1);
                            tempCommand.Parameters.AddWithValue("@CHReferNo1", CHReferNo1);
                            tempCommand.Parameters.AddWithValue("@CHMotherHIV", CHMotherHIV);
                            tempCommand.Parameters.AddWithValue("@CHChildBreastfeed", CHChildBreastfeed);
                            tempCommand.Parameters.AddWithValue("@CHHowLong", CHHowLong);
                            tempCommand.Parameters.AddWithValue("@CHChildOnNevirapine", CHChildOnNevirapine);
                            tempCommand.Parameters.AddWithValue("@CHPCR", CHPCR);
                            tempCommand.Parameters.AddWithValue("@CHPCRResult", CHPCRResult);
                            tempCommand.Parameters.AddWithValue("@CHReferClininc2", CHReferClininc2);
                            tempCommand.Parameters.AddWithValue("@CHReferNo2", CHReferNo2);
                            tempCommand.Parameters.AddWithValue("@CHImmunisationUpToDate", CHImmunisationUpToDate);
                            tempCommand.Parameters.AddWithValue("@CHImmunisationOutstanding1", CHImmunisationOutstanding1);
                            tempCommand.Parameters.AddWithValue("@CHImmunisationOutstanding2", CHImmunisationOutstanding2);
                            tempCommand.Parameters.AddWithValue("@CHImmunisationOutstanding3", CHImmunisationOutstanding3);
                            tempCommand.Parameters.AddWithValue("@CHImmunisationOutstanding4", CHImmunisationOutstanding4);
                            tempCommand.Parameters.AddWithValue("@CHImmunisationOutstanding5", CHImmunisationOutstanding5);
                            tempCommand.Parameters.AddWithValue("@CHReferClinic3", CHReferClinic3);
                            tempCommand.Parameters.AddWithValue("@CHReferNo3", CHReferNo3);
                            tempCommand.Parameters.AddWithValue("@CHMedsGiven", CHMedsGiven);
                            tempCommand.Parameters.AddWithValue("@CHWalkAppropriate", CHWalkAppropriate);
                            tempCommand.Parameters.AddWithValue("@CHTalkAppropriate", CHTalkAppropriate);
                            tempCommand.Parameters.AddWithValue("@CHChildConcerns1", CHChildConcerns1);
                            tempCommand.Parameters.AddWithValue("@CHChildConcerns2", CHChildConcerns2);
                            tempCommand.Parameters.AddWithValue("@CHChildConcerns3", CHChildConcerns3);
                            tempCommand.Parameters.AddWithValue("@CHChildConcerns4", CHChildConcerns4);
                            tempCommand.Parameters.AddWithValue("@CHChildConcerns5", CHChildConcerns5);
                            tempCommand.Parameters.AddWithValue("@CHReferClinic4", CHReferClinic4);
                            tempCommand.Parameters.AddWithValue("@CHReferOVC", CHReferOVC);
                            tempCommand.Parameters.AddWithValue("@CHReferNo4", CHReferNo4);                            

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
                            SqlCommand tempCommand = new SqlCommand("ScreeningImportInsertOther", tempConnectionOther);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ScreeningID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@OCondition1", OCondition1);
                            tempCommand.Parameters.AddWithValue("@OReferClinic1", OReferClinic1);
                            tempCommand.Parameters.AddWithValue("@OReferNo1", OReferNo1);
                            tempCommand.Parameters.AddWithValue("@OCondition2", OCondition2);
                            tempCommand.Parameters.AddWithValue("@OReferClinic2", OReferClinic2);
                            tempCommand.Parameters.AddWithValue("@OReferNo2", OReferNo2);
                            tempCommand.Parameters.AddWithValue("@OCondition3", OCondition3);
                            tempCommand.Parameters.AddWithValue("@OReferClinic3", OReferClinic3);
                            tempCommand.Parameters.AddWithValue("@OReferNo3", OReferNo3);
                            tempCommand.Parameters.AddWithValue("@OCondition4", OCondition4);
                            tempCommand.Parameters.AddWithValue("@OReferClinic4", OReferClinic4);
                            tempCommand.Parameters.AddWithValue("@OReferNo4", OReferNo4);
                            tempCommand.Parameters.AddWithValue("@OCondition5", OCondition5);
                            tempCommand.Parameters.AddWithValue("@OReferClinic5", OReferClinic5);
                            tempCommand.Parameters.AddWithValue("@OReferNo5", OReferNo5);

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
                catch(Exception ex)
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
