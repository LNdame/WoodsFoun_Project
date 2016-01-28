using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using Repository;
using Impilo_App.LocalModels;
using System.Configuration;

namespace Impilo_App.Views.ClinicData
{
    /// <summary>
    /// Interaction logic for ClinicVisit.xaml
    /// </summary>
    public partial class ClinicVisit : UserControl
    {
        static string sconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=impilo;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(sconn);

        public ClinicVisit(Impilo_App.LocalModels.Client client)
        {
            InitializeComponent();

            Impilo_App.LocalModels.Client cl = client;

            lblIdentity.Content = cl.ClientID;
            txtBioName.Text = cl.FirstName;
            txtBioSurname.Text = cl.LastName;
            txtBioLatitude.Text = cl.GPSLatitude;
            txtBioLongitude.Text = cl.GPSLongitude;
            cboBioClinic.SelectedItem = cl.ClinicUsed;
            txtBioIDNo.Text = cl.IDNo;
            dpBioDOB.Text = cl.DateOfBirth.ToString();
            
            //txtDateOfScreening.Text = DateTime.Now.ToString("dd MMMM yyyy h:mm");

            if (cl.Gender == "Male" || cl.Gender == "male")
            {
                radGendMale.IsChecked = true;
            }
            else
            {
                radGendFemale.IsChecked = true;
            }

            txtBioCapturer.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            
            string storedProcedure = "";


            #region Biographical

            Impilo_App.LocalModels.ClinicClientBiographical clinBio = new Impilo_App.LocalModels.ClinicClientBiographical();

            clinBio.ccbioID = 0;
            clinBio.ClinicID = 0;
            clinBio.ClientID = "";
            clinBio.ccbioContactNo = txtBioContactNo.Text;
            clinBio.ccbioFileNo = txtBioFileNo.Text;
            clinBio.ccbioNextOfKinRelationship = txtBioKinRelation.Text;
            clinBio.ccbioNextOfKinName = txtBioKinName.Text;
            clinBio.ccbioNextOfKinTelNo = txtBioKinContactNo.Text;
            clinBio.ccbioDoDHypertension = (DateTime) dpBioHptDiagDate.SelectedDate;
            clinBio.ccbioDoDDiabetes = (DateTime)dpBioDiaDiagDate.SelectedDate;
            clinBio.ccbioDoDEpilepsy = (DateTime)dpBioEpiDiagDate.SelectedDate;
            clinBio.ccbioDoDAsthma = (DateTime) dpBioAstDiagDate.SelectedDate;
            clinBio.ccbioDoDHIV = (DateTime) dpBioHivDiagDate.SelectedDate;
            clinBio.ccbioDoDTB = (DateTime)dpBioTbDiagDate.SelectedDate;
            clinBio.ccbioDoDMaternalHealth = (DateTime)dpBioMatDiagDate.SelectedDate;
            clinBio.ccbioDoDChildHealth = (DateTime)dpBioChDiagDate.SelectedDate;
            clinBio.ccbioOther = (DateTime)dpBioOtherDiagDate.SelectedDate;


            try
            {
                storedProcedure = "AddClinicClientBiographical";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ccbioID", clinBio.ccbioID);
                com.Parameters.AddWithValue("@ClinicID", clinBio.ClinicID );
                com.Parameters.AddWithValue("@ClientID", clinBio.ClientID);
                com.Parameters.AddWithValue("@ccbioContactNo", clinBio.ccbioContactNo);
                com.Parameters.AddWithValue("@ccbioFileNo", clinBio.ccbioFileNo );
                com.Parameters.AddWithValue("@ccbioNextOfKinRelationship", clinBio.ccbioNextOfKinRelationship );
                com.Parameters.AddWithValue("@ccbioNextOfKinName", clinBio.ccbioNextOfKinName);
                com.Parameters.AddWithValue("@ccbioNextOfKinTelNo", clinBio.ccbioNextOfKinTelNo);
                com.Parameters.AddWithValue("@ccbioDoDHypertension", clinBio.ccbioDoDHypertension);
                com.Parameters.AddWithValue("@ccbioDoDDiabetes", clinBio.ccbioDoDDiabetes);
                com.Parameters.AddWithValue("@ccbioDoDEpilepsy", clinBio.ccbioDoDEpilepsy);
                com.Parameters.AddWithValue("@ccbioDoDAsthma", clinBio.ccbioDoDAsthma);
                com.Parameters.AddWithValue("@ccbioDoDHIV", clinBio.ccbioDoDHIV);
                com.Parameters.AddWithValue("@ccbioDoDTB", clinBio.ccbioDoDTB);
                com.Parameters.AddWithValue("@ccbioDoDMaternalHealth", clinBio.ccbioDoDMaternalHealth);
                com.Parameters.AddWithValue("@ccbioDoDChildHealth", clinBio.ccbioDoDChildHealth);
                com.Parameters.AddWithValue("@ccbioOther", clinBio.ccbioOther);
                
                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            #endregion


            #region Visit Details

            Impilo_App.LocalModels.ClinicVisitData clinVD = new Impilo_App.LocalModels.ClinicVisitData();

            clinVD.cvdID = 0;
            clinVD.EncounterID = 0;
           
            clinVD.cvdWeight =decimal.Parse( txtVdWeight.Text);
            clinVD.cvdBMI = decimal.Parse(txtVdBmi.Text);
            clinVD.cvdNextVisitDate = (DateTime)dpVdNextVisDt.SelectedDate;
            clinVD.cvdHypertension = (radVdHptYes.IsChecked == true) ? true : false; 
            clinVD.cvdDiabetes = (radVdDiaYes.IsChecked == true) ? true : false;
            clinVD.cvdEpilepsy = (radVdEpiYes.IsChecked == true) ? true : false;
            clinVD.cvdAsthma = (radVdAstYes.IsChecked == true) ? true : false;
            clinVD.cvdHIV = (radVdHivYes.IsChecked == true) ? true : false;
            clinVD.cvdTB = (radVdTbYes.IsChecked == true) ? true : false;
            clinVD.cvdMaternalHealth = (radVdMatYes.IsChecked == true) ? true : false;
            clinVD.cvdChildHealth = (radVdChYes.IsChecked == true) ? true : false;
            clinVD.cvdOther = (radVdOthYes.IsChecked == true) ? true : false;
            clinVD.cvdDateOfVisit = (DateTime)dpVdVisDt.SelectedDate;

            try
            {
                storedProcedure = "AddClinicVisitData";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@cvdID", clinVD.cvdID);
                com.Parameters.AddWithValue("@EncounterID", clinVD.EncounterID);
                com.Parameters.AddWithValue("@cvdHeight", clinVD.cvdHeight);
                com.Parameters.AddWithValue("@cvdWeight", clinVD.cvdWeight);
                com.Parameters.AddWithValue("@cvdBMI", clinVD.cvdBMI);
                com.Parameters.AddWithValue("@cvdNextVisitDate", clinVD.cvdNextVisitDate);
                com.Parameters.AddWithValue("@cvdHypertension", clinVD.cvdHypertension);
                com.Parameters.AddWithValue("@cvdDiabetes", clinVD.cvdDiabetes);
                com.Parameters.AddWithValue("@cvdEpilepsy", clinVD.cvdEpilepsy);
                com.Parameters.AddWithValue("@cvdAsthma", clinVD.cvdAsthma);
                com.Parameters.AddWithValue("@cvdHIV", clinVD.cvdHIV);
                com.Parameters.AddWithValue("@cvdTB", clinVD.cvdTB);
                com.Parameters.AddWithValue("@cvdMaternalHealth", clinVD.cvdMaternalHealth);
                com.Parameters.AddWithValue("@cvdChildHealth", clinVD.cvdChildHealth);
                com.Parameters.AddWithValue("@cvdOther", clinVD.cvdOther);
                com.Parameters.AddWithValue("@cvdDateOfVisit", clinVD.cvdDateOfVisit);
                

                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            #endregion

            #region Hypertension

            Impilo_App.LocalModels.ClinicHypertension clinHyp = new Impilo_App.LocalModels.ClinicHypertension();
            clinHyp.chID =0;
            clinHyp.EncounterID =0;
            clinHyp.chDWFReferral = (radHypDwfRefYes.IsChecked == true) ? true : false; 
            clinHyp.chDiagAndTreatSystolic = decimal.Parse(txtHypDiagnosedOnTreatmentSys.Text);
            clinHyp.chDiagAndTreatDiastolic = decimal.Parse(txtHypDiagnosedOnTreatmentDia.Text);
            clinHyp.chNotOnMedsSystolic = decimal.Parse(txtHypNotPutOnMedsSys.Text);
            clinHyp.chNotOnMedsDiastolic = decimal.Parse(txtHypNotPutOnMedsDia.Text);
            clinHyp.chNextReviewDate = (DateTime)dpHypNextRevDt.SelectedDate;
            clinHyp.chOnMedsSystolic = decimal.Parse(txtHypOnMedsSys.Text);
            clinHyp.chOnMedsDiastolic = decimal.Parse(txtHypOnMedsDia.Text);
            clinHyp.chBloodSugarLevel = txtHypBloodSugarLevel.Text;
            clinHyp.chCreatinine = txtHypCreatinine.Text;
            clinHyp.chCholesterol = txtHypCholesterol.Text;
            clinHyp.chDateOfVisit = (DateTime)dpHypVisDt.SelectedDate;
            
            
            try
            {
                storedProcedure = "AddClinicHypertension";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@chID", clinHyp.chID);
                com.Parameters.AddWithValue("@EncounterID", clinHyp.EncounterID);
                com.Parameters.AddWithValue("@chDWFReferral", clinHyp.chDWFReferral);
                com.Parameters.AddWithValue("@chDiagAndTreatSystolic", clinHyp.chDiagAndTreatSystolic);
                com.Parameters.AddWithValue("@chDiagAndTreatDiastolic", clinHyp.chDiagAndTreatDiastolic);
                com.Parameters.AddWithValue("@chNotOnMedsSystolic", clinHyp.chNotOnMedsSystolic);
                com.Parameters.AddWithValue("@chNotOnMedsDiastolic", clinHyp.chNotOnMedsDiastolic);
                com.Parameters.AddWithValue("@chNextReviewDate", clinHyp.chNextReviewDate);
                com.Parameters.AddWithValue("@chOnMedsSystolic", clinHyp.chOnMedsSystolic);
                com.Parameters.AddWithValue("@chOnMedsDiastolic", clinHyp.chOnMedsDiastolic);
                com.Parameters.AddWithValue("@chBloodSugarLevel", clinHyp.chBloodSugarLevel);
                com.Parameters.AddWithValue("@chCreatinine", clinHyp.chCreatinine);
                com.Parameters.AddWithValue("@chCholesterol", clinHyp.chCholesterol);
                com.Parameters.AddWithValue("@chDateOfVisit", clinHyp.chDateOfVisit);
                
                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            #endregion



            #region Diabetes

            Impilo_App.LocalModels.ClinicDiabetes clinDia = new Impilo_App.LocalModels.ClinicDiabetes();

            clinDia.cdID =1;//random assign number
            clinDia.EncounterID =1;
            clinDia.cdDWFReferral = (radDiaDwfRefYes.IsChecked == true) ? true : false;
            clinDia.cdNotOnMedsBSLevel = txtDiaNoMedsBS.Text;
            clinDia.cdNextReviewDate = (DateTime) dpDiaNextRevDt.SelectedDate;
            clinDia.cdOnMedsBSLevel = txtDiaOnMedsBS.Text;
            clinDia.cdHbA1C = txtDiaHbAc1.Text;
            clinDia.cdCreatinine = txtDiaCreatinine.Text;
            clinDia.cdCholesterol = txtDiaCholesterol.Text;
            clinDia.cdFootExam = txtDiaFootExam.Text;
            clinDia.cdEyeTest = txtDiaFootExam.Text;
            //clinDia.cdReferToClinic = 
            //clinDia.cdReferralNo = 
            clinDia.cdBPDiastolic = decimal.Parse(txtDiaBpSys.Text);
            clinDia.cdBPSystolic = decimal.Parse(txtDiaBpDia.Text);
            clinDia.cdDateOfVisit = (DateTime)dpDiaVisitDt.SelectedDate;
            clinDia.cdDiagnosedAndGivenTreatment = (radDiaDiagOnTreatmentYes.IsChecked == true) ? true : false;

            
            
            try
            {
                storedProcedure = "AddClinicDiabetes";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@cdID", clinDia.cdID);
                com.Parameters.AddWithValue("@EncounterID", clinDia.EncounterID);
                com.Parameters.AddWithValue("@cdDWFReferral", clinDia.cdDWFReferral);
                com.Parameters.AddWithValue("@cdNotOnMedsBSLevel", clinDia.cdNotOnMedsBSLevel);
                com.Parameters.AddWithValue("@cdNextReviewDate", clinDia.cdNextReviewDate);
                com.Parameters.AddWithValue("@cdOnMedsBSLevel", clinDia.cdOnMedsBSLevel);
                com.Parameters.AddWithValue("@cdHbA1C", clinDia.cdHbA1C);
                com.Parameters.AddWithValue("@cdCreatinine", clinDia.cdCreatinine);
                com.Parameters.AddWithValue("@cdCholesterol", clinDia.cdCholesterol);
                com.Parameters.AddWithValue("@cdFootExam", clinDia.cdFootExam);
                com.Parameters.AddWithValue("@cdEyeTest", clinDia.cdEyeTest);
                com.Parameters.AddWithValue("@cdBPDiastolic", clinDia.cdBPDiastolic);
                com.Parameters.AddWithValue("cdBPSystolic", clinDia.cdBPSystolic);
                com.Parameters.AddWithValue("@cdDateOfVisit", clinDia.cdDateOfVisit);
                com.Parameters.AddWithValue("@cdDiagnosedAndGivenTreatment", clinDia.cdDiagnosedAndGivenTreatment);

                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            #endregion

            #region Epilepsy

            Impilo_App.LocalModels.ClinicEpilepsy clinEpi = new Impilo_App.LocalModels.ClinicEpilepsy();

            clinEpi.ceID = 0;
            clinEpi.EncounterID =0 ;
            clinEpi.ceDWFReferral = (radEpiDwfRefYes.IsChecked == true) ? true : false;
            clinEpi.ceNoFitsInLastMonth = int .Parse(txtEpiFitsLastMonth.Text);
            clinEpi.ceDrugSideEffects = txtEpiDrugSideEffects.Text;
            clinEpi.ceBPSystolic = decimal.Parse(txtEpiBpSys.Text);
            clinEpi.ceBPDiastolic = decimal.Parse(txtEpiBpDia.Text);
            clinEpi.ceDateOfVisit = (DateTime) dpEpiVisDt.SelectedDate;

            
            
            try
            {
                storedProcedure = "AddClinicEpilepsy";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ceID", clinEpi.ceID);
                com.Parameters.AddWithValue("@EncounterID", clinEpi.EncounterID);
                com.Parameters.AddWithValue("@ceDWFReferral", clinEpi.ceDWFReferral);
                com.Parameters.AddWithValue("@ceNoFitsInLastMonth", clinEpi.ceNoFitsInLastMonth);
                com.Parameters.AddWithValue("@ceDrugSideEffects", clinEpi.ceDrugSideEffects);
                com.Parameters.AddWithValue("@ceBPSystolic", clinEpi.ceBPSystolic);
                com.Parameters.AddWithValue("@ceBPDiastolic", clinEpi.ceBPDiastolic);
                com.Parameters.AddWithValue("@ceDateOfVisit", clinEpi.ceDateOfVisit);
               

                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            #endregion
           

            #region Asthma

            Impilo_App.LocalModels.ClinicAsthma clinAst = new Impilo_App.LocalModels.ClinicAsthma();

            clinAst.caID =0;
            clinAst.EncounterID =0;
            clinAst.caDWFReferral = (radAstDwfRefYes.IsChecked == true) ? true : false;
            clinAst.caPeakRespiratoryFlowRate = txtAstPeakExFlRt.Text;
            clinAst.caBPSystolic = decimal.Parse(txtAstBpSys.Text);
            clinAst.caBPDiastolic = decimal.Parse(txtAstBpDia.Text);
            clinAst.caDateOfVisit = (DateTime)dpAstVisDt.SelectedDate;

            
            

            try
            {
                storedProcedure = "AddClinicAsthma";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@caID", clinAst.caID);
                com.Parameters.AddWithValue("@EncounterID", clinAst.EncounterID);
                com.Parameters.AddWithValue("@caDWFReferral", clinAst.caDWFReferral);
                com.Parameters.AddWithValue("@caPeakRespiratoryFlowRate", clinAst.caPeakRespiratoryFlowRate);
                com.Parameters.AddWithValue("@caBPSystolic", clinAst.caBPSystolic);
                com.Parameters.AddWithValue("@caBPDiastolic", clinAst.caBPDiastolic);
                com.Parameters.AddWithValue("@caDateOfVisit", clinAst.caDateOfVisit);


                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            #endregion

            #region HIV

            Impilo_App.LocalModels.ClinicHIV clinHiv = new Impilo_App.LocalModels.ClinicHIV();

            clinHiv.chivID =0;
            clinHiv.EncounterID =0;
            clinHiv.chivDWFReferral = (radHivDwfRefYes.IsChecked == true) ? true : false;
            clinHiv.chivCD4 = decimal.Parse(txtHivCD4count.Text);
            clinHiv.chivViralLoad = decimal.Parse(txtHivViralLoad.Text);
            clinHiv.chivBPSystolic = decimal.Parse(txtHivBpSys.Text);
            clinHiv.chivBPDiastolic = decimal.Parse(txtHivBpDia.Text);
            clinHiv.chivDateOfVisit = (DateTime) dpHivVisDt.SelectedDate;

            
            
            try
            {
                storedProcedure = "AddClinicHIV";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@chivID", clinHiv.chivID);
                com.Parameters.AddWithValue("@EncounterID", clinHiv.EncounterID);
                com.Parameters.AddWithValue("@chivDWFReferral", clinHiv.chivDWFReferral);
                com.Parameters.AddWithValue("@chivCD4", clinHiv.chivCD4);
                com.Parameters.AddWithValue("@chivViralLoad", clinHiv.chivViralLoad);
                com.Parameters.AddWithValue("@chivBPSystolic", clinHiv.chivBPSystolic);
                com.Parameters.AddWithValue("@chivBPDiastolic", clinHiv.chivBPDiastolic);
                com.Parameters.AddWithValue("@chivDateOfVisit", clinHiv.chivDateOfVisit);


                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            #endregion

            #region TB
            //fol.TBARVsConcern 
            //fol.TBStartDate 
            Impilo_App.LocalModels.ClinicTB clinTb = new Impilo_App.LocalModels.ClinicTB();

            clinTb.ctbID =0;
            clinTb.EncounterID =0;
            clinTb.ctbDWFReferral = (radTbDwfRefYes.IsChecked == true) ? true : false;
            clinTb.ctbSputumTaken = (radTbSpatumTakenYes.IsChecked == true) ? true : false;
            clinTb.ctbTestResultsReviewDate = (DateTime)dpTbResultRevDt.SelectedDate;
            clinTb.ctbResultsGenexpert = txtTbGenexpert.Text;
            clinTb.ctbResultsAFB = txtTbAfb.Text;
            clinTb.ctbDateOfVisit = (DateTime)dpTbVisDt.SelectedDate;



            try
            {
                storedProcedure = "AddClinicTB";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ctbID", clinTb.ctbID);
                com.Parameters.AddWithValue("@EncounterID", clinTb.EncounterID);
                com.Parameters.AddWithValue("@ctbDWFReferral", clinTb.ctbDWFReferral);
                com.Parameters.AddWithValue("@ctbSputumTaken", clinTb.ctbSputumTaken);
                com.Parameters.AddWithValue("@ctbTestResultsReviewDate", clinTb.ctbTestResultsReviewDate);
                com.Parameters.AddWithValue("@ctbResultsGenexpert", clinTb.ctbResultsGenexpert);
                com.Parameters.AddWithValue("@ctbResultsAFB", clinTb.ctbResultsAFB);
                com.Parameters.AddWithValue("@ctbDateOfVisit", clinTb.ctbDateOfVisit);


                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            #endregion

            #region Mat Health

            Impilo_App.LocalModels.ClinicMaternalHealth clinMat = new Impilo_App.LocalModels.ClinicMaternalHealth();

            clinMat.cmhID =0;
            clinMat.EncounterID =0;
            clinMat.cmhDWFReferral = (radMatDwfRefYes.IsChecked == true) ? true : false;
            clinMat.cmhMomConnectRegistered = (radMatMomConnYes.IsChecked == true) ? true : false;
            clinMat.cmhANCVisitNo = txtMatAncVisNo.Text;
            clinMat.cmhPNC1Week = (radMatPNC1Yes.IsChecked == true) ? "Yes" : "No";
            clinMat.cmhPCRDone = (radMatPCRYes.IsChecked == true) ? true : false;
            clinMat.cmhPNC6Week = (radMatPNC6Yes.IsChecked == true) ? "Yes" : "No";
            clinMat.cmhDateOfVisit = (DateTime) dpMatVisDt.SelectedDate;


            try
            {
                storedProcedure = "AddClinicMaternalHealth";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@cmhID", clinMat.cmhID);
                com.Parameters.AddWithValue("@EncounterID", clinMat.EncounterID);
                com.Parameters.AddWithValue("@cmhDWFReferral", clinMat.cmhDWFReferral);
                com.Parameters.AddWithValue("@cmhMomConnectRegistered", clinMat.cmhMomConnectRegistered);
                com.Parameters.AddWithValue("@cmhANCVisitNo", clinMat.cmhANCVisitNo);
                com.Parameters.AddWithValue("@cmhPNC1Week", clinMat.cmhPNC1Week);
                com.Parameters.AddWithValue("@cmhPCRDone", clinMat.cmhPCRDone);
                com.Parameters.AddWithValue("@cmhPNC6Week", clinMat.cmhPNC6Week);
                com.Parameters.AddWithValue("@cmhDateOfVisit", clinMat.cmhDateOfVisit);


                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            #endregion

            #region Child Health

            Impilo_App.LocalModels.ClinicChildHealth clinCh = new Impilo_App.LocalModels.ClinicChildHealth();

            clinCh.cchID =0;
            clinCh.EncounterID =0; //Kevin bellow those field do not exist
            clinCh.cchDWFReferral = (radChiDwfRefYes.IsChecked == true) ? true : false;
            clinCh.cchPCRDone = (radChiPCRDoneYes.IsChecked == true) ? true : false;
            clinCh.cchCurrentRTHC = (radChiCurrRTHCYes.IsChecked == true) ? true : false;
            clinCh.cchVaccinationsUpToDate = (radChiVaccUpToDateYes.IsChecked == true) ? true : false;
            clinCh.cchDateOfVisit = (DateTime) dpChiVisDt.SelectedDate;



            try
            {
                storedProcedure = "AddClinicChildHealth";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@cchID", clinCh.cchID);
                com.Parameters.AddWithValue("@EncounterID", clinCh.EncounterID);
                com.Parameters.AddWithValue("@cchDWFReferral", clinCh.cchDWFReferral);
                com.Parameters.AddWithValue("@cchPCRDone", clinCh.cchPCRDone);
                com.Parameters.AddWithValue("@cchCurrentRTHC", clinCh.cchCurrentRTHC);
                com.Parameters.AddWithValue("@cchVaccinationsUpToDate", clinCh.cchVaccinationsUpToDate);
                com.Parameters.AddWithValue("@cchDateOfVisit", clinCh.cchDateOfVisit);
                

                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            #endregion

            #region Other

            Impilo_App.LocalModels.ClinicOther clinOth = new Impilo_App.LocalModels.ClinicOther();

            clinOth.coID =0;
            clinOth.EncounterID =0;
            clinOth.coDWFReferral = (radOthDwfRefYes.IsChecked == true) ? true : false;
            clinOth.coCondition = txtOthCondition.Text;
            clinOth.coOutcome = txtOthOutcome.Text;
            clinOth.coBPSystolic = decimal.Parse(txtOthBpSys.Text);
            clinOth.coBPDiastolic =decimal.Parse(txtOthBpDia.Text);
            clinOth.coDateOfVisit = (DateTime)dpOthVisDt.SelectedDate;



            try
            {
                storedProcedure = "AddClinicOther";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@coID", clinOth.coID);
                com.Parameters.AddWithValue("@EncounterID", clinOth.EncounterID);
                com.Parameters.AddWithValue("@coDWFReferral", clinOth.coDWFReferral);
                com.Parameters.AddWithValue("@coCondition", clinOth.coCondition);
                com.Parameters.AddWithValue("@coOutcome", clinOth.coOutcome);
                com.Parameters.AddWithValue("@coBPSystolic", clinOth.coBPSystolic);
                com.Parameters.AddWithValue("@coBPDiastolic", clinOth.coBPDiastolic);
                com.Parameters.AddWithValue("@coDateOfVisit", clinOth.coDateOfVisit);


                com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            #endregion

        }
    }//end of class
}