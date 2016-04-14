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
using Impilo_App.Views.Screening;

namespace Impilo_App.Views.FollowUpVisit
{
    /// <summary>
    /// Interaction logic for FollowUp.xaml
    /// </summary>
    public partial class FollowUp : UserControl
    {
        static string sconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        // SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=impilo;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(sconn);
        Impilo_App.LocalModels.Client currentClient;
        int encounterID = 0;
        DataSet ds = null;
        DataSource datasource = null;

        public FollowUp(Impilo_App.LocalModels.Client client)
        {
            InitializeComponent();

          ///  Activve the button
           // Window parent = Window.GetWindow(this);
           // MainWindow parent =(MainWindow) VisualTreeHelper.GetParent(this);
           // parent.btnFollowUp.Background = Brushes.Black;
            currentClient = client;
            datasource = new DataSource();
            this.DataContext = datasource;

            loadBio();
            bindClinicCBO();

        }

        public void bindClinicCBO()
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Clinic", conn);
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
                //  dt = ds.Tables[0];
                ClinicUsed.ItemsSource = ds.Tables[0].DefaultView;
               // cboClinic.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public void loadBio()
        {
            lblIdentity.Content = currentClient.ClientID;

            txtName.Text = currentClient.FirstName;
            txtLastName.Text = currentClient.LastName;
            txtIDNumber.Text = currentClient.IDNo;
            txtDateOfScreening.Text = DateTime.Now.ToString("dd MMMM yyyy h:mm");
            //txtDateofBirth.DisplayDate = currentClient.DateOfBirth;
            txtDateofBirth.SelectedDate = currentClient.DateOfBirth;
            txtLatitude.Text = currentClient.GPSLatitude;
            txtLongitude.Text = currentClient.GPSLongitude;


            if (currentClient.Gender == "Male" || currentClient.Gender == "male")
            {
                rdoMale.IsChecked = true;
            }
            else
            {
                rdoFemale.IsChecked = true;
            }

            if (currentClient.HeadOfHousehold == "yes")
            {
                rdohYes.IsChecked = true;
                
            }
            else
            {
                rdoHNo.IsChecked = true;
                
            }


            if (currentClient.AttendingSchool)
            {
                radioAttYes.IsChecked = true;
            }
            else radioAttNo.IsChecked = true;

            NameofSchool.SelectedIndex = NameofSchool.Items.IndexOf(currentClient.NameofSchool);
            Grade.SelectedIndex = Grade.Items.IndexOf(currentClient.Grade);
            grpBio.IsEnabled = false;
        }

