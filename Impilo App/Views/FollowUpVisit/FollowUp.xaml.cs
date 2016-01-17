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

namespace Impilo_App.Views.FollowUpVisit
{
    /// <summary>
    /// Interaction logic for FollowUp.xaml
    /// </summary>
    public partial class FollowUp : UserControl
    {
        static string sconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=impilo;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(sconn);

        public FollowUp()
        {
            InitializeComponent();
        }

        
private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Impilo_App.LocalModels.FollowUp fol = new Impilo_App.LocalModels.FollowUp();
            string storedProcedure = "";
            string folID = ""; // 

            #region Visit Detaisl
            fol.FollowUpIDNumber = folID;
            //fol.DateofScreen = scrDate;
            fol.VisitNextVisit = dpNextVisit.Text.ToString();
            fol.VisitOutCome = ((ComboBoxItem)cboListOutcome.SelectedItem).Content.ToString();
            fol.VisitHPT = (radVisHptYes.IsChecked == true) ? true : false;
            fol.VisitDiabetes = (radVisDiaYes.IsChecked == true) ? true : false;
            fol.VisitEpilepsy = (radVisEpiYes.IsChecked == true) ? true : false;
            fol.VisitHIV = (radVisHivYes.IsChecked == true) ? true : false;
            fol.VisitTB = (radVisHptYes.IsChecked == true) ? true : false;
            fol.VisitMatHealth = (radVisHptYes.IsChecked == true) ? true : false;
            fol.VisitChildHealth = (radVisHptYes.IsChecked == true) ? true : false;
            fol.VisitOther = (radVisHptYes.IsChecked == true) ? true : false;
            fol.VisitDooortoDoor = (radVisHptYes.IsChecked == true) ? true : false;

            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

                //com.Parameters.AddWithValue("@", fol.FollowUpID);//param

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

            fol.HyperWentToClinic1 = (radHyClinYes.IsChecked == true) ? true : false;
            fol.HyperReReferToClinic1 = (radHyRefClin1Yes.IsChecked == true) ? true : false;
            fol.HyperReferralNo1 = txtHyRefNo1.Text;
            fol.HyperCurrentlyOnMeds = (radHyCurMedsYes.IsChecked == true) ? true : false;
            fol.HyperStartDate = dpHyStartDt.Text.ToString();
            fol.HyperScreenBPReadingSystolic = txtHyScrSys.Text;
            fol.HyperScreenBPReadingDiastolic = txtHyScrDia.Text;
            fol.HyperTodayTestReadingSystolic = txtHyTodSys.Text;
            fol.HyperTodayTestReadingDiastolic = txtHyTodSys.Text;
            fol.HyperAlreadyOnTreatment = (radHyCurTreYes.IsChecked == true) ? true : false;
            fol.HyperReReferToClinic2 = (radHyRefClin2Yes.IsChecked == true) ? true : false; ;
            fol.HyperReferralNo2 = txtHyRefNo2.Text;
            fol.HyperMedication = ((ComboBoxItem)cboHyMeds.SelectedItem).Content.ToString();
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

            #region Diabetes

            fol.DiaReReferToClinic1 = (radDiaReRefClinYes.IsChecked == true) ? true : false; ;
            fol.DiaReferralNo1 = txtDiaRefNo1.Text;
            fol.DiaCurrentlyOnMeds = (radDiaCurMedsYes.IsChecked == true) ? true : false; ;
            fol.DiaStartDate = dpDiaStartDt.Text.ToString();
            fol.DiaScreenTestReading1 = txtDiaScrReading.Text;
            fol.DiaFollowUpTestReading1 = txtDiaFolReading.Text;
            fol.DiaReferToClinic2 = (radDiaRefClinYes.IsChecked == true) ? true : false; ;
            fol.DiaReferralNo2 = txtDiaRefNo2.Text;
            fol.DiaMedication = ((ComboBoxItem)cboDiaMeds.SelectedItem).Content.ToString();


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

            fol.EpiWentToClinic = (radEpiClinYes.IsChecked == true) ? true : false; ;
            fol.EpiReReferToClinic1 = (radEpiReRefClinYes.IsChecked == true) ? true : false; ;
            fol.EpiReferralNo1 = txtEpiRefNo1.Text;
            fol.EpiFitInLastMonth = (radEpiFitsLastMonthYes.IsChecked == true) ? true : false; ;
            fol.EpiCurrentlyOnMeds = (radEpiCurMedsYes.IsChecked == true) ? true : false;
            fol.EpiStartDate = dpEpiStartDt.Text.ToString();
            fol.EpiMoreThan3FitsInLastMonth = (radEpi3FitsLastMonthYes.IsChecked == true) ? true : false; ;
            fol.EpiReReferToClinic2 = (radEpiRefClin2Yes.IsChecked == true) ? true : false; ;
            fol.EpiReferralNo2 = txtEpiRefNo2.Text;
            fol.EpiMedication = ((ComboBoxItem)cboEpiMeds.SelectedItem).Content.ToString();

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
            fol.AsDateOfVisit = dpAstVisitDt.Text.ToString();
            fol.AsWentToClinic = (radAstClinYes.IsChecked == true) ? true : false;
            fol.AsReReferToClinic1 = (radAstReRefClinYes.IsChecked == true) ? true : false;
            fol.AsReferralNo1 = txtAstRefNo1.Text;
            //fol.AsFitInLastMonth 
            fol.AsReferToClinic = (radAsthClinRefYes.IsChecked == true) ? true : false;
            fol.AsReferralNo2 = txtAsthRefNo2.Text;
            fol.AsCurrentlyOnMeds = (radAstCurMedsYes.IsChecked == true) ? true : false;
            fol.AsStartDate = "";
            fol.AsIncreasedNoOfAsthmaAttacks = (radAstIncrAttacksYes.IsChecked == true) ? true : false;
            fol.AsReReferToClinic2 = (radAsthClinRefYes.IsChecked == true) ? true : false;
            fol.AsReferralNo2 = txtAsthRefNo2.Text;
            fol.AsMedication = ((ComboBoxItem)cboAsthMeds.SelectedItem).Content.ToString();

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
            //fol.HIVDateOfVisit =

