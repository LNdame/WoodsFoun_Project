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

        //DAL da = new DAL();
        public FollowUp()
        {
            InitializeComponent();
        }
      
        private void btnCreateFollowUp_Click(object sender, RoutedEventArgs e)
        {
            #region old code
            //Follow follow = new Follow();
            ////Bio
            //follow.FollowUpIDNumber = ((ComboBoxItem)comboFollowUpIDNumber.SelectedItem).Content.ToString();
            //follow.DateofScreen = txtDateofScreen.Text;

            ////Visit Details
            //follow.VisitNextVisit = txtVisitNextVisit.Text;
            //follow.VisitOutCome = ((ComboBoxItem)comboVisitOutCome.SelectedItem).Content.ToString();
            //string VisitHPT;
            //if (VisitHPT1.IsChecked == true)
            //{
            //    VisitHPT = "yes";
            //}
            //else if (VisitHPT2.IsChecked == true)
            //{
            //    VisitHPT = "no";
            //}
            //else
            //{
            //     VisitHPT = "no data";
            //}

            //follow.VisitHPT = VisitHPT;

            //string VisitDiabetes;
            //if (VisitDiabetes1.IsChecked == true)
            //{
            //    VisitDiabetes = "yes";
            //}
            //else if (VisitDiabetes2.IsChecked == true)
            //{
            //    VisitDiabetes = "no";
            //}
            //else
            //{
            //    VisitDiabetes = "no data";
            //}
            //follow.VisitDiabetes = VisitDiabetes;

            //string VisitEpilepsy;
            //if (VisitEpilepsy1.IsChecked == true)
            //{
            //    VisitEpilepsy = "yes";
            //}
            //else if (VisitEpilepsy2.IsChecked == true)
            //{
            //    VisitEpilepsy = "no";
            //}
            //else
            //{
            //    VisitEpilepsy = "no data";
            //}
            //follow.VisitEpilepsy = VisitEpilepsy;


            //string VisitHIV;
            //if (VisitHIV1.IsChecked == true)
            //{
            //    VisitHIV = "yes";
            //}
            //else if (VisitHIV2.IsChecked == true)
            //{
            //    VisitHIV = "no";
            //}
            //else
            //{
            //    VisitHIV = "no data";
            //}

            //follow.VisitHIV = VisitHIV;


            //string VisitTB;
            //if (VisitTB1.IsChecked == true)
            //{
            //    VisitTB = "yes";
            //}
            //else if (VisitTB2.IsChecked == true)
            //{
            //    VisitTB = "no";
            //}
            //else
            //{
            //    VisitTB = "no data";
            //}

            //follow.VisitTB = VisitTB;

            //string VisitMatHealth;
            //if (VisitMatHealth1.IsChecked == true)
            //{
            //    VisitMatHealth = "yes";
            //}
            //else if (VisitMatHealth2.IsChecked == true)
            //{
            //    VisitMatHealth = "no";
            //}
            //else
            //{
            //    VisitMatHealth = "no data";
            //}

            //follow.VisitMatHealth = VisitMatHealth;

            //string VisitChildHealth;
            //if (VisitChildHealth1.IsChecked == true)
            //{
            //    VisitChildHealth = "yes";
            //}
            //else if (VisitChildHealth2.IsChecked == true)
            //{
            //    VisitChildHealth = "no";
            //}
            //else
            //{
            //    VisitChildHealth = "no data";
            //}

            //follow.VisitChildHealth = VisitChildHealth;
            //follow.VisitOther = txtVisitOther.Text;

            //string VisitDooortoDoor;
            //if (VisitDooortoDoor1.IsChecked == true)
            //{
            //    VisitDooortoDoor = "yes";
            //}
            //else if (VisitDooortoDoor2.IsChecked == true)
            //{
            //    VisitDooortoDoor = "no";
            //}
            //else
            //{
            //    VisitDooortoDoor = "no data";
            //}

            //follow.VisitDooortoDoor = VisitDooortoDoor;

            ////Hypertension
            //string HyperWentToClinic;
            //if (HyperWentToClinic11.IsChecked == true)
            //{
            //    HyperWentToClinic = "yes";
            //}
            //else if (HyperWentToClinic12.IsChecked == true)
            //{
            //    HyperWentToClinic = "no";
            //}
            //else
            //{
            //    HyperWentToClinic = "no data";
            //}

            //follow.HyperWentToClinic1 = HyperWentToClinic;

            //string HyperReReferToClinic;
            //if (HyperReReferToClinic11.IsChecked == true)
            //{
            //    HyperReReferToClinic = "yes";
            //}
            //else if (HyperReReferToClinic12.IsChecked == true)
            //{
            //    HyperReReferToClinic = "no";
            //}
            //else
            //{
            //    HyperReReferToClinic = "no data";
            //}

            //follow.HyperReReferToClinic1 = HyperReReferToClinic;
            //follow.HyperReferralNo1 = txtHyperReferralNo1.Text;

            //string HyperCurrentlyOnMeds;
            //if (HyperCurrentlyOnMeds1.IsChecked == true)
            //{
            //    HyperCurrentlyOnMeds = "yes";
            //}
            //else if (HyperCurrentlyOnMeds2.IsChecked == true)
            //{
            //    HyperCurrentlyOnMeds = "no";
            //}
            //else
            //{
            //    HyperCurrentlyOnMeds = "no data";
            //}
            //follow.HyperCurrentlyOnMeds = HyperCurrentlyOnMeds;
            //follow.HyperStartDate = txtHyperStartDate.Text;
            //follow.HyperBPReading = txtHyperBPReading.Text;
            //follow.HyperTodayTestReading = txtHyperFollowUpTestReading.Text;

            //string HyperReReferToClinic2;
            //if (HyperReReferToClinic21.IsChecked == true)
            //{
            //    HyperReReferToClinic2 = "yes";
            //}
            //else if (HyperReReferToClinic22.IsChecked == true)
            //{
            //    HyperReReferToClinic2 = "no";
            //}
            //else
            //{
            //    HyperReReferToClinic2 = "no data";
            //}
            //follow.HyperReReferToClinic2 = HyperReReferToClinic2;
            //follow.HyperReferralNo2 = txtHyperReferralNo2.Text;
            //follow.HyperCheckReading = txtHyperCheckReading.Text;
            //follow.HyperMedication = ((ComboBoxItem)comboHyperMedication.SelectedItem).Content.ToString();
            //string HyperReReferToClinic3;
            //if (HyperReReferToClinic31.IsChecked == true)
            //{
            //    HyperReReferToClinic3 = "yes";
            //}
            //else if (HyperReReferToClinic32.IsChecked == true)
            //{
            //    HyperReReferToClinic3 = "no";
            //}
            //else
            //{
            //    HyperReReferToClinic3 = "no data";
            //}
            //follow.HyperReReferToClinic3 = HyperReReferToClinic3;
            //follow.HyperReferralNo3 = txtHyperReferralNo3.Text;
            //follow.HyperFollowUpTestReading = txtHyperFollowUpTestReading.Text;

            ////Diabetes
            //string DiaWentToClinic;
            //if (DaiWentToClinic1.IsChecked == true)
            //{
            //    DiaWentToClinic = "yes";
            //}
            //else if (DaiWentToClinic2.IsChecked == true)
            //{
            //    DiaWentToClinic = "no";
            //}
            //else
            //{
            //    DiaWentToClinic = "no data";
            //}
            //follow.DiaWentToClinic = DiaWentToClinic;

            //string DiaReReferToClinic;
            //if (DiaReReferToClinic11.IsChecked == true)
            //{
            //    DiaReReferToClinic = "yes";
            //}
            //else if (DiaReReferToClinic12.IsChecked == true)
            //{
            //    DiaReReferToClinic = "no";
            //}
            //else
            //{
            //    DiaReReferToClinic = "no data";
            //}
            //follow.DiaReReferToClinic1 = DiaReReferToClinic;
            //follow.DiaReferralNo1 = txtDiaReferralNo1.Text;

            //string DiaCurrentlyOnMeds;
            //if (DiaCurrentlyOnMeds1.IsChecked == true)
            //{
            //    DiaCurrentlyOnMeds = "yes";
            //}
            //else if (DiaCurrentlyOnMeds2.IsChecked == true)
            //{
            //    DiaCurrentlyOnMeds = "no";
            //}
            //else
            //{
            //    DiaCurrentlyOnMeds = "no data";
            //}
            //follow.DiaCurrentlyOnMeds = DiaCurrentlyOnMeds;
            //follow.DiaStartDate = txtDiaStartDate.Text;
            //follow.DiaFollowUpTestReading1 = txtDiaFollowUpTestReading1.Text;

            //string DiaReferToClinic;
            //if (DiaReferToClinic21.IsChecked == true)
            //{
            //    DiaReferToClinic = "yes";
            //}
            //else if (DiaReferToClinic22.IsChecked == true)
            //{
            //    DiaReferToClinic = "no";
            //}
            //else
            //{
            //    DiaReferToClinic = "no data";
            //}
            //follow.DiaReferToClinic2 = DiaReferToClinic;
            //follow.DiaReferralNo2 = txtDiaReferralNo2.Text;
            //follow.DiaCheckReading = txtDiaCheckReading.Text;
            //follow.DiaMedication = ((ComboBoxItem)comboDiaMedication.SelectedItem).Content.ToString();
            //string DiaReReferToClinics;
            //if (DiaReReferToClinic31.IsChecked == true)
            //{
            //    DiaReReferToClinics = "yes";
            //}
            //else if (DiaReReferToClinic32.IsChecked == true)
            //{
            //    DiaReReferToClinics = "no";
            //}
            //else
            //{
            //    DiaReReferToClinics = "no data";
            //}
            //follow.DiaReReferToClinic3 = DiaReReferToClinics;
            //follow.DiaReferralNo3 = txtDiaReferralNo3.Text;
            //follow.DiaFollowUpTestReading3 = txtDiaFollowUpTestReading3.Text;

            ////Epilepsy
            //string EpiWentToClinic;
            //if (EpiWentToClinic1.IsChecked == true)
            //{
            //    EpiWentToClinic = "yes";
            //}
            //else if (EpiWentToClinic2.IsChecked == true)
            //{
            //    EpiWentToClinic = "no";
            //}
            //else
            //{
            //    EpiWentToClinic = "no data";
            //}

            //follow.EpiWentToClinic = EpiWentToClinic;

            //string EpiReReferToClinic1;
            //if (EpiReReferToClinic11.IsChecked == true)
            //{
            //    EpiReReferToClinic1 = "yes";
            //}
            //else if (EpiReReferToClinic12.IsChecked == true)
            //{
            //    EpiReReferToClinic1 = "no";
            //}
            //else
            //{
            //    EpiReReferToClinic1 = "no data";
            //}

            //follow.EpiReReferToClinic1 = EpiReReferToClinic1;
            //follow.EpiReferralNo1 = txtEpiReferralNo1.Text;

            //string EpiFitInLastMonth;
            //if (EpiFitInLastMonth1.IsChecked == true)
            //{
            //    EpiFitInLastMonth = "yes";
            //}
            //else if (EpiFitInLastMonth2.IsChecked == true)
            //{
            //    EpiFitInLastMonth = "no";
            //}
            //else
            //{
            //    EpiFitInLastMonth = "no data";
            //}

            //follow.EpiFitInLastMonth = EpiFitInLastMonth;

            //string EpiReferToClinic;
            //if (EpiReferToClinic1.IsChecked == true)
            //{
            //    EpiReferToClinic = "yes";
            //}
            //else if (EpiReferToClinic2.IsChecked == true)
            //{
            //    EpiReferToClinic = "no";
            //}
            //else
            //{
            //    EpiReferToClinic = "no data";
            //}

            //follow.EpiReferToClinic = EpiReferToClinic;


            //string EpiCurrentlyOnMeds;
            //if (EpiCurrentlyOnMeds1.IsChecked == true)
            //{
            //    EpiCurrentlyOnMeds = "yes";
            //}
            //else if (EpiCurrentlyOnMeds2.IsChecked == true)
            //{
            //    EpiCurrentlyOnMeds = "no";
            //}
            //else
            //{
            //    EpiCurrentlyOnMeds = "no data";
            //}

            //follow.EpiCurrentlyOnMeds = EpiCurrentlyOnMeds;
            //follow.EpiStartDate = txtEpiStartDate.Text;

            //string EpiMoreThan3FitsInLastMonth;
            //if (EpiMoreThan3FitsInLastMonth1.IsChecked == true)
            //{
            //    EpiMoreThan3FitsInLastMonth = "yes";
            //}
            //else if (EpiMoreThan3FitsInLastMonth2.IsChecked == true)
            //{
            //    EpiMoreThan3FitsInLastMonth = "no";
            //}
            //else
            //{
            //    EpiMoreThan3FitsInLastMonth = "no data";
            //}

            //follow.EpiMoreThan3FitsInLastMonth = EpiMoreThan3FitsInLastMonth;


            //string EpiReReferToClinic2;
            //if (EpiReReferToClinic21.IsChecked == true)
            //{
            //    EpiReReferToClinic2 = "yes";
            //}
            //else if (EpiReReferToClinic22.IsChecked == true)
            //{
            //    EpiReReferToClinic2 = "no";
            //}
            //else
            //{
            //    EpiReReferToClinic2 = "no data";
            //}

            //follow.EpiReReferToClinic2 = EpiReReferToClinic2;
            //follow.EpiReferralNo2 = txtEpiReferralNo2.Text;
            //follow.EpiMedication = ((ComboBoxItem)comboEpiMedication.SelectedItem).Content.ToString();
            ////Asthma

            //follow.AsDateOfVisit = txtAsDateOfVisit.Text;
            //string AsWentToClinic;
            //if (AsWentToClinic1.IsChecked == true)
            //{
            //    AsWentToClinic = "yes";
            //}
            //else if (AsWentToClinic2.IsChecked == true)
            //{
            //    AsWentToClinic = "no";
            //}
            //else
            //{
            //    AsWentToClinic = "no data";
            //}

            //follow.AsWentToClinic = AsWentToClinic;

            //string AsReReferToClinic1;
            //if (AsReReferToClinic11.IsChecked == true)
            //{
            //    AsReReferToClinic1 = "yes";
            //}
            //else if (AsReReferToClinic12.IsChecked == true)
            //{
            //    AsReReferToClinic1 = "no";
            //}
            //else
            //{
            //    AsReReferToClinic1 = "no data";
            //}

            //follow.AsReReferToClinic1 = AsReReferToClinic1;
            //follow.AsReferralNo1 = txtAsReferralNo1.Text;

            //string AsFitInLastMonth;
            //if (AsFitInLastMonth1.IsChecked == true)
            //{
            //    AsFitInLastMonth = "yes";
            //}
            //else if (AsFitInLastMonth2.IsChecked == true)
            //{
            //    AsFitInLastMonth = "no";
            //}
            //else
            //{
            //    AsFitInLastMonth = "no data";
            //}

            //follow.AsFitInLastMonth = AsFitInLastMonth;

            //string AsReferToClinic;
            //if (AsReferToClinic1.IsChecked == true)
            //{
            //    AsReferToClinic = "yes";
            //}
            //else if (AsReferToClinic2.IsChecked == true)
            //{
            //    AsReferToClinic = "no";
            //}
            //else
            //{
            //    AsReferToClinic = "no data";
            //}

            //follow.AsReferToClinic = AsReferToClinic;
            //follow.AsReferralNo2 = txtAsReferralNo2.Text;

            //string AsCurrentlyOnMeds;
            //if (AsCurrentlyOnMeds1.IsChecked == true)
            //{
            //    AsCurrentlyOnMeds = "yes";
            //}
            //else if (AsCurrentlyOnMeds2.IsChecked == true)
            //{
            //    AsCurrentlyOnMeds = "no";
            //}
            //else
            //{
            //    AsCurrentlyOnMeds = "no data";
            //}

            //follow.AsCurrentlyOnMeds = AsCurrentlyOnMeds;
            //follow.AsStartDate = txtAsStartDate.Text;

            //string AsIncreasedNoOfAsthmaAttacks;
            //if (AsIncreasedNoOfAsthmaAttacks1.IsChecked == true)
            //{
            //    AsIncreasedNoOfAsthmaAttacks = "yes";
            //}
            //else if (AsIncreasedNoOfAsthmaAttacks2.IsChecked == true)
            //{
            //    AsIncreasedNoOfAsthmaAttacks = "no";
            //}
            //else
            //{
            //    AsIncreasedNoOfAsthmaAttacks = "no data";
            //}

            //follow.AsIncreasedNoOfAsthmaAttacks = AsIncreasedNoOfAsthmaAttacks;

            //string AsReReferToClinic2;
            //if (AsReReferToClinic21.IsChecked == true)
            //{
            //    AsReReferToClinic2 = "yes";
            //}
            //else if (AsReReferToClinic22.IsChecked == true)
            //{
            //    AsReReferToClinic2 = "no";
            //}
            //else
            //{
            //    AsReReferToClinic2 = "no data";
            //}

            //follow.AsReReferToClinic2 = AsReReferToClinic2;
            //follow.AsReferralNo3 = txtAsReferralNo3.Text;
            //follow.AsMedication = ((ComboBoxItem)comboAsMedication.SelectedItem).Content.ToString();

            ////HIV
            //follow.HIVDateOfVisit = txtHIVDateOfVisit.Text;
            //follow.HIVReferralNo1 = txtHIVReferralNo1.Text;
            //string HIVWentToClinic;
            //if (HIVWentToClinic1.IsChecked == true)
            //{
            //    HIVWentToClinic = "yes";
            //}
            //else if (HIVWentToClinic2.IsChecked == true)
            //{
            //    HIVWentToClinic = "no";
            //}
            //else
            //{
            //    HIVWentToClinic = "no data";
            //}

            //follow.HIVWentToClinic = HIVWentToClinic;

            //string HIVRereferToClinic;
            //if (HIVReReferToClinic1.IsChecked == true)
            //{
            //    HIVRereferToClinic = "yes";
            //}
            //else if (HIVReReferToClinic2.IsChecked == true)
            //{
            //    HIVRereferToClinic = "no";
            //}
            //else
            //{
            //    HIVRereferToClinic = "no data";
            //}

            //follow.HIVRereferToClinic = HIVRereferToClinic;

            //string HIVReferToClinic1;
            //if (HIVReferToClinic11.IsChecked == true)
            //{
            //    HIVReferToClinic1 = "yes";
            //}
            //else if (HIVReferToClinic12.IsChecked == true)
            //{
            //    HIVReferToClinic1 = "no";
            //}
            //else
            //{
            //    HIVReferToClinic1 = "no data";
            //}

            //follow.HIVReferToClinic1 = HIVReferToClinic1;

            //follow.HIVReferralNo2 = txtHIVReferralNo2.Text;
            //follow.HIVStatus = ((ComboBoxItem)comboHIVStatus.SelectedItem).Content.ToString();

            //string HIVOnARVs;
            //if (HIVOnARVs1.IsChecked == true)
            //{
            //    HIVOnARVs = "yes";
            //}
            //else if (HIVOnARVs2.IsChecked == true)
            //{
            //    HIVOnARVs = "no";
            //}
            //else
            //{
            //    HIVOnARVs = "no data";
            //}

            //follow.HIVOnARVs = HIVOnARVs;
            //follow.HIVStartDate1 = txtHIVStartDate1.Text;
            //string HIVAdherenceOK;
            //if (HIVAdherenceOK1.IsChecked == true)
            //{
            //    HIVAdherenceOK = "yes";
            //}
            //else if (HIVAdherenceOK2.IsChecked == true)
            //{
            //    HIVAdherenceOK = "no";
            //}
            //else
            //{
            //    HIVAdherenceOK = "no data";
            //}

            //follow.HIVAdherenceOK = HIVAdherenceOK;

            //string HIVConcerns;
            //if (HIVConcerns1.IsChecked == true)
            //{
            //    HIVConcerns = "yes";
            //}
            //else if (HIVConcerns2.IsChecked == true)
            //{
            //    HIVConcerns = "no";
            //}
            //else
            //{
            //    HIVConcerns = "no data";
            //}

            //follow.HIVConcerns = HIVConcerns;

            //string HIVReferToClinic2;
            //if (HIVReferToClinic21.IsChecked == true)
            //{
            //    HIVReferToClinic2 = "yes";
            //}
            //else if (HIVReferToClinic22.IsChecked == true)
            //{
            //    HIVReferToClinic2 = "no";
            //}
            //else
            //{
            //    HIVReferToClinic2 = "no data";
            //}

            //follow.HIVReferToClinic2 = HIVReferToClinic2;
            //follow.HIVReferralNo3 = txtHIVReferralNo3.Text;

            //string HIVARVsConsern;
            //if (HIVARVsConcerns1.IsChecked == true)
            //{
            //    HIVARVsConsern = "yes";
            //}
            //else if (HIVARVsConcerns2.IsChecked == true)
            //{
            //    HIVARVsConsern = "no";
            //}
            //else
            //{
            //    HIVARVsConsern = "no data";
            //}

            //follow.HIVARVsConsern = HIVARVsConsern;

            //string HIVReferToClinic3;
            //if (HIVReferToClinic31.IsChecked == true)
            //{
            //    HIVReferToClinic3 = "yes";
            //}
            //else if (HIVReferToClinic32.IsChecked == true)
            //{
            //    HIVReferToClinic3 = "no";
            //}
            //else
            //{
            //    HIVReferToClinic3 = "no data";
            //}

            //follow.HIVReferToClinic3 = HIVReferToClinic3;
            //follow.HIVReferralNo4 = txtHIVReferralNo4.Text;

            //string HIVTestingDone;
            //if (HIVTestingDone1.IsChecked == true)
            //{
            //    HIVTestingDone = "yes";
            //}
            //else if (HIVTestingDone2.IsChecked == true)
            //{
            //    HIVTestingDone = "no";
            //}
            //else
            //{
            //    HIVTestingDone = "no data";
            //}

            //follow.HIVTestingDone = HIVTestingDone;

            //string HIVTestDone;
            //if (HIVHIVTestDone1.IsChecked == true)
            //{
            //    HIVTestDone = "yes";
            //}
            //else if (HIVHIVTestDone2.IsChecked == true)
            //{
            //    HIVTestDone = "no";
            //}
            //else
            //{
            //    HIVTestDone = "no data";
            //}

            //follow.HIVTestDone = HIVTestDone;
            //follow.HIVTestResults = txtHIVTestResults.Text;

            //string HIVReferToClinic4;
            //if (HIVReferToClinic41.IsChecked == true)
            //{
            //    HIVReferToClinic4 = "yes";
            //}
            //else if (HIVReferToClinic42.IsChecked == true)
            //{
            //    HIVReferToClinic4 = "no";
            //}
            //else
            //{
            //    HIVReferToClinic4 = "no data";
            //}

            //follow.HIVReferToClinic4 = HIVReferToClinic4;
            //follow.HIVReferralNo5 = txtHIVReferralNo5.Text;
            //follow.HIVMedication = ((ComboBoxItem)HIVMedication.SelectedItem).Content.ToString();

            ////TB
            //follow.TBDateOfVisit = txtTBDateOfVisit.Text;
            //string TBARVsConcern;
            //if (TBARVsConcern1.IsChecked == true)
            //{
            //    TBARVsConcern = "yes";
            //}
            //else if (TBARVsConcern2.IsChecked == true)
            //{
            //    TBARVsConcern = "no";
            //}
            //else
            //{
            //    TBARVsConcern = "no data";
            //}

            //follow.TBARVsConcern = TBARVsConcern;

            //string TBReferToClinic1;
            //if (TBReferToClinic11.IsChecked == true)
            //{
            //    TBReferToClinic1 = "yes";
            //}
            //else if (TBReferToClinic12.IsChecked == true)
            //{
            //    TBReferToClinic1 = "no";
            //}
            //else
            //{
            //    TBReferToClinic1 = "no data";
            //}

            //follow.TBReferToClinic1 = TBReferToClinic1;
            //follow.TBReferralNo1 = txtTBReferralNo1.Text;

            //string TBRecentUnplannedLoseOfWeight;
            //if (TBRecentUnplannedLoseOfWeight1.IsChecked == true)
            //{
            //    TBRecentUnplannedLoseOfWeight = "yes";
            //}
            //else if (TBRecentUnplannedLoseOfWeight2.IsChecked == true)
            //{
            //    TBRecentUnplannedLoseOfWeight = "no";
            //}
            //else
            //{
            //    TBRecentUnplannedLoseOfWeight = "no data";
            //}

            //follow.TBRecentUnplannedLoseOfWeight = TBRecentUnplannedLoseOfWeight;

            //string TBExcessiveSweatingAtNight;
            //if (TBExcessiveSweatingAtNight1.IsChecked == true)
            //{
            //    TBExcessiveSweatingAtNight = "yes";
            //}
            //else if (TBExcessiveSweatingAtNight2.IsChecked == true)
            //{
            //    TBExcessiveSweatingAtNight = "no";
            //}
            //else
            //{
            //    TBExcessiveSweatingAtNight = "no data";
            //}

            //follow.TBExcessiveSweatingAtNight = TBExcessiveSweatingAtNight;

            //string TBFeverOver2Weeks;
            //if (TBFeverOver2Weeks1.IsChecked == true)
            //{
            //    TBFeverOver2Weeks = "yes";
            //}
            //else if (TBFeverOver2Weeks2.IsChecked == true)
            //{
            //    TBFeverOver2Weeks = "no";
            //}
            //else
            //{
            //    TBFeverOver2Weeks = "no data";
            //}

            //follow.TBFeverOver2Weeks = TBFeverOver2Weeks;

            //string TBCoughMoreThan2Week;
            //if (TBCoughMoreThan2Week1.IsChecked == true)
            //{
            //    TBCoughMoreThan2Week = "yes";
            //}
            //else if (TBCoughMoreThan2Week2.IsChecked == true)
            //{
            //    TBCoughMoreThan2Week = "no";
            //}
            //else
            //{
            //    TBCoughMoreThan2Week = "no data";
            //}

            //follow.TBCoughMoreThan2Week = TBCoughMoreThan2Week;

            //string TBLossOfApetite;
            //if (TBLossOfApetite1.IsChecked == true)
            //{
            //    TBLossOfApetite = "yes";
            //}
            //else if (TBLossOfApetite2.IsChecked == true)
            //{
            //    TBLossOfApetite = "no";
            //}
            //else
            //{
            //    TBLossOfApetite = "no data";
            //}

            //follow.TBLossOfApetite = TBLossOfApetite;

            //string TBReferredToClinic2;
            //if (TBReferredToClinic21.IsChecked == true)
            //{
            //    TBReferredToClinic2 = "yes";
            //}
            //else if (TBReferredToClinic22.IsChecked == true)
            //{
            //    TBReferredToClinic2 = "no";
            //}
            //else
            //{
            //    TBReferredToClinic2 = "no data";
            //}

            //follow.TBReferredToClinic2 = TBReferredToClinic2;
            //follow.TBReferralNo2 = txtTBReferralNo2.Text;
            //follow.TBResult = ((ComboBoxItem)comboTBResult.SelectedItem).Content.ToString();

            //string TBNewlyDiagnosed;
            //if (TBNewlyDiagnosed1.IsChecked == true)
            //{
            //    TBNewlyDiagnosed = "yes";
            //}
            //else if (TBNewlyDiagnosed2.IsChecked == true)
            //{
            //    TBNewlyDiagnosed = "no";
            //}
            //else
            //{
            //    TBNewlyDiagnosed = "no data";
            //}

            //follow.TBNewlyDiagnosed = TBNewlyDiagnosed;
            //follow.TBStartDate = txtTBStartDate.Text;

            //string TBReferTBContactsToClinic;
            //if (TBReferTBContactsToClinic1.IsChecked == true)
            //{
            //    TBReferTBContactsToClinic = "yes";
            //}
            //else if (TBReferTBContactsToClinic2.IsChecked == true)
            //{
            //    TBReferTBContactsToClinic = "no";
            //}
            //else
            //{
            //    TBReferTBContactsToClinic = "no data";
            //}

            //follow.TBReferTBContactsToClinic = TBReferTBContactsToClinic;

            //string TBPreviouslyOnMeds;
            //if (TBPreviouslyOnMeds1.IsChecked == true)
            //{
            //    TBPreviouslyOnMeds = "yes";
            //}
            //else if (TBPreviouslyOnMeds2.IsChecked == true)
            //{
            //    TBPreviouslyOnMeds = "no";
            //}
            //else
            //{
            //    TBPreviouslyOnMeds = "no data";
            //}

            //follow.TBPreviouslyOnMeds = TBPreviouslyOnMeds;
            //follow.TBFinishDate = txtTBFinishDate.Text;

            //string TBConcerns;
            //if (TBConcerns1.IsChecked == true)
            //{
            //    TBConcerns = "yes";
            //}
            //else if (TBConcerns2.IsChecked == true)
            //{
            //    TBConcerns = "no";
            //}
            //else
            //{
            //    TBConcerns = "no data";
            //}

            //follow.TBConcerns = TBConcerns;

            //string TBReferToClinic3;
            //if (TBReferToClinic31.IsChecked == true)
            //{
            //    TBReferToClinic3 = "yes";
            //}
            //else if (TBReferToClinic32.IsChecked == true)
            //{
            //    TBReferToClinic3 = "no";
            //}
            //else
            //{
            //    TBReferToClinic3 = "no data";
            //}

            //follow.TBReferToClinic3 = TBReferToClinic3;
            //follow.TBReferralNo3 = txtTBReferralNo3.Text;
            //follow.TBMedication = ((ComboBoxItem)comboTBMedication.SelectedItem).Content.ToString();

            ////MatHealth
            //follow.MatDateOfVisit = txtMatDateOfVisit.Text;
            //string MatReReferToClinic1;
            //if (MatReReferToClinic11.IsChecked == true)
            //{
            //    MatReReferToClinic1 = "yes";
            //}
            //else if (MatReReferToClinic12.IsChecked == true)
            //{
            //    MatReReferToClinic1 = "no";
            //}
            //else
            //{
            //    MatReReferToClinic1 = "no data";
            //}

            //follow.MatReReferToClinic1 = MatReReferToClinic1;
            //follow.AsReferralNo1 = txtMatReferralNo1.Text;

            //string MatIsItPosibleYouArePregnent;
            //if (MatIsItPosibleYouArePregnent1.IsChecked == true)
            //{
            //    MatIsItPosibleYouArePregnent = "yes";
            //}
            //else if (MatIsItPosibleYouArePregnent2.IsChecked == true)
            //{
            //    MatIsItPosibleYouArePregnent = "no";
            //}
            //else
            //{
            //    MatIsItPosibleYouArePregnent = "no data";
            //}

            //follow.MatIsItPosibleYouArePregnent = MatIsItPosibleYouArePregnent;

            //string MatPregnancyTestDone;
            //if (MatPregnancyTestDone1.IsChecked == true)
            //{
            //    MatPregnancyTestDone = "yes";
            //}
            //else if (MatPregnancyTestDone2.IsChecked == true)
            //{
            //    MatPregnancyTestDone = "no";
            //}
            //else
            //{
            //    MatPregnancyTestDone = "no data";
            //}

            //follow.MatPregnancyTestDone = MatPregnancyTestDone;

            //string MatWentToClinic;
            //if (MatWentToClinic1.IsChecked == true)
            //{
            //    MatWentToClinic = "yes";
            //}
            //else if (MatWentToClinic2.IsChecked == true)
            //{
            //    MatWentToClinic = "no";
            //}
            //else
            //{
            //    MatWentToClinic = "no data";
            //}

            //follow.MatWentToClinic = MatWentToClinic;
            //follow.MatReferralNo1 = txtMatReferralNo1.Text;

            //follow.MatResult = ((ComboBoxItem)comboMatResult.SelectedItem).Content.ToString();

            //string MatReferredToClinic2;
            //if (MatReferredToClinic21.IsChecked == true)
            //{
            //    MatReferredToClinic2 = "yes";
            //}
            //else if (MatReferredToClinic22.IsChecked == true)
            //{
            //    MatReferredToClinic2 = "no";
            //}
            //else
            //{
            //    MatReferredToClinic2 = "no data";
            //}

            //follow.MatReferredToClinic2 = MatReferredToClinic2;
            //follow.MatReferralNo2 = txtMatReferralNo2.Text;
            //follow.MatDateOf1stANC = txtMatDateOf1stANC.Text;
            //follow.MatDateOfLastANC = txtMatDateOfLastANC.Text;

            //string MatReferredToClinic3;
            //if (MatReferredToClinic31.IsChecked == true)
            //{
            //    MatReferredToClinic3 = "yes";
            //}
            //else if (MatReferredToClinic32.IsChecked == true)
            //{
            //    MatReferredToClinic3 = "no";
            //}
            //else
            //{
            //    MatReferredToClinic3 = "no data";
            //}

            //follow.MatReferredToClinic3 = MatReferredToClinic3;
            //follow.MatReferralNo3 = txtMatReferralNo3.Text;
            //string MatRegisteredForMoMConnect;
            //if (MatRegisteredForMoMConnect1.IsChecked == true)
            //{
            //    MatRegisteredForMoMConnect = "yes";
            //}
            //else if (MatRegisteredForMoMConnect2.IsChecked == true)
            //{
            //    MatRegisteredForMoMConnect = "no";
            //}
            //else
            //{
            //    MatRegisteredForMoMConnect = "no data";
            //}

            //follow.MatRegisteredForMoMConnect = MatRegisteredForMoMConnect;
            //follow.MatDateOfNextANC = txtMatDateOfNextANC.Text;

            //string MatReferToClinic;
            //if (MatReferToClinic1.IsChecked == true)
            //{
            //    MatReferToClinic = "yes";
            //}
            //else if (MatReferToClinic2.IsChecked == true)
            //{
            //    MatReferToClinic = "no";
            //}
            //else
            //{
            //    MatReferToClinic = "no data";
            //}

            //follow.MatReferToClinic = MatReferToClinic;
            //follow.MatReferralNo4 = txtMatReferralNo4.Text;
            //follow.MatExpectedDateOfDelivery = txtMatExpectedDateOfDelivery.Text;

            //string MatIntendBreastfeed;
            //if (MatIntendBreastfeed1.IsChecked == true)
            //{
            //    MatIntendBreastfeed = "yes";
            //}
            //else if (MatIntendBreastfeed2.IsChecked == true)
            //{
            //    MatIntendBreastfeed = "no";
            //}
            //else
            //{
            //    MatIntendBreastfeed = "no data";
            //}

            //follow.MatIntendBreastfeed = MatIntendBreastfeed;

            //string MatIntendFormulaFeed;
            //if (MatIntendFormulaFeed1.IsChecked == true)
            //{
            //    MatIntendFormulaFeed = "yes";
            //}
            //else if (MatIntendFormulaFeed2.IsChecked == true)
            //{
            //    MatIntendFormulaFeed = "no";
            //}
            //else
            //{
            //    MatIntendFormulaFeed = "no data";
            //}

            //follow.MatIntendFormulaFeed = MatIntendFormulaFeed;

            ////Child Health 

            //follow.ChildDateOfVisit = txtChildDateOfVisit.Text;

            //string ChildARVsConcern;
            //if (ChildARVsConcern1.IsChecked == true)
            //{
            //    ChildARVsConcern = "yes";
            //}
            //else if (ChildARVsConcern2.IsChecked == true)
            //{
            //    ChildARVsConcern = "no";
            //}
            //else
            //{
            //    ChildARVsConcern = "no data";
            //}

            //follow.ChildARVsConcern = ChildARVsConcern;

            //string ChildReferToClinic1;
            //if (ChildReferToClinic11.IsChecked == true)
            //{
            //    ChildReferToClinic1 = "yes";
            //}
            //else if (ChildReferToClinic12.IsChecked == true)
            //{
            //    ChildReferToClinic1 = "no";
            //}
            //else
            //{
            //    ChildReferToClinic1 = "no data";
            //}

            //follow.ChildReferToClinic1 = ChildReferToClinic1;
            //follow.ChildReferralNo1 = txtChildReferralNo1.Text;

            //string ChildWalkAppropriateForAge;
            //if (ChildWalkAppropriateForAge1.IsChecked == true)
            //{
            //    ChildWalkAppropriateForAge = "yes";
            //}
            //else if (ChildWalkAppropriateForAge2.IsChecked == true)
            //{
            //    ChildWalkAppropriateForAge = "no";
            //}
            //else
            //{
            //    ChildWalkAppropriateForAge = "no data";
            //}

            //follow.ChildWalkAppropriateForAge = ChildWalkAppropriateForAge;

            //string ChildTalkAppropriateForAge;
            //if (ChildTalkAppropriateForAge1.IsChecked == true)
            //{
            //    ChildTalkAppropriateForAge = "yes";
            //}
            //else if (ChildTalkAppropriateForAge2.IsChecked == true)
            //{
            //    ChildTalkAppropriateForAge = "no";
            //}
            //else
            //{
            //    ChildTalkAppropriateForAge = "no data";
            //}

            //follow.ChildTalkAppropriateForAge = ChildTalkAppropriateForAge;

            //string ChildReferToClinic2;
            //if (ChildReferToClinic21.IsChecked == true)
            //{
            //    ChildReferToClinic2 = "yes";
            //}
            //else if (ChildReferToClinic22.IsChecked == true)
            //{
            //    ChildReferToClinic2 = "no";
            //}
            //else
            //{
            //    ChildReferToClinic2 = "no data";
            //}

            //follow.ChildReferToClinic2 = ChildReferToClinic2;
            //follow.ChildReferralNo2 = txtChildReferralNo2.Text;

            //string ChildChildAssisted;
            //if (ChildChildAssisted1.IsChecked == true)
            //{
            //    ChildChildAssisted = "yes";
            //}
            //else if (ChildChildAssisted2.IsChecked == true)
            //{
            //    ChildChildAssisted = "no";
            //}
            //else
            //{
            //    ChildChildAssisted = "no data";
            //}

            //follow.ChildChildAssisted = ChildChildAssisted;

            //string ChildReReferToSD;
            //if (ChildReReferToSD1.IsChecked == true)
            //{
            //    ChildReReferToSD = "yes";
            //}
            //else if (ChildReReferToSD2.IsChecked == true)
            //{
            //    ChildReReferToSD = "no";
            //}
            //else
            //{
            //    ChildReReferToSD = "no data";
            //}

            //follow.ChildReReferToSD = ChildReReferToSD;
            //follow.ChildReferralNo3 = txtChildReferralNo3.Text;
            //follow.ChildListConcernsReChild = txtChildListConcernsReChild.Text;

            //string ChildReferToClinic3;
            //if (ChildReferToClinic31.IsChecked == true)
            //{
            //    ChildReferToClinic3 = "yes";
            //}
            //else if (ChildReferToClinic32.IsChecked == true)
            //{
            //    ChildReferToClinic3 = "no";
            //}
            //else
            //{
            //    ChildReferToClinic3 = "no data";
            //}

            //follow.ChildReferToClinic3 = ChildReferToClinic3;

            //string ChildreferToSD;
            //if (ChildreferToSD1.IsChecked == true)
            //{
            //    ChildreferToSD = "yes";
            //}
            //else if (ChildreferToSD2.IsChecked == true)
            //{
            //    ChildreferToSD = "no";
            //}
            //else
            //{
            //    ChildreferToSD = "no data";
            //}

            //follow.ChildreferToSD = ChildreferToSD;
            //follow.ChildReferralNo4 = txtChildReferralNo4.Text;

            //string ChildChildWithRTHC;
            //if (ChildChildWithRTHC1.IsChecked == true)
            //{
            //    ChildChildWithRTHC = "yes";
            //}
            //else if (ChildChildWithRTHC2.IsChecked == true)
            //{
            //    ChildChildWithRTHC = "no";
            //}
            //else
            //{
            //    ChildChildWithRTHC = "no data";
            //}

            //follow.ChildChildWithRTHC = ChildChildWithRTHC;

            //string ChildReferToClinic4;
            //if (ChildReferToClinic41.IsChecked == true)
            //{
            //    ChildReferToClinic4 = "yes";
            //}
            //else if (ChildReferToClinic42.IsChecked == true)
            //{
            //    ChildReferToClinic4 = "no";
            //}
            //else
            //{
            //    ChildReferToClinic4 = "no data";
            //}

            //follow.ChildReferToClinic4 = ChildReferToClinic4;
            //follow.ChildReferralNo5 = txtChildReferralNo5.Text;

            //string ChildMotherTHVPositive;
            //if (ChildMotherTHVPositive1.IsChecked == true)
            //{
            //    ChildMotherTHVPositive = "yes";
            //}
            //else if (ChildMotherTHVPositive2.IsChecked == true)
            //{
            //    ChildMotherTHVPositive = "no";
            //}
            //else
            //{
            //    ChildMotherTHVPositive = "no data";
            //}

            //follow.ChildMotherTHVPositive = ChildMotherTHVPositive;

            //string ChildChildBreastfed;
            //if (ChildChildBreastfed1.IsChecked == true)
            //{
            //    ChildChildBreastfed = "yes";
            //}
            //else if (ChildChildBreastfed2.IsChecked == true)
            //{
            //    ChildChildBreastfed = "no";
            //}
            //else
            //{
            //    ChildChildBreastfed = "no data";
            //}

            //follow.ChildChildBreastfed = ChildChildBreastfed;
            //follow.ChildHowLong = txtChildHowLong.Text;

            //string ChildClildEverOnNevirapine;
            //if (ChildClildEverOnNevirapine1.IsChecked == true)
            //{
            //    ChildClildEverOnNevirapine = "yes";
            //}
            //else if (ChildClildEverOnNevirapine2.IsChecked == true)
            //{
            //    ChildClildEverOnNevirapine = "no";
            //}
            //else
            //{
            //    ChildClildEverOnNevirapine = "no data";
            //}

            //follow.ChildClildEverOnNevirapine = ChildClildEverOnNevirapine;

            //string ChildReferToClinic5;
            //if (ChildReferToClinic51.IsChecked == true)
            //{
            //    ChildReferToClinic5 = "yes";
            //}
            //else if (ChildReferToClinic52.IsChecked == true)
            //{
            //    ChildReferToClinic5 = "no";
            //}
            //else
            //{
            //    ChildReferToClinic5 = "no data";
            //}

            //follow.ChildReferToClinic5 = ChildReferToClinic5;
            //follow.ChildReferralNo6 = txtChildReferralNo6.Text;

            //string ChildHowPCRHasDone;
            //if (ChildHowPCRHasDone1.IsChecked == true)
            //{
            //    ChildHowPCRHasDone = "yes";
            //}
            //else if (ChildHowPCRHasDone2.IsChecked == true)
            //{
            //    ChildHowPCRHasDone = "no";
            //}
            //else
            //{
            //    ChildHowPCRHasDone = "no data";
            //}

            //follow.ChildHowPCRHasDone = ChildHowPCRHasDone;

            //string ChildReferToClinic6;
            //if (ChildReferToClinic61.IsChecked == true)
            //{
            //    ChildReferToClinic6 = "yes";
            //}
            //else if (ChildReferToClinic62.IsChecked == true)
            //{
            //    ChildReferToClinic6 = "no";
            //}
            //else
            //{
            //    ChildReferToClinic6 = "no data";
            //}

            //follow.ChildReferToClinic6 = ChildReferToClinic6;
            //follow.ChildReferralNo7 = txtChildReferralNo7.Text;

            //string ChildImmunisationUpToDate;
            //if (ChildImmunisationUpToDate1.IsChecked == true)
            //{
            //    ChildImmunisationUpToDate = "yes";
            //}
            //else if (ChildImmunisationUpToDate2.IsChecked == true)
            //{
            //    ChildImmunisationUpToDate = "no";
            //}
            //else
            //{
            //    ChildImmunisationUpToDate = "no data";
            //}

            //follow.ChildImmunisationUpToDate = ChildImmunisationUpToDate;
            //follow.ChildWhichImmunisationsOutStanding = ((ComboBoxItem)comboChildWhichImmunisationsOutStanding.SelectedItem).Content.ToString();

            //string ChildVITAandWormMedsGivenEachMonth;
            //if (ChildVITAandWormMedsGivenEachMonth1.IsChecked == true)
            //{
            //    ChildVITAandWormMedsGivenEachMonth = "yes";
            //}
            //else if (ChildVITAandWormMedsGivenEachMonth2.IsChecked == true)
            //{
            //    ChildVITAandWormMedsGivenEachMonth = "no";
            //}
            //else
            //{
            //    ChildVITAandWormMedsGivenEachMonth = "no data";
            //}

            //follow.ChildVITAandWormMedsGivenEachMonth = ChildVITAandWormMedsGivenEachMonth;

            //string ChildReferToClinic7;
            //if (ChildReferToClinic71.IsChecked == true)
            //{
            //    ChildReferToClinic7 = "yes";
            //}
            //else if (ChildReferToClinic72.IsChecked == true)
            //{
            //    ChildReferToClinic7 = "no";
            //}
            //else
            //{
            //    ChildReferToClinic7 = "no data";
            //}

            //follow.ChildReferToClinic7 = ChildReferToClinic7;
            //follow.ChildReferralNo8 = txtChildReferralNo8.Text;

            ////Other
            //follow.OtherDateOfVisit = txtOtherDateOfVisit.Text;

            //string OtherWentToClinic;
            //if (OtherWentToClinic1.IsChecked == true)
            //{
            //    OtherWentToClinic = "yes";
            //}
            //else if (OtherWentToClinic2.IsChecked == true)
            //{
            //    OtherWentToClinic = "no";
            //}
            //else
            //{
            //    OtherWentToClinic = "no data";
            //}

            //follow.OtherWentToClinic = OtherWentToClinic;

            //string OtherReReferToClinic;
            //if (OtherReReferToClinic1.IsChecked == true)
            //{
            //    OtherReReferToClinic = "yes";
            //}
            //else if (OtherReReferToClinic2.IsChecked == true)
            //{
            //    OtherReReferToClinic = "no";
            //}
            //else
            //{
            //    OtherReReferToClinic = "no data";
            //}

            //follow.OtherReReferToClinic = OtherReReferToClinic;
            //follow.OtherReferralNo1 = txtOtherReferralNo1.Text;
            //follow.OtherConditionTha = txtOtherConditionTha.Text;

            //string OtherReferToClinic1;
            //if (OtherReferToClinic11.IsChecked == true)
            //{
            //    OtherReferToClinic1 = "yes";
            //}
            //else if (OtherReferToClinic12.IsChecked == true)
            //{
            //    OtherReferToClinic1 = "no";
            //}
            //else
            //{
            //    OtherReferToClinic1 = "no data";
            //}

            //follow.OtherReferToClinic1 = OtherReferToClinic1;
            //follow.OtherReferralNo2 = txtOtherReferralNo2.Text;


            //da.FollowUpVisit(follow);
            //MessageBoxResult result = MessageBox.Show("A Follow-Up is successful saved", "Confirmation");
            //user.ClinicUsed = ((ComboBoxItem)ClinicUsed.SelectedItem).Content.ToString();
            //user.DateOfBirth = DateTime.Parse(txtDateofBirth.Text);
            //user.NameofSchool = ((ComboBoxItem)NameofSchool.SelectedItem).Content.ToString();

            //string gender;
            //if (radioMale.IsChecked == true)
            //{
            //    gender = "male";
            //}
            //else
            //{
            //    gender = "female";
            //}

            #endregion  

            string storedProcedure = "";


            #region Biographical
            /*

            //Not needed as clients already exists in the DB

            //clinBio.ccbioID = 0;
            //clinBio.ClinicID = 0;
            //clinBio.ClientID = "";
            //clinBio.ccbioContactNo = txtBioContactNo.Text;
            //clinBio.ccbioFileNo = txtBioFileNo.Text;
            //clinBio.ccbioNextOfKinRelationship = txtBioKinRelation.Text;
            //clinBio.ccbioNextOfKinName = txtBioKinName.Text;
            //clinBio.ccbioNextOfKinTelNo = txtBioKinContactNo.Text;
            //clinBio.ccbioDoDHypertension = (DateTime)dpBioHptDiagDate.SelectedDate;
            //clinBio.ccbioDoDDiabetes = (DateTime)dpBioDiaDiagDate.SelectedDate;
            //clinBio.ccbioDoDEpilepsy = (DateTime)dpBioEpiDiagDate.SelectedDate;
            //clinBio.ccbioDoDAsthma = (DateTime)dpBioAstDiagDate.SelectedDate;
            //clinBio.ccbioDoDHIV = (DateTime)dpBioHivDiagDate.SelectedDate;
            //clinBio.ccbioDoDTB = (DateTime)dpBioTbDiagDate.SelectedDate;
            //clinBio.ccbioDoDMaternalHealth = (DateTime)dpBioMatDiagDate.SelectedDate;
            //clinBio.ccbioDoDChildHealth = (DateTime)dpBioChDiagDate.SelectedDate;
            //clinBio.ccbioOther = (DateTime)dpBioOtherDiagDate.SelectedDate;


            try
            {
                storedProcedure = "AddClinicClientBiographical";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ccbioID", clinBio.ccbioID);
                com.Parameters.AddWithValue("@ClinicID", clinBio.ClinicID);
                com.Parameters.AddWithValue("@ClientID", clinBio.ClientID);
                com.Parameters.AddWithValue("@ccbioContactNo", clinBio.ccbioContactNo);
                com.Parameters.AddWithValue("@ccbioFileNo", clinBio.ccbioFileNo);
                com.Parameters.AddWithValue("@ccbioNextOfKinRelationship", clinBio.ccbioNextOfKinRelationship);
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
            }*/

            #endregion // not needed in follow up

            #region Visit Details

            Impilo_App.LocalModels.FollowUpVisitDetails folVD = new Impilo_App.LocalModels.FollowUpVisitDetails();

            folVD.fuvdID = 0;
            folVD.EncounterID = 0;
            folVD.fuvdVisitDate = DateTime.Now;
            folVD.fuvdNextVisitDate = (DateTime)dpVisitNextVisit.SelectedDate;
            folVD.duvdOutcome = ((ComboBoxItem)comboVisitOutCome.SelectedItem).Content.ToString();
            folVD.duvdHypertension = (VisitHPTYes.IsChecked == true) ? true : false;
            folVD.duvdDiabetes = (VisitDiabetesYes.IsChecked == true) ? true : false;
            folVD.duvdEpilepsy = (VisitEpilepsyYes.IsChecked == true) ? true : false;
            //folVD.duvdAsthma = (VisitAstYes.IsChecked == true) ? true : false;
            folVD.duvdHIV = (VisitHIVYes.IsChecked == true) ? true : false;
            folVD.duvdTB = (VisitTBYes.IsChecked == true) ? true : false;
            folVD.duvdMaternalHealth = (VisitMatHealthYes.IsChecked == true) ? true : false;
            folVD.duvdChildHealth = (VisitChildHealthYes.IsChecked == true) ? true : false;
            folVD.duvdOther = txtVisitOther.Text;
            folVD.duvdDoorDoor = (VisitDoortoDoorYes.IsChecked == true) ? true : false;

            try
            {
                storedProcedure = "AddFollowUpVisitData";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@duvdID", folVD.fuvdID);
                com.Parameters.AddWithValue("@EncounterID", folVD.EncounterID);
                com.Parameters.AddWithValue("@fuvdVisitDate", folVD.fuvdVisitDate);
                com.Parameters.AddWithValue("@fuvdNextVisitDate", folVD.fuvdNextVisitDate);
                com.Parameters.AddWithValue("@duvdOutcome", folVD.duvdOutcome);
                com.Parameters.AddWithValue("@duvdHypertension", folVD.duvdHypertension);
                com.Parameters.AddWithValue("@duvdHypertension", folVD.duvdHypertension);
                com.Parameters.AddWithValue("@duvdDiabetes", folVD.duvdDiabetes);
                com.Parameters.AddWithValue("@duvdEpilepsy", folVD.duvdEpilepsy);
                //com.Parameters.AddWithValue("@duvdAsthma", folVD.duvdAsthma);
                com.Parameters.AddWithValue("@duvdHIV", folVD.duvdHIV);
                com.Parameters.AddWithValue("@duvdTB", folVD.duvdTB);
                com.Parameters.AddWithValue("@duvdMaternalHealth", folVD.duvdMaternalHealth);
                com.Parameters.AddWithValue("@duvdChildHealth", folVD.duvdChildHealth);
                com.Parameters.AddWithValue("@duvdOther", folVD.duvdOther);
                com.Parameters.AddWithValue("@duvdDoorDoor", folVD.duvdDoorDoor);
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

            Impilo_App.LocalModels.FollowUpHypertension folHyp = new Impilo_App.LocalModels.FollowUpHypertension();
            folHyp.fuhID = 0;
            folHyp.EncounterID = 0;
            folHyp.fuhHiEHWentToClinic = (HyperWentToClinic1Yes.IsChecked == true) ? true : false;
            folHyp.fuhDateOfVisit = DateTime.Now;
            folHyp.fuhHiEHReReferToClinic = (HyperReReferToClinic1Yes.IsChecked == true) ? true : false; 
            folHyp.fuhHiEHRefNo = txtHyperReferralNo1.Text;
            folHyp.fuhHiEHCurrentlyOnMeds = (HyperCurrentlyOnMedsYes.IsChecked == true) ? true : false;
            folHyp.fuhHiEHStartDate = (DateTime)dpHyperStartDate.SelectedDate;
            folHyp.fuhHiEHBPScreeningSystolic = txtHyperBPReadingSystolic.Text;
            folHyp.fuhHiEHBPScreeningDiastolic = txtHyperBPReadingDiastolic.Text;
            folHyp.fuhHiEHBPTodaySystolic = decimal.Parse(txtHyperTodayTestReadingSystolic.Text);
            folHyp.fuhHiEHBPTodayDiastolic = decimal.Parse(txtHyperTodayTestReadingDiastolic.Text);
            folHyp.fuhHiEHReferToClinic = (HyperReReferToClinic21.IsChecked == true) ? true : false;
            folHyp.fuhHiEHRefNo2 = txtHyperReferralNo2.Text;
            folHyp.fuhCRReReferToClinic = (HyperReReferToClinic31.IsChecked == true) ? true : false;
            folHyp.fuhCRRefNo = txtHyperReferralNo3.Text;
            folHyp.fuhAlreadyOnTreatmentFollowUpTestReading = txtHyperFollowUpTestReading.Text;
            folHyp.fuhDoorToDoorCheckReading = txtHyperCheckReading.Text;
            //folHyp.fuhMedication = (ComboBoxItem)comboHyperMedication.SelectedItem).Content.ToString();


            try
            {
                storedProcedure = "AddFollowUpHypertension";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fuhID", folHyp.fuhID);
                com.Parameters.AddWithValue("@EncounterID", folHyp.EncounterID);
                com.Parameters.AddWithValue("@fuhHiEHWentToClinic", folHyp.fuhHiEHWentToClinic);
                com.Parameters.AddWithValue("@fuhDateOfVisit", folHyp.fuhDateOfVisit);
                com.Parameters.AddWithValue("@fuhHiEHReReferToClinic", folHyp.fuhHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fuhHiEHRefNo", folHyp.fuhHiEHRefNo);
                com.Parameters.AddWithValue("@fuhHiEHCurrentlyOnMeds", folHyp.fuhHiEHCurrentlyOnMeds);
                com.Parameters.AddWithValue("@fuhHiEHStartDate", folHyp.fuhHiEHStartDate);
                com.Parameters.AddWithValue("@fuhHiEHBPScreeningSystolic", folHyp.fuhHiEHBPScreeningSystolic);
                com.Parameters.AddWithValue("@fuhHiEHBPScreeningDiastolic", folHyp.fuhHiEHBPScreeningDiastolic);
                com.Parameters.AddWithValue("@fuhHiEHBPTodaySystolic", folHyp.fuhHiEHBPTodaySystolic);
                com.Parameters.AddWithValue("@fuhHiEHBPTodayDiastolic", folHyp.fuhHiEHBPTodayDiastolic);
                com.Parameters.AddWithValue("@fuhHiEHReferToClinic", folHyp.fuhHiEHReferToClinic);
                com.Parameters.AddWithValue("@fuhHiEHRefNo2", folHyp.fuhHiEHRefNo2);
                com.Parameters.AddWithValue("@fuhCRReReferToClinic", folHyp.fuhCRReReferToClinic);
                com.Parameters.AddWithValue("@fuhCRRefNo", folHyp.fuhCRRefNo);
                com.Parameters.AddWithValue("@fuhAlreadyOnTreatmentFollowUpTestReading", folHyp.fuhAlreadyOnTreatmentFollowUpTestReading);
                com.Parameters.AddWithValue("@fuhDoorToDoorCheckReading", folHyp.fuhDoorToDoorCheckReading);
                com.Parameters.AddWithValue("@fuhMedication", folHyp.fuhMedication);

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

            Impilo_App.LocalModels.FollowUpDiabetes folDia = new Impilo_App.LocalModels.FollowUpDiabetes();

            folDia.fudID = 1;
            folDia.EncounterID = 1;
            folDia.fudDateOfVisit = DateTime.Now;
            folDia.fudHiEHWentToClinic = (DaiWentToClinic1.IsChecked == true) ? true : false;
            folDia.fudHiEHReReferToClinic = (DiaReReferToClinic11.IsChecked == true) ? true : false;
            folDia.fudHiEHRefNo = txtDiaReferralNo1.Text;
            folDia.fudHiEHCurrentlyOnMeds = (DiaCurrentlyOnMeds1.IsChecked == true) ? true : false;
            folDia.fudHiEHStartDate = (DateTime)DiaStartDate.SelectedDate; 
            folDia.fudHiEHFollowUpTestReading = decimal.Parse(txtDiaFollowUpTestReading1.Text);
            folDia.fudHiEHReferToClinic2 = (DiaReferToClinic21.IsChecked == true) ? true : false;
            folDia.fudHiEHRefNo2 = txtDiaReferralNo2.Text;
            folDia.fudClinicRefReferToClinic = (DiaReReferToClinic31.IsChecked == true) ? true : false;
            folDia.fudClinicRefRefNo = txtDiaReferralNo3.Text;
            folDia.fudAlreadyOnTreatmentFollowUpTestReading = decimal.Parse(txtDiaFollowUpTestReading3.Text);
            folDia.fudDoorDoor = decimal.Parse(txtDiaCheckReading.Text);
            //folDia.fudMedication = (ComboBoxItem)comboDiaMedication.SelectedItem).Content.ToString();



            try
            {
                storedProcedure = "AddFollowUpDiabetes";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fudID", folDia.fudID);
                com.Parameters.AddWithValue("@EncounterID", folDia.EncounterID);
                com.Parameters.AddWithValue("@fudDateOfVisit", folDia.fudDateOfVisit);
                com.Parameters.AddWithValue("@fudHiEHWentToClinic", folDia.fudHiEHWentToClinic);
                com.Parameters.AddWithValue("@fudHiEHReReferToClinic", folDia.fudHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fudHiEHRefNo", folDia.fudHiEHRefNo);
                com.Parameters.AddWithValue("@fudHiEHCurrentlyOnMeds", folDia.fudHiEHCurrentlyOnMeds);
                com.Parameters.AddWithValue("@fudHiEHStartDate", folDia.fudHiEHStartDate);
                com.Parameters.AddWithValue("@fudHiEHFollowUpTestReading", folDia.fudHiEHFollowUpTestReading);
                com.Parameters.AddWithValue("@fudHiEHReferToClinic2", folDia.fudHiEHReferToClinic2);
                com.Parameters.AddWithValue("@fudHiEHRefNo2", folDia.fudHiEHRefNo2);
                com.Parameters.AddWithValue("@fudClinicRefReferToClinic", folDia.fudClinicRefReferToClinic);
                com.Parameters.AddWithValue("@fudClinicRefRefNo", folDia.fudClinicRefRefNo);
                com.Parameters.AddWithValue("@fudAlreadyOnTreatmentFollowUpTestReading", folDia.fudAlreadyOnTreatmentFollowUpTestReading);
                com.Parameters.AddWithValue("@fudDoorDoor", folDia.fudDoorDoor);
                com.Parameters.AddWithValue("@fudMedication", folDia.fudMedication);

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

            Impilo_App.LocalModels.FollowUpEpilepsy folEpi = new Impilo_App.LocalModels.FollowUpEpilepsy();

            folEpi.fueID = 0;
            folEpi.EncounterID = 0;
            folEpi.fueHiEHWentToClinic = (EpiWentToClinic1.IsChecked == true) ? true : false;
            folEpi.fueHiEHReReferToClinic = (EpiReReferToClinic11.IsChecked == true) ? true : false;
            folEpi.fueHiEHRefNo = txtEpiReferralNo1.Text;
            folEpi.fueCRFitInLastMonth = (EpiFitInLastMonth1.IsChecked == true) ? true : false;
            folEpi.fueCRReferToClinic = (EpiReferToClinic1.IsChecked == true) ? true : false;
            folEpi.fueCRRefNo = txtEpiReferralNo2.Text;
            folEpi.fueOnTreatmentCurrentlyOnMeds = (EpiCurrentlyOnMeds1.IsChecked==true) ? true : false;
            folEpi.fueOnTreatmentStartDate = (DateTime)txtEpiStartDate.SelectedDate;
            folEpi.fueOnTreatmentMoreThan3FitsSinceLastMonth = (EpiMoreThan3FitsInLastMonth1.IsChecked == true) ? true : false;
            folEpi.fueOnTreatmentReReferToClinic = (EpiReReferToClinic21.IsChecked == true) ? true : false;
            folEpi.fueOnTreatmentRefNo = txtEpiReferralNo2.Text;



            try
            {
                storedProcedure = "AddClinicEpilepsy";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fueID", folEpi.fueID);
                com.Parameters.AddWithValue("@EncounterID", folEpi.EncounterID);
                com.Parameters.AddWithValue("@fueHiEHWentToClinic", folEpi.fueHiEHWentToClinic);
                com.Parameters.AddWithValue("@fueHiEHReReferToClinic", folEpi.fueHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fueHiEHRefNo", folEpi.fueHiEHRefNo);
                com.Parameters.AddWithValue("@fueCRFitInLastMonth", folEpi.fueCRFitInLastMonth);
                com.Parameters.AddWithValue("@fueCRReferToClinic", folEpi.fueCRReferToClinic);
                com.Parameters.AddWithValue("@fueCRRefNo", folEpi.fueCRRefNo);
                com.Parameters.AddWithValue("@fueOnTreatmentCurrentlyOnMeds", folEpi.fueOnTreatmentCurrentlyOnMeds);
                com.Parameters.AddWithValue("@fueOnTreatmentStartDate", folEpi.fueOnTreatmentStartDate);
                com.Parameters.AddWithValue("@fueOnTreatmentMoreThan3FitsSinceLastMonth", folEpi.fueOnTreatmentMoreThan3FitsSinceLastMonth);
                com.Parameters.AddWithValue("@fueOnTreatmentReReferToClinic", folEpi.fueOnTreatmentReReferToClinic);
                com.Parameters.AddWithValue("@fueOnTreatmentRefNo", folEpi.fueOnTreatmentRefNo);



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

            Impilo_App.LocalModels.FollowUpAsthma folAst = new Impilo_App.LocalModels.FollowUpAsthma();

            folAst.fuaID = 0;
            folAst.EncounterID = 0;
            folAst.fuaDateOfVisit = (DateTime)txtAsDateOfVisit.SelectedDate;
            folAst.fuaHiEHWentToClinic = (AsWentToClinic1.IsChecked == true) ? true : false;
            folAst.fuaHiEHReReferToClinic = (AsReReferToClinic11.IsChecked == true) ? true : false;
            folAst.fuaHiHRefNo = txtAsReferralNo1.Text;
            folAst.fuaCRDifficultyBreathingAndWheezing = (AsFitInLastMonth1.IsChecked == true) ? true : false;
            folAst.fuaCRReferToClinic = (AsReferToClinic1.IsChecked == true) ? true : false;
            folAst.fuaCRRefNo = txtAsReferralNo2.Text;
            folAst.fuaOTCurrentlyOnMeds = (AsCurrentlyOnMeds1.IsChecked == true) ? true : false;
            folAst.fuaOTStartDate = (DateTime)txtAsStartDate.SelectedDate;
            folAst.fuaOTIncreasedNoOfAsthmaAttacks = (AsIncreasedNoOfAsthmaAttacks1.IsChecked == true) ? true : false;
            folAst.fuaOTReReferToClinic = (AsReReferToClinic21.IsChecked == true) ? true : false;
            folAst.fuaOTRefNo = txtAsReferralNo3.Text;
            //folAst.fuaMedication = (ComboBoxItem)comboAsMedication.SelectedItem.Content.ToString();






            try
            {
                storedProcedure = "AddFollowUpAsthma";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fuaID", folAst.fuaID);
                com.Parameters.AddWithValue("@EncounterID", folAst.EncounterID);
                com.Parameters.AddWithValue("@fuaDateOfVisit", folAst.fuaDateOfVisit);
                com.Parameters.AddWithValue("@fuaHiEHWentToClinic", folAst.fuaHiEHWentToClinic);
                com.Parameters.AddWithValue("@fuaHiEHReReferToClinic", folAst.fuaHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fuaHiHRefNo", folAst.fuaHiHRefNo);
                com.Parameters.AddWithValue("@fuaCRDifficultyBreathingAndWheezing", folAst.fuaCRDifficultyBreathingAndWheezing);
                com.Parameters.AddWithValue("@fuaCRReferToClinic", folAst.fuaCRReferToClinic);
                com.Parameters.AddWithValue("@fuaCRRefNo", folAst.fuaCRRefNo);
                com.Parameters.AddWithValue("@fuaOTCurrentlyOnMeds", folAst.fuaOTCurrentlyOnMeds);
                com.Parameters.AddWithValue("@fuaOTStartDate", folAst.fuaOTStartDate);
                com.Parameters.AddWithValue("@fuaOTIncreasedNoOfAsthmaAttacks", folAst.fuaOTIncreasedNoOfAsthmaAttacks);
                com.Parameters.AddWithValue("@fuaOTReReferToClinic", folAst.fuaOTReReferToClinic);
                com.Parameters.AddWithValue("@fuaOTRefNo", folAst.fuaOTRefNo);
                com.Parameters.AddWithValue("@fuaMedication", folAst.fuaMedication);

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

            Impilo_App.LocalModels.FollowUpHIV folHiv = new Impilo_App.LocalModels.FollowUpHIV();

            folHiv.fuhiv = 0;
            folHiv.EncounterID = 0;
            folHiv.fuhivDateOfVisit = (DateTime)txtHIVDateOfVisit.SelectedDate;
            folHiv.fuhivHiEHWentToClinic = (HIVWentToClinic1.IsChecked == true) ? true : false;
            folHiv.fuhivHiEHReReferToClinic = (HIVReReferToClinic1.IsChecked == true) ? true : false;
            folHiv.fuhHiEHRefNo = txtHIVReferralNo1.Text;
            folHiv.fuhCRReferToClinic = (HIVReferToClinic11.IsChecked == true) ? true : false;
            folHiv.fuhCRRefNo = txtHIVReferralNo2.Text;
            //folHiv.fuhHIVStatus = (ComboBoxItem)comboHIVStatus.SelectedItem.Content.ToString(); 
            folHiv.fuhIPOnARV = (HIVOnARVs1.IsChecked == true) ? true : false;
            folHiv.fuhIPStartDate = (DateTime)txtHIVStartDate1.SelectedDate;
            folHiv.fuhIPAdherenceOK = (HIVAdherenceOK1.IsChecked ==true) ? true : false;
            folHiv.fuhIPConcerns = (HIVConcerns1.IsChecked == true) ? true : false;
            folHiv.fuhIPReferToClinic = (HIVReferToClinic21.IsChecked == true) ? true : false;
            folHiv.fuhIPRefNo = txtHIVReferralNo3.Text;
            folHiv.fuhIPNotOnARV = (HIVARVsConcerns1.IsChecked == true) ? true : false;
            folHiv.fuhIPReferToClinic2 = (HIVReferToClinic31.IsChecked == true) ? true : false;
            folHiv.fuhIPRefNo2 = txtHIVReferralNo4.Text;
            folHiv.fuhINCounsellingDone = (HIVCounsellingDone1.IsChecked == true) ? true : false;
            folHiv.fuhIUHIVTestDone = (HIVTestingDone1.IsChecked == true) ? true : false;
            folHiv.fuhHIVTestResults = txtHIVTestResults.Text;
            folHiv.fuhHIVTestReferToClinic = (HIVReferToClinic41.IsChecked == true) ? true : false;
            folHiv.fuhHIVRefNo = txtHIVReferralNo5.Text;
            //folHiv.fuhHIVMedication = (ComboBoxItem)HIVMedication.SelectedItem.Content.ToString();


            try
            {
                storedProcedure = "AddFollowUpHIV";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fuhiv", folHiv.fuhiv);
                com.Parameters.AddWithValue("@EncounterID", folHiv.EncounterID);
                com.Parameters.AddWithValue("@fuhivDateOfVisit", folHiv.fuhivDateOfVisit);
                com.Parameters.AddWithValue("@fuhivHiEHWentToClinic", folHiv.fuhivHiEHWentToClinic);
                com.Parameters.AddWithValue("@fuhivHiEHReReferToClinic", folHiv.fuhivHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fuhHiEHRefNo", folHiv.fuhHiEHRefNo);
                com.Parameters.AddWithValue("@fuhCRReferToClinic", folHiv.fuhCRReferToClinic);
                com.Parameters.AddWithValue("@fuhCRRefNo", folHiv.fuhCRRefNo);
                com.Parameters.AddWithValue("@fuhHIVStatus", folHiv.fuhHIVStatus);
                com.Parameters.AddWithValue("@fuhIPOnARV", folHiv.fuhIPOnARV);
                com.Parameters.AddWithValue("@fuhIPStartDate", folHiv.fuhIPStartDate);
                com.Parameters.AddWithValue("@fuhIPAdherenceOK", folHiv.fuhIPAdherenceOK);
                com.Parameters.AddWithValue("@fuhIPConcerns", folHiv.fuhIPConcerns);
                com.Parameters.AddWithValue("@fuhIPReferToClinic", folHiv.fuhIPReferToClinic);
                com.Parameters.AddWithValue("@fuhIPRefNo", folHiv.fuhIPRefNo);
                com.Parameters.AddWithValue("@fuhIPNotOnARV", folHiv.fuhIPNotOnARV);
                com.Parameters.AddWithValue("@fuhIPReferToClinic2", folHiv.fuhIPReferToClinic2);
                com.Parameters.AddWithValue("@fuhIPRefNo2", folHiv.fuhIPRefNo2);
                com.Parameters.AddWithValue("@fuhINCounsellingDone", folHiv.fuhINCounsellingDone);
                com.Parameters.AddWithValue("@fuhIUHIVTestDone", folHiv.fuhIUHIVTestDone);
                com.Parameters.AddWithValue("@fuhHIVTestResults", folHiv.fuhHIVTestResults);
                com.Parameters.AddWithValue("@fuhHIVTestReferToClinic", folHiv.fuhHIVTestReferToClinic);
                com.Parameters.AddWithValue("@fuhHIVRefNo", folHiv.fuhHIVRefNo);
                com.Parameters.AddWithValue("@fuhHIVMedication", folHiv.fuhHIVMedication);


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
            Impilo_App.LocalModels.FollowUpTB folTb = new Impilo_App.LocalModels.FollowUpTB();

            folTb.futID = 0;
            folTb.EncounterID = 0;
            folTb.futbDateOfVisit = (DateTime)txtTBDateOfVisit.SelectedDate;
            folTb.futbHiEHWentToClinic = (TBWentToClinic1.IsChecked == true) ? true : false;
            folTb.futbHiEHReReferToClinic = (TBReferToClinic11.IsChecked == true) ? true : false;
            folTb.futbHiEHRefNo = txtTBReferralNo1.Text;
            folTb.futbTBSRecentUnplannedLoseOfWeight = (TBRecentUnplannedLoseOfWeight1.IsChecked == true) ? true : false;
            folTb.futbTBSExcessiveSweatingAtNight = (TBExcessiveSweatingAtNight1.IsChecked == true) ? true : false;
            folTb.futbTBSFeverOver2Weeks = (TBFeverOver2Weeks1.IsChecked == true) ? true : false;
            folTb.futbTBSCoughMoreThan2Weeks = (TBCoughMoreThan2Week1.IsChecked == true) ? true : false;
            folTb.futbTBSLossOfApetite = (TBLossOfApetite1.IsChecked == true) ? true : false;
            folTb.futbTBSReferToClinic = (TBReferredToClinic21.IsChecked == true) ? true : false;
            folTb.futbTBSRefNo = txtTBReferralNo2.Text;
            folTb.futbTBSResults = ((ComboBoxItem)comboTBResult.SelectedItem).Content.ToString();
            folTb.futbTBOTNewlyDiagnosedInLastMonth = (TBNewlyDiagnosed1.IsChecked == true) ? true : false;
            folTb.futbTBOTStartDate = (DateTime)txtTBStartDate.SelectedDate;
            folTb.futbTBOTReferTBContactsToClinic = (TBReferTBContactsToClinic1.IsChecked == true) ? true : false;
            folTb.futbTBOTPreviouslyOnMeds = (TBPreviouslyOnMeds1.IsChecked == true) ? true : false;
            folTb.futbTBOTFinishDate = (DateTime)txtTBFinishDate.SelectedDate;
            folTb.futbTBOTConcerns = (TBConcerns1.IsChecked == true) ? true : false; 
            folTb.futbTBOTReferToClinic = (TBReferToClinic31.IsChecked == true) ? true : false;
            folTb.futbTBOTRefNo = txtTBReferralNo3.Text;
            
            try
            {
                storedProcedure = "AddFollowUpAsthma";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@futbID", folTb.futID);
                com.Parameters.AddWithValue("@EncounterID", folTb.EncounterID);
                com.Parameters.AddWithValue("@futbDateOfVisit", folTb.futbDateOfVisit);
                com.Parameters.AddWithValue("@futbHiEHWentToClinic", folTb.futbHiEHWentToClinic);
                com.Parameters.AddWithValue("@futbHiEHReReferToClinic", folTb.futbHiEHReReferToClinic);
                com.Parameters.AddWithValue("@futbHiHRefNo", folTb.futbHiEHRefNo);
                com.Parameters.AddWithValue("@futbTBSRecentUnplannedLoseOfWeight", folTb.futbTBSRecentUnplannedLoseOfWeight);
                com.Parameters.AddWithValue("@futbTBSExcessiveSweatingAtNight", folTb.futbTBSExcessiveSweatingAtNight);
                com.Parameters.AddWithValue("@futbTBSFeverOver2Weeks", folTb.futbTBSFeverOver2Weeks);
                com.Parameters.AddWithValue("@futbTBSCoughMoreThan2Weeks", folTb.futbTBSCoughMoreThan2Weeks);
                com.Parameters.AddWithValue("@futbTBSLossOfApetite", folTb.futbTBSLossOfApetite);
                com.Parameters.AddWithValue("@futbTBSReferToClinic", folTb.futbTBSReferToClinic);
                com.Parameters.AddWithValue("@futbTBSRefNo", folTb.futbTBSRefNo);
                com.Parameters.AddWithValue("@futbTBSResults", folTb.futbTBSResults);
                com.Parameters.AddWithValue("@futbTBOTNewlyDiagnosedInLastMonth", folTb.futbTBOTNewlyDiagnosedInLastMonth);
                com.Parameters.AddWithValue("@futbTBOTStartDate", folTb.futbTBOTStartDate);
                com.Parameters.AddWithValue("@futbTBOTReferTBContactsToClinic", folTb.futbTBOTReferTBContactsToClinic);
                com.Parameters.AddWithValue("@futbTBOTPreviouslyOnMeds", folTb.futbTBOTPreviouslyOnMeds);
                com.Parameters.AddWithValue("@futbTBOTFinishDate", folTb.futbTBOTFinishDate);
                com.Parameters.AddWithValue("@futbTBOTConcerns", folTb.futbTBOTConcerns);
                com.Parameters.AddWithValue("@futbTBOTReferToClinic", folTb.futbTBOTReferToClinic);
                com.Parameters.AddWithValue("@futbTBOTRefNo", folTb.futbTBOTRefNo);

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

            #region Maternal health
            Impilo_App.LocalModels.FollowUpMaternalHealth folMat = new Impilo_App.LocalModels.FollowUpMaternalHealth();

            folMat.fumhID = 0;
            folMat.EncounterID = 0;
            folMat.fumhDateOfVisit = (DateTime)txtMatDateOfVisit.SelectedDate;
            folMat.fumhHiEHWentToClinic = (MatWentToClinic11.IsChecked == true) ? true : false;
            folMat.fumhHiEHReReferToClinic = (MatReReferToClinic11.IsChecked == true) ? true : false;
            folMat.fumhHiEHRefNo = txtMatReferralNo1.Text;
            folMat.fumhCPDateOfFirstANC = (DateTime)txtMatDateOf1stANC.SelectedDate;
            folMat.fumhCPDateOfLastANC = (DateTime)txtMatDateOfLastANC.SelectedDate;
            folMat.fumhCPReferToClinic = (MatReferredToClinic31.IsChecked == true) ? true : false;
            folMat.fumhCPRefNo = txtMatReferralNo3.Text;
            folMat.fumhCPRegisteredForMomConnect = (MatRegisteredForMoMConnect1.IsChecked == true) ? true : false;
            folMat.fumhCPReferToClinic2 = (MatReferredToClinic31.IsChecked == true) ? true : false;
            folMat.fumhCPRefNo2 = txtTBReferralNo2.Text;
            folMat.fumhCPDateOfNextANC = (DateTime)txtMatDateOfNextANC.SelectedDate;
            folMat.fumhCPExpectedDateOfDelivery = (DateTime)txtMatExpectedDateOfDelivery.SelectedDate;
            folMat.fumhCPIntendBreastFeed = (MatIntendBreastfeed1.IsChecked == true) ? true : false;
            folMat.fumhCPIntendFormulaFeed = (MatIntendFormulaFeed1.IsChecked == true) ? true : false;
            folMat.fumhPPPossiblePregnant = (MatIsItPosibleYouArePregnent1.IsChecked == true) ? true : false;
            folMat.fumhPPTestDone = (MatPregnancyTestDone1.IsChecked == true) ? true : false;
            folMat.fumhPPResult = ((ComboBoxItem)comboMatResult.SelectedItem).Content.ToString();
            folMat.fumhPPReferToClinic = (MatReferredToClinic21.IsChecked == true) ? true : false;
            folMat.fumhPPRefNo = txtMatReferralNo2.Text;

            try
            {
                storedProcedure = "AddFollowUpMaternalHealth";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fumhID", folMat.fumhID);
                com.Parameters.AddWithValue("@EncounterID", folMat.EncounterID);
                com.Parameters.AddWithValue("@fumhDateOfVisit", folMat.fumhDateOfVisit);
                com.Parameters.AddWithValue("@fumhHiEHWentToClinic", folMat.fumhHiEHWentToClinic);
                com.Parameters.AddWithValue("@fumhHiEHReReferToClinic", folMat.fumhHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fumhHiHRefNo", folMat.fumhHiEHRefNo);
                com.Parameters.AddWithValue("@fumhCPDateOfFirstANC", folMat.fumhCPDateOfFirstANC);
                com.Parameters.AddWithValue("@fumhCPDateOfLastANC", folMat.fumhCPDateOfLastANC);
                com.Parameters.AddWithValue("@fumhCPReferToClinic", folMat.fumhCPReferToClinic);
                com.Parameters.AddWithValue("@fumhCPRefNo", folMat.fumhCPRefNo);
                com.Parameters.AddWithValue("@fumhCPRegisteredForMomConnect", folMat.fumhCPRegisteredForMomConnect);
                com.Parameters.AddWithValue("@fumhCPReferToClinic2", folMat.fumhCPReferToClinic2);
                com.Parameters.AddWithValue("@fumhCPRefNo2", folMat.fumhCPRefNo2);
                com.Parameters.AddWithValue("@fumhCPDateOfNextANC", folMat.fumhCPDateOfNextANC);
                com.Parameters.AddWithValue("@fumhCPExpectedDateOfDelivery", folMat.fumhCPExpectedDateOfDelivery);
                com.Parameters.AddWithValue("@fumhCPIntendBreastFeed", folMat.fumhCPIntendBreastFeed);
                com.Parameters.AddWithValue("@fumhCPIntendFormulaFeed", folMat.fumhCPIntendFormulaFeed);
                com.Parameters.AddWithValue("@fumhPPPossiblePregnant", folMat.fumhPPPossiblePregnant);
                com.Parameters.AddWithValue("@fumhPPTestDone", folMat.fumhPPTestDone);
                com.Parameters.AddWithValue("@fumhPPResult", folMat.fumhPPResult);
                com.Parameters.AddWithValue("@fumhPPReferToClinic", folMat.fumhPPReferToClinic);
                com.Parameters.AddWithValue("@ffumhPPRefNo", folMat.fumhPPRefNo);

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

            Impilo_App.LocalModels.FollowUpChildHealth folCh = new Impilo_App.LocalModels.FollowUpChildHealth();

            folCh.fuchID = 0;
            folCh.EncounterID = 0;
            folCh.fuchDateOfVisit = (DateTime)txtChildDateOfVisit.SelectedDate;
            folCh.fuchHiEHWentToClinic = (ChiWentToClinic1.IsChecked == true) ? true : false;
            folCh.fuchHiEHReReferToClinic = (ChildReferToClinic11.IsChecked == true) ? true : false;
            folCh.fuchHiEHRefNo = txtChildReferralNo1.Text;
            folCh.fuchNewChildWithRTHC = (ChildChildWithRTHC1.IsChecked == true) ? true : false;
            folCh.fuchNewReferToClinic = (ChildReferToClinic41.IsChecked == true) ? true : false;
            folCh.fuchNewRefNo = txtChildReferralNo5.Text;
            folCh.fuchNewMotherHIVPos = (ChildMotherTHVPositive1.IsChecked == true) ? true : false;
            folCh.fuchNewChildBreastfed = (ChildChildBreastfed1.IsChecked == true) ? true : false;
            folCh.fuchNewHowLong = txtChildHowLong.Text;
            folCh.fuchNewChildEverOnNevirapine = (ChildClildEverOnNevirapine1.IsChecked == true) ? true : false;
            folCh.fuchNewReferToClinic2 = (ChildReferToClinic51.IsChecked == true) ? true : false;
            folCh.fuchNewRefNo2 = txtChildReferralNo6.Text;
            folCh.fuchNewHasPCRBeenDone = (ChildHowPCRHasDone1.IsChecked == true) ? true : false;
            folCh.fuchNewReferToClinic3 = (ChildReferToClinic61.IsChecked == true) ? true : false;
            folCh.fuchNewRefNo3 = txtChildReferralNo7.Text;
            folCh.fuchNewImmunisationUpToDate = (ChildImmunisationUpToDate1.IsChecked == true) ? true : false;
            folCh.fuchNewVitAWormMedsGivenEachMonth = (ChildVITAandWormMedsGivenEachMonth1.IsChecked == true) ? true : false;
            folCh.fuchNewReferToClinic4 = (ChildReferToClinic71.IsChecked == true) ? true : false;
            folCh.fuchNewRefNo4 = txtChildReferralNo8.Text;
            folCh.fuchCDevWalkAppropriatelyForAge = (ChildWalkAppropriateForAge1.IsChecked == true) ? true : false;
            folCh.fuchCDevTalkAppropriateForAge = (ChildTalkAppropriateForAge1.IsChecked == true) ? true : false;
            folCh.fuchCDevReferToClinic = (ChildReferToClinic21.IsChecked == true) ? true : false;
            folCh.fuchCDevRefNo = txtChildReferralNo2.Text;
            folCh.fuchSocDevChildAssisted = (ChildChildAssisted1.IsChecked == true) ? true : false;
            folCh.fuchSocDevReReferToSD = (ChildReReferToSD1.IsChecked == true) ? true : false;
            folCh.fuchSocDevRefNo = txtChildReferralNo3.Text;
            folCh.fuchCurSocDevReferToClinic = (ChildReferToClinic31.IsChecked == true) ? true : false;
            folCh.fuchCurSocDevReferToSD = (ChildreferToSD1.IsChecked == true) ? true : false;
            folCh.fuchCurSocDevRefNo = txtChildReferralNo4.Text;

            try
            {
                storedProcedure = "AddFollowUpOther";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fuoID", folCh.fuchID);
                com.Parameters.AddWithValue("@EncounterID", folCh.EncounterID);
                com.Parameters.AddWithValue("@fuchDateOfVisit", folCh.fuchDateOfVisit);
                com.Parameters.AddWithValue("@fuchHiEHWentToClinic", folCh.fuchHiEHWentToClinic);
                com.Parameters.AddWithValue("@fuchHiEHReReferToClinic", folCh.fuchHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fuchHiEHRefNo", folCh.fuchHiEHRefNo);
                com.Parameters.AddWithValue("@fuchNewChildWithRTHC", folCh.fuchNewChildWithRTHC);
                com.Parameters.AddWithValue("@fuchNewReferToClinic", folCh.fuchNewReferToClinic);
                com.Parameters.AddWithValue("@fuchNewRefNo", folCh.fuchNewRefNo);
                com.Parameters.AddWithValue("@fuchNewMotherHIVPos", folCh.fuchNewMotherHIVPos);
                com.Parameters.AddWithValue("@fuchNewChildBreastfed", folCh.fuchNewChildBreastfed);
                com.Parameters.AddWithValue("@fuchNewChildEverOnNevirapine", folCh.fuchNewChildEverOnNevirapine);
                com.Parameters.AddWithValue("@fuchNewReferToClinic2", folCh.fuchNewReferToClinic2);
                com.Parameters.AddWithValue("@fuchNewRefNo2", folCh.fuchNewRefNo2);
                com.Parameters.AddWithValue("@fuchNewHasPCRBeenDone", folCh.fuchNewHasPCRBeenDone);
                com.Parameters.AddWithValue("@fuchNewReferToClinic3", folCh.fuchNewReferToClinic3);
                com.Parameters.AddWithValue("@fuchNewRefNo3", folCh.fuchNewRefNo3);
                com.Parameters.AddWithValue("@fuchNewImmunisationUpToDate", folCh.fuchNewImmunisationUpToDate);
                com.Parameters.AddWithValue("@fuchNewVitAWormMedsGivenEachMonth", folCh.fuchNewVitAWormMedsGivenEachMonth);
                com.Parameters.AddWithValue("@fuchNewReferToClinic4", folCh.fuchNewReferToClinic4);
                com.Parameters.AddWithValue("@fuchNewRefNo4", folCh.fuchNewRefNo4);
                com.Parameters.AddWithValue("@fuchCDevWalkAppropriatelyForAge", folCh.fuchCDevWalkAppropriatelyForAge);
                com.Parameters.AddWithValue("@fuchCDevTalkAppropriateForAg", folCh.fuchCDevTalkAppropriateForAge);
                com.Parameters.AddWithValue("@fuchCDevReferToClinic", folCh.fuchCDevReferToClinic);
                com.Parameters.AddWithValue("@fuchCDevRefNo", folCh.fuchCDevRefNo);
                com.Parameters.AddWithValue("@fuchSocDevChildAssisted", folCh.fuchSocDevChildAssisted);
                com.Parameters.AddWithValue("@fuchSocDevReReferToSD", folCh.fuchSocDevReReferToSD);
                com.Parameters.AddWithValue("@fuchSocDevRefNo", folCh.fuchSocDevRefNo);
                com.Parameters.AddWithValue("@fuchCurSocDevReferToClinic", folCh.fuchCurSocDevReferToClinic);
                com.Parameters.AddWithValue("@fuchCurSocDevReferToSD", folCh.fuchCurSocDevReferToSD);
                com.Parameters.AddWithValue("@fuchCurSocDevRefNo", folCh.fuchCurSocDevRefNo);



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

            Impilo_App.LocalModels.FollowUpOther folOth = new Impilo_App.LocalModels.FollowUpOther();

            folOth.fuoID = 0;
            folOth.EncounterID = 0;
            folOth.fuoDateOfVisit = (DateTime)txtOtherDateOfVisit.SelectedDate;
            folOth.fuoHiEHWentToClinic = (OtherWentToClinic1.IsChecked == true) ? true : false;
            folOth.fuoHiEHReReferToClinic = (OtherReReferToClinic1.IsChecked == true) ? true : false;
            folOth.fuoHiEHRefNo = txtOtherReferralNo1.Text;
            folOth.fuoOCCondition = txtOtherConditionTha.Text;
            folOth.fuoOCReferToClinic = (OtherReferToClinic11.IsChecked == true)? true: false; 
            folOth.fuoOCRefNo = txtOtherReferralNo2.Text;


            try
            {
                storedProcedure = "AddFollowUpOther";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fuoID", folOth.fuoID);
                com.Parameters.AddWithValue("@EncounterID", folOth.EncounterID);
                com.Parameters.AddWithValue("@fuoDateOfVisit", folOth.fuoDateOfVisit);
                com.Parameters.AddWithValue("@fuoHiEHWentToClinic", folOth.fuoHiEHWentToClinic);
                com.Parameters.AddWithValue("@fuoHiEHReReferToClinic", folOth.fuoHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fuoHiEHRefNo", folOth.fuoHiEHRefNo);
                com.Parameters.AddWithValue("@fuoOCCondition", folOth.fuoOCCondition);
                com.Parameters.AddWithValue("@fuoOCReferToClinic", folOth.fuoOCReferToClinic);
                com.Parameters.AddWithValue("@fuoOCRefNo", folOth.fuoOCRefNo);


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
