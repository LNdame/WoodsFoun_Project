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

        public ClinicVisit()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Impilo_App.LocalModels.ClinicClientBiographical clinBio = new Impilo_App.LocalModels.ClinicClientBiographical();
            string storedProcedure = "";
           

            #region Biographical
            clinBio.ccbioID = "";
            clinBio.ClinicID = "";
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


            #endregion


            #region Visit Details

            Impilo_App.LocalModels.ClinicVisitData clinVD = new Impilo_App.LocalModels.ClinicVisitData();

            clinVD.cvdID = "";
            clinVD.EncounterID = "";
            clinVD.cvdHeight = txtVDHeight;
            clinVD.cvdWeight = txtVdWeight.Text;
            clinVD.cvdBMI = txtVdBmi.Text;
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
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

                com.Parameters.AddWithValue("@", fol.FollowUpID);//param

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
            clinHyp.chID =;

            clinHyp.EncounterID =;

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
            //sp place
            //connection
            try
            {
                storedProcedure = "AddHypertention";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ScreeningID", hyper.ScreeningID);//param
                com.Parameters.AddWithValue("@YearOfDiagnosis", hyper.YearOfDiagnosis);//param
                com.Parameters.AddWithValue("@Headache", hyper.Headache);//param
                com.Parameters.AddWithValue("@BlurredVision", hyper.BlurredVision);//param
                com.Parameters.AddWithValue("@ChestPain", hyper.ChestPain);//param
                com.Parameters.AddWithValue("@ReferralToClinic", hyper.ReferralToClinic);//param
                com.Parameters.AddWithValue("@ReferalNo", hyper.ReferalNo);//param
                com.Parameters.AddWithValue("@EverHadStroke", hyper.EverHadStroke);//param
                com.Parameters.AddWithValue("@YearOfStroke", hyper.YearOfStroke);//param
                com.Parameters.AddWithValue("@AnyOneInFamilyHadStroke", hyper.AnyOneInFamilyHadStroke);//param

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

            clinDia.cdID =;
            clinDia.EncounterID =;
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

            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

            //com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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

            clinEpi.ceID 
            clinEpi.EncounterID 
            clinEpi.ceDWFReferral 
            clinEpi.ceNoFitsInLastMonth 
            clinEpi.ceDrugSideEffects 
            clinEpi.ceBPSystolic 
            clinEpi.ceBPDiastolic 
            clinEpi.ceDateOfVisit 
   
            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

            //com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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

            clinAst.caID =;
            clinAst.EncounterID =;
            clinAst.caDWFReferral = (radAstDwfRefYes.IsChecked == true) ? true : false;
            clinAst.caPeakRespiratoryFlowRate = txtAstPeakExFlRt.Text;
            clinAst.caBPSystolic = decimal.Parse(txtAstBpSys.Text);
            clinAst.caBPDiastolic = decimal.Parse(txtAstBpDia.Text);
            clinAst.caDateOfVisit = (DateTime)dpAstVisDt.SelectedDate;

            //sp place
            //connection

            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

             //com.Parameters.AddWithValue("@");//param

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

            Impilo_App.LocalModels.ClinicHIV clinHiv = new Impilo_App.LocalModels.ClinicHiv();

            clinHiv.chivID =;
            clinHiv.EncounterID =;
            clinHiv.chivDWFReferral = (radHivDwfRefYes.IsChecked == true) ? true : false;
            clinHiv.chivCD4 = txtHivCD4count.Text;
            clinHiv.chivViralLoad = txtHivViralLoad.Text;
            clinHiv.chivBPSystolic = txtHivBpSys.Text;
            clinHiv.chivBPDiastolic = txtHivBpDia.Text;
            clinHiv.chivDateOfVisit = dpHivVisDt.Text;

            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

//com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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

            clinTb.ctbID =;
            clinTb.EncounterID =;
            clinTb.ctbDWFReferral = (radTbDwfRefYes.IsChecked == true) ? true : false;
            clinTb.ctbSputumTaken = (radTbSpatumTakenYes.IsChecked == true) ? true : false;
            clinTb.ctbTestResultsReviewDate = (DateTime)dpTbResultRevDt.SelectedDate;
            clinTb.ctbResultsGenexpert = txtTbGenexpert.Text;
            clinTb.ctbResultsAFB = txtTbAfb.Text;
            clinTb.ctbDateOfVisit = (DateTime)dpTbVisDt.SelectedDate;

            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

//com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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

            clinMat.cmhID =;

            clinMat.EncounterID =;

            clinMat.cmhDWFReferral = (radMatDwfRefYes.IsChecked == true) ? true : false;

            clinMat.cmhMomConnectRegistered = (radMatMomConnYes.IsChecked == true) ? true : false;

            clinMat.cmhANCVisitNo = txtMatAncVisNo.Text;

            clinMat.cmhPNC1Week = (radMatPNC1Yes.IsChecked == true) ? true : false;

            clinMat.cmhPCRDone = (radMatPCRYes.IsChecked == true) ? true : false;

            clinMat.cmhPNC6Week = (radMatPNC6Yes.IsChecked == true) ? true : false;

            clinMat.cmhDateOfVisit = (DateTime) dpMatVisDt.SelectedDate;
            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

            //com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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

            clinCh.cchID =;

            clinCh.EncounterID =;

            clinCh.cchDWFReferral = (radChiDwfRefYes.IsChecked == true) ? true : false;
            
            clinCh.cchPCRDone = (radChiPCRYes.IsChecked == true) ? true : false;

            clinCh.cchCurrentRTHC = (radChiCurrRTHCYes.IsChecked == true) ? true : false;

            clinCh.cchVaccinationsUpToDate = (radChiVaccUpToDtNo.IsChecked == true) ? true : false;

            clinCh.cchDateOfVisit = (DateTime) dpChiVisDt.SelectedDate;

            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

            //com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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

            clinOth.coID =;
            clinOth.EncounterID =;
            clinOth.coDWFReferral = (radOthDwfRefYes.IsChecked == true) ? true : false;
            clinOth.coCondition = txtOthCondition.Text;
            clinOth.coOutcome = txtOthOutcome.Text;
            clinOth.coBPSystolic = decimal.Parse(txtOthBpSys.Text);
            clinOth.coBPDiastolic =decimal.Parse(txtOthBpDia.Text);
            clinOth.coDateOfVisit = (DateTime)dpOthVisDt.SelectedDate;

            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

            //com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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
    