            //fol.HIVWentToClinic =

            //fol.HIVRereferToClinic =

            fol.HIVReferralNo1 = txtHivRe.Text;

            fol.HIVReferToClinic1 = (radHyRefClin1Yes_Copy.IsChecked == true) ? true : false;

            //fol.HIVReferralNo2 = txtHivRe2.Text;

            //fol.HIVStatus

            fol.HIVOnARVs = (radHivOnARVYes.IsChecked == true) ? true : false;

            fol.HIVStartDate1 = dpARVStartDT.Text.ToString();

            fol.HIVAdherenceOK = (radHivAdhereYes.IsChecked == true) ? true : false;

            fol.HIVConcerns = (radHivConcernsYes.IsChecked == true) ? true : false;

            fol.HIVReferToClinic2 = (radHyRefClin4Yes_Copy.IsChecked == true) ? true : false;

            //fol.HIVReferralNo3 = 

            fol.HIVARVsConcern = (radHivARVsConcernYes.IsChecked == true) ? true : false;

            //fol.HIVReferToClinic3

            //fol.HIVReferralNo4

            fol.HIVTestingDone = (radHivStatusKnownYes.IsChecked == true) ? true : false;

            fol.HIVTestDone = (radHivTestDoneYes.IsChecked == true) ? true : false;

            fol.HIVTestResults = (radHivPos.IsChecked == true) ? true : false;

            fol.HIVReferToClinic4 = (radHyRefClin4Yes_Copy.IsChecked == true) ? true : false;

            fol.HIVReferralNo5 = txtHivRe2.Text;

            fol.HIVMedication = ((ComboBoxItem)cboListMeds.SelectedItem).Content.ToString();

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

            fol.TBDateOfVisit = dpTBDateOfVisit.Text.ToString();
            fol.TBReferToClinic1 = (radTBReferToClinic1Yes.IsChecked == true) ? true : false;
            fol.TBReferralNo1 = txtTBReferralNo1.Text;
            fol.TBRecentUnplannedLoseOfWeight = (radTBRecWeightLostYes.IsChecked == true) ? true : false;
            fol.TBExcessiveSweatingAtNight = (radTBExcessSweatYes.IsChecked == true) ? true : false;
            fol.TBFeverOver2Weeks = (radTBFeverYes.IsChecked == true) ? true : false;
            fol.TBCoughMoreThan2Week = (radTBCoughYes.IsChecked == true) ? true : false;
            fol.TBLossOfApetite = (radTBApetiteYes.IsChecked == true) ? true : false;
            fol.TBPreviouslyOnMeds = (radTBPrevMedsYes.IsChecked == true) ? true : false;
            fol.TBFinishDate = dpTBFinishDates.Text.ToString();
            fol.TBConcerns = (radTBConcernsYes.IsChecked == true) ? true : false;

            fol.TBResult = txtTBResults.Text;
            fol.TBReferredToClinic2 = (radTBReferToClinic2Yes.IsChecked == true) ? true : false;
            fol.TBReferralNo2 = txtTBReferralNo2.Text;
            fol.TBMedication = ((ComboBoxItem)cboTBMedicaiton.SelectedItem).Content.ToString();

            //fol.TBNewlyDiagnosed 
            //fol.TBReferTBContactsToClinic         
            //fol.TBReferToClinic3 
            //fol.TBReferralNo3 

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
            fol.MatDateOfVisit = txtMatDateOfVisit.Text.ToString();
            fol.MatWentToClinic = (MatWentToClinic1.IsChecked == true) ? true : false;

            fol.MatReReferToClinic1 = (MatReReferToClinic11.IsChecked == true) ? true : false;

            fol.MatReferralNo1 = txtMatReferralNo1.Text;

            fol.MatIsItPosibleYouArePregnent = (MatIsItPosibleYouArePregnent1.IsChecked == true) ? true : false;

            fol.MatPregnancyTestDone = (MatPregnancyTestDone1.IsChecked == true) ? true : false;

            fol.MatResult = ((ComboBoxItem)comboMatResult.SelectedItem).Content.ToString();

