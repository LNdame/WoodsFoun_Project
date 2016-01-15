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
    class ClinicImport
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

                        string BioDataCapturer = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(0).StringCellValue;
                        string BioUniqueID = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(1).NumericCellValue.ToString();
                        string BioName = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(2).StringCellValue;
                        string BioSurname = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(3).StringCellValue;
                        string BioLatitude = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(4).StringCellValue;
                        string BioLongitude = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(5).StringCellValue;
                        string BioIDNumber = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(6).StringCellValue;
                        string BioClinic = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(7).StringCellValue;
                        string BioDateOfBirth = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(8).DateCellValue.ToString();
                        string BioMale = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(9).StringCellValue;
                        string BioFemale = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(10).StringCellValue;
                        string BioContactNo = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(11).NumericCellValue.ToString();
                        string BioFileNo = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(12).StringCellValue;
                        string BioNextOfKinRelationship = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(13).StringCellValue;
                        string BioNextOfKinName = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(14).StringCellValue;
                        string BioNextOfKinTelephone = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(15).NumericCellValue.ToString();
                        string BioHPT = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(17).StringCellValue;
                        string BioDiabetes = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(18).StringCellValue;
                        string BioEpilepsy = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(19).StringCellValue;
                        string BioAsthma = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(20).StringCellValue;
                        string BioHIV = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(21).StringCellValue;
                        string BioTB = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(22).StringCellValue;
                        string BioMaternalHealth = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(23).StringCellValue;
                        string BioChildHealth = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(24).StringCellValue;
                        string BioEpilepsy2 = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(25).StringCellValue;
                        string BioOther = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(26).StringCellValue;

                        # endregion

                        #region Visit Data

                        string VDDateOfVisit = MyWorkbook.GetSheet("Visit data").GetRow(2).GetCell(0).DateCellValue.ToString();
                        string VDWeight = MyWorkbook.GetSheet("Visit data").GetRow(2).GetCell(1).NumericCellValue.ToString();

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
