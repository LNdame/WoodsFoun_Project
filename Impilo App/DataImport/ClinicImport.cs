using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
                        string BioUniqueID = "";
                        string BioClinic = "";
                        string BioFileNo = "";
                        string BioContactNo = "";
                        string BioNextOfKinRelationship = "";
                        string BioNextOfKinName ="";
                        string BioNextOfKinTelephone = "";
                        string BioHPT = "";
                        string BioDiabetes = "";
                        string BioEpilepsy = "";
                        string BioAsthma = "";
                        string BioHIV = "";
                        string BioTB = "";
                        string BioMaternalHealth = "";
                        string BioChildHealth = "";
                        string BioEpilepsy2 = "";
                        string BioOther = "";
                        List<string> Treatments = new List<string>();

                        // Read file values here

                        #region Biographical and Encounters Insert

                        try
                        {
                            string BioDataCapturer = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(0).StringCellValue;
                            BioUniqueID = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(1).NumericCellValue.ToString();
                            string BioName = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(2).StringCellValue;
                            string BioSurname = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(3).StringCellValue;
                            string BioLatitude = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(4).StringCellValue;
                            string BioLongitude = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(5).StringCellValue;
                            string BioIDNumber = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(6).StringCellValue;
                            BioClinic = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(7).StringCellValue;
                            string BioDateOfBirth = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(8).DateCellValue.ToString();
                            string BioMale = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(9).StringCellValue;
                            string BioFemale = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(10).StringCellValue;
                            BioContactNo = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(11).NumericCellValue.ToString();
                            BioFileNo = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(12).StringCellValue;
                            BioNextOfKinRelationship = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(13).StringCellValue;
                            BioNextOfKinName = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(14).StringCellValue;
                            BioNextOfKinTelephone = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(15).NumericCellValue.ToString();
                            BioHPT = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(17).StringCellValue;
                            BioDiabetes = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(18).StringCellValue;
                            BioEpilepsy = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(19).StringCellValue;
                            BioAsthma = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(20).StringCellValue;
                            BioHIV = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(21).StringCellValue;
                            BioTB = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(22).StringCellValue;
                            BioMaternalHealth = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(23).StringCellValue;
                            BioChildHealth = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(24).StringCellValue;
                            BioEpilepsy2 = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(25).StringCellValue;
                            BioOther = MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(26).StringCellValue;
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
                            ArrayList HypertensionEntriesCurrent = new ArrayList();
                            

                            while (MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(14).StringCellValue != "")
                            {                               
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
                                // ------------>12 - Treatment

                                if (MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                                {
                                    if (HypertensionEntriesCurrent.Count > 0)
                                    {
                                        HypertensionEntriesCurrent.Add(Treatments);
                                        HypertensionEntries.Add(HypertensionEntriesCurrent);
                                    }

                                    HypertensionEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
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
                                }

                                Treatments.Add(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(14).StringCellValue);                            
                               
                                Row++;
                            }

                            if (HypertensionEntriesCurrent.Count > 0)
                            {
                                HypertensionEntriesCurrent.Add(Treatments);
                                HypertensionEntries.Add(HypertensionEntriesCurrent);
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
                        ArrayList DiabetesEntriesCurrent = new ArrayList();

                        Row = 4;

                        try
                        {
                            while (MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(18).StringCellValue.Trim() != "")
                            {
                                
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
                                //
                                //

                                if (MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                                {
                                    if (DiabetesEntriesCurrent.Count > 0)
                                    {
                                        DiabetesEntriesCurrent.Add(Treatments);
                                        DiabetesEntries.Add(DiabetesEntriesCurrent);
                                    }
                                    DiabetesEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
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
                                   
                                    DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(16).NumericCellValue.ToString());
                                    DiabetesEntriesCurrent.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(17).StringCellValue);
                                }

                                Treatments.Add(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(18).StringCellValue);
                                Row++;
                            }

                            if (DiabetesEntriesCurrent.Count > 0)
                            {
                                DiabetesEntriesCurrent.Add(Treatments);
                                DiabetesEntries.Add(DiabetesEntriesCurrent);
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
                        ArrayList EpilepsyEntriesCurrent = new ArrayList();

                        Row = 5;

                        try
                        {
                            while (MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(6).StringCellValue.Trim() != "")
                            {
                                
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

                                if (MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(8).StringCellValue.Trim() != "")
                                {
                                    if (EpilepsyEntriesCurrent.Count > 0)
                                    {
                                        EpilepsyEntriesCurrent.Add(Treatments);
                                        EpilepsyEntries.Add(EpilepsyEntriesCurrent);
                                    }
                                    EpilepsyEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
                                    EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                    EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(1).StringCellValue);
                                    EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(2).NumericCellValue.ToString());
                                    EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(3).StringCellValue);
                                    EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(5).NumericCellValue.ToString());
                                    EpilepsyEntriesCurrent.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(6).NumericCellValue.ToString());                                   

                                }
                                Treatments.Add(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(8).StringCellValue);
                                Row++;
                            }

                            if (EpilepsyEntriesCurrent.Count > 0)
                            {
                                EpilepsyEntriesCurrent.Add(Treatments);
                                EpilepsyEntries.Add(EpilepsyEntriesCurrent);
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
                        ArrayList AsthmaEntriesCurrent = new ArrayList();

                        Row = 5;

                        try
                        {
                            while (MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(6).StringCellValue.Trim() != "")
                            {
                                
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

                                if (MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                                {
                                    if(AsthmaEntriesCurrent.Count > 0)
                                    {
                                        AsthmaEntriesCurrent.Add(Treatments);
                                        AsthmaEntries.Add(AsthmaEntriesCurrent);
                                    }

                                    AsthmaEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
                                    AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                    AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(1).StringCellValue);
                                    AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(2).StringCellValue);
                                    AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(4).NumericCellValue.ToString());
                                    AsthmaEntriesCurrent.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(5).NumericCellValue.ToString());
                                    

                                }
                                Treatments.Add(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(6).StringCellValue);
                                
                                Row++;
                            }

                            if (AsthmaEntriesCurrent.Count > 0)
                            {
                                AsthmaEntriesCurrent.Add(Treatments);
                                AsthmaEntries.Add(AsthmaEntriesCurrent);
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
                        ArrayList HIVEntriesCurrent = new ArrayList();

                        Row = 3;

                        try
                        {
                            while (MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(4).StringCellValue.Trim() != "")
                            {
                                
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

                                if (MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                                {
                                    if (HIVEntriesCurrent.Count > 0)
                                    {
                                        HIVEntriesCurrent.Add(Treatments);
                                        HIVEntries.Add(HIVEntriesCurrent);
                                    }
                                    HIVEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
                                    HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                    HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(1).StringCellValue);
                                    HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(2).NumericCellValue.ToString());
                                    HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(3).NumericCellValue.ToString());

                                    HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(6).NumericCellValue.ToString());
                                    HIVEntriesCurrent.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(7).NumericCellValue.ToString());

                                }

                                Treatments.Add(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(4).StringCellValue);
                                Row++;
                            }

                            if (HIVEntriesCurrent.Count > 0)
                            {
                                HIVEntriesCurrent.Add(Treatments);
                                HIVEntries.Add(HIVEntriesCurrent);
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
                        ArrayList TBEntriesCurrent = new ArrayList();

                        Row = 3;

                        try
                        {
                            while (MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(7).StringCellValue.Trim() != "")
                            {
                                
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

                                if (MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                                {
                                    if (TBEntriesCurrent.Count > 0)
                                    {
                                        TBEntriesCurrent.Add(Treatments);
                                        TBEntries.Add(TBEntriesCurrent);
                                    }

                                    TBEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
                                    TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(0).DateCellValue.ToString());
                                    TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(1).StringCellValue);
                                    TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(2).StringCellValue);
                                    TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(3).DateCellValue.ToString());
                                    TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(5).StringCellValue);
                                    TBEntriesCurrent.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(6).StringCellValue);
                                }
                                
                                Treatments.Add(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(7).StringCellValue);
                                Row++;
                            }

                            if (TBEntriesCurrent.Count > 0)
                            {
                                TBEntriesCurrent.Add(Treatments);
                                TBEntries.Add(TBEntriesCurrent);
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

                        SqlConnection tempConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        int EncounterID = -1;
                        int ccbID = -1;
                        int tempID = -1;

                        #region Encounters

                        try
                        {
                            tempConnection.Open();
                            SqlCommand tempCommand = new SqlCommand("AddEncounter", tempConnection);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterDate", DateTime.Now);
                            tempCommand.Parameters.AddWithValue("@ClientID", BioUniqueID);
                            tempCommand.Parameters.AddWithValue("@EncounterType", 3);

                            EncounterID = int.Parse((string)tempCommand.ExecuteScalar());
                        }
                        catch { }
                        finally
                        {
                            tempConnection.Close();
                        }

                        #endregion

                        #region ClinicClientBio

                        try
                        {
                            tempConnection.Open();
                            SqlCommand tempCommand = new SqlCommand("AddClinicClientBiographical", tempConnection);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ClinicID", BioClinic);
                            tempCommand.Parameters.AddWithValue("@ClientID", BioUniqueID);
                            tempCommand.Parameters.AddWithValue("@FileNo", BioFileNo);
                            tempCommand.Parameters.AddWithValue("@ContactNo", BioContactNo);
                            tempCommand.Parameters.AddWithValue("@NextOfKinRelationship", BioNextOfKinRelationship);
                            tempCommand.Parameters.AddWithValue("@NextOfKinName", BioNextOfKinName);
                            tempCommand.Parameters.AddWithValue("@NextOfKinTelNo", BioNextOfKinTelephone);
                            tempCommand.Parameters.AddWithValue("@DoDHypertension", DateTime.Parse(BioHPT));
                            tempCommand.Parameters.AddWithValue("@DoDEpilepsy", DateTime.Parse(BioEpilepsy)); 
                            tempCommand.Parameters.AddWithValue("@DoDAsthma", DateTime.Parse(BioAsthma));
                            tempCommand.Parameters.AddWithValue("@DoDHIV", DateTime.Parse(BioHIV));
                            tempCommand.Parameters.AddWithValue("@DoDTB", DateTime.Parse(BioTB));
                            tempCommand.Parameters.AddWithValue("@DoDMaternalHealth", DateTime.Parse(BioMaternalHealth));
                            tempCommand.Parameters.AddWithValue("@DoDChildHealth", DateTime.Parse(BioChildHealth));
                            tempCommand.Parameters.AddWithValue("@DoDOther", DateTime.Parse(BioOther));


                            ccbID = int.Parse((string)tempCommand.ExecuteScalar());
                        }
                        catch { }
                        finally
                        {
                            tempConnection.Close();
                        }

                        #endregion

                        #region EncountersBridgeClinicClientBiographical

                        try
                        {
                            tempConnection.Open();
                            SqlCommand tempCommand = new SqlCommand("AddEncountersBridgeClinicClientBiographical", tempConnection);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                            tempCommand.Parameters.AddWithValue("@ccbioID", ccbID);
                            
                            tempCommand.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            tempConnection.Close();
                        }
                        
                        #endregion


                        #region Visit Data

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

                        foreach (ArrayList Current in VisitDataEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicVisitData", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@Weight", decimal.Parse((string)Current[1]));
                                tempCommand.Parameters.AddWithValue("@Height", decimal.Parse((string)Current[2]));
                                tempCommand.Parameters.AddWithValue("@BMI", decimal.Parse((string)Current[3]));
                                tempCommand.Parameters.AddWithValue("@NextVisitDate", DateTime.Parse((string)Current[4]));
                                tempCommand.Parameters.AddWithValue("@Hypertension", (string)Current[5] == "Yes"||(string)Current[5] == "1"?true:false);
                                tempCommand.Parameters.AddWithValue("@Epilepsy", (string)Current[5] == "Yes" || (string)Current[6] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@Asthma", (string)Current[5] == "Yes" || (string)Current[7] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@HIV", (string)Current[5] == "Yes" || (string)Current[8] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@TB", (string)Current[5] == "Yes" || (string)Current[9] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@MaternalHealth", (string)Current[5] == "Yes" || (string)Current[10] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@ChildHealth", (string)Current[5] == "Yes" || (string)Current[11] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@Other", (string)Current[5] == "Yes" || (string)Current[13] == "1" ? true : false);

                                tempCommand.ExecuteNonQuery();
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region Hypertension

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

                        foreach (ArrayList Current in HypertensionEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicHypertension", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[10] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@DiagAndTreatSystolic", decimal.Parse((string)Current[2]));
                                tempCommand.Parameters.AddWithValue("@DiagAndTreatDiastolic", decimal.Parse((string)Current[3]));
                                tempCommand.Parameters.AddWithValue("@NotOnMedsSystolic", decimal.Parse((string)Current[4]));
                                tempCommand.Parameters.AddWithValue("@NotOnMedsDiastolic", DateTime.Parse((string)Current[5]));
                                tempCommand.Parameters.AddWithValue("@NextReviewDate", DateTime.Parse((string)Current[6]));
                                tempCommand.Parameters.AddWithValue("@OnMedsDiastolic", decimal.Parse((string)Current[7]));
                                tempCommand.Parameters.AddWithValue("@OnMedsSystolic", decimal.Parse((string)Current[8]));
                                tempCommand.Parameters.AddWithValue("@BloodSugarLevel", (string)Current[9]);
                                tempCommand.Parameters.AddWithValue("@Creatinine", (string)Current[9]);
                                tempCommand.Parameters.AddWithValue("@Cholesterol", (string)Current[9]);
                                //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[12]);

                                tempID = int.Parse((string)tempCommand.ExecuteScalar());

                                foreach (string CurrentTreatment in (List<string>)Current[12])
                                {
                                    tempCommand = new SqlCommand("AddClinicConditionTreatment", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@mcID", 1);
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    tempCommand.Parameters.AddWithValue("@cctName", CurrentTreatment);
                                    tempCommand.ExecuteNonQuery();
                                }
                                
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region Diabetes

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

                        foreach (ArrayList Current in DiabetesEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicDiabetes", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[1] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@DiagnosedAndGivenTreatment", (string)Current[1] == "Yes" || (string)Current[2] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@NotOnMedsBSLevel", decimal.Parse((string)Current[3]));
                                tempCommand.Parameters.AddWithValue("@NextReviewDate", DateTime.Parse((string)Current[4]));
                                tempCommand.Parameters.AddWithValue("@OnMedsBSLevel", (string)Current[5]);
                                tempCommand.Parameters.AddWithValue("@BPSystolic",  decimal.Parse((string)Current[6]));
                                tempCommand.Parameters.AddWithValue("@BPDiastolic",  decimal.Parse((string)Current[7]));
                                tempCommand.Parameters.AddWithValue("@HbA1C",  (string)Current[8]);
                                tempCommand.Parameters.AddWithValue("@Creatinine",  (string)Current[9]);
                                tempCommand.Parameters.AddWithValue("@Cholesterol",  (string)Current[10]);
                                tempCommand.Parameters.AddWithValue("@FootExam",  (string)Current[11]);
                                tempCommand.Parameters.AddWithValue("@EyeTest",  (string)Current[12]);
                                tempCommand.Parameters.AddWithValue("@ReferToClinic", (string)Current[13] == "Yes" || (string)Current[13] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@ReferralNo",  (string)Current[14]);
                                //tempCommand.Parameters.AddWithValue("@Treatment",  (string)Current[15]);

                                tempID = int.Parse((string)tempCommand.ExecuteScalar());

                                foreach (string CurrentTreatment in (List<string>)Current[15])
                                {
                                    tempCommand = new SqlCommand("AddClinicConditionTreatment", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@mcID", 2);
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    tempCommand.Parameters.AddWithValue("@cctName", CurrentTreatment);
                                    tempCommand.ExecuteNonQuery();
                                }
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region Epilepsy

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

                        foreach (ArrayList Current in EpilepsyEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicEpilepsy", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[1] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@NoOfFitsInLastMonth", int.Parse((string)Current[2]));
                                tempCommand.Parameters.AddWithValue("@DrugSideEffectsIfAny", (string)Current[3]);
                                tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.Parse((string)Current[4]));
                                tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.Parse((string)Current[5]));                               
                                //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[6]);

                                tempID = int.Parse((string)tempCommand.ExecuteScalar());

                                foreach (string CurrentTreatment in (List<string>)Current[6])
                                {
                                    tempCommand = new SqlCommand("AddClinicConditionTreatment", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@mcID", 3);
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    tempCommand.Parameters.AddWithValue("@cctName", CurrentTreatment);
                                    tempCommand.ExecuteNonQuery();
                                }
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region Asthma

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

                        foreach (ArrayList Current in AsthmaEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicAsthma", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[1] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@PeakExpiratoryFlowRate", int.Parse((string)Current[2]));
                                tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.Parse((string)Current[3]));
                                tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.Parse((string)Current[4]));
                                //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[5]);

                                tempID = int.Parse((string)tempCommand.ExecuteScalar());

                                foreach (string CurrentTreatment in (List<string>)Current[5])
                                {
                                    tempCommand = new SqlCommand("AddClinicConditionTreatment", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@mcID", 4);
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    tempCommand.Parameters.AddWithValue("@cctName", CurrentTreatment);
                                    tempCommand.ExecuteNonQuery();
                                }
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region HIV

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

                        foreach (ArrayList Current in HIVEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicHIV", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[1] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@CD4", decimal.Parse((string)Current[2]));
                                tempCommand.Parameters.AddWithValue("@ViralLoad", decimal.Parse((string)Current[3]));
                                //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[4]);
                                tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.Parse((string)Current[5]));
                                tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.Parse((string)Current[6]));

                                tempID = int.Parse((string)tempCommand.ExecuteScalar());

                                foreach (string CurrentTreatment in (List<string>)Current[4])
                                {
                                    tempCommand = new SqlCommand("AddClinicConditionTreatment", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@mcID", 5);
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    tempCommand.Parameters.AddWithValue("@cctName", CurrentTreatment);
                                    tempCommand.ExecuteNonQuery();
                                }
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region TB

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

                        foreach (ArrayList Current in TBEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicTB", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[1] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@SputumTaken", (string)Current[2] == "Yes" || (string)Current[2] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@TestResultsReviewDate", DateTime.Parse((string)Current[3]));
                                tempCommand.Parameters.AddWithValue("@ResultsGenexpert", (string)Current[4]);
                                tempCommand.Parameters.AddWithValue("@ResultsAFB", (string)Current[5]);
                                //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[6]);

                                tempID = int.Parse((string)tempCommand.ExecuteScalar());

                                foreach (string CurrentTreatment in (List<string>)Current[6])
                                {
                                    tempCommand = new SqlCommand("AddClinicConditionTreatment", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@mcID", 6);
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    tempCommand.Parameters.AddWithValue("@cctName", CurrentTreatment);
                                    tempCommand.ExecuteNonQuery();
                                }
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region MaternalHealth

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

                        foreach (ArrayList Current in MatHealthEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicMaternalHealth", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[1] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@MomConnectRegistered", (string)Current[2] == "Yes" || (string)Current[2] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@ANCVisitNo", (string)Current[3]);
                                tempCommand.Parameters.AddWithValue("@PNC1Week", decimal.Parse((string)Current[4]));
                                tempCommand.Parameters.AddWithValue("@PCRDone", (string)Current[5] == "Yes" || (string)Current[5] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@PNC6Week", (string)Current[6]);

                                tempCommand.ExecuteNonQuery();
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region Child Health

                        // Index
                        // 0 - Visit date
                        // 1 - DWF referral	
                        // 2 - PCR done	
                        // 3 - Current RTHC?
                        // 4 - Vaccinations up to date

                        foreach (ArrayList Current in ChildHealthEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicChildHealth", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[1] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@PCRDone", (string)Current[2] == "Yes" || (string)Current[2] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@CurrentRTHC", (string)Current[3] == "Yes" || (string)Current[3] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@VaccinationsUpToDate", (string)Current[4] == "Yes" || (string)Current[4] == "1" ? true : false);

                                tempCommand.ExecuteNonQuery();
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        #region Other

                        // Index
                        // 0 - Visit date
                        // 1 - DWF referral	
                        // Other Condition
                        // 2 - Condition that required referral
                        // 3 - Outcome
                        // BP Reading
                        // 4 - Systolic	
                        // 5 - Diastolic   

                        foreach (ArrayList Current in OtherEntries)
                        {
                            try
                            {
                                tempConnection.Open();
                                SqlCommand tempCommand = new SqlCommand("AddClinicOther", tempConnection);
                                tempCommand.CommandType = CommandType.StoredProcedure;
                                tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                                tempCommand.Parameters.AddWithValue("@DWFReferral", (string)Current[1] == "Yes" || (string)Current[1] == "1" ? true : false);
                                tempCommand.Parameters.AddWithValue("@Condition", (string)Current[2]);
                                tempCommand.Parameters.AddWithValue("@Outcome", (string)Current[3]);
                                tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.Parse((string)Current[4]));
                                tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.Parse((string)Current[5]));

                                tempCommand.ExecuteNonQuery();
                            }
                            catch { }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion


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