            fol.MatReferredToClinic2 = (MatReferredToClinic21.IsChecked == true) ? true : false;

            fol.MatReferralNo2 = txtMatReferralNo2.Text;

            fol.MatDateOf1stANC = txtMatDateOf1stANC.Text.ToString();

            fol.MatDateOfLastANC = txtMatDateOfLastANC.Text.ToString();

            fol.MatReferredToClinic3 = (MatReferredToClinic31.IsChecked == true) ? true : false;

            //fol.MatReferralNo3 

            fol.MatRegisteredForMoMConnect = (MatRegisteredForMoMConnect1.IsChecked == true) ? true : false;

            fol.MatDateOfNextANC = txtMatDateOfNextANC.Text.ToString();

            fol.MatReferToClinic = (MatReferToClinic1.IsChecked == true) ? true : false;

            //fol.MatReferralNo4 

            fol.MatExpectedDateOfDelivery = txtMatExpectedDateOfDelivery.Text.ToString();

            fol.MatIntendBreastfeed = (MatIntendBreastfeed1.IsChecked == true) ? true : false;

            fol.MatIntendFormulaFeed = (MatIntendFormulaFeed1.IsChecked == true) ? true : false;

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
            fol.ChildDateOfVisit = txtChildDateOfVisit.Text.ToString();

            fol.ChildARVsConcern = (ChildARVsConcern1.IsChecked == true) ? true : false;

            fol.ChildReferToClinic1 = (ChildReferToClinic11.IsChecked == true) ? true : false;

            fol.ChildReferralNo1 = txtChildReferralNo1.Text;

            fol.ChildWalkAppropriateForAge = (ChildWalkAppropriateForAge1.IsChecked == true) ? true : false;

            fol.ChildTalkAppropriateForAge = (ChildTalkAppropriateForAge1.IsChecked == true) ? true : false;

            fol.ChildReferToClinic2 = (ChildReferToClinic21.IsChecked == true) ? true : false;

            fol.ChildReferralNo2 = txtChildReferralNo2.Text;

            fol.ChildChildAssisted = (ChildChildAssisted1.IsChecked == true) ? true : false;

            fol.ChildReReferToSD = (ChildReReferToSD1.IsChecked == true) ? true : false;

            fol.ChildReferralNo3 = txtChildReferralNo3.Text;

            fol.ChildListConcernsReChild = txtChildListConcernsReChild.Text;

            fol.ChildReferToClinic3 = (ChildReferToClinic31.IsChecked == true) ? true : false;

            fol.ChildreferToSD = (ChildReReferToSD1.IsChecked == true) ? true : false;

            fol.ChildReferralNo4 = txtChildReferralNo4.Text;

            fol.ChildChildWithRTHC = (ChildChildWithRTHC1.IsChecked == true) ? true : false;

            fol.ChildReferToClinic4 = (ChildReferToClinic41.IsChecked == true) ? true : false;

            fol.ChildReferralNo5 = txtChildReferralNo5.Text;

            fol.ChildMotherTHVPositive = (ChildMotherTHVPositive1.IsChecked == true) ? true : false;

            fol.ChildChildBreastfed = (ChildChildBreastfed1.IsChecked == true) ? true : false;

            fol.ChildHowLong = txtChildHowLong.Text;

            fol.ChildClildEverOnNevirapine = (ChildClildEverOnNevirapine1.IsChecked == true) ? true : false;

            fol.ChildReferToClinic5 = (ChildReferToClinic51.IsChecked == true) ? true : false;

            fol.ChildReferralNo6 = txtChildReferralNo6.Text;

            fol.ChildHowPCRHasDone = (ChildHowPCRHasDone1.IsChecked == true) ? true : false;

            fol.ChildReferToClinic6 = (ChildReferToClinic61.IsChecked == true) ? true : false;

            fol.ChildReferralNo7 = txtChildReferralNo7.Text;

            fol.ChildImmunisationUpToDate = (ChildImmunisationUpToDate1.IsChecked == true) ? true : false;

            fol.ChildWhichImmunisationsOutStanding = ((ComboBoxItem)comboChildWhichImmunisationsOutStanding.SelectedItem).Content.ToString();

            fol.ChildVITAandWormMedsGivenEachMonth = (ChildVITAandWormMedsGivenEachMonth1.IsChecked == true) ? true : false;


            fol.ChildReferToClinic7 = (ChildReferToClinic71.IsChecked == true) ? true : false;

            fol.ChildReferralNo8 = txtChildReferralNo8.Text;

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
            fol.OtherDateOfVisit = txtOtherDateOfVisit.Text;

            fol.OtherWentToClinic = (OtherWentToClinic1.IsChecked == true) ? true : false;

            fol.OtherReReferToClinic = (OtherReferToClinic11.IsChecked == true) ? true : false;

            fol.OtherReferralNo1 = txtOtherReferralNo1.Text;

            fol.OtherConditionTha = txtOtherConditionTha.Text;

            fol.OtherReferToClinic1 = (OtherReferToClinic11.IsChecked == true) ? true : false;

            fol.OtherReferralNo2 = txtOtherReferralNo2.Text;

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
    


    }


    }
}
