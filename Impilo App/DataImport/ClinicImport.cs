using System;
using System.Collections;
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
                        int Row = 0;

                        MyWorkbook = new XSSFWorkbook(file);

                        // Read file values here

                        #region Biographical

                        try
                        {
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
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Biographical:  " + ex.Message);
                        }

                        # endregion

                        #region Visit Data

                        List<ArrayList> VisitDataEntries = new List<ArrayList>();

                        Row = 2;

                        try
                        {
                            while (MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990,1,1))
                            {
                                ArrayList VisitDataEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Date of Visit
                                // 1 - Weight
                                // 2 - Height
                                // 3 - BMI
                                // 4 - Next Visit Date
                                // 5 - HPT
                                // 6 - Diabetes	
                                // 7 - Epilepsy
                                // 8 - Asthma
                                // 9 - HIV
                                // 10 - TB
                                // 11 - Mat Hlth
                                // 12 - Child Hlth
                                // 13 - Epilepsy
                                // 14 - Other

                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(1).NumericCellValue.ToString());
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(2).NumericCellValue.ToString());
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(3).NumericCellValue.ToString());
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(5).DateCellValue.ToString());
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(7).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(8).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(9).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(10).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(11).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(12).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(13).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(14).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(15).StringCellValue);
                                VisitDataEntriesCurrent.Add(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(16).StringCellValue);
                                
                                VisitDataEntries.Add(VisitDataEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Visit data:  " + ex.Message);
                        }
                        #endregion

                        #region Hypertension

                        List<ArrayList> HypertensionEntries = new List<ArrayList>();

                        Row = 5;

                        try
                        {
                            while (MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList HypertensionEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // 1 - DWF Referral
                                // 2 - Diagnosed and given treatment systolic
                                // 3 - Diagnosed and given treatment diastolic
                                // 4 - Not put on meds systolic
                                // 5 - Not put on meds diastolic
                                // 6 - Next review date
                                // 7 - On meds systolic
                                // 8 - On meds diastolic
                                // 9 - Blood sugar level
                                // 10 - Results Creatinine
                                // 11 - Results Cholesterol
                                // 12 - Treatment

                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(1).StringCellValue);
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(2).NumericCellValue.ToString());
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(3).NumericCellValue.ToString());
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(4).NumericCellValue.ToString());
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(5).NumericCellValue.ToString());
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(6).DateCellValue);
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(7).NumericCellValue.ToString());
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(8).NumericCellValue.ToString());

                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(10).NumericCellValue.ToString());

                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(12).NumericCellValue.ToString());
                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(13).StringCellValue);

                                HypertensionEntriesCurrent.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(14).StringCellValue);
                            
                                HypertensionEntries.Add(HypertensionEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Hypertension:  " + ex.Message);
                        }
                        #endregion

                        #region Diabetes

                        List<ArrayList> DiabetesEntries = new List<ArrayList>();

                        Row = 4;

                        try
                        {
                            while (MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList DiabetesEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // Blood sugar levels
                                // 1 - DWF referral	
                                // 2 - Diagnosed & given treatment	
                                // 3 - Not on meds Blood Sugar level
                                // 4 - Next review date
                                // 5 - On meds Blood Sugar level
                                // BP Reading
                                // 6 - Systolic	
                                // 7 - Diastolic
                                // Results
                                // 8 - HbA1C	
                                // 9 - Creatinine
                                // 10 - Cholesterol
                                // 11 - Foot exam
                                // 12 - Eye test
                                //
                                // 13 - Refer to clinic
                                // 14 - Referral No.
                                //
                                // 15 - Treatment


                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(1).StringCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(2).StringCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(3).StringCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(4).DateCellValue.ToString());
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(5).StringCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(7).DateCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(8).NumericCellValue.ToString());
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(10).NumericCellValue.ToString());
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(11).NumericCellValue.ToString());
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(12).StringCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(13).StringCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(14).StringCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(15).StringCellValue);
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(16).NumericCellValue.ToString());
                                DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(17).StringCellValue);

                                DiabetesEntries.Add(DiabetesEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Diabetes:  " + ex.Message);
                        }
                        #endregion

                        #region Epilepsy

                        List<ArrayList> EpilepsyEntries = new List<ArrayList>();

                        Row = 5;

                        try
                        {
                            while (MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList EpilepsyEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // Checks
                                // 1 - DWF referral	
                                // 2 - No. of fits in last month
                                // 3 - Drug side-effects if any
                                // BP Reading
                                // 4 - Systolic	
                                // 5 - Diastolic                                
                                //
                                // 6 - Treatment

                                EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(1).StringCellValue);
                                EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(2).NumericCellValue.ToString());
                                EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(3).StringCellValue);
                                EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(5).NumericCellValue.ToString());
                                EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(6).NumericCellValue.ToString());
                                EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(8).StringCellValue);
                               
                                EpilepsyEntries.Add(EpilepsyEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Epilepsy:  " + ex.Message);
                        }
                        #endregion

                        #region Asthma

                        List<ArrayList> AsthmaEntries = new List<ArrayList>();

                        Row = 5;

                        try
                        {
                            while (MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList AsthmaEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // Results
                                // 1 - DWF referral	
                                // 2 - Peak Expiratory Flow Rate
                                // BP Reading
                                // 3 - Systolic	
                                // 4 - Diastolic                                
                                //
                                // 5 - Treatment

                                AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(1).StringCellValue);
                                AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(2).StringCellValue);
                                AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(4).NumericCellValue.ToString());
                                AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(5).NumericCellValue.ToString());
                                AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(6).StringCellValue);
                       
                                AsthmaEntries.Add(AsthmaEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Asthma:  " + ex.Message);
                        }
                        #endregion

                        #region HIV

                        List<ArrayList> HIVEntries = new List<ArrayList>();

                        Row = 3;

                        try
                        {
                            while (MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList HIVEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // Results
                                // 1 - DWF referral	
                                // 2 - CD4
                                // 3 - Viral Load                                                              
                                //
                                // 4 - Treatment
                                // BP Reading
                                // 5 - Systolic	
                                // 6 - Diastolic  

                                HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(1).StringCellValue);
                                HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(2).NumericCellValue.ToString());
                                HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(3).NumericCellValue.ToString());
                                HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(4).StringCellValue);
                                HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(6).NumericCellValue.ToString());
                                HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(7).NumericCellValue.ToString());

                                HIVEntries.Add(HIVEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - HIV:  " + ex.Message);
                        }
                        #endregion

                        #region TB

                        List<ArrayList> TBEntries = new List<ArrayList>();

                        Row = 3;

                        try
                        {
                            while (MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList TBEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // 1 - DWF referral	
                                // Check
                                // 2 - Sputum Taken
                                // Test Results Review
                                // 3 - Date                                                              
                                // Results
                                // 4 - Genexpert
                                // 5 - AFB	
                                // 6 - Treatment  

                                TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(1).StringCellValue);
                                TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(2).StringCellValue);
                                TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(3).DateCellValue.ToString());
                                TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(5).StringCellValue);
                                TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(6).StringCellValue);
                                TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(7).StringCellValue);

                                TBEntries.Add(TBEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - TB:  " + ex.Message);
                        }
                        #endregion

                        #region Mat Health

                        List<ArrayList> MatHealthEntries = new List<ArrayList>();

                        Row = 3;

                        try
                        {
                            while (MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList MatHealthEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // 1 - DWF referral	
                                // 2 - Mom Connect Registered
                                // Monthly ANC visit                                
                                // 3 - ANC visit no                                                              
                                // PNC Visits
                                // 4 - PNC 1-week
                                // 5 - PCR done	
                                // 6 - PNC 6-weeks  

                                MatHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                MatHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(1).StringCellValue);
                                MatHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(2).StringCellValue);
                                MatHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(3).NumericCellValue.ToString());
                                MatHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(4).StringCellValue);
                                MatHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(5).StringCellValue);
                                MatHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(6).StringCellValue);

                                MatHealthEntries.Add(MatHealthEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Mat Health:  " + ex.Message);
                        }
                        #endregion

                        #region Child Health

                        List<ArrayList> ChildHealthEntries = new List<ArrayList>();

                        Row = 2;

                        try
                        {
                            while (MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList ChildHealthEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // 1 - DWF referral	
                                // 2 - PCR done	
                                // 3 - Current RTHC?
                                // 4 - Vaccinations up to date

                                ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(1).StringCellValue);
                                ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(2).StringCellValue);
                                ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(3).StringCellValue);
                                ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(4).StringCellValue);

                                ChildHealthEntries.Add(ChildHealthEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Child Health:  " + ex.Message);
                        }
                        #endregion

                        #region Other

                        List<ArrayList> OtherEntries = new List<ArrayList>();

                        Row = 5;

                        try
                        {
                            while (MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                            {
                                ArrayList OtherEntriesCurrent = new ArrayList();
                                // Index
                                // 0 - Visit date
                                // 1 - DWF referral	
                                // Other Condition
                                // 2 - Condition that required referral
                                // 3 - Outcome
                                // BP Reading
                                // 4 - Systolic	
                                // 5 - Diastolic   

                                OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(1).StringCellValue);
                                OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(3).StringCellValue);
                                OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(4).StringCellValue);
                                OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(6).NumericCellValue.ToString());
                                OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(7).NumericCellValue.ToString());

                                OtherEntries.Add(OtherEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            ErrorList.Add(CurrentFile + " - Other:  " + ex.Message);
                        }
                        #endregion

                        // Queries here
                    }
                }
                catch (Exception ex)
                {
                    Success = false;
                    ErrorList.Add(CurrentFile + ":  " + ex.Message);
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
