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

                        // Queries here


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
