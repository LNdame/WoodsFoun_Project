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
