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
                        string BioChowName = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(0).StringCellValue;
                        string BioUniqueID = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(1).StringCellValue;
                        string BioDateOfScreen = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(2).StringCellValue;
                        string BioHeadOfHousehold = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(3).StringCellValue;
                        string BioName = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(4).StringCellValue;
                        string BioSurname = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(5).StringCellValue;
                        string BioGPSLat = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(6).StringCellValue;
                        string BioGPSLong = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(7).StringCellValue;
                        string BioIDNum = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(8).StringCellValue;
                        string BioClinicUsed = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(9).StringCellValue;
                        string BioDateOfBirth = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(10).StringCellValue;
                        string BioMale = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(11).StringCellValue;
                        string BioFemale = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(12).StringCellValue;
                        string BioAttendingSchool = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(13).StringCellValue;
                        string BioSchoolName = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(14).StringCellValue;
                        string BioGrade = MyWorkbook.GetSheet("Biographical").GetRow(4).GetCell(15).StringCellValue;
                        #endregion

                        #region Environmental
                        //Notes:  Did not make provisions for multiple Clinics,  - Client must confirm if it is necessary
                        //Notes"  Made provision for 5 huts in total
                        //Notes"  Made provision for 2 water Supplies
                        string EnNoOfPeople = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(1).StringCellValue;
                        string EnNoLiveAway = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(2).StringCellValue;
                        string EnListWhere0 = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(3).StringCellValue;
                        string EnListWhere1 = MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(3).StringCellValue;
                        string EnListWhere2 = MyWorkbook.GetSheet("Environmental").GetRow(6).GetCell(3).StringCellValue;
                        string EnListWhere3 = MyWorkbook.GetSheet("Environmental").GetRow(7).GetCell(3).StringCellValue;
                        string EnListWhere4 = MyWorkbook.GetSheet("Environmental").GetRow(8).GetCell(3).StringCellValue;
                        string EnFamilyLastVisit0 = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(4).StringCellValue;
                        string EnFamilyLastVisit1 = MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(4).StringCellValue;
                        string EnFamilyLastVisit2 = MyWorkbook.GetSheet("Environmental").GetRow(6).GetCell(4).StringCellValue;
                        string EnFamilyLastVisit3 = MyWorkbook.GetSheet("Environmental").GetRow(7).GetCell(4).StringCellValue;
                        string EnFamilyLastVisit4 = MyWorkbook.GetSheet("Environmental").GetRow(8).GetCell(4).StringCellValue;
                        string EnWhichClinic = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(5).StringCellValue;
                        string EnMainHutStructure = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(6).StringCellValue;
                        string EnMainHutTypeRoof = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(7).StringCellValue;
                        string EnMainHutVentilation = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(8).StringCellValue;
                        string EnMainHutTotalRooms = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(9).StringCellValue;
                        string EnHut2Structure = MyWorkbook.GetSheet("Environmental").GetRow(9).GetCell(6).StringCellValue;
                        string EnHut2TypeRoof = MyWorkbook.GetSheet("Environmental").GetRow(9).GetCell(7).StringCellValue;
                        string EnHut2Ventilation = MyWorkbook.GetSheet("Environmental").GetRow(9).GetCell(8).StringCellValue;
                        string EnHut2TotalRooms = MyWorkbook.GetSheet("Environmental").GetRow(9).GetCell(9).StringCellValue;
                        string EnHut3Structure = MyWorkbook.GetSheet("Environmental").GetRow(13).GetCell(6).StringCellValue;
                        string EnHut3TypeRoof = MyWorkbook.GetSheet("Environmental").GetRow(13).GetCell(7).StringCellValue;
                        string EnHut3Ventilation = MyWorkbook.GetSheet("Environmental").GetRow(13).GetCell(8).StringCellValue;
                        string EnHut3TotalRooms = MyWorkbook.GetSheet("Environmental").GetRow(13).GetCell(9).StringCellValue;
                        string EnHut4Structure = MyWorkbook.GetSheet("Environmental").GetRow(18).GetCell(6).StringCellValue;
                        string EnHut4TypeRoof = MyWorkbook.GetSheet("Environmental").GetRow(18).GetCell(7).StringCellValue;
                        string EnHut4Ventilation = MyWorkbook.GetSheet("Environmental").GetRow(18).GetCell(8).StringCellValue;
                        string EnHut4TotalRooms = MyWorkbook.GetSheet("Environmental").GetRow(18).GetCell(9).StringCellValue;
                        string EnHut5Structure = MyWorkbook.GetSheet("Environmental").GetRow(22).GetCell(6).StringCellValue;
                        string EnHut5TypeRoof = MyWorkbook.GetSheet("Environmental").GetRow(22).GetCell(7).StringCellValue;
                        string EnHut5Ventilation = MyWorkbook.GetSheet("Environmental").GetRow(22).GetCell(8).StringCellValue;
                        string EnHut5TotalRooms = MyWorkbook.GetSheet("Environmental").GetRow(22).GetCell(9).StringCellValue;
                        string EnNoSleepingInOneRoom = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(10).StringCellValue;
                        string EnNoOfStructures = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(11).StringCellValue;
                        string EnRainWaterCollection = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(12).StringCellValue;
                        string EnWaterSupply = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(13).StringCellValue;
                        string EnWaterSupply1 = MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(13).StringCellValue;
                        string EnWalkingDistanceWater = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(14).StringCellValue;
                        string EnTreatWater = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(15).StringCellValue;
                        string EnHutElectricity = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(16).StringCellValue;
                        string EnFridge = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(17).StringCellValue;
                        string EnUseForCooking = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(18).StringCellValue;
                        string EnUseForCooking1 = MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(18).StringCellValue;
                        string EnUseForCooking2 = MyWorkbook.GetSheet("Environmental").GetRow(6).GetCell(18).StringCellValue;
                        string EnTypeToilet = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(19).StringCellValue;
                        string EnDisposeWaste = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(20).StringCellValue;
                        string EnDisposeWaste1 = MyWorkbook.GetSheet("Environmental").GetRow(5).GetCell(20).StringCellValue;
                        string EnSourceIncomeHousehold = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(21).StringCellValue;
                        string EnFoodParcel = MyWorkbook.GetSheet("Environmental").GetRow(4).GetCell(22).StringCellValue;
                        #endregion

                        #region General
                        string GenWeight = MyWorkbook.GetSheet("General").GetRow(4).GetCell(1).StringCellValue;
                        string GenHeight = MyWorkbook.GetSheet("General").GetRow(4).GetCell(2).StringCellValue;
                        string GenBMI = MyWorkbook.GetSheet("General").GetRow(4).GetCell(3).StringCellValue;
                        string GenCurrentOnMeds = MyWorkbook.GetSheet("General").GetRow(4).GetCell(5).StringCellValue;
                        string GenCurrentNotOnMeds = MyWorkbook.GetSheet("General").GetRow(4).GetCell(6).StringCellValue;
                        string GenCurrentHPT = MyWorkbook.GetSheet("General").GetRow(4).GetCell(8).StringCellValue;
                        string GenCurrentHPTListMeds1 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(9).StringCellValue;
                        string GenCurrentHPTListMeds2 = MyWorkbook.GetSheet("General").GetRow(5).GetCell(9).StringCellValue;
                        string GenCurrentHPTListMeds3 = MyWorkbook.GetSheet("General").GetRow(6).GetCell(9).StringCellValue;
                        string GenCurrentHPTListMeds4 = MyWorkbook.GetSheet("General").GetRow(7).GetCell(9).StringCellValue;
                        string GenCurrentHPTListMeds5 = MyWorkbook.GetSheet("General").GetRow(8).GetCell(9).StringCellValue;
                        string GenCurrentHPTStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(10).StringCellValue;
                        string GenCurrentHPTDefaulting = MyWorkbook.GetSheet("General").GetRow(4).GetCell(11).StringCellValue;
                        string GenCurrentHPTReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(12).StringCellValue;
                        string GenCurrentHPTReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(13).StringCellValue;
                        string GenCurrentDiabetes = MyWorkbook.GetSheet("General").GetRow(4).GetCell(15).StringCellValue;
                        string GenCurrentDiabetesListMeds1 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(16).StringCellValue;
                        string GenCurrentDiabetesListMeds2 = MyWorkbook.GetSheet("General").GetRow(5).GetCell(16).StringCellValue;
                        string GenCurrentDiabetesListMeds3 = MyWorkbook.GetSheet("General").GetRow(6).GetCell(16).StringCellValue;
                        string GenCurrentDiabetesListMeds4 = MyWorkbook.GetSheet("General").GetRow(7).GetCell(16).StringCellValue;
                        string GenCurrentDiabetesListMeds5 = MyWorkbook.GetSheet("General").GetRow(8).GetCell(16).StringCellValue;
                        string GenCurrentDiabetesStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(17).StringCellValue;
                        string GenCurrentDiabetesDefaulting = MyWorkbook.GetSheet("General").GetRow(4).GetCell(18).StringCellValue;
                        string GenCurrentDiabetesReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(19).StringCellValue;
                        string GenCurrentDiabetesReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(20).StringCellValue;
                        string GenCurrentEpilepsy = MyWorkbook.GetSheet("General").GetRow(4).GetCell(22).StringCellValue;
                        string GenCurrentEpilepsyListMeds1 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(23).StringCellValue;
                        string GenCurrentEpilepsyListMeds2 = MyWorkbook.GetSheet("General").GetRow(5).GetCell(23).StringCellValue;
                        string GenCurrentEpilepsyListMeds3 = MyWorkbook.GetSheet("General").GetRow(6).GetCell(23).StringCellValue;
                        string GenCurrentEpilepsyListMeds4 = MyWorkbook.GetSheet("General").GetRow(7).GetCell(23).StringCellValue;
                        string GenCurrentEpilepsyListMeds5 = MyWorkbook.GetSheet("General").GetRow(8).GetCell(23).StringCellValue;
                        string GenCurrentEpilepsyStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(24).StringCellValue;
                        string GenCurrentEpilepsyDefaulting = MyWorkbook.GetSheet("General").GetRow(4).GetCell(25).StringCellValue;
                        string GenCurrentEpilepsyReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(26).StringCellValue;
                        string GenCurrentEpilepsyReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(27).StringCellValue;
                        string GenCurrentAsthma = MyWorkbook.GetSheet("General").GetRow(4).GetCell(29).StringCellValue;
                        string GenCurrentAsthmaListMeds1 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(30).StringCellValue;
                        string GenCurrentAsthmaListMeds2 = MyWorkbook.GetSheet("General").GetRow(5).GetCell(30).StringCellValue;
                        string GenCurrentAsthmaListMeds3 = MyWorkbook.GetSheet("General").GetRow(6).GetCell(30).StringCellValue;
                        string GenCurrentAsthmaListMeds4 = MyWorkbook.GetSheet("General").GetRow(7).GetCell(30).StringCellValue;
                        string GenCurrentAsthmaListMeds5 = MyWorkbook.GetSheet("General").GetRow(8).GetCell(30).StringCellValue;
                        string GenCurrentAsthmaStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(31).StringCellValue;
                        string GenCurrentAsthmaDefaulting = MyWorkbook.GetSheet("General").GetRow(4).GetCell(32).StringCellValue;
                        string GenCurrentAsthmaReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(33).StringCellValue;
                        string GenCurrentAsthmaReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(34).StringCellValue;
                        string GenCurrentOther = MyWorkbook.GetSheet("General").GetRow(4).GetCell(36).StringCellValue;
                        string GenCurrentOtherListMeds1 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(37).StringCellValue;
                        string GenCurrentOtherListMeds2 = MyWorkbook.GetSheet("General").GetRow(5).GetCell(37).StringCellValue;
                        string GenCurrentOtherListMeds3 = MyWorkbook.GetSheet("General").GetRow(6).GetCell(37).StringCellValue;
                        string GenCurrentOtherListMeds4 = MyWorkbook.GetSheet("General").GetRow(7).GetCell(37).StringCellValue;
                        string GenCurrentOtherListMeds5 = MyWorkbook.GetSheet("General").GetRow(8).GetCell(37).StringCellValue;
                        string GenCurrentOtherStartDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(38).StringCellValue;
                        string GenCurrentOtherDefaulting = MyWorkbook.GetSheet("General").GetRow(4).GetCell(39).StringCellValue;
                        string GenCurrentOtherReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(40).StringCellValue;
                        string GenCurrentOtherReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(41).StringCellValue;
                        
                        string GenBPOnMedsSystolic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(43).StringCellValue;
                        string GenBPOnMedsDiatolic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(44).StringCellValue;
                        string GenBPNotOnMedsSystolic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(45).StringCellValue;
                        string GenBPNotOnMedsDiatolic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(46).StringCellValue;
                        string GenBPReferCHOW = MyWorkbook.GetSheet("General").GetRow(4).GetCell(47).StringCellValue;
                        string GenBPReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(48).StringCellValue;
                        string GenBPReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(49).StringCellValue;

                        string GenBSOnMeds = MyWorkbook.GetSheet("General").GetRow(4).GetCell(51).StringCellValue;
                        string GenBSNotOnMeds = MyWorkbook.GetSheet("General").GetRow(4).GetCell(52).StringCellValue;
                        string GenBSReferChow = MyWorkbook.GetSheet("General").GetRow(4).GetCell(53).StringCellValue;
                        string GenBSReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(54).StringCellValue;
                        string GenBSReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(55).StringCellValue;

                        string GenEPFitsMonth = MyWorkbook.GetSheet("General").GetRow(4).GetCell(57).StringCellValue;
                        string GenEPReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(58).StringCellValue;
                        string GenEPReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(59).StringCellValue;

                        string GenHIVPosStatus = MyWorkbook.GetSheet("General").GetRow(4).GetCell(61).StringCellValue;
                        string GenHIVNegStatus = MyWorkbook.GetSheet("General").GetRow(4).GetCell(62).StringCellValue;
                        string GenHIVTestDone = MyWorkbook.GetSheet("General").GetRow(4).GetCell(63).StringCellValue;
                        string GenHIVResult = MyWorkbook.GetSheet("General").GetRow(4).GetCell(64).StringCellValue;
                        string GenHIVReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(65).StringCellValue;
                        string GenHIVReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(66).StringCellValue;

                        string GenPregCurrently = MyWorkbook.GetSheet("General").GetRow(4).GetCell(68).StringCellValue;
                        string GenPregPossible = MyWorkbook.GetSheet("General").GetRow(4).GetCell(69).StringCellValue;
                        string GenPregTestDate = MyWorkbook.GetSheet("General").GetRow(4).GetCell(70).StringCellValue;
                        string GenPregResult = MyWorkbook.GetSheet("General").GetRow(4).GetCell(71).StringCellValue;
                        string GenPregReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(72).StringCellValue;
                        string GenPregReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(73).StringCellValue;

                        string GenTBCurrentHave = MyWorkbook.GetSheet("General").GetRow(4).GetCell(75).StringCellValue;
                        string GenTBCurrentMeds1 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(76).StringCellValue;
                        string GenTBCurrentMeds2 = MyWorkbook.GetSheet("General").GetRow(5).GetCell(76).StringCellValue;
                        string GenTBCurrentMeds3 = MyWorkbook.GetSheet("General").GetRow(6).GetCell(76).StringCellValue;
                        string GenTBCurrentMeds4 = MyWorkbook.GetSheet("General").GetRow(7).GetCell(76).StringCellValue;
                        string GenTBCurrentMeds5 = MyWorkbook.GetSheet("General").GetRow(8).GetCell(76).StringCellValue;
                        string GenTBCurrentDefaulting = MyWorkbook.GetSheet("General").GetRow(4).GetCell(77).StringCellValue;
                        string GenTBSymtomWeightLoss = MyWorkbook.GetSheet("General").GetRow(4).GetCell(78).StringCellValue;
                        string GenTBSymtomSweat = MyWorkbook.GetSheet("General").GetRow(4).GetCell(79).StringCellValue;
                        string GenTBSymtomFeaver = MyWorkbook.GetSheet("General").GetRow(4).GetCell(80).StringCellValue;
                        string GenTBSymtomCough = MyWorkbook.GetSheet("General").GetRow(4).GetCell(81).StringCellValue;
                        string GenTBSymtomApetite = MyWorkbook.GetSheet("General").GetRow(4).GetCell(82).StringCellValue;
                        string GenTBSymtomReferClininc = MyWorkbook.GetSheet("General").GetRow(4).GetCell(83).StringCellValue;
                        string GenTBSymtomReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(84).StringCellValue;
                        string GenTBTraceHousholdOnMeds = MyWorkbook.GetSheet("General").GetRow(4).GetCell(85).StringCellValue;
                        string GenTBTraceReferClininc = MyWorkbook.GetSheet("General").GetRow(4).GetCell(86).StringCellValue;
                        string GenTBTraceReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(87).StringCellValue;

                        string GenOtherBloodUrine = MyWorkbook.GetSheet("General").GetRow(4).GetCell(89).StringCellValue;
                        string GenOtherReferClinic1 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(90).StringCellValue;
                        string GenOtherReferNo1 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(91).StringCellValue;
                        string GenOtherSmoking = MyWorkbook.GetSheet("General").GetRow(4).GetCell(93).StringCellValue;
                        string GenOtherAlcohol = MyWorkbook.GetSheet("General").GetRow(4).GetCell(94).StringCellValue;
                        string GenOtherDiarrhoea = MyWorkbook.GetSheet("General").GetRow(4).GetCell(96).StringCellValue;
                        string GenOtherReferClinic2 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(97).StringCellValue;
                        string GenOtherReferNo2 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(98).StringCellValue;
                        string GenOtherInitiationSchool = MyWorkbook.GetSheet("General").GetRow(4).GetCell(100).StringCellValue;
                        string GenOtherLegCramps = MyWorkbook.GetSheet("General").GetRow(4).GetCell(102).StringCellValue;
                        string GenOtherLegNumb = MyWorkbook.GetSheet("General").GetRow(4).GetCell(103).StringCellValue;
                        string GenOtherFootUlcer = MyWorkbook.GetSheet("General").GetRow(4).GetCell(104).StringCellValue;
                        string GenOtherReferClinic3 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(105).StringCellValue;
                        string GenOtherReferNo3 = MyWorkbook.GetSheet("General").GetRow(4).GetCell(106).StringCellValue;

                        string GenElderAmputation = MyWorkbook.GetSheet("General").GetRow(4).GetCell(108).StringCellValue;
                        string GenElderVision = MyWorkbook.GetSheet("General").GetRow(4).GetCell(109).StringCellValue;
                        string GenElderBedridden = MyWorkbook.GetSheet("General").GetRow(4).GetCell(110).StringCellValue;
                        string GenElderMovingAid = MyWorkbook.GetSheet("General").GetRow(4).GetCell(111).StringCellValue;
                        string GenElderWash = MyWorkbook.GetSheet("General").GetRow(4).GetCell(112).StringCellValue;
                        string GenElderFeed = MyWorkbook.GetSheet("General").GetRow(4).GetCell(113).StringCellValue;
                        string GenElderDress = MyWorkbook.GetSheet("General").GetRow(4).GetCell(114).StringCellValue;
                        string GenElderReferClinic = MyWorkbook.GetSheet("General").GetRow(4).GetCell(115).StringCellValue;
                        string GenElderReferNo = MyWorkbook.GetSheet("General").GetRow(4).GetCell(116).StringCellValue;

                        string GenFamilyPlan = MyWorkbook.GetSheet("General").GetRow(4).GetCell(118).StringCellValue;

                        #endregion

                        #region Hypertention
                        string HypYear = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(1).StringCellValue;
                        string HypHeadache = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(3).StringCellValue;
                        string HypVision = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(4).StringCellValue;
                        string HypShortBreath = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(5).StringCellValue;
                        string HypConfusion = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(6).StringCellValue;
                        string HypChestPain = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(7).StringCellValue;
                        string HypReferClinic = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(8).StringCellValue;
                        string HypReferNo = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(9).StringCellValue;
                        string HypHadStroke = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(11).StringCellValue;
                        string HypHadStrokeYear = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(12).StringCellValue;
                        string HypFamilyOnMeds = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(13).StringCellValue;
                        string HypFamilyStroke = MyWorkbook.GetSheet("Hypertention").GetRow(3).GetCell(14).StringCellValue;
                        #endregion

                        #region Diabetes
                        string DYear = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(1).StringCellValue;
                        string DThirsty = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(3).StringCellValue;
                        string DWeightloss = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(4).StringCellValue;
                        string DVision = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(5).StringCellValue;
                        string DUrinate = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(6).StringCellValue;
                        string DNausea = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(7).StringCellValue;
                        string DFoot = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(8).StringCellValue;
                        string DReferClinic = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(9).StringCellValue;
                        string DReferNo = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(10).StringCellValue;
                        string DFamilyMember = MyWorkbook.GetSheet("Diabetes").GetRow(3).GetCell(12).StringCellValue;
                        #endregion
                        
                        #region HIV
                        string HIVYear = MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(1).StringCellValue;
                        string HIVOnMeds = MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(2).StringCellValue;
                        string HIVListMeds1 = MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(3).StringCellValue;
                        string HIVListMeds2 = MyWorkbook.GetSheet("HIV").GetRow(3).GetCell(3).StringCellValue;
                        string HIVListMeds3 = MyWorkbook.GetSheet("HIV").GetRow(4).GetCell(3).StringCellValue;
                        string HIVListMeds4 = MyWorkbook.GetSheet("HIV").GetRow(5).GetCell(3).StringCellValue;
                        string HIVListMeds5 = MyWorkbook.GetSheet("HIV").GetRow(6).GetCell(3).StringCellValue;
                        string HIVAdherence = MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(4).StringCellValue;
                        string HIVReferClinic = MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(5).StringCellValue;
                        string HIVReferNo = MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(6).StringCellValue;
                        string HIVARVNo = MyWorkbook.GetSheet("HIV").GetRow(2).GetCell(7).StringCellValue;
                        #endregion

                        #region Maternal Health
                        string MHPregnantBefore = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(1).StringCellValue;
                        string MHNoPregnant = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(2).StringCellValue;
                        string MHNOSuccessful = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(3).StringCellValue;
                        string MHWhereDeliveredLast = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(4).StringCellValue;
                        string MHCaesarian = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(5).StringCellValue;
                        string MHBabyUnder25 = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(6).StringCellValue;
                        string MHChildrenDied1 = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(8).StringCellValue;
                        string MHChildrenDied15 = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(9).StringCellValue;
                        string MHPAPSmear = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(10).StringCellValue;
                        string MHBloodResult = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(11).StringCellValue;
                        string MHFirstANCDate = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(13).StringCellValue;
                        string MHLastANCDate = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(14).StringCellValue;
                        string MHReferClinic = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(15).StringCellValue;
                        string MHReferNo = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(16).StringCellValue;
                        string MHNextANCDate = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(17).StringCellValue;
                        string MHExpectedDeliverDate = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(18).StringCellValue;
                        string MHBreastfeed = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(19).StringCellValue;
                        string MHFormula = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(20).StringCellValue;
                        string MHRegisteredMomConnect = MyWorkbook.GetSheet("Maternal health").GetRow(3).GetCell(21).StringCellValue;
                        #endregion

                        #region Child Health
                        string CHNameMother = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(1).StringCellValue;
                        string CHChildRTHC = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(3).StringCellValue;
                        string CHReferClinic1 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(4).StringCellValue;
                        string CHReferNo1 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(5).StringCellValue;
                        string CHMotherHIV = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(7).StringCellValue;
                        string CHChildBreastfeed = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(8).StringCellValue;
                        string CHHowLong = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(9).StringCellValue;
                        string CHChildOnNevirapine = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(10).StringCellValue;
                        string CHPCR = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(11).StringCellValue;
                        string CHPCRResult = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(12).StringCellValue;
                        string CHReferClininc2 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(13).StringCellValue;
                        string CHReferNo2 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(14).StringCellValue;
                        string CHImmunisationUpToDate = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(15).StringCellValue;
                        string CHImmunisationOutstanding1 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(16).StringCellValue;
                        string CHImmunisationOutstanding2 = MyWorkbook.GetSheet("Child health").GetRow(3).GetCell(16).StringCellValue;
                        string CHImmunisationOutstanding3 = MyWorkbook.GetSheet("Child health").GetRow(4).GetCell(16).StringCellValue;
                        string CHImmunisationOutstanding4 = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(16).StringCellValue;
                        string CHImmunisationOutstanding5 = MyWorkbook.GetSheet("Child health").GetRow(6).GetCell(16).StringCellValue;
                        string CHReferClinic3 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(17).StringCellValue;
                        string CHReferNo3 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(18).StringCellValue;
                        string CHMedsGiven = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(19).StringCellValue;
                        string CHWalkAppropriate = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(21).StringCellValue;
                        string CHTalkAppropriate = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(22).StringCellValue;
                        string CHChildConcerns1 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(24).StringCellValue;
                        string CHChildConcerns2 = MyWorkbook.GetSheet("Child health").GetRow(3).GetCell(24).StringCellValue;
                        string CHChildConcerns3 = MyWorkbook.GetSheet("Child health").GetRow(4).GetCell(24).StringCellValue;
                        string CHChildConcerns4 = MyWorkbook.GetSheet("Child health").GetRow(5).GetCell(24).StringCellValue;
                        string CHChildConcerns5 = MyWorkbook.GetSheet("Child health").GetRow(6).GetCell(24).StringCellValue;
                        string CHReferClinic4 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(25).StringCellValue;
                        string CHReferOVC = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(26).StringCellValue;
                        string CHReferNo4 = MyWorkbook.GetSheet("Child health").GetRow(2).GetCell(27).StringCellValue;
                        #endregion

                        #region Other
                        string OCondition1 = MyWorkbook.GetSheet("Other").GetRow(2).GetCell(1).StringCellValue;
                        string OReferClinic1 = MyWorkbook.GetSheet("Other").GetRow(2).GetCell(2).StringCellValue;
                        string OReferNo1 = MyWorkbook.GetSheet("Other").GetRow(2).GetCell(2).StringCellValue;
                        string OCondition2 = MyWorkbook.GetSheet("Other").GetRow(3).GetCell(1).StringCellValue;
                        string OReferClinic2 = MyWorkbook.GetSheet("Other").GetRow(3).GetCell(2).StringCellValue;
                        string OReferNo2 = MyWorkbook.GetSheet("Other").GetRow(3).GetCell(2).StringCellValue;
                        string OCondition3 = MyWorkbook.GetSheet("Other").GetRow(4).GetCell(1).StringCellValue;
                        string OReferClinic3 = MyWorkbook.GetSheet("Other").GetRow(4).GetCell(2).StringCellValue;
                        string OReferNo3 = MyWorkbook.GetSheet("Other").GetRow(4).GetCell(2).StringCellValue;
                        string OCondition4 = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(1).StringCellValue;
                        string OReferClinic4 = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(2).StringCellValue;
                        string OReferNo4 = MyWorkbook.GetSheet("Other").GetRow(5).GetCell(2).StringCellValue;
                        string OCondition5 = MyWorkbook.GetSheet("Other").GetRow(6).GetCell(1).StringCellValue;
                        string OReferClinic5 = MyWorkbook.GetSheet("Other").GetRow(6).GetCell(2).StringCellValue;
                        string OReferNo5 = MyWorkbook.GetSheet("Other").GetRow(6).GetCell(2).StringCellValue;
                        #endregion


                        // Queries here
                        string ScreeningID = Utilities.GenerateScreeningID(BioName, BioSurname);

                        #region Biographical
                        SqlConnection tempConnectionBio = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        try
                        {
                            tempConnectionBio.Open();
                            SqlCommand tempCommand = new SqlCommand("InsertBiographical", tempConnectionBio);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", BioChowName);
                            tempCommand.Parameters.AddWithValue("@", BioUniqueID);
                            tempCommand.Parameters.AddWithValue("@", BioDateOfScreen);
                            tempCommand.Parameters.AddWithValue("@", BioHeadOfHousehold);
                            tempCommand.Parameters.AddWithValue("@", BioName);
                            tempCommand.Parameters.AddWithValue("@", BioSurname);
                            tempCommand.Parameters.AddWithValue("@", BioGPSLat);
                            tempCommand.Parameters.AddWithValue("@", BioGPSLong);
                            tempCommand.Parameters.AddWithValue("@", BioIDNum);
                            tempCommand.Parameters.AddWithValue("@", BioClinicUsed);
                            tempCommand.Parameters.AddWithValue("@", BioDateOfBirth);
                            tempCommand.Parameters.AddWithValue("@", BioMale);
                            tempCommand.Parameters.AddWithValue("@", BioFemale);
                            tempCommand.Parameters.AddWithValue("@", BioAttendingSchool);
                            tempCommand.Parameters.AddWithValue("@", BioSchoolName);
                            tempCommand.Parameters.AddWithValue("@", BioGrade);

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
                            SqlCommand tempCommand = new SqlCommand("InsertEnvironmental", tempConnectionEnv);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", EnNoOfPeople);
                            tempCommand.Parameters.AddWithValue("@", EnNoLiveAway);
                            tempCommand.Parameters.AddWithValue("@", EnListWhere0);
                            tempCommand.Parameters.AddWithValue("@", EnListWhere1);
                            tempCommand.Parameters.AddWithValue("@", EnListWhere2);
                            tempCommand.Parameters.AddWithValue("@", EnListWhere3);
                            tempCommand.Parameters.AddWithValue("@", EnListWhere4);
                            tempCommand.Parameters.AddWithValue("@", EnFamilyLastVisit0);
                            tempCommand.Parameters.AddWithValue("@", EnFamilyLastVisit1);
                            tempCommand.Parameters.AddWithValue("@", EnFamilyLastVisit2);
                            tempCommand.Parameters.AddWithValue("@", EnFamilyLastVisit3);
                            tempCommand.Parameters.AddWithValue("@", EnFamilyLastVisit4);
                            tempCommand.Parameters.AddWithValue("@", EnWhichClinic);
                            tempCommand.Parameters.AddWithValue("@", EnMainHutStructure);
                            tempCommand.Parameters.AddWithValue("@", EnMainHutTypeRoof);
                            tempCommand.Parameters.AddWithValue("@", EnMainHutVentilation);
                            tempCommand.Parameters.AddWithValue("@", EnMainHutTotalRooms);
                            tempCommand.Parameters.AddWithValue("@", EnHut2Structure);
                            tempCommand.Parameters.AddWithValue("@", EnHut2TypeRoof);
                            tempCommand.Parameters.AddWithValue("@", EnHut2Ventilation);
                            tempCommand.Parameters.AddWithValue("@", EnHut2TotalRooms);
                            tempCommand.Parameters.AddWithValue("@", EnHut3Structure);
                            tempCommand.Parameters.AddWithValue("@", EnHut3TypeRoof);
                            tempCommand.Parameters.AddWithValue("@", EnHut3Ventilation);
                            tempCommand.Parameters.AddWithValue("@", EnHut3TotalRooms);
                            tempCommand.Parameters.AddWithValue("@", EnHut4Structure);
                            tempCommand.Parameters.AddWithValue("@", EnHut4TypeRoof);
                            tempCommand.Parameters.AddWithValue("@", EnHut4Ventilation);
                            tempCommand.Parameters.AddWithValue("@", EnHut4TotalRooms);
                            tempCommand.Parameters.AddWithValue("@", EnHut5Structure);
                            tempCommand.Parameters.AddWithValue("@", EnHut5TypeRoof);
                            tempCommand.Parameters.AddWithValue("@", EnHut5Ventilation);
                            tempCommand.Parameters.AddWithValue("@", EnHut5TotalRooms);
                            tempCommand.Parameters.AddWithValue("@", EnNoSleepingInOneRoom);
                            tempCommand.Parameters.AddWithValue("@", EnNoOfStructures);
                            tempCommand.Parameters.AddWithValue("@", EnRainWaterCollection);
                            tempCommand.Parameters.AddWithValue("@", EnWaterSupply);
                            tempCommand.Parameters.AddWithValue("@", EnWaterSupply1);
                            tempCommand.Parameters.AddWithValue("@", EnWalkingDistanceWater);
                            tempCommand.Parameters.AddWithValue("@", EnTreatWater);
                            tempCommand.Parameters.AddWithValue("@", EnHutElectricity);
                            tempCommand.Parameters.AddWithValue("@", EnFridge);
                            tempCommand.Parameters.AddWithValue("@", EnUseForCooking);
                            tempCommand.Parameters.AddWithValue("@", EnUseForCooking1);
                            tempCommand.Parameters.AddWithValue("@", EnUseForCooking2);
                            tempCommand.Parameters.AddWithValue("@", EnTypeToilet);
                            tempCommand.Parameters.AddWithValue("@", EnDisposeWaste);
                            tempCommand.Parameters.AddWithValue("@", EnDisposeWaste1);
                            tempCommand.Parameters.AddWithValue("@", EnSourceIncomeHousehold);
                            tempCommand.Parameters.AddWithValue("@", EnFoodParcel);

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
                            SqlCommand tempCommand = new SqlCommand("InsertGeneral", tempConnectionGen);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", GenWeight);
                            tempCommand.Parameters.AddWithValue("@", GenHeight);
                            tempCommand.Parameters.AddWithValue("@", GenBMI);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOnMeds);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentNotOnMeds);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPT);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTListMeds1);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTListMeds2);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTListMeds3);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTListMeds4);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTListMeds5);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTStartDate);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTDefaulting);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentHPTReferNo);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetes);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesListMeds1);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesListMeds2);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesListMeds3);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesListMeds4);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesListMeds5);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesStartDate);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesDefaulting);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentDiabetesReferNo);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsy);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyListMeds1);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyListMeds2);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyListMeds3);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyListMeds4);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyListMeds5);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyStartDate);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyDefaulting);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentEpilepsyReferNo);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthma);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaListMeds1);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaListMeds2);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaListMeds3);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaListMeds4);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaListMeds5);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaStartDate);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaDefaulting);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentAsthmaReferNo);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOther);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherListMeds1);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherListMeds2);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherListMeds3);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherListMeds4);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherListMeds5);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherStartDate);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherDefaulting);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenCurrentOtherReferNo);

                            tempCommand.Parameters.AddWithValue("@", GenBPOnMedsSystolic);
                            tempCommand.Parameters.AddWithValue("@", GenBPOnMedsDiatolic);
                            tempCommand.Parameters.AddWithValue("@", GenBPNotOnMedsSystolic);
                            tempCommand.Parameters.AddWithValue("@", GenBPNotOnMedsDiatolic);
                            tempCommand.Parameters.AddWithValue("@", GenBPReferCHOW);
                            tempCommand.Parameters.AddWithValue("@", GenBPReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenBPReferNo);

                            tempCommand.Parameters.AddWithValue("@", GenBSOnMeds);
                            tempCommand.Parameters.AddWithValue("@", GenBSNotOnMeds);
                            tempCommand.Parameters.AddWithValue("@", GenBSReferChow);
                            tempCommand.Parameters.AddWithValue("@", GenBSReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenBSReferNo);

                            tempCommand.Parameters.AddWithValue("@", GenEPFitsMonth);
                            tempCommand.Parameters.AddWithValue("@", GenEPReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenEPReferNo);

                            tempCommand.Parameters.AddWithValue("@", GenHIVPosStatus);
                            tempCommand.Parameters.AddWithValue("@", GenHIVNegStatus);
                            tempCommand.Parameters.AddWithValue("@", GenHIVTestDone);
                            tempCommand.Parameters.AddWithValue("@", GenHIVResult);
                            tempCommand.Parameters.AddWithValue("@", GenHIVReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenHIVReferNo);

                            tempCommand.Parameters.AddWithValue("@", GenPregCurrently);
                            tempCommand.Parameters.AddWithValue("@", GenPregPossible);
                            tempCommand.Parameters.AddWithValue("@", GenPregTestDate);
                            tempCommand.Parameters.AddWithValue("@", GenPregResult);
                            tempCommand.Parameters.AddWithValue("@", GenPregReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenPregReferNo);

                            tempCommand.Parameters.AddWithValue("@", GenTBCurrentHave);
                            tempCommand.Parameters.AddWithValue("@", GenTBCurrentMeds1);
                            tempCommand.Parameters.AddWithValue("@", GenTBCurrentMeds2);
                            tempCommand.Parameters.AddWithValue("@", GenTBCurrentMeds3);
                            tempCommand.Parameters.AddWithValue("@", GenTBCurrentMeds4);
                            tempCommand.Parameters.AddWithValue("@", GenTBCurrentMeds5);
                            tempCommand.Parameters.AddWithValue("@", GenTBCurrentDefaulting);
                            tempCommand.Parameters.AddWithValue("@", GenTBSymtomWeightLoss);
                            tempCommand.Parameters.AddWithValue("@", GenTBSymtomSweat);
                            tempCommand.Parameters.AddWithValue("@", GenTBSymtomFeaver);
                            tempCommand.Parameters.AddWithValue("@", GenTBSymtomCough);
                            tempCommand.Parameters.AddWithValue("@", GenTBSymtomApetite);
                            tempCommand.Parameters.AddWithValue("@", GenTBSymtomReferClininc);
                            tempCommand.Parameters.AddWithValue("@", GenTBSymtomReferNo);
                            tempCommand.Parameters.AddWithValue("@", GenTBTraceHousholdOnMeds);
                            tempCommand.Parameters.AddWithValue("@", GenTBTraceReferClininc);
                            tempCommand.Parameters.AddWithValue("@", GenTBTraceReferNo);

                            tempCommand.Parameters.AddWithValue("@", GenOtherBloodUrine);
                            tempCommand.Parameters.AddWithValue("@", GenOtherReferClinic1);
                            tempCommand.Parameters.AddWithValue("@", GenOtherReferNo1);
                            tempCommand.Parameters.AddWithValue("@", GenOtherSmoking);
                            tempCommand.Parameters.AddWithValue("@", GenOtherAlcohol);
                            tempCommand.Parameters.AddWithValue("@", GenOtherDiarrhoea);
                            tempCommand.Parameters.AddWithValue("@", GenOtherReferClinic2);
                            tempCommand.Parameters.AddWithValue("@", GenOtherReferNo2);
                            tempCommand.Parameters.AddWithValue("@", GenOtherInitiationSchool);
                            tempCommand.Parameters.AddWithValue("@", GenOtherLegCramps);
                            tempCommand.Parameters.AddWithValue("@", GenOtherLegNumb);
                            tempCommand.Parameters.AddWithValue("@", GenOtherFootUlcer);
                            tempCommand.Parameters.AddWithValue("@", GenOtherReferClinic3);
                            tempCommand.Parameters.AddWithValue("@", GenOtherReferNo3);

                            tempCommand.Parameters.AddWithValue("@", GenElderAmputation);
                            tempCommand.Parameters.AddWithValue("@", GenElderVision);
                            tempCommand.Parameters.AddWithValue("@", GenElderBedridden);
                            tempCommand.Parameters.AddWithValue("@", GenElderMovingAid);
                            tempCommand.Parameters.AddWithValue("@", GenElderWash);
                            tempCommand.Parameters.AddWithValue("@", GenElderFeed);
                            tempCommand.Parameters.AddWithValue("@", GenElderDress);
                            tempCommand.Parameters.AddWithValue("@", GenElderReferClinic);
                            tempCommand.Parameters.AddWithValue("@", GenElderReferNo);

                            tempCommand.Parameters.AddWithValue("@", GenFamilyPlan);
                            
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
                            SqlCommand tempCommand = new SqlCommand("InsertHypertention", tempConnectionHyp);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", HypYear);
                            tempCommand.Parameters.AddWithValue("@", HypHeadache);
                            tempCommand.Parameters.AddWithValue("@", HypVision);
                            tempCommand.Parameters.AddWithValue("@", HypShortBreath);
                            tempCommand.Parameters.AddWithValue("@", HypConfusion);
                            tempCommand.Parameters.AddWithValue("@", HypChestPain);
                            tempCommand.Parameters.AddWithValue("@", HypReferClinic);
                            tempCommand.Parameters.AddWithValue("@", HypReferNo);
                            tempCommand.Parameters.AddWithValue("@", HypHadStroke);
                            tempCommand.Parameters.AddWithValue("@", HypHadStrokeYear);
                            tempCommand.Parameters.AddWithValue("@", HypFamilyOnMeds);
                            tempCommand.Parameters.AddWithValue("@", HypFamilyStroke);        

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
                            SqlCommand tempCommand = new SqlCommand("InsertDiabetes", tempConnectionDia);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", DYear);
                            tempCommand.Parameters.AddWithValue("@", DThirsty);
                            tempCommand.Parameters.AddWithValue("@", DWeightloss);
                            tempCommand.Parameters.AddWithValue("@", DVision);
                            tempCommand.Parameters.AddWithValue("@", DUrinate);
                            tempCommand.Parameters.AddWithValue("@", DNausea);
                            tempCommand.Parameters.AddWithValue("@", DFoot);
                            tempCommand.Parameters.AddWithValue("@", DReferClinic);
                            tempCommand.Parameters.AddWithValue("@", DReferNo);
                            tempCommand.Parameters.AddWithValue("@", DFamilyMember);

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
                            SqlCommand tempCommand = new SqlCommand("InsertHIV", tempConnectionHIV);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", HIVYear);
                            tempCommand.Parameters.AddWithValue("@", HIVOnMeds);
                            tempCommand.Parameters.AddWithValue("@", HIVListMeds1);
                            tempCommand.Parameters.AddWithValue("@", HIVListMeds2);
                            tempCommand.Parameters.AddWithValue("@", HIVListMeds3);
                            tempCommand.Parameters.AddWithValue("@", HIVListMeds4);
                            tempCommand.Parameters.AddWithValue("@", HIVListMeds5);
                            tempCommand.Parameters.AddWithValue("@", HIVAdherence);
                            tempCommand.Parameters.AddWithValue("@", HIVReferClinic);
                            tempCommand.Parameters.AddWithValue("@", HIVReferNo);
                            tempCommand.Parameters.AddWithValue("@", HIVARVNo);

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
                            SqlCommand tempCommand = new SqlCommand("InsertMaternalHealth", tempConnectionMat);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", MHPregnantBefore);
                            tempCommand.Parameters.AddWithValue("@", MHNoPregnant);
                            tempCommand.Parameters.AddWithValue("@", MHNOSuccessful);
                            tempCommand.Parameters.AddWithValue("@", MHWhereDeliveredLast);
                            tempCommand.Parameters.AddWithValue("@", MHCaesarian);
                            tempCommand.Parameters.AddWithValue("@", MHBabyUnder25);
                            tempCommand.Parameters.AddWithValue("@", MHChildrenDied1);
                            tempCommand.Parameters.AddWithValue("@", MHChildrenDied15);
                            tempCommand.Parameters.AddWithValue("@", MHPAPSmear);
                            tempCommand.Parameters.AddWithValue("@", MHBloodResult);
                            tempCommand.Parameters.AddWithValue("@", MHFirstANCDate);
                            tempCommand.Parameters.AddWithValue("@", MHLastANCDate);
                            tempCommand.Parameters.AddWithValue("@", MHReferClinic);
                            tempCommand.Parameters.AddWithValue("@", MHReferNo);
                            tempCommand.Parameters.AddWithValue("@", MHNextANCDate);
                            tempCommand.Parameters.AddWithValue("@", MHExpectedDeliverDate);
                            tempCommand.Parameters.AddWithValue("@", MHBreastfeed);
                            tempCommand.Parameters.AddWithValue("@", MHFormula);
                            tempCommand.Parameters.AddWithValue("@", MHRegisteredMomConnect);

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
                            SqlCommand tempCommand = new SqlCommand("InsertChildHealth", tempConnectionChild);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", CHNameMother);
                            tempCommand.Parameters.AddWithValue("@", CHChildRTHC);
                            tempCommand.Parameters.AddWithValue("@", CHReferClinic1);
                            tempCommand.Parameters.AddWithValue("@", CHReferNo1);
                            tempCommand.Parameters.AddWithValue("@", CHMotherHIV);
                            tempCommand.Parameters.AddWithValue("@", CHChildBreastfeed);
                            tempCommand.Parameters.AddWithValue("@", CHHowLong);
                            tempCommand.Parameters.AddWithValue("@", CHChildOnNevirapine);
                            tempCommand.Parameters.AddWithValue("@", CHPCR);
                            tempCommand.Parameters.AddWithValue("@", CHPCRResult);
                            tempCommand.Parameters.AddWithValue("@", CHReferClininc2);
                            tempCommand.Parameters.AddWithValue("@", CHReferNo2);
                            tempCommand.Parameters.AddWithValue("@", CHImmunisationUpToDate);
                            tempCommand.Parameters.AddWithValue("@", CHImmunisationOutstanding1);
                            tempCommand.Parameters.AddWithValue("@", CHImmunisationOutstanding2);
                            tempCommand.Parameters.AddWithValue("@", CHImmunisationOutstanding3);
                            tempCommand.Parameters.AddWithValue("@", CHImmunisationOutstanding4);
                            tempCommand.Parameters.AddWithValue("@", CHImmunisationOutstanding5);
                            tempCommand.Parameters.AddWithValue("@", CHReferClinic3);
                            tempCommand.Parameters.AddWithValue("@", CHReferNo3);
                            tempCommand.Parameters.AddWithValue("@", CHMedsGiven);
                            tempCommand.Parameters.AddWithValue("@", CHWalkAppropriate);
                            tempCommand.Parameters.AddWithValue("@", CHTalkAppropriate);
                            tempCommand.Parameters.AddWithValue("@", CHChildConcerns1);
                            tempCommand.Parameters.AddWithValue("@", CHChildConcerns2);
                            tempCommand.Parameters.AddWithValue("@", CHChildConcerns3);
                            tempCommand.Parameters.AddWithValue("@", CHChildConcerns4);
                            tempCommand.Parameters.AddWithValue("@", CHChildConcerns5);
                            tempCommand.Parameters.AddWithValue("@", CHReferClinic4);
                            tempCommand.Parameters.AddWithValue("@", CHReferOVC);
                            tempCommand.Parameters.AddWithValue("@", CHReferNo4);                            

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
                            SqlCommand tempCommand = new SqlCommand("InsertBiographical", tempConnectionOther);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@", OCondition1);
                            tempCommand.Parameters.AddWithValue("@", OReferClinic1);
                            tempCommand.Parameters.AddWithValue("@", OReferNo1);
                            tempCommand.Parameters.AddWithValue("@", OCondition2);
                            tempCommand.Parameters.AddWithValue("@", OReferClinic2);
                            tempCommand.Parameters.AddWithValue("@", OReferNo2);
                            tempCommand.Parameters.AddWithValue("@", OCondition3);
                            tempCommand.Parameters.AddWithValue("@", OReferClinic3);
                            tempCommand.Parameters.AddWithValue("@", OReferNo3);
                            tempCommand.Parameters.AddWithValue("@", OCondition4);
                            tempCommand.Parameters.AddWithValue("@", OReferClinic4);
                            tempCommand.Parameters.AddWithValue("@", OReferNo4);
                            tempCommand.Parameters.AddWithValue("@", OCondition5);
                            tempCommand.Parameters.AddWithValue("@", OReferClinic5);
                            tempCommand.Parameters.AddWithValue("@", OReferNo5);

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
