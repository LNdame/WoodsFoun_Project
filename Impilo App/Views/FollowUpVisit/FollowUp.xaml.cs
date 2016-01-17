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

        
private void btnEditCountry_Click(object sender, RoutedEventArgs e)
        {
            FollowUp fol = new FollowUp();

            #region
            fol.FollowUpID = folID;
            fol.DateofScreen =
            fol.VisitNextVisit =
            fol.VisitOutCome =
            fol.VisitHPT = (radVisHptYes.IsChecked == true) ? true : false;
            fol.VisitDiabetes = (radVisHptYes.IsChecked == true) ? true : false;
            fol.VisitEpilepsy = (radVisHptYes.IsChecked == true) ? true : false;
            fol.VisitHIV = (radVisHptYes.IsChecked == true) ? true : false;
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

                com.Parameters.AddWithValue("@", fol.ScreeningID);//param

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

            #region Hypertension

            fol.HyperWentToClinic1 = (radHyClinYes.IsChecked == true) ? true : false;
            fol.HyperReReferToClinic1 = (radHyRefClin1Yes.IsChecked == true) ? true : false;
            fol.HyperReferralNo1 = txtHyRefNo1.Text;
            fol.HyperCurrentlyOnMeds = (radHyCurMedsYes.IsChecked == true) ? true : false; 
            fol.HyperStartDate =  ;
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

                com.Parameters.AddWithValue("@", fol.ScreeningID);//param

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

            Diabetes dia = new Diabetes
            {
                ScreeningID = scrID,

                fol.DiaReReferToClinic1 = (radDiaReRefClinYes.IsChecked == true) ? true : false;
                fol.DiaReferralNo1 = txtDiaRefNo1.Text;
                fol.DiaCurrentlyOnMeds = (radDiaCurMedsYes.IsChecked == true) ? true : false;
            fol.DiaStartDate =
           fol.DiaScreenTestReading1 = txtDiaScrReading.Text;
            fol.DiaFollowUpTestReading1 = txtDiaFolReading.Text;
            fol.DiaReferToClinic2 = (radDiaRefClinYes.IsChecked == true) ? true : false;

            fol.DiaReferralNo2 = txtDiaRefNo2.Text;
                 fol.DiaCheckReading
                 fol.DiaMedication
                 fol.DiaFollowUpTestReading3

            };

            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

                com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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

                 EpiWentToClinic
                 EpiReReferToClinic1
                 EpiReferralNo1
                 EpiFitInLastMonth
                 EpiReferToClinic
                 EpiCurrentlyOnMeds
                 EpiStartDate
                 EpiMoreThan3FitsInLastMonth
                 EpiReReferToClinic2
                 EpiReferralNo2
                 EpiMedication

                //sp place
                //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

                com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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
        fol.AsDateOfVisit 
        fol.AsWentToClinic 
        fol.AsReReferToClinic1 
        fol.AsReferralNo1 
        fol.AsFitInLastMonth 
        fol.AsReferToClinic 
        fol.AsReferralNo2 
        fol.AsCurrentlyOnMeds 
        fol.AsStartDate 
        fol.AsIncreasedNoOfAsthmaAttacks 
        fol.AsReReferToClinic2 
        fol.AsReferralNo3 
        fol.AsMedication

                    //sp place
                    //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

                com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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
            HIV_Tab hivTab = new HIV_Tab
            {
                ScreeningID = scrID,
                YearOfDiagnosis = ((ComboBoxItem)cboDiabeYear.SelectedItem).Content.ToString(),
                OnMeds = (radonMeds.IsChecked == true) ? true : false,
                AdherenceOK = (radadh.IsChecked == true) ? true : false,
                ReferToClinic = (hivref.IsChecked == true) ? true : false,
                ReferralNo = txtHIVRef.Text,
                ARVFileNo = txtARVFile.Text,

            };



            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

                com.Parameters.AddWithValue("@", dia.ScreeningID);//param

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

        
        
         
         TBDateOfVisit 
         TBARVsConcern 
         TBReferToClinic1 
         TBReferralNo1 
         TBRecentUnplannedLoseOfWeight 
         TBExcessiveSweatingAtNight 
         TBFeverOver2Weeks 
         TBCoughMoreThan2Week 
         TBLossOfApetite 
         TBReferredToClinic2 
         TBReferralNo2 
         TBResult 
         TBNewlyDiagnosed 
         TBStartDate 
         TBReferTBContactsToClinic 
         TBPreviouslyOnMeds 
         TBFinishDate 
         TBConcerns 
         TBReferToClinic3 
         TBReferralNo3 
         TBMedication 
         MatDateOfVisit 
         MatWentToClinic 

         MatReReferToClinic1 

         MatReferralNo1 

         MatIsItPosibleYouArePregnent 

         MatPregnancyTestDone 

         MatResult 

         MatReferredToClinic2 

         MatReferralNo2 

         MatDateOf1stANC 

         MatDateOfLastANC 

         MatReferredToClinic3 

         MatReferralNo3 

         MatRegisteredForMoMConnect 

         MatDateOfNextANC 

         MatReferToClinic 

         MatReferralNo4 

         MatExpectedDateOfDelivery 

         MatIntendBreastfeed 

         MatIntendFormulaFeed 

         ChildDateOfVisit 

         ChildARVsConcern 

         ChildReferToClinic1 

         ChildReferralNo1 

         ChildWalkAppropriateForAge 

         ChildTalkAppropriateForAge 

         ChildReferToClinic2 

         ChildReferralNo2 

         ChildChildAssisted 

         ChildReReferToSD 

         ChildReferralNo3 

         ChildListConcernsReChild 

         ChildReferToClinic3 

         ChildreferToSD 

         ChildReferralNo4 

         ChildChildWithRTHC 

         ChildReferToClinic4 

         ChildReferralNo5 

         ChildMotherTHVPositive 

         ChildChildBreastfed 

         ChildHowLong 

         ChildClildEverOnNevirapine 

         ChildReferToClinic5 

         ChildReferralNo6 

         ChildHowPCRHasDone 

         ChildReferToClinic6 

         ChildReferralNo7 

         ChildImmunisationUpToDate 

         ChildWhichImmunisationsOutStanding 

         ChildVITAandWormMedsGivenEachMonth 

         ChildReferToClinic7 

         ChildReferralNo8 

         OtherDateOfVisit 

         OtherWentToClinic 

         OtherReReferToClinic 

         OtherReferralNo1 

         OtherConditionTha 

         OtherReferToClinic1 

         OtherReferralNo2 

         FollowUpIDNumber 

         HIVDateOfVisit 

         HIVWentToClinic 

         HIVRereferToClinic 

         HIVReferralNo1 

         HIVReferToClinic1 

         HIVReferralNo2 

         HIVStatus 

         HIVOnARVs 

         HIVStartDate1 

         HIVAdherenceOK 

         HIVConcerns 

         HIVReferToClinic2 

         HIVReferralNo3 

         HIVARVsConsern 

         HIVReferToClinic3 

         HIVReferralNo4 

         HIVTestingDone 

         HIVTestDone 

         HIVTestResults 

         HIVReferToClinic4 

         HIVReferralNo5 

         HIVMedication 

    }
    }
	        

    }
}
