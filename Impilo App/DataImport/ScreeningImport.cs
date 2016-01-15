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
