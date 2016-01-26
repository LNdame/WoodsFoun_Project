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
        public static string GetCellValue(ICell Target)
        {
            string ReturnValue = "";

            if (Target.CellType == CellType.String)
            {
                ReturnValue = Target.StringCellValue;                
            }

            if (Target.CellType == CellType.Numeric)
            {
                if (DateUtil.IsCellDateFormatted(Target))
                    ReturnValue = Target.DateCellValue.ToString();
                else
                    ReturnValue = Target.NumericCellValue.ToString();
            }
            
            //try
            //{                
            //    ReturnValue = Target.StringCellValue;
            //}
            //catch
            //{
            //    try
            //    {
                    
            //        DateTime TestDate = new DateTime(1900, 1, 1);
            //        DateTime.TryParse(Target.DateCellValue.ToString(), out TestDate);

            //        if (TestDate > new DateTime(1900,1,1))
            //            ReturnValue = TestDate.ToString();
            //        else
            //            ReturnValue = Target.NumericCellValue.ToString();
            //    }
            //    catch
            //    {
            //        ReturnValue = Target.NumericCellValue.ToString();
            //    }
            //}

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
                        string BioDataCapturer = "";
                        string BioName ="";
                        string BioSurname = "";
                        string BioLatitude = "";
                        string BioLongitude = "";
                        string BioIDNumber = "";
                        string BioDateOfBirth = "";
                        string BioMale = "";
                        string BioFemale = "";
                        List<string> Treatments = new List<string>();
                        DateTime TestDate = DateTime.Now;
                        bool AtLeastOneLine = false;
                        bool bContinue = true;

                        // Read file values here

                        #region Biographical and Encounters Insert

                        try
                        {
                            BioDataCapturer = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(0));
                            BioUniqueID = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(1));
                            BioName = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(2));
                            BioSurname = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(3));
                            BioLatitude = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(4));
                            BioLongitude = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(5));
                            BioIDNumber = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(6));
                            BioClinic = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(7));
                            BioDateOfBirth = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(8));
                            BioMale = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(9));
                            BioFemale = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(10));
                            BioContactNo = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(11));
                            BioFileNo = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(12));
                            BioNextOfKinRelationship = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(13));
                            BioNextOfKinName = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(14));
                            BioNextOfKinTelephone = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(15));
                            BioHPT = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(17));
                            BioDiabetes = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(18));
                            BioEpilepsy = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(19));
                            BioAsthma = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(20));
                            BioHIV = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(21));
                            BioTB = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(22));
                            BioMaternalHealth = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(23));
                            BioChildHealth = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(24));
                            BioEpilepsy2 = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(25));
                            BioOther = GetCellValue(MyWorkbook.GetSheet("Biographical").GetRow(5).GetCell(26));                           
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                            {
                                Success = false;
                                ErrorList.Add(CurrentFile + " - Biographical:  " + ex.Message);
                            }
                        }

                        # endregion

                        #region Visit Data

                        List<ArrayList> VisitDataEntries = new List<ArrayList>();

                        Row = 2;

                        try
                        {
                            while (MyWorkbook.GetSheet("Visit data").GetRow(Row).Cells.Count > 0)
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

                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(0)));                                
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(1)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(2)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(3)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(5)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(7)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(8)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(9)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(10)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(11)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(12)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(13)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(14)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(15)));
                                VisitDataEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Visit data").GetRow(Row).GetCell(16)));
                                
                                VisitDataEntries.Add(VisitDataEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                            {
                                Success = false;
                                ErrorList.Add(CurrentFile + " - Visit data:  " + ex.Message);
                            }
                        }
                        #endregion

                        #region Hypertension

                        List<ArrayList> HypertensionEntries = new List<ArrayList>();

                        Row = 5;

                        try
                        {
                            ArrayList HypertensionEntriesCurrent = new ArrayList();

                            AtLeastOneLine = false;
                            bContinue = true;

                            //while (bContinue && DateTime.TryParse(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(0).DateCellValue.ToString(),out TestDate) || MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(14).StringCellValue != "")
                           while(bContinue && MyWorkbook.GetSheet("Hypertension").GetRow(Row).Cells.Count > 0)
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
                                string TheDate = "";

                                TheDate = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(0));
                                DateTime.TryParse(TheDate, out TestDate);
                               
                                if (TestDate > new DateTime(1900,1,1))
                                {
                                    AtLeastOneLine = true;

                                    if (HypertensionEntriesCurrent.Count > 0)
                                    {
                                        HypertensionEntriesCurrent.Add(Treatments);
                                        HypertensionEntries.Add(HypertensionEntriesCurrent);
                                    }

                                    HypertensionEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();

                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(0)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(1)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(2)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(3)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(4)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(5)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(6)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(7)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(8)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(10)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(12)));
                                   HypertensionEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(13)));                                  
                                }

                                if (AtLeastOneLine)
                                {
                                    try
                                    {
                                        string Treatment = GetCellValue(MyWorkbook.GetSheet("Hypertension").GetRow(Row).GetCell(14));

                                        if (Treatment.Trim() != "")
                                        {
                                            Treatments.Add(Treatment.Trim());
                                            Row++;
                                        }
                                        else AtLeastOneLine = false;
                                    }
                                    catch
                                    {
                                        AtLeastOneLine = false;
                                        Row++;
                                    }
                                }
                                else
                                    bContinue = false;
                            }

                            if (HypertensionEntriesCurrent.Count > 0)
                            {
                                HypertensionEntriesCurrent.Add(Treatments);
                                HypertensionEntries.Add(HypertensionEntriesCurrent);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                            {
                                Success = false;
                                ErrorList.Add(CurrentFile + " - Hypertension:  " + ex.Message);
                            }
                        }
                        #endregion

                        #region Diabetes

                        List<ArrayList> DiabetesEntries = new List<ArrayList>();
                        ArrayList DiabetesEntriesCurrent = new ArrayList();                       

                        Row = 4;
                        AtLeastOneLine = false;
                        bContinue = true;

                        try
                        {
                            //while (bContinue && MyWorkbook.GetSheet("Diabetes").GetRow(Row).Cells.Count > 0 && DateTime.TryParse(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(0).DateCellValue.ToString(),out TestDate) || MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(18).StringCellValue.Trim() != "")
                            while (bContinue && MyWorkbook.GetSheet("Diabetes").GetRow(Row).Cells.Count > 0)
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

                                string TheDate = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(0));
                                DateTime.TryParse(TheDate, out TestDate);

                                if (TestDate > new DateTime(1900, 1, 1))
                                {
                                    AtLeastOneLine = true;

                                    if (DiabetesEntriesCurrent.Count > 0)
                                    {
                                        DiabetesEntriesCurrent.Add(Treatments);
                                        DiabetesEntries.Add(DiabetesEntriesCurrent);
                                    }
                                    DiabetesEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(0)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(1)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(2)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(3)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(4)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(5)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(7)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(8)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(10)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(11)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(12)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(13)));
                                    DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(14)));
                                   
                                    //DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(16)));
                                    //DiabetesEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(17)));
                                }

                                if (AtLeastOneLine)
                                {
                                    try
                                    { 
                                        string Treatment = GetCellValue(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(15));

                                        if (Treatment.Trim() != "")
                                        {
                                            Treatments.Add(Treatment.Trim());
                                            Row++;
                                        }
                                        else 
                                            AtLeastOneLine = false;
                                    }
                                    catch
                                    {
                                        AtLeastOneLine = false;
                                        Row++;
                                    }
                                }
                                else
                                    bContinue = false;
                            }

                            if (DiabetesEntriesCurrent.Count > 0)
                            {
                                DiabetesEntriesCurrent.Add(Treatments);
                                DiabetesEntries.Add(DiabetesEntriesCurrent);
                            }
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                DateTime.TryParse(MyWorkbook.GetSheet("Diabetes").GetRow(Row).GetCell(0).DateCellValue.ToString(), out TestDate);
                               
                                if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                                {
                                    Success = false;
                                    ErrorList.Add(CurrentFile + " - Diabetes:  " + ex.Message);
                                }
                            }
                            catch { }                            
                        }
                        #endregion

                        #region Epilepsy

                        List<ArrayList> EpilepsyEntries = new List<ArrayList>();
                        ArrayList EpilepsyEntriesCurrent = new ArrayList();
                        AtLeastOneLine = false;
                        bContinue = true;

                        Row = 5;

                        try
                        {
                            //while (MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(6).StringCellValue.Trim() != "")
                            while (bContinue && MyWorkbook.GetSheet("Epilepsy").GetRow(Row).Cells.Count > 0)
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

                                string TheDate = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(0));

                                DateTime.TryParse(TheDate, out TestDate);

                                if (TestDate > new DateTime(1900, 1, 1))
                                {
                                    AtLeastOneLine = true;
                                    if (EpilepsyEntriesCurrent.Count > 0)
                                    {
                                        EpilepsyEntriesCurrent.Add(Treatments);
                                        EpilepsyEntries.Add(EpilepsyEntriesCurrent);
                                    }
                                    EpilepsyEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
                                    EpilepsyEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(0)));
                                    EpilepsyEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(1)));
                                    EpilepsyEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(2)));
                                    EpilepsyEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(3)));
                                    EpilepsyEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(5)));
                                    EpilepsyEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(6)));                                   

                                }

                                if (AtLeastOneLine)
                                {
                                    try
                                    {
                                        string Treatment = GetCellValue(MyWorkbook.GetSheet("Epilepsy").GetRow(Row).GetCell(8));

                                        if (Treatment.Trim() != "")
                                        {
                                            Treatments.Add(Treatment.Trim());
                                            Row++;
                                        }
                                        else AtLeastOneLine = false;
                                    }
                                    catch
                                    {
                                        AtLeastOneLine = false;
                                        Row++;
                                    }
                                }
                                else bContinue = false;
                            }

                            if (EpilepsyEntriesCurrent.Count > 0)
                            {
                                EpilepsyEntriesCurrent.Add(Treatments);
                                EpilepsyEntries.Add(EpilepsyEntriesCurrent);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                            {
                                Success = false;
                                ErrorList.Add(CurrentFile + " - Epilepsy:  " + ex.Message);
                            }
                        }
                        #endregion

                        #region Asthma

                        List<ArrayList> AsthmaEntries = new List<ArrayList>();
                        ArrayList AsthmaEntriesCurrent = new ArrayList();
                        AtLeastOneLine = false;
                        bContinue = true;

                        Row = 5;

                        try
                        {
                            //while (MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(6).StringCellValue.Trim() != "")
                            while (bContinue && MyWorkbook.GetSheet("Asthma").GetRow(Row).Cells.Count > 0)
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

                                string TheDate = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(0));

                                DateTime.TryParse(TheDate, out TestDate);

                                if (TestDate > new DateTime(1900, 1, 1))
                                {
                                    AtLeastOneLine = true;
                                    if(AsthmaEntriesCurrent.Count > 0)
                                    {
                                        AsthmaEntriesCurrent.Add(Treatments);
                                        AsthmaEntries.Add(AsthmaEntriesCurrent);
                                    }

                                    AsthmaEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();

                                    AsthmaEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(0)));
                                    AsthmaEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(1)));
                                    AsthmaEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(2)));
                                    AsthmaEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(4)));
                                    AsthmaEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(5)));                                  
                                }

                                if (AtLeastOneLine)
                                {
                                    try
                                    {
                                        string Treatment = GetCellValue(MyWorkbook.GetSheet("Asthma").GetRow(Row).GetCell(6));

                                        if (Treatment.Trim() != "")
                                        {
                                            Treatments.Add(Treatment.Trim());
                                            Row++;
                                        }
                                        else AtLeastOneLine = false;
                                    }
                                    catch
                                    {
                                        AtLeastOneLine = false;
                                        Row++;
                                    }
                                }
                                else
                                    bContinue = false;
                            }

                            if (AsthmaEntriesCurrent.Count > 0)
                            {
                                AsthmaEntriesCurrent.Add(Treatments);
                                AsthmaEntries.Add(AsthmaEntriesCurrent);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                            {
                                Success = false;
                                ErrorList.Add(CurrentFile + " - Asthma:  " + ex.Message);
                            }
                        }
                        #endregion

                        #region HIV

                        List<ArrayList> HIVEntries = new List<ArrayList>();
                        ArrayList HIVEntriesCurrent = new ArrayList();
                        AtLeastOneLine = false;
                        bContinue = true;

                        Row = 3;

                        try
                        {
                            //while (MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(4).StringCellValue.Trim() != "")
                            while(bContinue && MyWorkbook.GetSheet("HIV").GetRow(Row).Cells.Count > 0)
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

                                string TheDate = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(0));

                                DateTime.TryParse(TheDate, out TestDate);

                                if (TestDate > new DateTime(1900, 1, 1))
                                {
                                    AtLeastOneLine = true;
                                    if (HIVEntriesCurrent.Count > 0)
                                    {
                                        HIVEntriesCurrent.Add(Treatments);
                                        HIVEntries.Add(HIVEntriesCurrent);
                                    }
                                    HIVEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
                                    HIVEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(0)));
                                    HIVEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(1)));
                                    HIVEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(2)));
                                    HIVEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(3)));

                                    HIVEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(6)));
                                    HIVEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(7)));

                                }

                                if (AtLeastOneLine)
                                {
                                    try
                                    {
                                        string Treatment = GetCellValue(MyWorkbook.GetSheet("HIV").GetRow(Row).GetCell(4));

                                        if (Treatment.Trim() != "")
                                        {
                                            Treatments.Add(Treatment.Trim());
                                            Row++;
                                        }
                                        else AtLeastOneLine = false;
                                    }
                                    catch
                                    {
                                        AtLeastOneLine = false;
                                        Row++;
                                    }
                                }
                                else
                                    bContinue = false;
                            }

                            if (HIVEntriesCurrent.Count > 0)
                            {
                                HIVEntriesCurrent.Add(Treatments);
                                HIVEntries.Add(HIVEntriesCurrent);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                            {
                                Success = false;
                                ErrorList.Add(CurrentFile + " - HIV:  " + ex.Message);
                            }
                        }
                        #endregion

                        #region TB

                        List<ArrayList> TBEntries = new List<ArrayList>();
                        ArrayList TBEntriesCurrent = new ArrayList();
                        AtLeastOneLine = false;
                        bContinue = true;

                        Row = 3;

                        try
                        {
                            //while (MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1) || MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(7).StringCellValue.Trim() != "")
                            while(bContinue && MyWorkbook.GetSheet("TB").GetRow(Row).Cells.Count > 0)
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

                                string TheDate = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(0));

                                DateTime.TryParse(TheDate, out TestDate);

                                if (TestDate > new DateTime(1900, 1, 1))
                                {
                                    AtLeastOneLine = true;

                                    if (TBEntriesCurrent.Count > 0)
                                    {
                                        TBEntriesCurrent.Add(Treatments);
                                        TBEntries.Add(TBEntriesCurrent);
                                    }

                                    TBEntriesCurrent = new ArrayList();
                                    Treatments = new List<string>();
                                    TBEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(0)));
                                    TBEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(1)));
                                    TBEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(2)));
                                    TBEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(3)));
                                    TBEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(5)));
                                    TBEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(6)));
                                }

                                if (AtLeastOneLine)
                                {
                                    try
                                    { 
                                        string Treatment = GetCellValue(MyWorkbook.GetSheet("TB").GetRow(Row).GetCell(7));
                                    
                                        if (Treatment.Trim() != "")
                                        {
                                            Treatments.Add(Treatment.Trim());
                                            Row++;
                                        }
                                        else AtLeastOneLine = false;
                                    }
                                    catch
                                    {
                                        AtLeastOneLine = false;
                                        Row++;
                                    }
                                }
                                else
                                    bContinue = false;
                            }

                            if (TBEntriesCurrent.Count > 0)
                            {
                                TBEntriesCurrent.Add(Treatments);
                                TBEntries.Add(TBEntriesCurrent);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                            {
                                Success = false;
                                ErrorList.Add(CurrentFile + " - TB:  " + ex.Message);
                            }
                        }
                        #endregion

                        #region Mat Health

                        List<ArrayList> MatHealthEntries = new List<ArrayList>();

                        Row = 3;

                        try
                        {
                            //while (MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                           while(MyWorkbook.GetSheet("Mat Health").GetRow(Row).Cells.Count > 0)
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
                                MatHealthEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(0)));
                                MatHealthEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(1)));
                                MatHealthEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(2)));
                                MatHealthEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(3)));
                                MatHealthEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(4)));
                                MatHealthEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(5)));
                                MatHealthEntriesCurrent.Add(GetCellValue(MyWorkbook.GetSheet("Mat Health").GetRow(Row).GetCell(6)));

                                MatHealthEntries.Add(MatHealthEntriesCurrent);
                                Row++;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.Contains("Object reference not set to an instance of an object"))
                            {
                                Success = false;
                                ErrorList.Add(CurrentFile + " - Mat Health:  " + ex.Message);
                            }
                        }
                        #endregion

                        //#region Child Health

                        //List<ArrayList> ChildHealthEntries = new List<ArrayList>();

                        //Row = 2;

                        //try
                        //{
                        //    while (MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                        //    {
                        //        ArrayList ChildHealthEntriesCurrent = new ArrayList();
                        //        // Index
                        //        // 0 - Visit date
                        //        // 1 - DWF referral	
                        //        // 2 - PCR done	
                        //        // 3 - Current RTHC?
                        //        // 4 - Vaccinations up to date

                        //        ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(0).DateCellValue.ToString());
                        //        ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(1).StringCellValue);
                        //        ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(2).StringCellValue);
                        //        ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(3).StringCellValue);
                        //        ChildHealthEntriesCurrent.Add(MyWorkbook.GetSheet("Child Health").GetRow(Row).GetCell(4).StringCellValue);

                        //        ChildHealthEntries.Add(ChildHealthEntriesCurrent);
                        //        Row++;
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Success = false;
                        //    ErrorList.Add(CurrentFile + " - Child Health:  " + ex.Message);
                        //}
                        //#endregion

                        //#region Other

                        //List<ArrayList> OtherEntries = new List<ArrayList>();

                        //Row = 5;

                        //try
                        //{
                        //    while (MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(0).DateCellValue > new DateTime(1990, 1, 1))
                        //    {
                        //        ArrayList OtherEntriesCurrent = new ArrayList();
                        //        // Index
                        //        // 0 - Visit date
                        //        // 1 - DWF referral	
                        //        // Other Condition
                        //        // 2 - Condition that required referral
                        //        // 3 - Outcome
                        //        // BP Reading
                        //        // 4 - Systolic	
                        //        // 5 - Diastolic   

                        //        OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(0).DateCellValue.ToString());
                        //        OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(1).StringCellValue);
                        //        OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(3).StringCellValue);
                        //        OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(4).StringCellValue);
                        //        OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(6).NumericCellValue.ToString());
                        //        OtherEntriesCurrent.Add(MyWorkbook.GetSheet("Other").GetRow(Row).GetCell(7).NumericCellValue.ToString());

                        //        OtherEntries.Add(OtherEntriesCurrent);
                        //        Row++;
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Success = false;
                        //    ErrorList.Add(CurrentFile + " - Other:  " + ex.Message);
                        //}
                        //#endregion

                        // Queries here

                        SqlConnection tempConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        int EncounterID = -1;
                        int ccbID = -1;
                        int tempID = -1;
                        int ClinicID = -1;

                        #region Find Clinic ID

                        int Errors = 0;

                        try
                        {
                            tempConnection.Open();
                            SqlCommand tempCommand = new SqlCommand("FindClinicID", tempConnection);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ClinicName", BioClinic);

                            ClinicID = (int)tempCommand.ExecuteScalar();
                        }
                        catch (Exception ex) {  }
                        finally
                        {
                            tempConnection.Close();
                        }

                        

                        #endregion


                        #region Client

                        try
                        {
                            tempConnection.Open();
                            SqlCommand tempCommand = new SqlCommand("AddClient", tempConnection);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@ClientID", BioUniqueID);
                            tempCommand.Parameters.AddWithValue("@HeadOfHousehold", "");
                            tempCommand.Parameters.AddWithValue("@FirstName ", BioName);
                            tempCommand.Parameters.AddWithValue("@LastName", BioSurname);
                            tempCommand.Parameters.AddWithValue("@GPSLatitude", BioLatitude);
                            tempCommand.Parameters.AddWithValue("@GPSLongitude", BioLongitude);
                            tempCommand.Parameters.AddWithValue("@IDNo", BioIDNumber);
                            tempCommand.Parameters.AddWithValue("@ClinicID", ClinicID);
                            DateTime CurrentDate = new DateTime(1800, 1, 1);
                            DateTime.TryParse(BioDateOfBirth, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DateOfBirth", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                            tempCommand.Parameters.AddWithValue("@Gender", BioMale.ToLower() == "yes" || BioMale == "1" ? "Male" : "Female");
                            tempCommand.Parameters.AddWithValue("@AttendingSchool", false);
                            tempCommand.Parameters.AddWithValue("@Grade", "");
                            tempCommand.Parameters.AddWithValue("@NameofSchool", "");
                            tempCommand.Parameters.AddWithValue("@Area", "");

                            tempCommand.ExecuteNonQuery();
                        }
                        catch(Exception ex) { }
                        finally
                        {
                            tempConnection.Close();
                        }

                        #endregion

                        #region Encounters

                        try
                        {
                            tempConnection.Open();
                            SqlCommand tempCommand = new SqlCommand("AddEncounters", tempConnection);
                            tempCommand.CommandType = CommandType.StoredProcedure;
                            tempCommand.Parameters.AddWithValue("@EncounterDate", DateTime.Now);
                            tempCommand.Parameters.AddWithValue("@ClientID", BioUniqueID);
                            tempCommand.Parameters.AddWithValue("@EncounterType", 3);
                            tempCommand.Parameters.AddWithValue("@EncounterCapturedBy", BioDataCapturer);

                            EncounterID = (int)((decimal)tempCommand.ExecuteScalar());
                        }
                        catch (Exception ex) { System.Windows.MessageBox.Show(ex.ToString()); }
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
                            tempCommand.Parameters.AddWithValue("@ClinicID", ClinicID);
                            tempCommand.Parameters.AddWithValue("@ClientID", BioUniqueID);
                            tempCommand.Parameters.AddWithValue("@FileNo", BioFileNo);
                            tempCommand.Parameters.AddWithValue("@ContactNo", BioContactNo);
                            tempCommand.Parameters.AddWithValue("@NextOfKinRelationship", BioNextOfKinRelationship);
                            tempCommand.Parameters.AddWithValue("@NextOfKinName", BioNextOfKinName);
                            tempCommand.Parameters.AddWithValue("@NextOfKinTelNo", BioNextOfKinTelephone);
                            DateTime CurrentDate = new DateTime(1800, 1, 1);
                            if (BioHPT != "") DateTime.TryParse(BioHPT, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DoDHypertension", CurrentDate);
                            CurrentDate = new DateTime(1800, 1, 1);
                            if (BioDiabetes != "") DateTime.TryParse(BioDiabetes, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DoDDiabetes", CurrentDate);
                            CurrentDate = new DateTime(1800, 1, 1);
                            if (BioEpilepsy != "") DateTime.TryParse(BioEpilepsy, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DoDEpilepsy", CurrentDate);
                            CurrentDate = new DateTime(1800, 1, 1);
                            if (BioAsthma != "") DateTime.TryParse(BioAsthma, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DoDAsthma", CurrentDate);
                            CurrentDate = new DateTime(1800, 1, 1);
                            if (BioHIV != "") DateTime.TryParse(BioHIV, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DoDHIV", CurrentDate);
                            CurrentDate = new DateTime(1800, 1, 1);
                            if (BioTB != "") DateTime.TryParse(BioTB, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DoDTB", CurrentDate);
                            CurrentDate = new DateTime(1800, 1, 1);
                            if (BioMaternalHealth != "") DateTime.TryParse(BioMaternalHealth, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DoDMaternalHealth", CurrentDate);
                            CurrentDate = new DateTime(1800, 1, 1);
                            if (BioChildHealth != "") DateTime.TryParse(BioChildHealth, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@DoDChildHealth", CurrentDate);
                            CurrentDate = new DateTime(1800, 1, 1);
                            if (BioOther != "") DateTime.TryParse(BioOther, out CurrentDate);
                            tempCommand.Parameters.AddWithValue("@Other", CurrentDate);

                            ccbID = (int)((decimal)tempCommand.ExecuteScalar());
                        }
                        catch (Exception ex) { }
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
                        catch (Exception ex) {  }
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
                                if (((string)Current[0]).Trim() != "")
                                {
                                    tempConnection.Open();
                                    SqlCommand tempCommand = new SqlCommand("AddClinicVisitData", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    DateTime CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[0], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@DateOfVisit", CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@Weight", decimal.Parse((string)Current[1]));
                                    tempCommand.Parameters.AddWithValue("@Height", decimal.Parse((string)Current[2]));
                                    tempCommand.Parameters.AddWithValue("@BMI", decimal.Parse((string)Current[3]));
                                    CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[4], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@NextVisitDate", DateTime.Parse((string)Current[4]));
                                    tempCommand.Parameters.AddWithValue("@Hypertension", ((string)Current[5]).ToLower() == "yes" || (string)Current[5] == "1" ? 1 : 0);
                                    tempCommand.Parameters.AddWithValue("@Diabetes", ((string)Current[6]).ToLower() == "yes" || (string)Current[6] == "1" ? 1 : 0);
                                    tempCommand.Parameters.AddWithValue("@Epilepsy", ((string)Current[7]).ToLower() == "yes" || (string)Current[7] == "1" ? 1 : 0);
                                    tempCommand.Parameters.AddWithValue("@Asthma", ((string)Current[8]).ToLower() == "yes" || (string)Current[8] == "1" ? 1 : 0);
                                    tempCommand.Parameters.AddWithValue("@HIV", ((string)Current[9]).ToLower() == "yes" || (string)Current[9] == "1" ? 1 : 0);
                                    tempCommand.Parameters.AddWithValue("@TB", ((string)Current[10]).ToLower() == "yes" || (string)Current[10] == "1" ? 1 : 0);
                                    tempCommand.Parameters.AddWithValue("@MaternalHealth", ((string)Current[11]).ToLower() == "yes" || (string)Current[11] == "1" ? 1 : 0);
                                    tempCommand.Parameters.AddWithValue("@ChildHealth", ((string)Current[12]).ToLower() == "yes" || (string)Current[12] == "1" ? 1 : 0);
                                    tempCommand.Parameters.AddWithValue("@Other", ((string)Current[14]).ToLower() == "yes" || (string)Current[14] == "1" ? 1 : 0);

                                    tempCommand.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex) { }
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
                                if (((string)Current[0]).Trim() != "")
                                {
                                    decimal TheValue = -1;
                                    tempConnection.Open();
                                    SqlCommand tempCommand = new SqlCommand("AddClinicHypertension", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    DateTime CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[0], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@DateOfVisit", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[10] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@DiagAndTreatSystolic", decimal.TryParse((string)Current[2],out TheValue)?TheValue:-1);
                                    tempCommand.Parameters.AddWithValue("@DiagAndTreatDiastolic", decimal.TryParse((string)Current[3], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@NotOnMedsSystolic", decimal.TryParse((string)Current[4], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@NotOnMedsDiastolic", decimal.TryParse((string)Current[5], out TheValue) ? TheValue : -1);
                                    CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[6], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@NextReviewDate", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@OnMedsDiastolic", decimal.TryParse((string)Current[7], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@OnMedsSystolic", decimal.TryParse((string)Current[8], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@BloodSugarLevel", (string)Current[9]);
                                    tempCommand.Parameters.AddWithValue("@Creatinine", (string)Current[10]);
                                    tempCommand.Parameters.AddWithValue("@Cholesterol", (string)Current[11]);
                                    //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[12]);

                                    tempID = (int)((decimal)tempCommand.ExecuteScalar());

                                    foreach (string CurrentTreatment in (List<string>)Current[12])
                                    {
                                        tempCommand = new SqlCommand("AddClinicHypertensionTreatment", tempConnection);
                                        tempCommand.CommandType = CommandType.StoredProcedure;
                                        tempCommand.Parameters.AddWithValue("@chID", tempID);
                                        tempCommand.Parameters.AddWithValue("@chtName", CurrentTreatment);
                                        tempCommand.ExecuteNonQuery();
                                    }
                                }     
                            }
                            catch (Exception ex) { /*System.Windows.MessageBox.Show(ex.Message);*/ }
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
                                if (((string)Current[0]).Trim() != "")
                                {
                                    decimal TheValue = -1;
                                    tempConnection.Open();
                                    SqlCommand tempCommand = new SqlCommand("AddClinicDiabetes", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    DateTime CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[0], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@DateOfVisit", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[1] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@DiagnosedAndGivenTreatment", ((string)Current[2]).ToLower() == "yes" || (string)Current[2] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@NotOnMedsBSLevel", decimal.TryParse((string)Current[3], out TheValue) ? TheValue : -1);
                                    CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[4], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@NextReviewDate", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@OnMedsBSLevel", (string)Current[5]);
                                    tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.TryParse((string)Current[6], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.TryParse((string)Current[7], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@HbA1C", (string)Current[8]);
                                    tempCommand.Parameters.AddWithValue("@Creatinine", (string)Current[9]);
                                    tempCommand.Parameters.AddWithValue("@Cholesterol", (string)Current[10]);
                                    tempCommand.Parameters.AddWithValue("@FootExam", (string)Current[11]);
                                    tempCommand.Parameters.AddWithValue("@EyeTest", (string)Current[12]);
                                    tempCommand.Parameters.AddWithValue("@ReferToClinic", false);
                                    tempCommand.Parameters.AddWithValue("@ReferralNo", -1);
                                    //tempCommand.Parameters.AddWithValue("@ReferToClinic", ((string)Current[13]).ToLower() == "yes" || (string)Current[13] == "1" ? true : false);
                                    //int TempValue = -1;
                                    //tempCommand.Parameters.AddWithValue("@ReferralNo", int.TryParse((string)Current[14], out TempValue) ? TempValue : -1);
                                    //tempCommand.Parameters.AddWithValue("@Treatment",  (string)Current[15]);

                                    tempID = (int)((decimal)tempCommand.ExecuteScalar());

                                    foreach (string CurrentTreatment in (List<string>)Current[13])
                                    {
                                        tempCommand = new SqlCommand("AddClinicDiabetesTreatment", tempConnection);
                                        tempCommand.CommandType = CommandType.StoredProcedure;
                                        tempCommand.Parameters.AddWithValue("@cdID", tempID);
                                        tempCommand.Parameters.AddWithValue("@cdtName", CurrentTreatment);
                                        tempCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception ex) { /*System.Windows.MessageBox.Show(ex.Message);*/ }
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
                                if (((string)Current[0]).Trim() != "")
                                {
                                    decimal TheValue = -1;
                                    int intValue = -1;
                                    tempConnection.Open();
                                    SqlCommand tempCommand = new SqlCommand("AddClinicEpilepsy", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    DateTime CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[0], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@DateOfVisit", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[1] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@NoFitsInLastMonth", int.TryParse((string)Current[2], out intValue) ? intValue : -1);
                                    tempCommand.Parameters.AddWithValue("@DrugSideEffects", (string)Current[3]);
                                    tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.TryParse((string)Current[4], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.TryParse((string)Current[5], out TheValue) ? TheValue : -1);
                                    //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[6]);

                                    tempID = (int)((decimal)tempCommand.ExecuteScalar());

                                    foreach (string CurrentTreatment in (List<string>)Current[6])
                                    {
                                        tempCommand = new SqlCommand("AddClinicEpilepsyTreatment", tempConnection);
                                        tempCommand.CommandType = CommandType.StoredProcedure;
                                        tempCommand.Parameters.AddWithValue("@ceID", tempID);
                                        tempCommand.Parameters.AddWithValue("@cetName", CurrentTreatment);
                                        tempCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception ex) {/* System.Windows.MessageBox.Show(ex.Message);*/ }
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
                                if (((string)Current[0]).Trim() != "")
                                {
                                    decimal TheValue = -1;
                                    int intValue = -1;
                                    tempConnection.Open();
                                    SqlCommand tempCommand = new SqlCommand("AddClinicAsthma", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    DateTime CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[0], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@DateOfVisit", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[1] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@PeakRespiratoryFlowRate", (string)Current[2]);
                                    tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.TryParse((string)Current[3], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.TryParse((string)Current[4], out TheValue) ? TheValue : -1);
                                    //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[5]);

                                    tempID = (int)((decimal)tempCommand.ExecuteScalar());

                                    foreach (string CurrentTreatment in (List<string>)Current[5])
                                    {
                                        tempCommand = new SqlCommand("AddClinicAsthmaTreatment", tempConnection);
                                        tempCommand.CommandType = CommandType.StoredProcedure;
                                        tempCommand.Parameters.AddWithValue("@caID", tempID);
                                        tempCommand.Parameters.AddWithValue("@catName", CurrentTreatment);
                                        tempCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception ex) { /*System.Windows.MessageBox.Show(ex.Message);*/ }
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
                                if (((string)Current[0]).Trim() != "")
                                {
                                    decimal TheValue = -1;
                                    int intValue = -1;
                                    tempConnection.Open();
                                    SqlCommand tempCommand = new SqlCommand("AddClinicHIV", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    DateTime CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[0], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@DateOfVisit", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[1] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@CD4", decimal.TryParse((string)Current[2], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@ViralLoad", decimal.TryParse((string)Current[3], out TheValue) ? TheValue : -1);
                                    //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[4]);
                                    tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.TryParse((string)Current[4], out TheValue) ? TheValue : -1);
                                    tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.TryParse((string)Current[5], out TheValue) ? TheValue : -1);

                                    tempID = (int)((decimal)tempCommand.ExecuteScalar());

                                    foreach (string CurrentTreatment in (List<string>)Current[6])
                                    {
                                        tempCommand = new SqlCommand("AddClinicHIVTreatment", tempConnection);
                                        tempCommand.CommandType = CommandType.StoredProcedure;
                                        tempCommand.Parameters.AddWithValue("@chivID", tempID);
                                        tempCommand.Parameters.AddWithValue("@chivtName", CurrentTreatment);
                                        tempCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception ex) { /*System.Windows.MessageBox.Show(ex.Message);*/ }
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
                                if (((string)Current[0]).Trim() != "")
                                {
                                    decimal TheValue = -1;
                                    int intValue = -1;
                                    tempConnection.Open();
                                    SqlCommand tempCommand = new SqlCommand("AddClinicTB", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    DateTime CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[0], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@DateOfVisit", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[1] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@SputumTaken", ((string)Current[2]).ToLower() == "yes" || (string)Current[2] == "1" ? true : false);
                                    CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[3], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@TestResultsReviewDate", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@ResultsGenexpert", (string)Current[4]);
                                    tempCommand.Parameters.AddWithValue("@ResultsAFB", (string)Current[5]);
                                    //tempCommand.Parameters.AddWithValue("@Treatment", (string)Current[6]);

                                    tempID = (int)((decimal)tempCommand.ExecuteScalar());

                                    foreach (string CurrentTreatment in (List<string>)Current[6])
                                    {
                                        tempCommand = new SqlCommand("AddClinicTBTreatment", tempConnection);
                                        tempCommand.CommandType = CommandType.StoredProcedure;
                                        tempCommand.Parameters.AddWithValue("@ctbID", tempID);
                                        tempCommand.Parameters.AddWithValue("@ctbtName", CurrentTreatment);
                                        tempCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception ex) { /*System.Windows.MessageBox.Show(ex.Message);*/ }
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
                                if (((string)Current[0]).Trim() != "")
                                {
                                    decimal TheValue = -1;
                                    int intValue = -1;
                                    tempConnection.Open();
                                    SqlCommand tempCommand = new SqlCommand("AddClinicMaternalHealth", tempConnection);
                                    tempCommand.CommandType = CommandType.StoredProcedure;
                                    tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                                    DateTime CurrentDate = new DateTime(1800, 1, 1);
                                    DateTime.TryParse((string)Current[0], out CurrentDate);
                                    tempCommand.Parameters.AddWithValue("@DateOfVisit", CurrentDate > new DateTime(1800, 1, 1) ? CurrentDate : new DateTime(1800, 1, 1));
                                    tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[1] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@MomConnectRegistered", ((string)Current[2]).ToLower() == "yes" || (string)Current[2] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@ANCVisitNo", (string)Current[3]);
                                    tempCommand.Parameters.AddWithValue("@PNC1Week", ((string)Current[4]).ToLower() == "yes" || (string)Current[4] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@PCRDone", ((string)Current[5]).ToLower() == "yes" || (string)Current[5] == "1" ? true : false);
                                    tempCommand.Parameters.AddWithValue("@PNC6Week", ((string)Current[6]).ToLower() == "yes" || (string)Current[6] == "1" ? true : false);

                                    tempCommand.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
                            finally
                            {
                                tempConnection.Close();
                            }
                        }

                        #endregion

                        //#region Child Health

                        //// Index
                        //// 0 - Visit date
                        //// 1 - DWF referral	
                        //// 2 - PCR done	
                        //// 3 - Current RTHC?
                        //// 4 - Vaccinations up to date

                        //foreach (ArrayList Current in ChildHealthEntries)
                        //{
                        //    try
                        //    {
                        //        tempConnection.Open();
                        //        SqlCommand tempCommand = new SqlCommand("AddClinicChildHealth", tempConnection);
                        //        tempCommand.CommandType = CommandType.StoredProcedure;
                        //        tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                        //        tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                        //        tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[1] == "1" ? true : false);
                        //        tempCommand.Parameters.AddWithValue("@PCRDone", ((string)Current[2]).ToLower() == "yes" || (string)Current[2] == "1" ? true : false);
                        //        tempCommand.Parameters.AddWithValue("@CurrentRTHC", ((string)Current[3]).ToLower() == "yes" || (string)Current[3] == "1" ? true : false);
                        //        tempCommand.Parameters.AddWithValue("@VaccinationsUpToDate", ((string)Current[4]).ToLower() == "yes" || (string)Current[4] == "1" ? true : false);

                        //        tempCommand.ExecuteNonQuery();
                        //    }
                        //    catch { }
                        //    finally
                        //    {
                        //        tempConnection.Close();
                        //    }
                        //}

                        //#endregion

                        //#region Other

                        //// Index
                        //// 0 - Visit date
                        //// 1 - DWF referral	
                        //// Other Condition
                        //// 2 - Condition that required referral
                        //// 3 - Outcome
                        //// BP Reading
                        //// 4 - Systolic	
                        //// 5 - Diastolic   

                        //foreach (ArrayList Current in OtherEntries)
                        //{
                        //    try
                        //    {
                        //        tempConnection.Open();
                        //        SqlCommand tempCommand = new SqlCommand("AddClinicOther", tempConnection);
                        //        tempCommand.CommandType = CommandType.StoredProcedure;
                        //        tempCommand.Parameters.AddWithValue("@EncounterID", EncounterID);
                        //        tempCommand.Parameters.AddWithValue("@DateOfVisit", DateTime.Parse((string)Current[0]));
                        //        tempCommand.Parameters.AddWithValue("@DWFReferral", ((string)Current[1]).ToLower() == "yes" || (string)Current[1] == "1" ? true : false);
                        //        tempCommand.Parameters.AddWithValue("@Condition", (string)Current[2]);
                        //        tempCommand.Parameters.AddWithValue("@Outcome", (string)Current[3]);
                        //        tempCommand.Parameters.AddWithValue("@BPSystolic", decimal.Parse((string)Current[4]));
                        //        tempCommand.Parameters.AddWithValue("@BPDiastolic", decimal.Parse((string)Current[5]));

                        //        tempCommand.ExecuteNonQuery();
                        //    }
                        //    catch { }
                        //    finally
                        //    {
                        //        tempConnection.Close();
                        //    }
                        //}

                        //#endregion


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
