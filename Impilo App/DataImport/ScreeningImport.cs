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

                        #region Biographical
                        //Notes:  Did not make provisions for multiple Clinics, Schools or Grades - Client must confirm if it is necessary
                        string BioChowName = GetCellValue (MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(0));
                        string BioUniqueID = GetCellValue (MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(1));
                        //string BioDateOfScreen = GetCellValue (MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(2));
                        string BioDateOfScreen = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(2).DateCellValue.ToString();
                        if (BioDateOfScreen == "0001/01/01 12:00:00 AM")
                            BioDateOfScreen = "";
                        string BioHeadOfHousehold = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(3));
                        string BioName = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(4));
                        string BioSurname = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(5));
                        string BioGPSLat = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(6));
                        string BioGPSLong = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(7));
                        string BioIDNum = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(8));
                        string BioClinicUsed = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(9));
                        string BioDateOfBirth = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(10).DateCellValue.ToString();
                        if (BioDateOfBirth == "0001/01/01 12:00:00 AM")
                            BioDateOfBirth = "";
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
                        string EnHut4Structure = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(17).GetCell(6));
                        string EnHut4TypeRoof = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(17).GetCell(7));
                        string EnHut4Ventilation = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(17).GetCell(8));
                        string EnHut4TotalRooms = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(17).GetCell(9));
                        string EnHut5Structure = GetCellValue (MyWorkbook.GetSheet("Environmental").GetRow(21).GetCell(6));
                        string EnHut5TypeRoof = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(21).GetCell(7));
                        string EnHut5Ventilation = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(21).GetCell(8));
                        string EnHut5TotalRooms = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(21).GetCell(9));
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
                        string EnSourceIncomeHousehold2 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(21));
                        string EnSourceIncomeHousehold3 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(6).GetCell(21));
                        string EnSourceIncomeHousehold4 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(7).GetCell(21));
                        string EnSourceIncomeHousehold5 = GetCellValue(MyWorkbook.GetSheet("Environmental").GetRow(8).GetCell(21));
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
                        string GenCurrentHPTStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(10).DateCellValue.ToString();
                        if (GenCurrentHPTStartDate == "0001/01/01 12:00:00 AM")
                            GenCurrentHPTStartDate = "";
                        string GenCurrentHPTDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(11));
                        string GenCurrentHPTReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(12));
                        string GenCurrentHPTReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(13));
                        string GenCurrentDiabetes = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(15));
                        string GenCurrentDiabetesListMeds1 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(16));
                        string GenCurrentDiabetesListMeds2 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(5).GetCell(16));
                        string GenCurrentDiabetesListMeds3 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(6).GetCell(16));
                        string GenCurrentDiabetesListMeds4 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(7).GetCell(16));
                        string GenCurrentDiabetesListMeds5 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(8).GetCell(16));
                        string GenCurrentDiabetesStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(17).DateCellValue.ToString();
                        if (GenCurrentDiabetesStartDate == "0001/01/01 12:00:00 AM")
                            GenCurrentDiabetesStartDate = "";
                        string GenCurrentDiabetesDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(18));
                        string GenCurrentDiabetesReferClinic = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(19));
                        string GenCurrentDiabetesReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(20));
                        string GenCurrentEpilepsy = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(22));
                        string GenCurrentEpilepsyListMeds1 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(23));
                        string GenCurrentEpilepsyListMeds2 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(5).GetCell(23));
                        string GenCurrentEpilepsyListMeds3 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(6).GetCell(23));
                        string GenCurrentEpilepsyListMeds4 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(7).GetCell(23));
                        string GenCurrentEpilepsyListMeds5 = GetCellValue (MyWorkbook.GetSheet("General").GetRow(8).GetCell(23));
                        string GenCurrentEpilepsyStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(24).DateCellValue.ToString();
                        if (GenCurrentEpilepsyStartDate == "0001/01/01 12:00:00 AM")
                            GenCurrentEpilepsyStartDate = "";
                        string GenCurrentEpilepsyDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(25));
                        string GenCurrentEpilepsyReferClinic = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(26));
                        string GenCurrentEpilepsyReferNo = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(27));
                        string GenCurrentAsthma = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(29));
                        string GenCurrentAsthmaListMeds1 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(30));
                        string GenCurrentAsthmaListMeds2 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(5).GetCell(30));
                        string GenCurrentAsthmaListMeds3 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(6).GetCell(30));
                        string GenCurrentAsthmaListMeds4 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(7).GetCell(30));
                        string GenCurrentAsthmaListMeds5 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(8).GetCell(30));
                        string GenCurrentAsthmaStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(31).DateCellValue.ToString();
                        if (GenCurrentAsthmaStartDate == "0001/01/01 12:00:00 AM")
                            GenCurrentAsthmaStartDate = "";
                        string GenCurrentAsthmaDefaulting = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(32));
                        string GenCurrentAsthmaReferClinic = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(33));
                        string GenCurrentAsthmaReferNo = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(34));
                        string GenCurrentOther = GetCellValue (MyWorkbook.GetSheet("General").GetRow(4).GetCell(36));
                        string GenCurrentOtherListMeds1 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(37));
                        string GenCurrentOtherListMeds2 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(5).GetCell(37));
                        string GenCurrentOtherListMeds3 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(6).GetCell(37));
                        string GenCurrentOtherListMeds4 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(7).GetCell(37));
                        string GenCurrentOtherListMeds5 = GetCellValue(MyWorkbook.GetSheet("General").GetRow(8).GetCell(37));
                        string GenCurrentOtherStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(38).DateCellValue.ToString();
                        if (GenCurrentOtherStartDate == "0001/01/01 12:00:00 AM")
                            GenCurrentOtherStartDate = "";
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
                        string GenPregTestDate = GetCellValue(MyWorkbook.GetSheet("General").GetRow(4).GetCell(70));
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
                        string HypYear = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(1));
                        string HypHeadache = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(3));
                        string HypVision = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(4));
                        string HypShortBreath = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(5));
                        string HypConfusion = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(6));
                        string HypChestPain = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(7));
                        string HypReferClinic = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(8));
                        string HypReferNo = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(9));
                        string HypHadStroke = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(11));
                        string HypHadStrokeYear = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(12));
                        string HypFamilyOnMeds = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(13));
                        string HypFamilyStroke = GetCellValue(MyWorkbook.GetSheet("Hypertension ").GetRow(3).GetCell(14));
                        #endregion

                        #region Diabetes
                        string DYear = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(1));
                        string DThirsty = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(3));
                        string DWeightloss = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(4));
                        string DVision = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(5));
                        string DUrinate = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(6));
                        string DNausea = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(7));
                        string DFoot = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(8));
                        string DReferClinic = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(9));
                        string DReferNo = GetCellValue (MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(10));
                        string DFamilyMember = GetCellValue(MyWorkbook.GetSheet("Diabetes ").GetRow(3).GetCell(12));
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
                        string MHPregnantBefore = GetCellValue(MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(1));
                        string MHNoPregnant = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(2));
                        string MHNOSuccessful = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(3));
                        string MHWhereDeliveredLast = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(4));
                        string MHCaesarian = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(5));
                        string MHBabyUnder25 = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(6));
                        string MHChildrenDied1 = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(8));
                        string MHChildrenDied15 = GetCellValue(MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(9));
                        string MHPAPSmear = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(10));
                        string MHBloodResult = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(11));
                        string MHFirstANCDate = MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(13).DateCellValue.ToString();
                        if (MHFirstANCDate == "0001/01/01 12:00:00 AM")
                            MHFirstANCDate = "";
                        string MHLastANCDate = MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(14).DateCellValue.ToString();
                        if (MHLastANCDate == "0001/01/01 12:00:00 AM")
                            MHLastANCDate = "";
                        string MHReferClinic = GetCellValue(MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(15));
                        string MHReferNo = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(16));
                        string MHNextANCDate = MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(17).DateCellValue.ToString();
                        if (MHNextANCDate == "0001/01/01 12:00:00 AM")
                            MHNextANCDate = "";
                        string MHExpectedDeliverDate = MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(18).DateCellValue.ToString();
                        if (MHExpectedDeliverDate == "0001/01/01 12:00:00 AM")
                            MHExpectedDeliverDate = "";
                        string MHBreastfeed = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(19));
                        string MHFormula = GetCellValue (MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(20));
                        string MHRegisteredMomConnect = GetCellValue(MyWorkbook.GetSheet("Maternal health ").GetRow(3).GetCell(21));
                        #endregion

                        #region Child Health
                        string CHNameMother = GetCellValue(MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(1));
                        string CHChildRTHC = GetCellValue(MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(3));
                        string CHReferClinic1 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(4));
                        string CHReferNo1 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(5));
                        string CHMotherHIV = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(7));
                        string CHChildBreastfeed = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(8));
                        string CHHowLong = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(9));
                        string CHChildOnNevirapine = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(10));
                        string CHPCR = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(11));
                        string CHPCRResult = GetCellValue(MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(12));
                        string CHReferClininc2 = GetCellValue(MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(13));
                        string CHReferNo2 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(14));
                        string CHImmunisationUpToDate = GetCellValue(MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(15));
                        string CHImmunisationOutstanding1 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(16));
                        string CHImmunisationOutstanding2 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(3).GetCell(16));
                        string CHImmunisationOutstanding3 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(4).GetCell(16));
                        string CHImmunisationOutstanding4 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(5).GetCell(16));
                        string CHImmunisationOutstanding5 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(6).GetCell(16));
                        string CHReferClinic3 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(17));
                        string CHReferNo3 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(18));
                        string CHMedsGiven = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(19));
                        string CHWalkAppropriate = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(21));
                        string CHTalkAppropriate = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(22));
                        string CHChildConcerns1 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(24));
                        string CHChildConcerns2 = GetCellValue(MyWorkbook.GetSheet("Child health ").GetRow(3).GetCell(24));
                        string CHChildConcerns3 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(4).GetCell(24));
                        string CHChildConcerns4 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(5).GetCell(24));
                        string CHChildConcerns5 = GetCellValue(MyWorkbook.GetSheet("Child health ").GetRow(6).GetCell(24));
                        string CHReferClinic4 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(25));
                        string CHReferOVC = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(26));
                        string CHReferNo4 = GetCellValue (MyWorkbook.GetSheet("Child health ").GetRow(2).GetCell(27));
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
                        int ClinicID = -1;
                        int EncounterID = -1;
                        int tempID = -1;
                        int tempIDGen = -1;
                        int tempIDHIV = -1;
                        int tempIDChild = -1;

                        #region Find Clinic ID
                        int Errors = 0;
                        SqlConnection tempConnectionFindClinic = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                        try
                        {
                            tempConnectionFindClinic.Open();
                            SqlCommand tempCommand = new SqlCommand("FindClinicID", tempConnectionFindClinic);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ClinicName", BioClinicUsed);

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
                            tempCommand.Parameters.AddWithValue("@HeadOfHousehold", BioHeadOfHousehold);
                            tempCommand.Parameters.AddWithValue("@FirstName", BioName);
                            tempCommand.Parameters.AddWithValue("@LastName", BioSurname);
                            tempCommand.Parameters.AddWithValue("@GPSLatitude", BioGPSLat);
                            tempCommand.Parameters.AddWithValue("@GPSLongitude", BioGPSLong);
                            tempCommand.Parameters.AddWithValue("@IDNo", BioIDNum);
                            tempCommand.Parameters.AddWithValue("@ClinicID", ClinicID);
                            tempCommand.Parameters.AddWithValue("@DateOfBirth", BioDateOfBirth);
                            tempCommand.Parameters.AddWithValue("@Gender", BioMale.ToLower() == "yes" || BioMale == "1" ? "Male" : "Female");
                            tempCommand.Parameters.AddWithValue("@AttendingSchool", BioAttendingSchool == "Yes" || BioAttendingSchool == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@Grade", BioGrade);
                            tempCommand.Parameters.AddWithValue("@NameOfSchool", BioSchoolName);
                            tempCommand.Parameters.AddWithValue("@Area", "");
                                                
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
                            tempCommand.Parameters.AddWithValue("@EncounterDate", BioDateOfScreen);
                            tempCommand.Parameters.AddWithValue("@ClientID", ScreeningID);
                            tempCommand.Parameters.AddWithValue("@EncounterType", 1);
                            tempCommand.Parameters.AddWithValue("@EncounterCapturedBy", BioChowName);

                            EncounterID = (int)((decimal)tempCommand.ExecuteScalar());
                        }
                        catch (Exception ex) { System.Windows.MessageBox.Show(ex.ToString()); }
                        finally
                        {
                            tempConnectionEncounter.Close();
                        }

                        #endregion

                        #region Environmental
                        SqlConnection tempConnectionEnv = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionEnv.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningEnvironmental", tempConnectionEnv);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@seNoPeopleInHousehold", EnNoOfPeople);
                            tempCommand.Parameters.AddWithValue("@seNoLivingAwayFromHousehold", EnNoLiveAway);

                            tempCommand.Parameters.AddWithValue("@seWhenDidYouOrMemberLastVisit", EnFamilyLastVisit0);
                            //tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit1", EnFamilyLastVisit1);
                            //tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit2", EnFamilyLastVisit2);
                            //tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit3", EnFamilyLastVisit3);
                            //tempCommand.Parameters.AddWithValue("@EnFamilyLastVisit4", EnFamilyLastVisit4);
                            tempCommand.Parameters.AddWithValue("@cID", ClinicID);
                            tempCommand.Parameters.AddWithValue("@seTotalNumberSleepingInOneRoomInMainHut", EnNoSleepingInOneRoom);
                            tempCommand.Parameters.AddWithValue("@seTotalNoStructures", EnNoOfStructures);
                            tempCommand.Parameters.AddWithValue("@seRainWaterCollection", EnRainWaterCollection == "Yes" || EnRainWaterCollection == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@seWaterSupply", EnWaterSupply);
                            tempCommand.Parameters.AddWithValue("@seWalkingDistanceFromWaterSupply", EnWalkingDistanceWater);
                            tempCommand.Parameters.AddWithValue("@seTreatWaterBeforeDrinking", EnTreatWater == "Yes" || EnTreatWater == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@seElectricityInAnyHut", EnHutElectricity == "Yes" || EnHutElectricity == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@seWorkingFridge", EnFridge == "Yes" || EnFridge == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@seCookingMethod", EnUseForCooking);
                            tempCommand.Parameters.AddWithValue("@seToiletType", EnTypeToilet);
                            tempCommand.Parameters.AddWithValue("@seWasteDisposalType", EnDisposeWaste);
                            tempCommand.Parameters.AddWithValue("@seFoodParcelInLast6Months", EnFoodParcel == "Yes" || EnFoodParcel == "1" ? 1 : 0);
                                                       
                            // tempCommand.Parameters.AddWithValue("@EnWaterSupply1", EnWaterSupply1);
                            //tempCommand.Parameters.AddWithValue("@EnUseForCooking1", EnUseForCooking1);
                            //tempCommand.Parameters.AddWithValue("@EnUseForCooking2", EnUseForCooking2);
                            //tempCommand.Parameters.AddWithValue("@EnDisposeWaste1", EnDisposeWaste1);
                            tempID = (int)((decimal)tempCommand.ExecuteScalar());
                            
                        }
                        catch { }
                        finally
                        {
                            tempConnectionEnv.Close();
                        }
                        #endregion

                        #region Environmental Huts
                        SqlConnection tempConnectionEnvHut = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionEnvHut.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningEnvironmentHuts", tempConnectionEnvHut);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 huts

                            tempCommand.Parameters.AddWithValue("@seID", tempID);
                            tempCommand.Parameters.AddWithValue("@sehutStructure", EnMainHutStructure);
                            tempCommand.Parameters.AddWithValue("@sehutTypeOfRoof", EnMainHutTypeRoof);
                            tempCommand.Parameters.AddWithValue("@sehutVentilation", EnMainHutVentilation);
                            tempCommand.Parameters.AddWithValue("@sehutNumberOfRooms", EnMainHutTotalRooms);
                            tempCommand.Parameters.AddWithValue("@sehutMain", true);
                            tempCommand.ExecuteScalar();

                            if (EnHut2Structure != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@sehutStructure", EnHut2Structure);
                                tempCommand.Parameters.AddWithValue("@sehutTypeOfRoof", EnHut2TypeRoof);
                                tempCommand.Parameters.AddWithValue("@sehutVentilation", EnHut2Ventilation);
                                tempCommand.Parameters.AddWithValue("@sehutNumberOfRooms", EnHut2TotalRooms);
                                tempCommand.Parameters.AddWithValue("@sehutMain", false);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnHut3Structure != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@sehutStructure", EnHut3Structure);
                                tempCommand.Parameters.AddWithValue("@sehutTypeOfRoof", EnHut3TypeRoof);
                                tempCommand.Parameters.AddWithValue("@sehutVentilation", EnHut3Ventilation);
                                tempCommand.Parameters.AddWithValue("@sehutNumberOfRooms", EnHut3TotalRooms);
                                tempCommand.Parameters.AddWithValue("@sehutMain", false);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnHut4Structure != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@sehutStructure", EnHut4Structure);
                                tempCommand.Parameters.AddWithValue("@sehutTypeOfRoof", EnHut4TypeRoof);
                                tempCommand.Parameters.AddWithValue("@sehutVentilation", EnHut4Ventilation);
                                tempCommand.Parameters.AddWithValue("@sehutNumberOfRooms", EnHut4TotalRooms);
                                tempCommand.Parameters.AddWithValue("@sehutMain", false);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnHut5Structure != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@sehutStructure", EnHut5Structure);
                                tempCommand.Parameters.AddWithValue("@sehutTypeOfRoof", EnHut5TypeRoof);
                                tempCommand.Parameters.AddWithValue("@sehutVentilation", EnHut5Ventilation);
                                tempCommand.Parameters.AddWithValue("@sehutNumberOfRooms", EnHut5TotalRooms);
                                tempCommand.Parameters.AddWithValue("@sehutMain", false);
                                tempCommand.ExecuteScalar();
                            }
                            
                        }
                        catch { }
                        finally
                        {
                            tempConnectionEnvHut.Close();
                        }
                        #endregion

                        #region Environmental Income
                        SqlConnection tempConnectionEnvIncome = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionEnvIncome.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningEnvironmentalIncomeSources", tempConnectionEnvIncome);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Income sources

                            tempCommand.Parameters.AddWithValue("@seID", tempID);
                            tempCommand.Parameters.AddWithValue("@seisName", EnSourceIncomeHousehold);

                            tempCommand.ExecuteScalar();

                            if (EnSourceIncomeHousehold2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@seisName", EnSourceIncomeHousehold2);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnSourceIncomeHousehold3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@seisName", EnSourceIncomeHousehold3);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnSourceIncomeHousehold4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@seisName", EnSourceIncomeHousehold4);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnSourceIncomeHousehold5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@seisName", EnSourceIncomeHousehold5);
                                tempCommand.ExecuteScalar();
                            }

                        }
                        catch { }
                        finally
                        {
                            tempConnectionEnvIncome.Close();
                        }
                        #endregion

                        #region Environmental Living Away
                        SqlConnection tempConnectionEnvLive = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionEnvLive.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningEnvironmentalLivingAway", tempConnectionEnvLive);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Income sources

                            tempCommand.Parameters.AddWithValue("@seID", tempID);
                            tempCommand.Parameters.AddWithValue("@selaName", EnListWhere0);

                            tempCommand.ExecuteScalar();

                            if (EnListWhere1 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@selaName", EnListWhere1);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnListWhere2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@selaName", EnListWhere2);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnListWhere3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@selaName", EnListWhere3);
                                tempCommand.ExecuteScalar();
                            }

                            if (EnListWhere4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@seID", tempID);
                                tempCommand.Parameters.AddWithValue("@selaName", EnListWhere4);
                                tempCommand.ExecuteScalar();
                            }

                        }
                        catch { }
                        finally
                        {
                            tempConnectionEnvLive.Close();
                        }
                        #endregion

                        #region General
                        SqlConnection tempConnectionGen = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionGen.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningGeneral", tempConnectionGen);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@sgWeight", Convert.ToDecimal(GenWeight));
                            tempCommand.Parameters.AddWithValue("@sgHeight", Convert.ToDecimal(GenHeight));
                            tempCommand.Parameters.AddWithValue("@sgBMI", GenBMI == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(GenBMI));
                            tempCommand.Parameters.AddWithValue("@sgOnMeds", GenCurrentOnMeds == "Yes" || GenCurrentOnMeds == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgNotOnMeds", GenCurrentNotOnMeds == "Yes" || GenCurrentNotOnMeds == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgHypertension", GenCurrentHPT == "Yes" || GenCurrentHPT == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgHypertensionStartDate", GenCurrentHPTStartDate);
                            tempCommand.Parameters.AddWithValue("@sgHypertensionDefaulting", GenCurrentHPTDefaulting == "Yes" || GenCurrentHPTDefaulting == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgHypertensionReferToClinic", GenCurrentHPTReferClinic == "Yes" || GenCurrentHPTReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgHypertensionRefNo", GenCurrentHPTReferNo);
                            tempCommand.Parameters.AddWithValue("@sgDiabetes", GenCurrentDiabetes == "Yes" || GenCurrentDiabetes == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgDiabetesStartDate", GenCurrentDiabetesStartDate);
                            tempCommand.Parameters.AddWithValue("@sgDiabetesDefaulting", GenCurrentDiabetesDefaulting == "Yes" || GenCurrentDiabetesDefaulting == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgDiabetesReferToClinic", GenCurrentDiabetesReferClinic == "Yes" || GenCurrentDiabetesReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgDiabetesRefNo", GenCurrentDiabetesReferNo);
                            tempCommand.Parameters.AddWithValue("@sgEpilepsy", GenCurrentEpilepsy == "Yes" || GenCurrentEpilepsy == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgEpilepsyStartDate", GenCurrentEpilepsyStartDate);
                            tempCommand.Parameters.AddWithValue("@sgEpilepsyDefaulting", GenCurrentEpilepsyDefaulting == "Yes" || GenCurrentEpilepsyDefaulting == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgEpilepsyReferToClinic", GenCurrentEpilepsyReferClinic == "Yes" || GenCurrentEpilepsyReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgEpilepsyRefNo", GenCurrentEpilepsyReferNo);
                            tempCommand.Parameters.AddWithValue("@sgAsthma", GenCurrentAsthma == "Yes" || GenCurrentAsthma == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgAsthmaStartDate", GenCurrentAsthmaStartDate);
                            tempCommand.Parameters.AddWithValue("@sgAsthmaDefaulting", GenCurrentAsthmaDefaulting == "Yes" || GenCurrentAsthmaDefaulting == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgAsthmaReferToClinic", GenCurrentAsthmaReferClinic == "Yes" || GenCurrentAsthmaReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgAsthmaRefNo", GenCurrentAsthmaReferNo);
                            tempCommand.Parameters.AddWithValue("@sgOther", GenCurrentOther);
                            tempCommand.Parameters.AddWithValue("@sgOtherStartDate", GenCurrentOtherStartDate);
                            tempCommand.Parameters.AddWithValue("@sgOtherDefaulting", GenCurrentOtherDefaulting == "Yes" || GenCurrentOtherDefaulting == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherReferToClinic", GenCurrentOtherReferClinic == "Yes" || GenCurrentOtherReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherRefNo", GenCurrentOtherReferNo);

                            tempCommand.Parameters.AddWithValue("@sgBPOnMedsSystolic", GenBPOnMedsSystolic == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(GenBPOnMedsSystolic));
                            tempCommand.Parameters.AddWithValue("@sgBPOnMedsDiastolic", GenBPOnMedsDiatolic == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(GenBPOnMedsDiatolic));
                            tempCommand.Parameters.AddWithValue("@sgBPNotOnMedsSystolic", GenBPNotOnMedsSystolic == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(GenBPNotOnMedsSystolic));
                            tempCommand.Parameters.AddWithValue("@sgBPNotOnMedsDiastolic", GenBPNotOnMedsDiatolic == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(GenBPNotOnMedsDiatolic));
                            tempCommand.Parameters.AddWithValue("@sgBPReferToCHOW", GenBPReferCHOW == "Yes" || GenBPReferCHOW == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgBPReferToClinic", GenBPReferClinic == "Yes" || GenBPReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgBPRefNo", GenBPReferNo);

                            tempCommand.Parameters.AddWithValue("@sgBSOnMeds", GenBSOnMeds == "Yes" || GenBSOnMeds == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgBSNotOnMedsBSReading", GenBSNotOnMeds == "" ? Convert.ToDecimal("-1") : Convert.ToDecimal(GenBSNotOnMeds));
                            tempCommand.Parameters.AddWithValue("@sgBSReferToChow", GenBSReferChow == "Yes" || GenBSReferChow == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgBSReferToClinic", GenBSReferClinic == "Yes" || GenBSReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgBSRefNo", GenBSReferNo);

                            tempCommand.Parameters.AddWithValue("@sgEpilepsyFitsInLastMonth", GenEPFitsMonth == "Yes" || GenEPFitsMonth == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgEpilepsyReferToClinic2", GenEPReferClinic == "Yes" || GenEPReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgEpilepsyRefNo2", GenEPReferNo);

                            tempCommand.Parameters.AddWithValue("@sgHIVKnownPosStatus", GenHIVPosStatus == "Yes" || GenHIVPosStatus == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgHIVKnownNegStatus", GenHIVNegStatus == "Yes" || GenHIVNegStatus == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgHIVTestDone", GenHIVTestDone == "Yes" || GenHIVTestDone == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgHIVResult", GenHIVResult);
                            tempCommand.Parameters.AddWithValue("@sgHIVReferToClinic", GenHIVReferClinic == "Yes" || GenHIVReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgHIVRefNo", GenHIVReferNo);

                            tempCommand.Parameters.AddWithValue("@sgPregnancyCurrentlyPregnant", GenPregCurrently == "Yes" || GenPregCurrently == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgPregnancyPossibleThatPregnant", GenPregPossible == "Yes" || GenPregPossible == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgPregnancyTestDone", GenPregTestDate == "Yes" || GenPregTestDate == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgPregnancyResult", GenPregResult);
                            tempCommand.Parameters.AddWithValue("@sgPregnancyReferToClinic", GenPregReferClinic == "Yes" || GenPregReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgPregancyRefNo", GenPregReferNo);

                            tempCommand.Parameters.AddWithValue("@sgTBCurrentlyHaveTB", GenTBCurrentHave == "Yes" || GenTBCurrentHave == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgTBDefaulting", GenTBCurrentDefaulting == "Yes" || GenTBCurrentDefaulting == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgTBRecentUnplannedWeightLoss", GenTBSymtomWeightLoss == "Yes" || GenTBSymtomWeightLoss == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgTBExcessiveSweatingAtNight", GenTBSymtomSweat == "Yes" || GenTBSymtomSweat == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgTBFeverOver2Weeks", GenTBSymtomFeaver == "Yes" || GenTBSymtomFeaver == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgTBCoughMoreThan2Weeks", GenTBSymtomCough == "Yes" || GenTBSymtomCough == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgTBLossOfApetite", GenTBSymtomApetite == "Yes" || GenTBSymtomApetite == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgTBReferToClinic", GenTBSymtomReferClininc == "Yes" || GenTBSymtomReferClininc == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgTBRefNo", GenTBSymtomReferNo);
                            tempCommand.Parameters.AddWithValue("@sgTBContactTracingHouseholdMemberOnMeds", GenTBTraceHousholdOnMeds);
                            tempCommand.Parameters.AddWithValue("@sgTBContactTracingReferToClinic", GenTBTraceReferClininc);
                            tempCommand.Parameters.AddWithValue("@sgTBContactTracingRefNo", GenTBTraceReferNo);
                            tempCommand.Parameters.AddWithValue("@sgTBHouseholdMemberOnTBMeds", null);

                            tempCommand.Parameters.AddWithValue("@sgOtherBloodInUrine", GenOtherBloodUrine == "Yes" || GenOtherBloodUrine == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherReferToClinic2", GenOtherReferClinic1 == "Yes" || GenOtherReferClinic1 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherRefNo2", GenOtherReferNo1);
                            tempCommand.Parameters.AddWithValue("@sgOtherSmoking", GenOtherSmoking == "Yes" || GenOtherSmoking == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherAlcoholUnitsPerWeek", GenOtherAlcohol == "Yes" || GenOtherAlcohol == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherDiarrhoeaOver3Days", GenOtherDiarrhoea == "Yes" || GenOtherDiarrhoea == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherReferToClinic3", GenOtherReferClinic2 == "Yes" || GenOtherReferClinic2 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherRefNo3", GenOtherReferNo2);
                            tempCommand.Parameters.AddWithValue("@sgOtherAttendedInitiationSchool", GenOtherInitiationSchool == "Yes" || GenOtherInitiationSchool == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherLegCrampsOver2Weeks", GenOtherLegCramps == "Yes" || GenOtherLegCramps == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherLegNumbnessOver2Weeks", GenOtherLegNumb == "Yes" || GenOtherLegNumb == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherFootUlcer", GenOtherFootUlcer == "Yes" || GenOtherFootUlcer == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherReferToClinic4", GenOtherReferClinic3 == "Yes" || GenOtherReferClinic3 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgOtherRefNo4", GenOtherReferNo3);

                            tempCommand.Parameters.AddWithValue("@sgElderlyAmputation", GenElderAmputation == "Yes" || GenElderAmputation == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgElderlyPassVisionTest", GenElderVision == "Yes" || GenElderVision == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgElderlyBedridden", GenElderBedridden == "Yes" || GenElderBedridden == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgElderlyUseAidToMove", GenElderMovingAid == "Yes" || GenElderMovingAid == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgElderlyWashYourself", GenElderWash == "Yes" || GenElderWash == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgElderlyFeedYourself", GenElderFeed == "Yes" || GenElderFeed == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgElderlyDressYourself", GenElderDress == "Yes" || GenElderDress == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgElderlyReferToClinic", GenElderReferClinic == "Yes" || GenElderReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sgElderlyRefNo", GenElderReferNo);

                            tempCommand.Parameters.AddWithValue("@sgFamilyPlanningAdviceGiven", GenFamilyPlan == "Yes" || GenFamilyPlan == "1" ? 1 : 0);
                            
                            tempIDGen = (int)(tempCommand.ExecuteScalar());

                        }
                        catch { }
                        finally
                        {
                            tempConnectionGen.Close();
                        }
                        #endregion

                        #region General HPT Meds
                        SqlConnection tempConnectionGenHPTMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionGenHPTMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningGeneralMedicalConditionsMeds", tempConnectionGenHPTMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Meds

                            tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentHPTListMeds1);
                            tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                            tempCommand.Parameters.AddWithValue("@mcID", 1);  //Hypertention

                            tempCommand.ExecuteScalar();

                            if (GenCurrentHPTListMeds2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentHPTListMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 1);  //Hypertention
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentHPTListMeds3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentHPTListMeds3);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 1);  //Hypertention
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentHPTListMeds4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentHPTListMeds4);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 1);  //Hypertention
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentHPTListMeds5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentHPTListMeds5);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 1);  //Hypertention;
                                tempCommand.ExecuteScalar();
                            }

                        }
                        catch { }
                        finally
                        {
                            tempConnectionGenHPTMeds.Close();
                        }
                        #endregion

                        #region General Diabetes Meds
                        SqlConnection tempConnectionGenDiabetesMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionGenDiabetesMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningGeneralMedicalConditionsMeds", tempConnectionGenDiabetesMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Meds

                            tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentDiabetesListMeds1);
                            tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                            tempCommand.Parameters.AddWithValue("@mcID", 2);  //Diabetes

                            tempCommand.ExecuteScalar();

                            if (GenCurrentDiabetesListMeds2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentDiabetesListMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 2);  
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentDiabetesListMeds3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentDiabetesListMeds3);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 2);  
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentDiabetesListMeds4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentDiabetesListMeds4);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 2);  
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentDiabetesListMeds5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentDiabetesListMeds5);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 2);  
                                tempCommand.ExecuteScalar();
                            }

                        }
                        catch { }
                        finally
                        {
                            tempConnectionGenDiabetesMeds.Close();
                        }
                        #endregion

                        #region General Epi Meds
                        SqlConnection tempConnectionGenEpiMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionGenEpiMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningGeneralMedicalConditionsMeds", tempConnectionGenEpiMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Meds

                            tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentEpilepsyListMeds1);
                            tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                            tempCommand.Parameters.AddWithValue("@mcID", 3);  //Epilepsy

                            tempCommand.ExecuteScalar();

                            if (GenCurrentEpilepsyListMeds2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentEpilepsyListMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 3);  
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentEpilepsyListMeds3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentEpilepsyListMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 3);  
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentEpilepsyListMeds4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentEpilepsyListMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 3);  
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentEpilepsyListMeds5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentEpilepsyListMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 3);  
                                tempCommand.ExecuteScalar();
                            }

                        }
                        catch { }
                        finally
                        {
                            tempConnectionGenEpiMeds.Close();
                        }
                        #endregion

                        #region General Asthma Meds
                        SqlConnection tempConnectionGenAsthmaMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionGenAsthmaMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningGeneralMedicalConditionsMeds", tempConnectionGenAsthmaMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Meds

                            tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentAsthmaListMeds1);
                            tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                            tempCommand.Parameters.AddWithValue("@mcID", 4);  //Asthma

                            tempCommand.ExecuteScalar();

                            if (GenCurrentAsthmaListMeds2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentAsthmaListMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 4);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentAsthmaListMeds3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentAsthmaListMeds3);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 4);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentAsthmaListMeds4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentAsthmaListMeds4);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 4);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentAsthmaListMeds5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentAsthmaListMeds5);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 4);
                                tempCommand.ExecuteScalar();
                            }

                        }
                        catch { }
                        finally
                        {
                            tempConnectionGenAsthmaMeds.Close();
                        }
                        #endregion

                        #region General Other Meds
                        SqlConnection tempConnectionGenOtherMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionGenOtherMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningGeneralMedicalConditionsMeds", tempConnectionGenOtherMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Meds

                            tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentOtherListMeds1);
                            tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                            tempCommand.Parameters.AddWithValue("@mcID", 9);  //Other

                            tempCommand.ExecuteScalar();

                            if (GenCurrentOtherListMeds2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentOtherListMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 9);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentOtherListMeds3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentOtherListMeds3);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 9);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentOtherListMeds4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentOtherListMeds4);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 9);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenCurrentOtherListMeds5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenCurrentOtherListMeds5);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 9);
                                tempCommand.ExecuteScalar();
                            }

                        }
                        catch { }
                        finally
                        {
                            tempConnectionGenOtherMeds.Close();
                        }
                        #endregion

                        #region General TB Meds
                        SqlConnection tempConnectionGenTBMeds = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionGenTBMeds.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningGeneralMedicalConditionsMeds", tempConnectionGenTBMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Meds

                            tempCommand.Parameters.AddWithValue("@sgmcmName", GenTBCurrentMeds1);
                            tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                            tempCommand.Parameters.AddWithValue("@mcID", 6);  //TB

                            tempCommand.ExecuteScalar();

                            if (GenTBCurrentMeds2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenTBCurrentMeds2);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 6);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenTBCurrentMeds3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenTBCurrentMeds3);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 6);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenTBCurrentMeds4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenTBCurrentMeds4);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 6);
                                tempCommand.ExecuteScalar();
                            }

                            if (GenTBCurrentMeds5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@sgmcmName", GenTBCurrentMeds5);
                                tempCommand.Parameters.AddWithValue("@sgID", tempIDGen);
                                tempCommand.Parameters.AddWithValue("@mcID", 6);
                                tempCommand.ExecuteScalar();
                            }

                        }
                        catch { }
                        finally
                        {
                            tempConnectionGenTBMeds.Close();
                        }
                        #endregion


                        #region Hypertention
                        SqlConnection tempConnectionHyp = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionHyp.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningHypertension", tempConnectionHyp);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@shYearOfDiagnosis", HypYear);
                            tempCommand.Parameters.AddWithValue("@shShortnessOfBreath", HypShortBreath == "Yes" || HypShortBreath == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@shChestPain", HypChestPain == "Yes" || HypChestPain == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@shReferralToClinic", HypReferClinic == "Yes" || HypReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@shRefNo", HypReferNo);
                            tempCommand.Parameters.AddWithValue("@shEverHadAStroke", HypHadStroke == "Yes" || HypHadStroke == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@shYearOfStroke", HypHadStrokeYear);
                            tempCommand.Parameters.AddWithValue("@shHowManyInFamilyOnMedsForHypertension", HypFamilyOnMeds);
                            tempCommand.Parameters.AddWithValue("@shAnyoneInFamilyHadStroke", HypFamilyStroke == "Yes" || HypFamilyStroke == "1" ? 1 : 0);      

                            //tempCommand.Parameters.AddWithValue("@HypHeadache", HypHeadache);
                            //tempCommand.Parameters.AddWithValue("@HypVision", HypVision);
                            //tempCommand.Parameters.AddWithValue("@HypConfusion", HypConfusion);

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
                            SqlCommand tempCommand = new SqlCommand("AddScreeningDiabetes", tempConnectionDia);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@sdYearOfDiagnosis", DYear);
                            tempCommand.Parameters.AddWithValue("@sdWeightLost", DWeightloss == "Yes" || DWeightloss == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sdUrinatingMore", DUrinate == "Yes" || DUrinate == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sdNauseaOrVomiting", DNausea == "Yes" || DNausea == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sdReferralToClinic", DReferClinic == "Yes" || DReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sdRefNo", DReferNo);
                            tempCommand.Parameters.AddWithValue("@sdFamilyMemberWith", DFamilyMember == "Yes" || DFamilyMember == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sdBlurredVision", DVision == "Yes" || DVision == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@sdFootExamResult", DFoot);
                            //tempCommand.Parameters.AddWithValue("@DThirsty", DThirsty);
                                                        
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
                            SqlCommand tempCommand = new SqlCommand("AddScreeningHIV", tempConnectionHIV);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@shivYearOfDiagnosis", HIVYear);
                            tempCommand.Parameters.AddWithValue("@shivOnMeds", HIVOnMeds == "Yes" || HIVOnMeds == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@shivAdherenceOK", HIVAdherence);
                            tempCommand.Parameters.AddWithValue("@shivReferToClinic", HIVReferClinic == "Yes" || HIVReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@shivRefNo", HIVReferNo);
                            tempCommand.Parameters.AddWithValue("@shivARVFileNo", HIVARVNo);

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
                            SqlCommand tempCommand = new SqlCommand("AddScreeningHIVMeds", tempConnectionHIVMeds);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Meds

                            tempCommand.Parameters.AddWithValue("@shivmName", HIVListMeds1);
                            tempCommand.Parameters.AddWithValue("@shivID", tempIDHIV);
                            
                            tempCommand.ExecuteScalar();

                            if (HIVListMeds2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@shivmName", HIVListMeds2);
                                tempCommand.Parameters.AddWithValue("@shivID", tempIDHIV);
                                tempCommand.ExecuteScalar();
                            }

                            if (HIVListMeds3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@shivmName", HIVListMeds3);
                                tempCommand.Parameters.AddWithValue("@shivID", tempIDHIV);
                                tempCommand.ExecuteScalar();
                            }

                            if (HIVListMeds4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@shivmName", HIVListMeds4);
                                tempCommand.Parameters.AddWithValue("@shivID", tempIDHIV);
                                tempCommand.ExecuteScalar();
                            }

                            if (HIVListMeds5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@shivmName", HIVListMeds5);
                                tempCommand.Parameters.AddWithValue("@shivID", tempIDHIV);
                                tempCommand.ExecuteScalar();
                            }

                            
                        }
                        catch { }
                        finally
                        {
                            tempConnectionHIVMeds.Close();
                        }
                        #endregion

                        #region Maternal Health
                        SqlConnection tempConnectionMat = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionMat.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningMaternalHealth", tempConnectionMat);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@smhPregnantBefore", MHPregnantBefore == "Yes" || MHPregnantBefore == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhNoOfPregnancies", MHNoPregnant);
                            tempCommand.Parameters.AddWithValue("@smhHowManySuccessful", MHNOSuccessful == "Yes" || MHNOSuccessful == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhWhereDeliveredLastBaby", MHWhereDeliveredLast);
                            tempCommand.Parameters.AddWithValue("@smhCaesarian", MHCaesarian == "Yes" || MHCaesarian == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhBabyUnder2KG", MHBabyUnder25 == "Yes" || MHBabyUnder25 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhChildrenDiedUnder1Year", MHChildrenDied1 == "Yes" || MHChildrenDied1 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhChildrenDiedBetween1To5Years", MHChildrenDied15 == "Yes" || MHChildrenDied15 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhPAPSmearInLast5Years", MHPAPSmear);
                            tempCommand.Parameters.AddWithValue("@smhLastBloodTestResult", MHBloodResult);
                            tempCommand.Parameters.AddWithValue("@smhCurrentDateOfFirstANC", MHFirstANCDate);
                            tempCommand.Parameters.AddWithValue("@smhCurrentDateOfLastANC", MHLastANCDate);
                            tempCommand.Parameters.AddWithValue("@smhReferredToClinic", MHReferClinic == "Yes" || MHReferClinic == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhRefNo", MHReferNo);
                            tempCommand.Parameters.AddWithValue("@smhDateOfNextANC", MHNextANCDate);
                            tempCommand.Parameters.AddWithValue("@smhExpectedDeliveryDate", MHExpectedDeliverDate);
                            tempCommand.Parameters.AddWithValue("@smhIntendBreastfeed", MHBreastfeed == "Yes" || MHBreastfeed == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhIntendFormulaFeed", MHFormula == "Yes" || MHFormula == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@smhRegisteredOnMomConnect", MHRegisteredMomConnect == "Yes" || MHRegisteredMomConnect == "1" ? 1 : 0);

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
                            SqlCommand tempCommand = new SqlCommand("AddScreeningChildHealth", tempConnectionChild);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@schNameOfMother", CHNameMother);
                            tempCommand.Parameters.AddWithValue("@schChildWithRTHC", CHChildRTHC == "Yes" || CHChildRTHC == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schReferToClinic", CHReferClinic1 == "Yes" || CHReferClinic1 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schRefNo", CHReferNo1);
                            tempCommand.Parameters.AddWithValue("@schMotherHIVPos", CHMotherHIV == "Yes" || CHMotherHIV == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schChildBreastfed", CHChildBreastfeed == "Yes" || CHChildBreastfeed == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schHowLong", CHHowLong);
                            tempCommand.Parameters.AddWithValue("@schChildEverOnNevirapine", CHChildOnNevirapine == "Yes" || CHChildOnNevirapine == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schPCRDone", CHPCR == "Yes" || CHPCR == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schPCRResult", CHPCRResult);
                            tempCommand.Parameters.AddWithValue("@schReferToClinic2", CHReferClininc2 == "Yes" || CHReferClininc2 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schRefNo2", CHReferNo2);
                            tempCommand.Parameters.AddWithValue("@schImmunisationUpToDate", CHImmunisationUpToDate == "Yes" || CHImmunisationUpToDate == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schReferToClinic3", CHReferClinic3 == "Yes" || CHReferClinic3 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schRefNo3", CHReferNo3);
                            tempCommand.Parameters.AddWithValue("@schVitAAndWormMedsGivenEachMonth", CHMedsGiven == "Yes" || CHMedsGiven == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schWalkAppropriateForAge", CHWalkAppropriate == "Yes" || CHWalkAppropriate == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schTalkAppropriateForAge", CHTalkAppropriate == "Yes" || CHTalkAppropriate == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schReferToClinic4", CHReferClinic4 == "Yes" || CHReferClinic4 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schReferToOVC", CHReferOVC == "Yes" || CHReferOVC == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schRefNo4", CHReferNo4);

                            tempIDChild = (int)((decimal)(tempCommand.ExecuteScalar()));
                        }
                        catch { }
                        finally
                        {
                            tempConnectionChild.Close();
                        }
                        #endregion

                        #region Child Health Immunisations
                        SqlConnection tempConnectionChildImm = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionChildImm.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningChildHealthImmunisationsOutstanding", tempConnectionChildImm);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Immunisations

                            tempCommand.Parameters.AddWithValue("@schioName", CHImmunisationOutstanding1);
                            tempCommand.Parameters.AddWithValue("@schID", tempIDChild);

                            tempCommand.ExecuteScalar();

                            if (CHImmunisationOutstanding2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@schioName", CHImmunisationOutstanding2);
                                tempCommand.Parameters.AddWithValue("@schID", tempIDChild);
                                tempCommand.ExecuteScalar();
                            }

                            if (CHImmunisationOutstanding3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@schioName", CHImmunisationOutstanding3);
                                tempCommand.Parameters.AddWithValue("@schID", tempIDChild);
                                tempCommand.ExecuteScalar();
                            }

                            if (CHImmunisationOutstanding4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@schioName", CHImmunisationOutstanding4);
                                tempCommand.Parameters.AddWithValue("@schID", tempIDChild);
                                tempCommand.ExecuteScalar();
                            }

                            if (CHImmunisationOutstanding5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@schioName", CHImmunisationOutstanding5);
                                tempCommand.Parameters.AddWithValue("@schID", tempIDChild);
                                tempCommand.ExecuteScalar();
                            }


                        }
                        catch { }
                        finally
                        {
                            tempConnectionChildImm.Close();
                        }
                        #endregion

                        #region Child Health Concerns
                        SqlConnection tempConnectionChildCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionChildCon.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningChildHealthConcerns", tempConnectionChildCon);
                            tempCommand.CommandType = CommandType.StoredProcedure;

                            //Provision for 5 Immunisations

                            tempCommand.Parameters.AddWithValue("@schcName", CHChildConcerns1);
                            tempCommand.Parameters.AddWithValue("@schID", tempIDChild);

                            tempCommand.ExecuteScalar();

                            if (CHChildConcerns2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@schcName", CHChildConcerns2);
                                tempCommand.Parameters.AddWithValue("@schID", tempIDChild);
                                tempCommand.ExecuteScalar();
                            }

                            if (CHChildConcerns3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@schcName", CHChildConcerns3);
                                tempCommand.Parameters.AddWithValue("@schID", tempIDChild);
                                tempCommand.ExecuteScalar();
                            }

                            if (CHChildConcerns4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@schcName", CHChildConcerns4);
                                tempCommand.Parameters.AddWithValue("@schID", tempIDChild);
                                tempCommand.ExecuteScalar();
                            }

                            if (CHChildConcerns5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@schcName", CHChildConcerns5);
                                tempCommand.Parameters.AddWithValue("@schID", tempIDChild);
                                tempCommand.ExecuteScalar();
                            }


                        }
                        catch { }
                        finally
                        {
                            tempConnectionChildCon.Close();
                        }
                        #endregion

                        #region Other
                        SqlConnection tempConnectionOther = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionOther.Open();
                            SqlCommand tempCommand = new SqlCommand("AddScreeningOther", tempConnectionOther);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@schOtherConditionFoundThatRequiredReferral", OCondition1);
                            tempCommand.Parameters.AddWithValue("@schReferredToClinic", OReferClinic1 == "Yes" || OReferClinic1 == "1" ? 1 : 0);
                            tempCommand.Parameters.AddWithValue("@schRefNo", OReferNo1);

                            if (OCondition2 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@schOtherConditionFoundThatRequiredReferral", OCondition2);
                                tempCommand.Parameters.AddWithValue("@schReferredToClinic", OReferClinic2 == "Yes" || OReferClinic2 == "1" ? 1 : 0);
                                tempCommand.Parameters.AddWithValue("@schRefNo", OReferNo2);
                            }
                            if (OCondition3 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@schOtherConditionFoundThatRequiredReferral", OCondition3);
                                tempCommand.Parameters.AddWithValue("@schReferredToClinic", OReferClinic3 == "Yes" || OReferClinic3 == "1" ? 1 : 0);
                                tempCommand.Parameters.AddWithValue("@schRefNo", OReferNo3);
                            }
                            if (OCondition4 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@schOtherConditionFoundThatRequiredReferral", OCondition4);
                                tempCommand.Parameters.AddWithValue("@schReferredToClinic", OReferClinic4 == "Yes" || OReferClinic4 == "1" ? 1 : 0);
                                tempCommand.Parameters.AddWithValue("@schRefNo", OReferNo4);
                            }
                            if (OCondition5 != "")
                            {
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@schOtherConditionFoundThatRequiredReferral", OCondition5);
                                tempCommand.Parameters.AddWithValue("@schReferredToClinic", OReferClinic5 == "Yes" || OReferClinic5 == "1" ? 1 : 0);
                                tempCommand.Parameters.AddWithValue("@schRefNo", OReferNo5);
                            }

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