        private void btnCreateFollowUp_Click(object sender, RoutedEventArgs e)
        {

            
            string storedProcedure = "";

        

            encounterID = 6;
            #region Testing area -------------------------------------------------------------------------------------

       
            #endregion -----------------------------------------------------------------------------------------------


            #region Not being tested right now
            #region SaveEncounters

            Impilo_App.LocalModels.Screening newSCreen;

            bool goforEncouter = false;
            try
            {
                newSCreen = new Impilo_App.LocalModels.Screening
                {
                    ScreeningID = "",
                    ScreeningDate = DateTime.Now,
                    ClientId = currentClient.ClientID,
                    EncounterCapturedBy = cboChow.SelectedItem.ToString()
                };

                goforEncouter = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "Currently Screened Tab", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }



            if (goforEncouter)
            {



                //sp place
                //connection
                try
                {
                    storedProcedure = "AddEncounters";// name of sp
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.AddWithValue("@ClientID", newSCreen.ClientId);//param
                    com.Parameters.AddWithValue("@EncounterDate", newSCreen.ScreeningDate);//param

                    com.Parameters.AddWithValue("@EncounterType", 2);//param
                    com.Parameters.AddWithValue("@EncounterCapturedBy", newSCreen.EncounterCapturedBy);

                    encounterID = (int)((decimal)com.ExecuteScalar());
                    //com.ExecuteNonQuery();//execute command

                    // MessageBox.Show(encounterID.ToString());


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }

            }

            #endregion


            #region Visit Details + tested +

            Impilo_App.LocalModels.FollowUpVisitDetails folVD = new Impilo_App.LocalModels.FollowUpVisitDetails();
            Random rnd = new Random();

            folVD.fuvdID = rnd.Next(1, 1000000);
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
                storedProcedure = "AddFollowUpVisitDetails";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fuvdID", folVD.fuvdID);
                com.Parameters.AddWithValue("@EncounterID", encounterID);
                com.Parameters.AddWithValue("@fuvdVisitDate", folVD.fuvdVisitDate);
                com.Parameters.AddWithValue("@fuvdNextVisitDate", folVD.fuvdNextVisitDate);
                com.Parameters.AddWithValue("@duvdOutcome", folVD.duvdOutcome);
                com.Parameters.AddWithValue("@duvdHypertension", folVD.duvdHypertension);

                com.Parameters.AddWithValue("@duvdDiabetes", folVD.duvdDiabetes);
                com.Parameters.AddWithValue("@duvdEpilepsy", folVD.duvdEpilepsy);
                //com.Parameters.AddWithValue("@duvdAsthma", folVD.duvdAsthma);
                com.Parameters.AddWithValue("@duvdHIV", folVD.duvdHIV);
                com.Parameters.AddWithValue("@duvdTB", folVD.duvdTB);
                com.Parameters.AddWithValue("@duvdMaternalHealth", folVD.duvdMaternalHealth);
                com.Parameters.AddWithValue("@duvdChildHealth", folVD.duvdChildHealth);
                com.Parameters.AddWithValue("@duvdOther", false); //assigned abitrally to be resived asap
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

<<<<<<< HEAD


=======
            
             
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
            #region Hypertension UI Fields needs attention intupdown tested+- [repairs done, not tested]

            bool hypFollow = false;
            Impilo_App.LocalModels.FollowUpHypertension folHyp = new Impilo_App.LocalModels.FollowUpHypertension();
            //Impilo_App.LocalModels.FollowUpHypertensionMedication folHypMed = new Impilo_App.LocalModels.FollowUpHypertensionMedication();
            try
            {

                //folHyp.fuhID = 0; //remove
                folHyp.EncounterID = encounterID;
                folHyp.fuhDateOfVisit = DateTime.Now;
                folHyp.fuhHiEHWentToClinic = (HyperWentToClinic1Yes.IsChecked == true) ? true : false;
                folHyp.fuhHiEHReferToClinic = (HyperReferToClinicYes.IsChecked == true) ? true : false;
                folHyp.fuhHiEHRefNo2 = txtHyperReferralNo.Text;
                folHyp.fuhHiEHCurrentlyOnMeds = (HyperCurrentlyOnMedsYes.IsChecked == true) ? true : false;
                folHyp.fuhHiEHStartDate = (DateTime)dpHyperStartDate.SelectedDate;
                folHyp.fuhHiEHBPScreeningSystolic = decimal.Parse(txtHyperBPReadingSystolic.Text);
                folHyp.fuhHiEHBPScreeningDiastolic = decimal.Parse(txtHyperBPReadingDiastolic.Text);
                folHyp.fuhHiEHBPTodaySystolic = decimal.Parse(txtHyperTodayTestReadingSystolic.Text);
                folHyp.fuhHiEHBPTodayDiastolic = decimal.Parse(txtHyperTodayTestReadingDiastolic.Text);
                folHyp.fuhHiEHReferToClinic = (HyperReferToClinicYes.IsChecked == true) ? true : false;
                folHyp.fuhHiEHRefNo = txtHyperReReferralNo.Text;
<<<<<<< HEAD
                folHyp.fuhHiEHReReferToClinic = (HyperReReferToClinicYes.IsChecked == true) ? true : false;
                folHyp.fuhHiEHRefNo2 = txtHyperReReferralNo.Text;
                //folHyp.fuhCRReReferToClinic =
                // folHyp.fuhCRRefNo = 
                folHyp.fuhHiEHNextVisit = ((ComboBoxItem)comboHyperRevisit.SelectedItem).Content.ToString();
                folHyp.fuhHiEHOutcomes = ((ComboBoxItem)comboHypVisitOutCome.SelectedItem).Content.ToString();

=======
                folHyp.fuhHiEHReReferToClinic =  (HyperReReferToClinicYes.IsChecked == true) ? true : false;
                folHyp.fuhHiEHRefNo2 =txtHyperReReferralNo.Text; 
                //folHyp.fuhCRReReferToClinic =
               // folHyp.fuhCRRefNo = 
                folHyp.fuhHiEHNextVisit = ((ComboBoxItem)comboHyperRevisit.SelectedItem).Content.ToString();
                folHyp.fuhHiEHOutcomes = ((ComboBoxItem)comboHypVisitOutCome.SelectedItem).Content.ToString();
                    
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
                //folHyp.fuhAlreadyOnTreatmentFollowUpTestReading = txtHyperFollowUpTestReading.Text;
                //folHyp.fuhDoorToDoorCheckReadingSys = decimal.Parse(txtDoortoDoorCheckReadingSys.Text);
                //folHyp.fuhDoorToDoorCheckReadingDia = decimal.Parse(txtDoortoDoorCheckReadingDia.Text);

                //folHyp.fuhMedication = (ComboBoxItem)comboHyperMedication.SelectedItem).Content.ToString();

                //  folHypMed.fuhmName = ((ComboBoxItem)comboHyperMedication.SelectedItem).Content.ToString();

                hypFollow = true;
            }

            catch (Exception)
            {

                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "Hypertension Tab", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            if (hypFollow)
            {


                try
                {
                    storedProcedure = "AddFollowUpHypertension";
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);
                    com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.AddWithValue("@fuhID", folHyp.fuhID);
                    com.Parameters.AddWithValue("@EncounterID", encounterID);
                    com.Parameters.AddWithValue("@fuhHiEHWentToClinic", folHyp.fuhHiEHWentToClinic);
                    com.Parameters.AddWithValue("@fuhDateOfVisit", folHyp.fuhDateOfVisit);
                    com.Parameters.AddWithValue("@fuhHiEHReferToClinic", folHyp.fuhHiEHReferToClinic);
                    com.Parameters.AddWithValue("@fuhHiEHRefNo", folHyp.fuhHiEHRefNo);
                    com.Parameters.AddWithValue("@fuhHiEHCurrentlyOnMeds", folHyp.fuhHiEHCurrentlyOnMeds);
                    com.Parameters.AddWithValue("@fuhHiEHStartDate", folHyp.fuhHiEHStartDate);
                    com.Parameters.AddWithValue("@fuhHiEHBPScreeningSystolic", folHyp.fuhHiEHBPScreeningSystolic);
                    com.Parameters.AddWithValue("@fuhHiEHBPScreeningDiastolic", folHyp.fuhHiEHBPScreeningDiastolic);
                    com.Parameters.AddWithValue("@fuhHiEHBPTodaySystolic", folHyp.fuhHiEHBPTodaySystolic);
                    com.Parameters.AddWithValue("@fuhHiEHBPTodayDiastolic", folHyp.fuhHiEHBPTodayDiastolic);
                    com.Parameters.AddWithValue("@fuhHiEHReReferToClinic", folHyp.fuhHiEHReReferToClinic);
                    com.Parameters.AddWithValue("@fuhHiEHReRefNo2", folHyp.fuhHiEHRefNo2);
                    //com.Parameters.AddWithValue("@fuhCRReReferToClinic", folHyp.fuhCRReReferToClinic);
                    //com.Parameters.AddWithValue("@fuhCRRefNo", folHyp.fuhCRRefNo);
                    com.Parameters.AddWithValue("@fuhHiEHNextVisit", folHyp.fuhHiEHNextVisit);
                    com.Parameters.AddWithValue("@fuhHiEHOutcomes", folHyp.fuhHiEHOutcomes);
                    //com.Parameters.AddWithValue("@fuhAlreadyOnTreatmentFollowUpTestReadingSystolic",70);
                    //com.Parameters.AddWithValue("@fuhAlreadyOnTreatmentFollowUpTestReadingDiastolic", 110);
                    //com.Parameters.AddWithValue("@fuhDoorToDoorCheckReadingSystolic", folHyp.fuhDoorToDoorCheckReadingSys);
                    //com.Parameters.AddWithValue("@fuhDoorToDoorCheckReadingDiastolic", folHyp.fuhDoorToDoorCheckReadingDia);
                    //com.Parameters.AddWithValue("@fuhMedication", folHyp.fuhMedication);

                    int lastestHypID = (int)((decimal)com.ExecuteScalar());

                    //adding Medication

                    if (listofHypMeds.Count > 0)
                    {
                        conn.Close();

                        foreach (var hypmed in listofHypMeds)
                        {
                            try
                            {
                                storedProcedure = "AddFollowUpHypertensionMedication";// name of sp
                                conn.Open();
                                SqlCommand tempcom = new SqlCommand(storedProcedure, conn);
                                tempcom.CommandType = CommandType.StoredProcedure;
                                tempcom.Parameters.AddWithValue("@fuhmID", lastestHypID);//param
                                tempcom.Parameters.AddWithValue("@fuhmName", hypmed.fuhmName);


                                tempcom.ExecuteNonQuery();//execute command
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString() + "Meds not added");
                            }

                            //com.ExecuteNonQuery();//execute command
                        }

                    }
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }

            #endregion 


<<<<<<< HEAD

=======
           
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
            #region Diabetes same issue s as hypertension tested+ [repairs done, not tested]

            bool diaFollow = false;

            Impilo_App.LocalModels.FollowUpDiabetes folDia = new Impilo_App.LocalModels.FollowUpDiabetes();
            Impilo_App.LocalModels.FollowUpDiabetesMedication folDiaMed = new Impilo_App.LocalModels.FollowUpDiabetesMedication();
            try
            {
                //folDia.fudID = 1;
                folDia.EncounterID = encounterID;
                folDia.fudDateOfVisit = DateTime.Now;
                folDia.fudHiEHWentToClinic = (DaiWentToClinic1.IsChecked == true) ? true : false;
                // folDia.fudHiEHReReferToClinic = (DiaReReferToClinic11.IsChecked == true) ? true : false;
                // folDia.fudHiEHRefNo = txtDiaReferralNo1.Text;
                folDia.fudHiEHCurrentlyOnMeds = (DiaCurrentlyOnMeds1.IsChecked == true) ? true : false;
                folDia.fudHiEHStartDate = (DateTime)DiaStartDate.SelectedDate;
                folDia.fudHiEHFollowUpTestReading = decimal.Parse(txtDiaFollowUpTestReading1.Text);
                folDia.fudHiEHReferToClinic = (DiaReferToClinicYes.IsChecked == true) ? true : false;
                folDia.fudHiEHRefNo = txtDiaReferralNo.Text;
                folDia.fudHiEHReReferToClinic = (DiaReReferToClinicYes.IsChecked == true) ? true : false;
                folDia.fudHiEHReRefNo = txtDiaReReferralNo.Text;
                folDia.fudHiEHNextVisit = ((ComboBoxItem)comboDiaRevisit.SelectedItem).Content.ToString();
                folDia.fudHiEHOutcomes = ((ComboBoxItem)comboDiaVisitOutCome.SelectedItem).Content.ToString();


                // folDia.fudAlreadyOnTreatmentFollowUpTestReading = decimal.Parse(txtDiaFollowUpTestReading3.Text);
                // folDia.fudDoorDoor = txtDiaCheckReading.Text;

                diaFollow = true;

            }
            catch (Exception)
            {

                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "Diabetes Tab", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            if (diaFollow)
            {

                try
                {
                    storedProcedure = "AddFollowUpDiabetes";
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);
                    com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.AddWithValue("@fudID", folDia.fudID);
                    com.Parameters.AddWithValue("@EncounterID", encounterID);
                    com.Parameters.AddWithValue("@fudDateOfVisit", folDia.fudDateOfVisit);
                    com.Parameters.AddWithValue("@fudHiEHWentToClinic", folDia.fudHiEHWentToClinic);
                    //com.Parameters.AddWithValue("@fudHiEHReReferToClinic", folDia.fudHiEHReReferToClinic);
                    //com.Parameters.AddWithValue("@fudHiEHRefNo", folDia.fudHiEHRefNo);
                    com.Parameters.AddWithValue("@fudHiEHCurrentlyOnMeds", folDia.fudHiEHCurrentlyOnMeds);
                    com.Parameters.AddWithValue("@fudHiEHStartDate", folDia.fudHiEHStartDate);
                    com.Parameters.AddWithValue("@fudStartDateUnknown", folDia.fudStartDateUnknown);
                    com.Parameters.AddWithValue("@fudHiEHFollowUpTestReading", folDia.fudHiEHFollowUpTestReading);
                    com.Parameters.AddWithValue("@fudHiEHReferToClinic", folDia.fudHiEHReferToClinic);
                    com.Parameters.AddWithValue("@fudHiEHRefNo", folDia.fudHiEHRefNo);
                    com.Parameters.AddWithValue("@fudHiEHReReferToClinic", folDia.fudHiEHReReferToClinic);
                    com.Parameters.AddWithValue("@fudHiEHReRefNo", folDia.fudHiEHReRefNo);
                    com.Parameters.AddWithValue("@fudHiEHNextVisit", folDia.fudHiEHNextVisit);
                    com.Parameters.AddWithValue("@fudHiEHOutcomes", folDia.fudHiEHOutcomes);

                    // com.Parameters.AddWithValue("@fudAlreadyOnTreatmentFollowUpTestReading", folDia.fudAlreadyOnTreatmentFollowUpTestReading);
                    // com.Parameters.AddWithValue("@fudDoorDoor", folDia.fudDoorDoor);
                    //com.Parameters.AddWithValue("@fudMedication", folDia.fudMedication);

                    int lastestDiaID = (int)((decimal)com.ExecuteScalar());

                    //adding Medication

                    if (listofDiaMeds.Count > 0)
                    {
                        conn.Close();

                        foreach (var diamed in listofDiaMeds)
                        {
                            try
                            {
                                storedProcedure = "AddFollowUpDiabetesMedication";// name of sp
                                conn.Open();
                                SqlCommand tempcom = new SqlCommand(storedProcedure, conn);
                                tempcom.CommandType = CommandType.StoredProcedure;
                                tempcom.Parameters.AddWithValue("@fudmID", lastestDiaID);//param
                                tempcom.Parameters.AddWithValue("@fudmName", folDiaMed.fudmName);


                                tempcom.ExecuteNonQuery();//execute command
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString() + "Meds not added");
                            }

                            //com.ExecuteNonQuery();//execute command
                        }

                    }
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
            #endregion
          
<<<<<<< HEAD


=======
           
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
            #region HIV [repairs done - not tested]

            bool hivFollow = false;

            Impilo_App.LocalModels.FollowUpHIV folHiv = new Impilo_App.LocalModels.FollowUpHIV();
            Impilo_App.LocalModels.FollowUpHIVMedication folHivMed = new Impilo_App.LocalModels.FollowUpHIVMedication();

            try
            {
                //folHiv.fuhivID = 0;
                folHiv.EncounterID = encounterID;
                folHiv.fuhivHiEHWentToClinic = (HIVWentToClinicYes.IsChecked == true) ? true : false;
                folHiv.fuhHIVStatus = ((ComboBoxItem)comboHIVStatus.SelectedItem).Content.ToString();
                folHiv.fuhivCRReferToClinic = (HIVReferToClinicYes.IsChecked == true) ? true : false;
                folHiv.fuhivCRRefNo = txtHIVReferralNo2.Text;
                folHiv.fuhivHiEHReReferToClinic = (HIVReReferToClinicYes.IsChecked == true) ? true : false;
<<<<<<< HEAD
                folHiv.fuhivHiEHRefNo = txtHIVReReferralNo.Text;
=======
                folHiv.fuhivHiEHRefNo = txtHIVReReferralNo.Text;            
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
                folHiv.fuhivIPOnARV = (HIVOnARVsYes.IsChecked == true) ? true : false;
                folHiv.fuhivIPStartDate = (DateTime)txtHIVStartDate.SelectedDate;
                folHiv.fuhivIPAdherenceOK = (HIVAdherenceOKYes.IsChecked == true) ? true : false;
                folHiv.fuhivINCounsellingDone = (HIVCounsellingGivenYes.IsChecked == true) ? true : false;
                folHiv.fuhivHIVTestResults = ((ComboBoxItem)comboHIVTestResults.SelectedItem).Content.ToString();
                folHiv.fuhivHIVTestReferToClinic = (HIVReferToClinic2Yes.IsChecked == true) ? true : false;
                folHiv.fuhivHIVRefNo = txtHIVReferralNo2.Text;
                folHiv.fuhivHiEHOutcomes = ((ComboBoxItem)comboHivVisitOutCome.SelectedItem).Content.ToString();
                folHiv.fuhivHiEHRevisit = ((ComboBoxItem)comboHivRevisit.SelectedItem).Content.ToString();

                //  folHiv.fuhivIPConcerns = (HIVConcerns1.IsChecked == true) ? true : false;
                // folHiv.fuhivIPReferToClinic = (HIVReferToClinic21.IsChecked == true) ? true : false;
                //   folHiv.fuhivIPRefNo = txtHIVReferralNo3.Text;
                //folHiv.fuhivIPNotOnARV = (HIVARVsConcernsYes.IsChecked == true) ? true : false;
                //   folHiv.fuhivIPReferToClinic2 = (HIVReferToClinic31.IsChecked == true) ? true : false;
                //   folHiv.fuhivIPRefNo2 = txtHIVReferralNo4.Text;

                //   folHiv.fuhivIUHIVTestDone = (HIVTestingDone1.IsChecked == true) ? true : false;
                //    folHiv.fuhivHIVTestResults = txtHIVTestResults.Text;



                hivFollow = true;
            }

            catch (Exception)
            {

                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "HIV Tab", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

            if (hivFollow)
            {
                try
                {
                    storedProcedure = "AddFollowUpHIV";
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);
                    com.CommandType = CommandType.StoredProcedure;

                    //com.Parameters.AddWithValue("@fuhivID", folHiv.fuhivID);
                    com.Parameters.AddWithValue("@EncounterID", folHiv.EncounterID);
                    com.Parameters.AddWithValue("@fuhivDateOfVisit", folHiv.fuhivDateOfVisit);
                    com.Parameters.AddWithValue("@fuhivHiEHWentToClinic", folHiv.fuhivHiEHWentToClinic);
                    com.Parameters.AddWithValue("@fuhivHIVStatus", folHiv.fuhivHIVStatus);
                    com.Parameters.AddWithValue("@fuhivCRReferToClinic", folHiv.fuhivCRReferToClinic);
                    com.Parameters.AddWithValue("@fuhivCRRefNo", folHiv.fuhivCRRefNo);
                    com.Parameters.AddWithValue("@fuhivHiEHReReferToClinic", folHiv.fuhivHiEHReReferToClinic);
                    com.Parameters.AddWithValue("@fuhivHiEHRefNo", folHiv.fuhivHiEHRefNo);
<<<<<<< HEAD


                    // com.Parameters.AddWithValue("@fuhivHIVStatus", folHiv.fuhivHIVStatus);
=======
                  
                    
                   // com.Parameters.AddWithValue("@fuhivHIVStatus", folHiv.fuhivHIVStatus);
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
                    com.Parameters.AddWithValue("@fuhivIPOnARV", folHiv.fuhivIPOnARV);
                    com.Parameters.AddWithValue("@fuhivIPStartDate", folHiv.fuhivIPStartDate);
                    com.Parameters.AddWithValue("@fuhivIPAdherenceOK", folHiv.fuhivIPAdherenceOK);
                    //com.Parameters.AddWithValue("@fuhivIPConcerns", folHiv.fuhivIPConcerns);
                    //com.Parameters.AddWithValue("@fuhivIPReferToClinic", folHiv.fuhivIPReferToClinic);
                    //com.Parameters.AddWithValue("@fuhivIPRefNo", folHiv.fuhivIPRefNo);
                    //com.Parameters.AddWithValue("@fuhivIPNotOnARV", folHiv.fuhivIPNotOnARV);
                    //com.Parameters.AddWithValue("@fuhivIPReferToClinic2", folHiv.fuhivIPReferToClinic2);
                    //com.Parameters.AddWithValue("@fuhivIPRefNo2", folHiv.fuhivIPRefNo2);
                    com.Parameters.AddWithValue("@fuhivINCounsellingDone", folHiv.fuhivINCounsellingDone);
                    //com.Parameters.AddWithValue("@fuhivIUHIVTestDone", folHiv.fuhivIUHIVTestDone);
                    com.Parameters.AddWithValue("@fuhivHIVTestResults", folHiv.fuhivHIVTestResults);
                    com.Parameters.AddWithValue("@fuhivHIVTestReferToClinic", folHiv.fuhivHIVTestReferToClinic);
                    com.Parameters.AddWithValue("@fuhivHIVRefNo", folHiv.fuhivHIVRefNo);
                    com.Parameters.AddWithValue("@fuhivHiEHOutcomes", folHiv.fuhivHiEHOutcomes);
                    com.Parameters.AddWithValue("@fuhivHiEHRevisit", folHiv.fuhivHiEHRevisit);
<<<<<<< HEAD

=======
                  
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086

                    int lastestID = (int)((decimal)com.ExecuteScalar());

                    //adding Medication

                    if (listofHIVMeds.Count > 0)
                    {
                        conn.Close();

                        foreach (var hivmed in listofHIVMeds)
                        {
                            try
                            {
                                storedProcedure = "AddFollowUpHIVMedication";// name of sp
                                conn.Open();
                                SqlCommand tempcom = new SqlCommand(storedProcedure, conn);
                                tempcom.CommandType = CommandType.StoredProcedure;
                                tempcom.Parameters.AddWithValue("@fuhivID", lastestID);//param
                                tempcom.Parameters.AddWithValue("@fuhivmName", folHivMed.fuhivmName);


                                tempcom.ExecuteNonQuery();//execute command
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString() + "Meds not added");
                            }

                            //com.ExecuteNonQuery();//execute command
                        }

                    }
                }

                catch (Exception ex)
                {

                    // MessageBox.Show(ex.Message.ToString()); // comment this secton at production
                }
                finally
                {
                    conn.Close();
                }
            }

            #endregion
<<<<<<< HEAD
=======
           
           
            
            #region Epilepsy tested + [repairs done - not tested]

            bool epiFollow = false;

            Impilo_App.LocalModels.FollowUpEpilepsy folEpi = new Impilo_App.LocalModels.FollowUpEpilepsy();
            Impilo_App.LocalModels.FollowUpEpilepsyMedication folEpiMed = new Impilo_App.LocalModels.FollowUpEpilepsyMedication();

            try
            {
                //folEpi.fueID = 0;
                folEpi.EncounterID = encounterID;
                folEpi.fueHiEHWentToClinic = (EpiWentToClinic1.IsChecked == true) ? true : false;
                folEpi.fueCRFitInLastMonth = (EpiFitInLastMonth1.IsChecked == true) ? true : false;
                folEpi.fueCRReferToClinic = (EpiReferToClinicYes.IsChecked == true) ? true : false;
                folEpi.fueCRRefNo = txtEpiRefNo.Text;
                folEpi.fueHiEHReReferToClinic = (EpiReReferToClinicYes.IsChecked == true) ? true : false;
                folEpi.fueHiEHRefNo = txtEpiReRefNo.Text;
                folEpi.fueOnTreatmentCurrentlyOnMeds = (EpiCurrentlyOnMeds1.IsChecked == true) ? true : false;
                folEpi.fueOnTreatmentStartDate = (DateTime)txtEpiStartDate.SelectedDate;
                folEpi.fueOnTreatmentMoreThan3FitsSinceLastMonth = (EpiMoreThan3FitsInLastMonth1.IsChecked == true) ? true : false;
                folEpi.fueHiEHRevisit = ((ComboBoxItem)comboEpiRevisit.SelectedItem).Content.ToString();
                folEpi.fueHiEHOutcomes = ((ComboBoxItem)comboEpiOutcomes.SelectedItem).Content.ToString();

                epiFollow = true;
            }
            catch (Exception)
            {

                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "HIV Tab", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            if (epiFollow)
            {


                try
                {
                    storedProcedure = "AddFollowUpEpilepsy";
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);
                    com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.AddWithValue("@fueID", folEpi.fueID);
                    com.Parameters.AddWithValue("@EncounterID", folEpi.EncounterID);
                    com.Parameters.AddWithValue("@fueHiEHWentToClinic", folEpi.fueHiEHWentToClinic);
                    com.Parameters.AddWithValue("@fueCRFitInLastMonth", folEpi.fueCRFitInLastMonth);
                    com.Parameters.AddWithValue("@fueCRReferToClinic", folEpi.fueCRReferToClinic);
                    com.Parameters.AddWithValue("@fueCRRefNo", folEpi.fueCRRefNo);
                    com.Parameters.AddWithValue("@fueHiEHReReferToClinic", folEpi.fueHiEHReReferToClinic);
                    com.Parameters.AddWithValue("@fueHiEHRefNo", folEpi.fueHiEHRefNo);
                    com.Parameters.AddWithValue("@fueOnTreatmentCurrentlyOnMeds", folEpi.fueOnTreatmentCurrentlyOnMeds);
                    com.Parameters.AddWithValue("@fueOnTreatmentStartDate", folEpi.fueOnTreatmentStartDate);
                    com.Parameters.AddWithValue("@fueOnTreatmentMoreThan3FitsSinceLastMonth", folEpi.fueOnTreatmentMoreThan3FitsSinceLastMonth);
                   // com.Parameters.AddWithValue("@fueOnTreatmentReReferToClinic", folEpi.fueOnTreatmentReReferToClinic);
                   // com.Parameters.AddWithValue("@fueOnTreatmentRefNo", folEpi.fueOnTreatmentRefNo);
                    com.Parameters.AddWithValue("@fueHiEHRevisit", folEpi.fueHiEHRevisit);
                    com.Parameters.AddWithValue("@fueHiEHOutcomes", folEpi.fueHiEHOutcomes);


                    int lastestEpiID = (int)((decimal)com.ExecuteScalar());
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086


<<<<<<< HEAD
=======
                    if (listofEpiMeds.Count > 0)
                    {
                        conn.Close();

                        foreach (var epimed in listofEpiMeds)
                        {
                            try
                            {
                                storedProcedure = "AddFollowUpHIVMedication";// name of sp
                                conn.Open();
                                SqlCommand tempcom = new SqlCommand(storedProcedure, conn);
                                tempcom.CommandType = CommandType.StoredProcedure;
                                tempcom.Parameters.AddWithValue("@fuemID", lastestEpiID);//param
                                tempcom.Parameters.AddWithValue("@fuemName", folEpiMed.fuemName);


                                tempcom.ExecuteNonQuery();//execute command
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString() + "Meds not added");
                            }

                            //com.ExecuteNonQuery();//execute command
                        }

                    }
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }



            #endregion

             
           
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
            #region Asthma tested+ [repairs done -not tested]

            FollowUpAsthma folAst = new FollowUpAsthma();
            FollowUpAsthmaMedication folAstMed = new FollowUpAsthmaMedication();

            bool astFollow = false;


            try
            {
                //folAst.fuaID = 0;
                folAst.EncounterID = encounterID;
                //folAst.fuaDateOfVisit = (DateTime)txtAsDateOfVisit.SelectedDate;
                folAst.fuaHiEHWentToClinic = (AsWentToClinic1.IsChecked == true) ? true : false;
                folAst.fuaAttacksLastMonth = (AsAttInLastMonthYes.IsChecked == true) ? true : false;
                folAst.fuaCRReferToClinic = (AsReferToClinic1.IsChecked == true) ? true : false;
                folAst.fuaCRRefNo = txtAsReferralNo2.Text;
                folAst.fuaHiEHReReferToClinic = (AsReReferToClinic11.IsChecked == true) ? true : false;
                folAst.fuaHiHRefNo = txtAsReferralNo1.Text;
                //folAst.fuaCRDifficultyBreathingAndWheezing = (AsFitInLastMonth1.IsChecked == true) ? true : false;
                folAst.fuaOTCurrentlyOnMeds = (AsCurrentlyOnMedsYes.IsChecked == true) ? true : false;
                folAst.fuaOTStartDate = (DateTime)txtAsStartDate.SelectedDate;
                folAst.fuaOTIncreasedNoOfAsthmaAttacks = (AsIncreasedNoOfAsthmaAttacks1.IsChecked == true) ? true : false;
                folAst.fuaHiEHRevisit = ((ComboBoxItem)comboAstRevisit.SelectedItem).Content.ToString();
                folAst.fuaHiEHOutcomes = ((ComboBoxItem)comboAstOutcomes.SelectedItem).Content.ToString();
                //folAst.fuaOTReReferToClinic = (AsReReferToClinicYes.IsChecked == true) ? true : false;
                //folAst.fuaOTRefNo = txtAsReferralNo3.Text;
                //folAst.fuaMedication = (ComboBoxItem)comboAsMedication.SelectedItem.Content.ToString();

                // folAstMed.fuamName = ((ComboBoxItem)comboAsMedication.SelectedItem).Content.ToString();
                astFollow = true;

            }

            catch (Exception)
            {

                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "Asthma Tab", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

            if (astFollow)
            {

                try
                {
                    storedProcedure = "AddFollowUpAsthma";
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);
                    com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.AddWithValue("@fuaID", folAst.fuaID);
                    com.Parameters.AddWithValue("@EncounterID", folAst.EncounterID);
                    //com.Parameters.AddWithValue("@fuaDateOfVisit", folAst.fuaDateOfVisit);
                    com.Parameters.AddWithValue("@fuaHiEHWentToClinic", folAst.fuaHiEHWentToClinic);
                    com.Parameters.AddWithValue("@fuaAttacksLastMonth", folAst.fuaAttacksLastMonth);
                    com.Parameters.AddWithValue("@fuaCRReferToClinic", folAst.fuaCRReferToClinic);
                    com.Parameters.AddWithValue("@fuaCRRefNo", folAst.fuaCRRefNo);
                    com.Parameters.AddWithValue("@fuaHiEHReReferToClinic", folAst.fuaHiEHReReferToClinic);
                    com.Parameters.AddWithValue("@fuaHiHRefNo", folAst.fuaHiHRefNo);
                    // com.Parameters.AddWithValue("@fuaCRDifficultyBreathingAndWheezing", folAst.fuaCRDifficultyBreathingAndWheezing);
<<<<<<< HEAD

                    com.Parameters.AddWithValue("@fuaOTCurrentlyOnMeds", folAst.fuaOTCurrentlyOnMeds);
                    com.Parameters.AddWithValue("@fuaOTStartDate", folAst.fuaOTStartDate);
                    com.Parameters.AddWithValue("@fuaOTIncreasedNoOfAsthmaAttacks", folAst.fuaOTIncreasedNoOfAsthmaAttacks);
                    // com.Parameters.AddWithValue("@fuaOTReReferToClinic", folAst.fuaOTReReferToClinic);
                    // com.Parameters.AddWithValue("@fuaOTRefNo", folAst.fuaOTRefNo);
=======
                    
                    com.Parameters.AddWithValue("@fuaOTCurrentlyOnMeds", folAst.fuaOTCurrentlyOnMeds);
                    com.Parameters.AddWithValue("@fuaOTStartDate", folAst.fuaOTStartDate);
                    com.Parameters.AddWithValue("@fuaOTIncreasedNoOfAsthmaAttacks", folAst.fuaOTIncreasedNoOfAsthmaAttacks);
                   // com.Parameters.AddWithValue("@fuaOTReReferToClinic", folAst.fuaOTReReferToClinic);
                   // com.Parameters.AddWithValue("@fuaOTRefNo", folAst.fuaOTRefNo);
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
                    com.Parameters.AddWithValue("@fuaHiEHRevisit", folAst.fuaHiEHRevisit);
                    com.Parameters.AddWithValue("@fuaHiEHOutcomes", folAst.fuaHiEHOutcomes);

                    //com.Parameters.AddWithValue("@fuaMedication", folAst.fuaMedication);

                    //Asthma Medication

                    int lastestAstID = (int)((decimal)com.ExecuteScalar());

                    //adding Medication

                    if (listofAstMeds.Count > 0)
                    {
                        conn.Close();

                        foreach (var astmed in listofAstMeds)
                        {
                            try
                            {
                                storedProcedure = "AddFollowUpTBMedication";// name of sp
                                conn.Open();
                                SqlCommand tempcom = new SqlCommand(storedProcedure, conn);
                                tempcom.CommandType = CommandType.StoredProcedure;
                                tempcom.Parameters.AddWithValue("@fuamID", lastestAstID);//param
                                tempcom.Parameters.AddWithValue("@fuamName", folAstMed.fuamName);


                                tempcom.ExecuteNonQuery();//execute command
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString() + "Meds not added");
                            }

                            //com.ExecuteNonQuery();//execute command
                        }

                    }
                }


                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
            #endregion




            #region TB tested+

            bool tbFollow = false;
            Impilo_App.LocalModels.FollowUpTB folTb = new Impilo_App.LocalModels.FollowUpTB();
            Impilo_App.LocalModels.FollowUpTBMedication folTbMed = new Impilo_App.LocalModels.FollowUpTBMedication();



            try
            {
                //folTb.futID = 0;
                folTb.EncounterID = encounterID;
                //  folTb.futbDateOfVisit = (DateTime)txtTBDateOfVisit.SelectedDate;
                folTb.futbHiEHWentToClinic = (TBWentToClinic1.IsChecked == true) ? true : false;
                folTb.futbTBSResults = ((ComboBoxItem)comboTBResult.SelectedItem).Content.ToString();
                //   
                folTb.futbTBSRecentUnplannedLoseOfWeight = (TBRecentUnplannedLoseOfWeight1.IsChecked == true) ? true : false;
                folTb.futbTBSExcessiveSweatingAtNight = (TBExcessiveSweatingAtNight1.IsChecked == true) ? true : false;
                folTb.futbTBSFeverOver2Weeks = (TBFeverOver2Weeks1.IsChecked == true) ? true : false;
                folTb.futbTBSCoughMoreThan2Weeks = (TBCoughMoreThan2Week1.IsChecked == true) ? true : false;
                folTb.futbTBSLossOfApetite = (TBLossOfApetite1.IsChecked == true) ? true : false;
                folTb.futbTBSReferToClinic = (TBReferredToClinic21.IsChecked == true) ? true : false;
                folTb.futbTBSRefNo = txtTBReferralNo2.Text;
                folTb.futbHiEHReReferToClinic = (TBReReferredToClinicYes.IsChecked == true) ? true : false;
                folTb.futbHiEHRefNo = txtTBReReferralNo.Text;
                folTb.futbTBOTNewlyDiagnosedInLastMonth = (TBNewlyDiagnosed1.IsChecked == true) ? true : false;
                folTb.futbTBOTStartDate = (DateTime)txtTBStartDate.SelectedDate;
                folTb.futbTBOTReferTBContactsToClinic = (TBReferTBContactsToClinic1.IsChecked == true) ? true : false;
                folTb.futbTBOTPreviouslyOnMeds = (TBPreviouslyOnMeds1.IsChecked == true) ? true : false;
                folTb.futbTBOTFinishDate = (DateTime)txtTBFinishDate.SelectedDate;
                folTb.futbTBOTConcerns = (TBConcerns1.IsChecked == true) ? true : false;
<<<<<<< HEAD
                folTb.futbHiEHOutcomes = ((ComboBoxItem)comboTbVisitOutCome.SelectedItem).Content.ToString();
                //  folTb.futbTBOTReferToClinic = (TBReferToClinic31.IsChecked == true) ? true : false;
                // folTb.futbTBOTRefNo = txtTBReferralNo3.Text;
=======
                folTb.futbHiEHOutcomes =((ComboBoxItem)comboTbVisitOutCome.SelectedItem).Content.ToString();
             //  folTb.futbTBOTReferToClinic = (TBReferToClinic31.IsChecked == true) ? true : false;
               // folTb.futbTBOTRefNo = txtTBReferralNo3.Text;
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086

                //folTbMed.futbmName = ((ComboBoxItem)comboTBMedication.SelectedItem).Content.ToString();

                tbFollow = true;
            }
            catch (Exception)
            {

                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "TB Tab", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            if (tbFollow)
            {

                try
                {
                    storedProcedure = "AddFollowUpTB";
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);
                    com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.AddWithValue("@futbID", folTb.futID);
                    com.Parameters.AddWithValue("@EncounterID", folTb.EncounterID);
                    com.Parameters.AddWithValue("@futbDateOfVisit", folTb.futbDateOfVisit);
                    com.Parameters.AddWithValue("@futbHiEHWentToClinic", folTb.futbHiEHWentToClinic);
                    com.Parameters.AddWithValue("@futbHiEHReReferToClinic", folTb.futbHiEHReReferToClinic);
                    com.Parameters.AddWithValue("@futbHiEHRefNo", folTb.futbHiEHRefNo);
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
                    //com.Parameters.AddWithValue("@futbTBOTReferToClinic", folTb.futbTBOTReferToClinic);
                    //com.Parameters.AddWithValue("@futbTBOTRefNo", folTb.futbTBOTRefNo);
                    com.Parameters.AddWithValue("@futbHiEHOutcomes", folTb.futbHiEHOutcomes);


                    int lastestTbID = (int)((decimal)com.ExecuteScalar());

                    //adding Medication

                    if (listofTbMeds.Count > 0)
                    {
                        conn.Close();

                        foreach (var tbmed in listofTbMeds)
                        {
                            try
                            {
                                storedProcedure = "AddFollowUpTBMedication";// name of sp
                                conn.Open();
                                SqlCommand tempcom = new SqlCommand(storedProcedure, conn);
                                tempcom.CommandType = CommandType.StoredProcedure;
                                tempcom.Parameters.AddWithValue("@futbID", lastestTbID);//param
                                tempcom.Parameters.AddWithValue("@futbmName", folTbMed.futbmName);


                                tempcom.ExecuteNonQuery();//execute command
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString() + "Medications not added");
                            }

                            //com.ExecuteNonQuery();//execute command
                        }

                    }
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
            #endregion
          


            /*----------------------------------------------------------------------------------------------*/

         

            /*Handled by Loic*/

            #region Epilepsy tested ++

            bool epiFollow = false;

            Impilo_App.LocalModels.FollowUpEpilepsy folEpi = new Impilo_App.LocalModels.FollowUpEpilepsy();
            Impilo_App.LocalModels.FollowUpEpilepsyMedication folEpiMed = new Impilo_App.LocalModels.FollowUpEpilepsyMedication();

            try
            {
                //folEpi.fueID = 0;
                folEpi.EncounterID = encounterID;
                folEpi.fueHiEHWentToClinic = (EpiWentToClinic1.IsChecked == true) ? true : false;
                folEpi.fueHiEHReReferToClinic = (EpiReferToClinicYes.IsChecked == true) ? true : false;
                folEpi.fueHiEHRefNo = txtEpiRefNo.Text;
                folEpi.fueCRFitInLastMonth = (EpiFitInLastMonth1.IsChecked == true) ? true : false;
                folEpi.fueCRReferToClinic = (EpiReReferToClinicYes.IsChecked == true) ? true : false;
                folEpi.fueCRRefNo = txtEpiReRefNo.Text;
                folEpi.fueOnTreatmentCurrentlyOnMeds = (EpiCurrentlyOnMeds1.IsChecked == true) ? true : false;
                folEpi.fueOnTreatmentStartDate = (DateTime)txtEpiStartDate.SelectedDate;
                folEpi.fueOnTreatmentMoreThan3FitsSinceLastMonth = (EpiMoreThan3FitsInLastMonth1.IsChecked == true) ? true : false;
                folEpi.fueOnTreatmentReReferToClinic = (EpiCurrentlyOnMeds1.IsChecked == true) ? true : false;
                folEpi.fueOnTreatmentRefNo = txtEpiReRefNo.Text;

                // folEpiMed.fuemName = ((ComboBoxItem)comboEpiMedication.SelectedItem).Content.ToString();

                epiFollow = true;
            }
            catch (Exception)
            {

                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "Epilepsy Tab", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            if (epiFollow)
            {


                try
                {
                    storedProcedure = "AddFollowUpEpilepsy";
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);
                    com.CommandType = CommandType.StoredProcedure;
                    //com.Parameters.AddWithValue("@fueID", folEpi.fueID);
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

                    int lastestEpiID = (int)((decimal)com.ExecuteScalar());

                    //adding Medication

                    /* if (listofEpiMeds.Count > 0)
                         {
                             conn.Close();

                             foreach (var epimed in listofEpiMeds)
                             {
                                 try
                                 {
                                     storedProcedure = "AddFollowUpHIVMedication";// name of sp
                                     conn.Open();
                                     SqlCommand tempcom = new SqlCommand(storedProcedure, conn);
                                     tempcom.CommandType = CommandType.StoredProcedure;
                                     tempcom.Parameters.AddWithValue("@fuemID", lastestEpiID);//param
                                     tempcom.Parameters.AddWithValue("@fuemName", folEpiMed.fuemName);


                                     tempcom.ExecuteNonQuery();//execute command
                                 }

                                 catch (Exception ex)
                                 {
                                     MessageBox.Show(ex.Message.ToString() + "Meds not added");
                                 }

                                 //com.ExecuteNonQuery();//execute command
                             }

                         }*/
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }



            #endregion
         
           

            #region Maternal health tested++
            Impilo_App.LocalModels.FollowUpMaternalHealth folMat = new Impilo_App.LocalModels.FollowUpMaternalHealth();

            //folMat.fumhID = 0;
            folMat.EncounterID = 0;
<<<<<<< HEAD
            folMat.fumhDateOfVisit = DateTime.Now;
=======
            //folMat.fumhDateOfVisit = (DateTime)txtMatDateOfVisit.SelectedDate;
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
            folMat.fumhHiEHWentToClinic = (MatWentToClinic11.IsChecked == true) ? true : false;
            folMat.fumhHiEHReReferToClinic = (MatReReferToClinic11.IsChecked == true) ? true : false;

            folMat.fumhHiEHRefNo = txtMatReferral.Text;
            folMat.fumhCPDateOfFirstANC = (DateTime)txtMatDateOf1stANC.SelectedDate;
            folMat.fumhCPDateOfLastANC = (DateTime)txtMatDateOfLastANC.SelectedDate;
            folMat.fumhCPReferToClinic = (MatReferredToClinic31.IsChecked == true) ? true : false;
            folMat.fumhCPRefNo = txtMatReferral.Text;
            folMat.fumhCPRegisteredForMomConnect = (MatRegisteredForMoMConnect1.IsChecked == true) ? true : false;
            folMat.fumhCPReferToClinic2 = (MatReferredToClinic31.IsChecked == true) ? true : false;
            folMat.fumhCPRefNo2 = txtMatReferral.Text;

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

                com.Parameters.AddWithValue("@EncounterID", encounterID);
                //com.Parameters.AddWithValue("@fumhDateOfVisit", folMat.fumhDateOfVisit);
                com.Parameters.AddWithValue("@fumhHiEHWentToClinic", folMat.fumhHiEHWentToClinic);
                com.Parameters.AddWithValue("@fumhHiEHReReferToClinic", folMat.fumhHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fumhHiEHRefNo", folMat.fumhHiEHRefNo);
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
                com.Parameters.AddWithValue("@fumhPPRefNo", folMat.fumhPPRefNo);

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

            #region Child Health tested++

            FollowUpChildHealth folCh = new FollowUpChildHealth();
            FollowUpChildHealthConcerns folChCon = new FollowUpChildHealthConcerns();
            FollowUpChildHealthImmunisationsOutstanding folChImm = new FollowUpChildHealthImmunisationsOutstanding();


            //folCh.fuchID = 0;
            folCh.EncounterID = 0;
            folCh.fuchDateOfVisit = (DateTime)txtChildDateOfVisit.SelectedDate;
            folCh.fuchHiEHWentToClinic = (ChiWentToClinic1.IsChecked == true) ? true : false;
            folCh.fuchHiEHReReferToClinic = (ChildReferToClinic11.IsChecked == true) ? true : false;
            folCh.fuchHiEHRefNo = txtChildReferralNo1.Text;

            folCh.fuchNewChildWithRTHC = (ChildChildWithRTHC1.IsChecked == true) ? true : false;
            folCh.fuchNewReferToClinic = (ChildReferToClinic41.IsChecked == true) ? true : false;
            folCh.fuchNewRefNo = txtChildReferralNo3.Text;
            folCh.fuchNewMotherHIVPos = (ChildMotherTHVPositive1.IsChecked == true) ? true : false;
            folCh.fuchNewChildBreastfed = (ChildChildBreastfed1.IsChecked == true) ? true : false;
            folCh.fuchNewHowLong = cboChBreastFedlong.SelectedItem.ToString();
            folCh.fuchNewChildEverOnNevirapine = (ChildClildEverOnNevirapine1.IsChecked == true) ? true : false;
            folCh.fuchNewReferToClinic2 = (ChildReferToClinic51.IsChecked == true) ? true : false;
            folCh.fuchNewRefNo2 = txtChildReferralNo6.Text;
            folCh.fuchNewHasPCRBeenDone = (ChildHowPCRHasDone1.IsChecked == true) ? true : false;
            folCh.fuchNewReferToClinic3 = (ChildReferToClinic61.IsChecked == true) ? true : false;
            folCh.fuchNewRefNo3 = txtChildReferralNo3.Text;
            folCh.fuchNewImmunisationUpToDate = (ChildImmunisationUpToDate1.IsChecked == true) ? true : false;
            folCh.fuchNewVitAWormMedsGivenEachMonth = (ChildVITAandWormMedsGivenEachMonth1.IsChecked == true) ? true : false;
            folCh.fuchNewReferToClinic4 = (ChildReferToClinic71.IsChecked == true) ? true : false;
            folCh.fuchNewRefNo4 = txtChildReferralNo3.Text;

            folCh.fuchCDevWalkAppropriatelyForAge = (ChildWalkAppropriateForAge1.IsChecked == true) ? true : false;
            folCh.fuchCDevTalkAppropriateForAge = (ChildTalkAppropriateForAge1.IsChecked == true) ? true : false;
            folCh.fuchCDevReferToClinic = (ChildReferToClinic21.IsChecked == true) ? true : false;
            folCh.fuchCDevRefNo = txtChildReferralNo3.Text;

            folCh.fuchSocDevChildAssisted = (ChildChildAssisted1.IsChecked == true) ? true : false;
            folCh.fuchSocDevReReferToSD = (ChildReReferToSD1.IsChecked == true) ? true : false;
            folCh.fuchSocDevRefNo = txtChildReferralNo3.Text;
            folCh.fuchCurSocDevReferToClinic = (ChildReferToClinic31.IsChecked == true) ? true : false;
            folCh.fuchCurSocDevReferToSD = (ChildreferToSD1.IsChecked == true) ? true : false;
            folCh.fuchCurSocDevRefNo = txtChildReferralNo4.Text;

            //Immunisation
            if (!folCh.fuchNewImmunisationUpToDate)
            {

                string listVacinne = datasource.SelectedImmunisationOutText;
                folCh.WhichImmunisatationsOutStanding = listVacinne.Split(',').ToList();
            }

            // Concerns

            folChCon.fuchcID = 0;
            folChCon.fuchID = 0;
            //folChCon.fuchcName = ; 

            try
            {
                storedProcedure = "AddFollowUpChildHealth";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@EncounterID", encounterID);
                com.Parameters.AddWithValue("@fuchDateOfVisit", DateTime.Now);
                com.Parameters.AddWithValue("@fuchHiEHWentToClinic", folCh.fuchHiEHWentToClinic);
                com.Parameters.AddWithValue("@fuchHiEHReReferToClinic", folCh.fuchHiEHReReferToClinic);
                com.Parameters.AddWithValue("@fuchHiEHRefNo", folCh.fuchHiEHRefNo);
                com.Parameters.AddWithValue("@fuchNewChildWithRTHC", folCh.fuchNewChildWithRTHC);
                com.Parameters.AddWithValue("@fuchNewReferToClinic", folCh.fuchNewReferToClinic);
                com.Parameters.AddWithValue("@fuchNewRefNo", folCh.fuchNewRefNo);
                com.Parameters.AddWithValue("@fuchNewMotherHIVPos", folCh.fuchNewMotherHIVPos);
                com.Parameters.AddWithValue("@fuchNewChildBreastfed", folCh.fuchNewChildBreastfed);
                com.Parameters.AddWithValue("@fuchNewHowLong", folCh.fuchNewHowLong);
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
                com.Parameters.AddWithValue("@fuchCDevTalkAppropriateForAge", folCh.fuchCDevTalkAppropriateForAge);
                com.Parameters.AddWithValue("@fuchCDevReferToClinic", folCh.fuchCDevReferToClinic);
                com.Parameters.AddWithValue("@fuchCDevRefNo", folCh.fuchCDevRefNo);
                com.Parameters.AddWithValue("@fuchSocDevChildAssisted", folCh.fuchSocDevChildAssisted);
                com.Parameters.AddWithValue("@fuchSocDevReReferToSD", folCh.fuchSocDevReReferToSD);
                com.Parameters.AddWithValue("@fuchSocDevRefNo", folCh.fuchSocDevRefNo);
                com.Parameters.AddWithValue("@fuchCurSocDevReferToClinic", folCh.fuchCurSocDevReferToClinic);
                com.Parameters.AddWithValue("@fuchCurSocDevReferToSD", folCh.fuchCurSocDevReferToSD);
                com.Parameters.AddWithValue("@fuchCurSocDevRefNo", folCh.fuchCurSocDevRefNo);

                //Concerns



                //immunistations



                // com.ExecuteNonQuery();//execute command


                int lastestChID = (int)((decimal)com.ExecuteScalar());

                //adding outstanding immunisation added (27 01) --
                //outImmList
                if (folCh.WhichImmunisatationsOutStanding != null && folCh.WhichImmunisatationsOutStanding.Count >= 1)
                {
                    conn.Close();
                    foreach (var imm in folCh.WhichImmunisatationsOutStanding)
                    {
                        try
                        {
                            storedProcedure = "AddFollowUpChildHealthImmunisationsOutstanding";// name of sp
                            conn.Open();
                            SqlCommand tempcom = new SqlCommand(storedProcedure, conn);
                            tempcom.CommandType = CommandType.StoredProcedure;
                            tempcom.Parameters.AddWithValue("@fuchID", lastestChID);//param
                            tempcom.Parameters.AddWithValue("@fuchioName", imm);//param

                            tempcom.ExecuteNonQuery();//execute command
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString() + "Immunisation not added");
                        }

                        finally
                        {
                            conn.Close();
                        }
                    }

                }


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


            #region Other tested+

            Impilo_App.LocalModels.FollowUpOther folOth = new Impilo_App.LocalModels.FollowUpOther();


            folOth.fuoID = 0;
            folOth.EncounterID = 0;
<<<<<<< HEAD

=======
            //folOth.fuoDateOfVisit = (DateTime)txtOtherDateOfVisit.SelectedDate;
>>>>>>> a557e6bbbffc20c6fe2fa09707d26b73644f2086
            folOth.fuoHiEHWentToClinic = (OtherWentToClinic1.IsChecked == true) ? true : false;
            folOth.fuoHiEHReReferToClinic = (OtherReReferToClinic1.IsChecked == true) ? true : false;
            folOth.fuoHiEHRefNo = txtOtherReferralNo1.Text;
            folOth.fuoOCCondition = txtOtherConditionTha.Text;
            folOth.fuoOCReferToClinic = (OtherReferToClinic11.IsChecked == true) ? true : false;
            folOth.fuoOCRefNo = txtOtherReferralNo2.Text;


            try
            {
                storedProcedure = "AddFollowUpOther";
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@EncounterID", encounterID);
                com.Parameters.AddWithValue("@fuoDateOfVisit", DateTime.Now);
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
       
            
            #endregion






        }

            

             
        #region save medications

        List<FollowUpHypertensionMedication> listofHypMeds = new List<FollowUpHypertensionMedication>();

        private void btnSaveHypMed_Click(object sender, RoutedEventArgs e)
        {
            FollowUpHypertensionMedication folHypMed = new FollowUpHypertensionMedication();

           // folHypMed.fuhmName = ((ComboBoxItem)comboHyperMedication.SelectedItem).Content.ToString();
         //   listofHypMeds.Add(hypmed);

        }

        List<string> listofAstMeds = new List<string>();

        private void btnSaveAstMed_Click(object sender, RoutedEventArgs e)
        {
            //FollowUpAsthmaMedication astmed = new FollowUpAsthmaMedication();
            //string hypmedstore = ((ComboBoxItem)comboHyperMedication.SelectedItem).Content.ToString();
           // listofAstMeds.Add(astmed); 
        }

       

        List<string> listofDiaMeds = new List<string>();
        private void btnSaveDiaMed_Click(object sender, RoutedEventArgs e)
        {

        }

        List<string> listofTbMeds = new List<string>();
        private void btnSaveTbMed_Click(object sender, RoutedEventArgs e)
        {

        }

        List<string> listofEpiMeds = new List<string>();
        private void btnSaveEpiMed_Click(object sender, RoutedEventArgs e)
        {

        }

        List<string> listofHIVMeds = new List<string>();

        private void AsCurrentlyOnMedsYes_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;

            if (rad.Name == "AsCurrentlyOnMedsYes") //radioAttYes  radioAttNo
            {

                if (rad.IsChecked == true)
                {
                    grpOnTreatment.IsEnabled = true;
                   
                }
            }
            else {
                grpOnTreatment.IsEnabled = false;
            }

        }

        private void MatIsItPosibleYouArePregnent1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;

            if (rad.Name == "MatIsItPosibleYouArePregnent1") //radioAttYes  radioAttNo
            {

                if (rad.IsChecked == true)
                {
                    grpCurPreg.IsEnabled = true;

                }
            }
            else
            {
                grpCurPreg.IsEnabled = false;
            }
        }

        private void EpiCurrentlyOnMeds1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;

            if (rad.Name == "EpiCurrentlyOnMeds1") //radioAttYes  radioAttNo
            {

                if (rad.IsChecked == true)
                {
                    grpEplOntreatMent.IsEnabled = true;

                }
            }
            else
            {
                grpEplOntreatMent.IsEnabled = false;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentClient == null)
            {
                MessageBox.Show("Oups! No Client was created in the new Client section prior to starting the screening");
                return;
            }

            Button edit = sender as Button;

            string content = edit.Content.ToString();

            if (content.Equals("Edit Bio", StringComparison.InvariantCultureIgnoreCase))
            {
                grpBio.IsEnabled = true;
                // ClinicUsed.SelectedIndex = (int.Parse( currentClient.ClientID))-1;
                DataTable dt = ds.Tables[0];
                DataRow[] dr = dt.Select(" ClinicID = '" + currentClient.ClinicUsed + "'");
                int index = dt.Rows.IndexOf(dr[0]);
                ClinicUsed.SelectedIndex = index;


                NameofSchool.SelectedIndex = NameofSchool.Items.IndexOf(currentClient.NameofSchool);
                Grade.SelectedIndex = Grade.Items.IndexOf(currentClient.Grade);

                edit.Content = "Stop Editing";
            }
            else
            {

                try
                {


                    if (!txtIDNumber.IsMaskFull)
                    {
                        MessageBox.Show("Please check the ID Number", "Current Client Tab", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!txtLatitude.IsMaskFull)
                    {
                        MessageBox.Show("Please check the latitude", "Current Client Tab", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }


                    if (!txtLongitude.IsMaskFull)
                    {
                        MessageBox.Show("Please check the longitude", "Current Client Tab", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }


                    currentClient.FirstName = txtName.Text;
                    currentClient.LastName = txtLastName.Text;
                    currentClient.HeadOfHousehold = (rdohYes.IsChecked == true) ? "yes" : "no";
                    currentClient.GPSLatitude = txtLatitude.Text;
                    currentClient.GPSLongitude = txtLongitude.Text;
                    currentClient.IDNo = txtIDNumber.Text;
                    currentClient.ClinicUsed = int.Parse(ClinicUsed.SelectedValue.ToString());
                    currentClient.DateOfBirth = DateTime.Parse(txtDateofBirth.Text);
                    currentClient.NameofSchool = NameofSchool.SelectedItem.ToString();//((ComboBoxItem)NameofSchool.SelectedItem).Content.ToString();NameofSchool.SelectedValue.ToString()
                    currentClient.Gender = (rdoMale.IsChecked == true) ? "Male" : "Female";
                    currentClient.AttendingSchool = (radioAttYes.IsChecked == true) ? true : false;

                    currentClient.Grade = Grade.SelectedItem.ToString();

                    if (MessageBox.Show("You are about to update the current client information; continue?", "Biography", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
                    {
                        updateClient(currentClient);
                    }

                    //  MessageBox.Show(newClient.ClinicUsed.ToString());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Some fields are missing data or were filled with incorrect data", "New Client", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                grpBio.IsEnabled = false;
                edit.Content = "Edit Bio";
            }

        }


        void updateClient(Impilo_App.LocalModels.Client updatedClient)
        {
            try
            {
              string  storedProcedure = "AddClient";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ClientID", updatedClient.ClientID);//param
                com.Parameters.AddWithValue("@HeadOfHousehold", updatedClient.HeadOfHousehold);//param
                com.Parameters.AddWithValue("@FirstName", updatedClient.FirstName);//param
                com.Parameters.AddWithValue("@LastName", updatedClient.LastName);//param

                com.Parameters.AddWithValue("@GPSLatitude", updatedClient.GPSLatitude);//param
                com.Parameters.AddWithValue("@GPSLongitude", updatedClient.GPSLongitude);//param
                com.Parameters.AddWithValue("@IDNo", updatedClient.IDNo);//param
                com.Parameters.AddWithValue("@ClinicID", updatedClient.ClinicUsed);//param dummy value added to be changed
                com.Parameters.AddWithValue("@DateOfBirth", updatedClient.DateOfBirth);//param
                com.Parameters.AddWithValue("@Gender", updatedClient.Gender);//param
                com.Parameters.AddWithValue("@AttendingSchool", updatedClient.AttendingSchool);//param
                com.Parameters.AddWithValue("@Grade", updatedClient.Grade);//param
                com.Parameters.AddWithValue("@NameofSchool", updatedClient.NameofSchool);//param
                com.Parameters.AddWithValue("@Area", " ");//param

                int i = 0;
                i = com.ExecuteNonQuery();//execute command
                if (i != 0)
                {
                    MessageBox.Show("New Client Updated Successfully", "Follow Up", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void radioAttNo_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void HyperWentToClinic1Yes_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
#endregion





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
