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

namespace Impilo_App.Views.Screening
{
    /// <summary>
    /// Interaction logic for ScreeningHome.xaml
    /// </summary>
    public partial class ScreeningHome : UserControl
    {

        static string sconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=impilo;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(sconn);
        int HutCounter = 1;
        int encounterID = 0;
        Impilo_App.LocalModels.Client currentClient;
        public ScreeningHome()
        {
            InitializeComponent();
        }
        //DAL dataAccess;
        public ScreeningHome(string ID, Impilo_App.LocalModels.Client cl)
        {
            InitializeComponent();
          
            //Filling the currently screening tab
            currentClient = cl;

            lblIdentity.Content = cl.ClientID;

            txtName.Text = cl.FirstName;
            txtLastName.Text = cl.LastName;
            txtIDNumber.Text = cl.IDNo;
            txtDateOfScreening.Text = DateTime.Now.ToString("dd MMMM yyyy h:mm");

            if (cl.Gender == "Male"|| cl.Gender == "male")
            {
                rdoMale.IsChecked = true;
            }
            else
            {
                rdoFemale.IsChecked = true;
            }
        }
        public ScreeningHome(string ID)
        {
           
            InitializeComponent();

           
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            //screen.ScreeningID = "";
            string name = "", surname = ""; 
            string scrID = Utilities.GenerateScreeningID(name, surname); // it is first use as the client id 
            string storedProcedure = "";

            //saving the sreening first encouter EncounterID
            #region SaveScreening +

            Impilo_App.LocalModels.Screening newSCreen = new Impilo_App.LocalModels.Screening
            {
                ScreeningID = "",
                ScreeningDate = DateTime.Now,
                ClientId = scrID
            };


            //sp place
            //connection
            try
            {
                storedProcedure = "AddEncounters";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
               
                com.Parameters.AddWithValue("@ClientID", newSCreen.ClientId);//param
                com.Parameters.AddWithValue("@EncouterDate", newSCreen.ScreeningDate);//param
                
                com.Parameters.AddWithValue("@EncouterType", 1);//param

                encounterID = int.Parse((string)com.ExecuteScalar());
                //com.ExecuteNonQuery();//execute command
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            scrID = encounterID.ToString();
            #endregion



           
            #region Hypertension ++

            Hypertension hyper = new Hypertension();
            hyper.ScreeningID = scrID;
            hyper.YearOfDiagnosis = ((ComboBoxItem)cboDiaYear.SelectedItem).Content.ToString();
            hyper.Headache =  (headYes.IsChecked == true) ? true : false;
            hyper.BlurredVision = (blurvYes.IsChecked == true) ? true : false;
            hyper.ShortnessOfBreath = (shortYes.IsChecked == true) ? true : false; 
            hyper.ChestPain = (chestYes.IsChecked == true) ? true : false;
            hyper.Confusion = (confYes.IsChecked == true) ? true : false;
            hyper.ReferralToClinic = (refYes.IsChecked == true) ? true : false; ;
            hyper.ReferalNo = txthyperRef.Text;
            hyper.EverHadStroke = (stroYes.IsChecked == true) ? true : false; ;
            hyper.YearOfStroke = ((ComboBoxItem)cboStrokeYear.SelectedItem).Content.ToString();
            hyper.AnyOneInFamilyHadStroke = (anyYes.IsChecked == true) ? true : false; ;
            hyper.HowManyInFamilyOnMedsForHypertension = int.Parse(numHPT.Text);
            //sp place
             //connection 
            try 
	        {
                storedProcedure = "AddScreeningHypertension";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EncounterID", hyper.ScreeningID);//param
                com.Parameters.AddWithValue("@shYearOfDiagnosis", hyper.YearOfDiagnosis);//param
                com.Parameters.AddWithValue("@shHeadache", hyper.Headache);//param
                com.Parameters.AddWithValue("@shBlurredVision", hyper.BlurredVision);//param
                com.Parameters.AddWithValue("@shShortnessOfBreath", hyper.ShortnessOfBreath);//param
                com.Parameters.AddWithValue("@shConfusion", hyper.Confusion);//param
                com.Parameters.AddWithValue("@shChestPain", hyper.ChestPain);//param
                com.Parameters.AddWithValue("@shReferralToClinic", hyper.ReferralToClinic);//param
                com.Parameters.AddWithValue("@shRefNo", hyper.ReferalNo);//param
                com.Parameters.AddWithValue("@shEverHadStroke", hyper.EverHadStroke);//param
                com.Parameters.AddWithValue("@shYearOfStroke", hyper.YearOfStroke);//param
                com.Parameters.AddWithValue("@shHowManyInFamilyOnMedsForHypertension", hyper.HowManyInFamilyOnMedsForHypertension);//param
                com.Parameters.AddWithValue("@shAnyoneInFamilyHadStroke", hyper.AnyOneInFamilyHadStroke);//param

                int i = com.ExecuteNonQuery();//execute command
	        }
	        catch (Exception ex)
	        {
		
	        	 MessageBox.Show(ex.Message.ToString());
	        }
            finally{
                 conn.Close();
            }

           

            #endregion


            #region Diabetes +-

            Diabetes dia = new Diabetes{ScreeningID = scrID, 
                BlurredVision= (blurvYes.IsChecked == true) ? true : false,
                YearOfDiagnosis=((ComboBoxItem)cboDiabeYear.SelectedItem).Content.ToString(),
                                     regularlyThirsty = (regYes.IsChecked == true) ? true : false,
                                        WeightLoss = (lossYes.IsChecked == true) ? true : false,
                                        UrinatingMore = (uriYes.IsChecked == true) ? true : false,
                                        NauseaOrVomitting = (nauYes.IsChecked == true) ? true : false,
                FootExamResult =((ComboBoxItem)cboFootExam.SelectedItem).Content.ToString(),
                                        ReferralToClinic = (refdiaYes.IsChecked == true) ? true : false,
                                        ReferralNo = txtdiaRef.Text,
                                        FamilyMemberWith = (famYes.IsChecked == true) ? true : false
        
        };
              
            //sp place
            //connection
            try
            {
                storedProcedure = "AddScreeningDiabetes";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.AddWithValue("@EncounterID", dia.ScreeningID);//param
                com.Parameters.AddWithValue("@sdYearOfDiagnosis", dia.YearOfDiagnosis);//param
                com.Parameters.AddWithValue("@sdRegularlyThirsty", dia.regularlyThirsty);//param
                com.Parameters.AddWithValue("@sdWeightLost", dia.WeightLoss);//param
                com.Parameters.AddWithValue("@sdUrinatingMore", dia.UrinatingMore);//param
                com.Parameters.AddWithValue("@sdNauseaOrVomiting", dia.NauseaOrVomitting);//param
                //com.Parameters.AddWithValue("@sdFootExamResult", dia.FootExamResult);//param-
                //com.Parameters.AddWithValue("@sdBlurredVision", dia.BlurredVision);//param-
                com.Parameters.AddWithValue("@sdReferralToClinic", dia.ReferralToClinic);//param
                com.Parameters.AddWithValue("@sdRefNo", dia.ReferralNo);//param
                com.Parameters.AddWithValue("@sdFamilyMemberWith", dia.FamilyMemberWith);//param

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

            #region HIV ++
            HIV_Tab hivTab = new HIV_Tab
            {
                ScreeningID = scrID,
                YearOfDiagnosis = ((ComboBoxItem)cboDiabeYear.SelectedItem).Content.ToString(),
                OnMeds = (radonMeds.IsChecked == true) ? true : false,
                AdherenceOK = (radadh.IsChecked == true) ? true : false,
                ReferToClinic=(hivref.IsChecked == true) ? true : false,
                ReferralNo = txtHIVRef.Text,
                ARVFileNo=txtARVFile.Text,

            };

            //missing the list of meds

            //sp place
            //connection
            try
            {
                storedProcedure = "AddScreeningHIV";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EncounterID", hivTab.ScreeningID);//param
                com.Parameters.AddWithValue("@shivYearOfDiagnosis", hivTab.YearOfDiagnosis);//param
                com.Parameters.AddWithValue("@shivOnMeds", hivTab.OnMeds);//param
                com.Parameters.AddWithValue("@shivAdherenceOK", hivTab.AdherenceOK);//param
                com.Parameters.AddWithValue("@shivReferToClinic", hivTab.ReferToClinic);//param
                com.Parameters.AddWithValue("@shivRefNo", hivTab.ReferralNo);//param
                com.Parameters.AddWithValue("@shivARVFileNo", hivTab.ARVFileNo);//param
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

            #region Maternal Health ++

            MentalHealth materHealth = new MentalHealth {
                ScreeningID = scrID,
                PregnantBefore =false,
                NoOfPregnancies =numPregnacies.Text,
                HowManySuccessful =numPregnSuc.Text,
                WhereDeliveredLasBaby =txtdelplace.Text,
                Caesarian = (radcae.IsChecked == true) ? "yes" : "no",
                BabyUnder2_5Kgs = txtbaby2.Text,
                ChildrenDiedUnder1Year =numchild1died.Text,
                ChildrenDiedBetween1to5Years = numchild5died.Text,
                PAPSmearInLast5Years = (radpap.IsChecked == true) ? true : false,
                LastBloodTestResult = ((ComboBoxItem)cboBloodExam.SelectedItem).Content.ToString(),
                DateOfFirstANC =(DateTime) txtDate1ANC.SelectedDate,
                DateOfLastANC = (DateTime)txtDatelastANC.SelectedDate,
                ReferredToClinic = (refMatyes.IsChecked == true) ? true : false,
                ReferralNo = txtMatref.Text,
                DateOfNextANC = (DateTime)txtDateNextANC.SelectedDate,
                ExpectedDateOfDelivery = (DateTime)txtDateDelivery.SelectedDate,
                IntendFormulaFeed = (radintformula.IsChecked == true) ? true : false,
                IntendBreastFeed = (radintbreas.IsChecked == true) ? true : false,
                RegisteredOnMomConnect=(radintbreas.IsChecked == true) ? true : false

        
            };

            //sp place
            //connection
            try
            {
                storedProcedure = "AddScreeningMaternalHealth";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EncounterID", materHealth.ScreeningID);//param
                com.Parameters.AddWithValue("@smhPregnantBefore", materHealth.PregnantBefore);//param
                com.Parameters.AddWithValue("@smhNoOfPregnancies", materHealth.NoOfPregnancies);//param
                com.Parameters.AddWithValue("@smhHowManySuccessful", materHealth.HowManySuccessful);//param
                com.Parameters.AddWithValue("@smhWhereDeliveredLastBaby", materHealth.WhereDeliveredLasBaby);//param
                com.Parameters.AddWithValue("@smhCaesarian", materHealth.Caesarian);//param
                com.Parameters.AddWithValue("@smhBabyUnder2KG", materHealth.BabyUnder2_5Kgs);//param
                com.Parameters.AddWithValue("@smhChildrenDiedUnder1Year", materHealth.ChildrenDiedUnder1Year);//param
                com.Parameters.AddWithValue("@smhChildrenDiedBetween1To5Years", materHealth.ChildrenDiedBetween1to5Years);//param
                com.Parameters.AddWithValue("@smhPAPSmearInLast5Years", materHealth.PAPSmearInLast5Years);//param
                com.Parameters.AddWithValue("@smhLastBloodTestResult", materHealth.LastBloodTestResult);//param
                com.Parameters.AddWithValue("@smhCurrentDateOfFirstANC", materHealth.DateOfFirstANC);//param
                com.Parameters.AddWithValue("@smhCurrentDateOfLastANC", materHealth.DateOfLastANC);//param
                com.Parameters.AddWithValue("@smhReferredToClinic", materHealth.ReferredToClinic);//param
                com.Parameters.AddWithValue("@smhRefNo", materHealth.ReferralNo);//param
                com.Parameters.AddWithValue("@smhDateOfNextANC", materHealth.DateOfNextANC);//param
                com.Parameters.AddWithValue("@smhExpectedDeliveryDate", materHealth.ExpectedDateOfDelivery);//param
                com.Parameters.AddWithValue("@smhIntendFormulaFeed", materHealth.IntendFormulaFeed);//param
                com.Parameters.AddWithValue("@smhIntendBreastfeed", materHealth.IntendBreastFeed);//param this could superfluu
                com.Parameters.AddWithValue("@smhRegisteredOnMomConnect", materHealth.RegisteredOnMomConnect);//param
               

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

            #region Child Health +-+

            ChildHealth childh = new ChildHealth {
                ScreeningID = scrID,
                NameOfMother =txtNaMother.Text,
                ChildWithRTHC = (radrthc.IsChecked == true) ? true : false,
                ReferToClinic = (radref.IsChecked == true) ? true : false,
                ReferalNo = txtref.Text,
                ListConcernsReChild = txtConRechild.Text,
                ReferToClinic2=(radref1.IsChecked == true) ? true : false,
                ReferToOVC = (radrefovc.IsChecked == true) ? "yes" : "No",
                ReferralNo2 = txtref1.Text,
                MotherHIVPlus = (radMohiv.IsChecked == true) ? true : false,
                ChildBreastFed=(radbreast.IsChecked == true) ? true : false,
                Howlong = ((ComboBoxItem)cbolong.SelectedItem).Content.ToString(),
                ChildEverOnNevirapine=false,
                PCRDone=(radpcr.IsChecked == true) ? true : false,
                PCRResults = ((ComboBoxItem)cboPCR.SelectedItem).Content.ToString(),
                ReferToClinic3=(radref2.IsChecked == true) ? true : false,
                ReferalNo3 = txtref2.Text,
                ImmunisationUpToDate=(radimm.IsChecked == true) ? true : false,
               
                ReferToClinic4=(radref3.IsChecked == true) ? true : false,
                ReferralNo4 = txtref3.Text,
                WalkAppropriateForAge = (radwalk.IsChecked == true) ? true : false,
                TalkAppropriateForAge = (radtalk.IsChecked == true) ? true : false,
                VITAandWarmMedsGivenEachMonth=false

            };

            childh.WhichImmunisatationsOutStanding = "";

            //sp place
            //connection
            try
            {
                storedProcedure = "AddScreeningChildHealth";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EncounterID", childh.ScreeningID);//param
                com.Parameters.AddWithValue("@schNameOfMother", childh.NameOfMother);//param
                com.Parameters.AddWithValue("@schChildWithRTHC", childh.ChildWithRTHC);//param
                com.Parameters.AddWithValue("@schReferToClinic", childh.ReferToClinic);//param
                com.Parameters.AddWithValue("@schRefNo", childh.ReferalNo);//param
              //  com.Parameters.AddWithValue("@ListConcernsReChild", childh.ListConcernsReChild);//param
                com.Parameters.AddWithValue("@schReferToClinic2", childh.ReferToClinic2);//param
                com.Parameters.AddWithValue("@schReferToOVC", childh.ReferToOVC);//param
                com.Parameters.AddWithValue("@schRefNo2", childh.ReferralNo2);//param
                com.Parameters.AddWithValue("@schMotherHIVPos", childh.MotherHIVPlus);//param
                com.Parameters.AddWithValue("@schChildBreastfed", childh.ChildBreastFed);//param
                com.Parameters.AddWithValue("@schHowLong", childh.Howlong);//param
                com.Parameters.AddWithValue("@schChildEverOnNevirapine", childh.ChildEverOnNevirapine);//param
                com.Parameters.AddWithValue("@schPCRDone", childh.PCRDone);//param
                com.Parameters.AddWithValue("@schPCRResult", childh.PCRResults);//param
                com.Parameters.AddWithValue("@schReferToClinic3", childh.ReferToClinic3);//param
                com.Parameters.AddWithValue("@schRefNo3", childh.ReferalNo3);//param
                com.Parameters.AddWithValue("@schImmunisationUpToDate", childh.ImmunisationUpToDate);//param
                com.Parameters.AddWithValue("@WhichImmunisatationsOutStanding", childh.WhichImmunisatationsOutStanding);//param
                com.Parameters.AddWithValue("@schReferToClinic4", childh.ReferToClinic4);//param
                com.Parameters.AddWithValue("@schRefNo4", childh.ReferralNo4);//param
                com.Parameters.AddWithValue("@schVitAAndWormMedsGivenEachMonth", childh.VITAandWarmMedsGivenEachMonth);//param
                com.Parameters.AddWithValue("@schWalkAppropriateForAge", childh.WalkAppropriateForAge);//param
                com.Parameters.AddWithValue("@schTalkAppropriateForAge", childh.TalkAppropriateForAge);//param
                

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

            //start of general
            #region General +-

            #region Measurement
            General genMeasurement = new General
            {
                ScreeningID = scrID,
                Weight= int.Parse( txtWeight.Text),
                Height = int.Parse(txtheight.Text),
                BMI = int.Parse(txtBMI.Text)
            };

            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@", genMeasurement.ScreeningID);//param
                com.Parameters.AddWithValue("@", genMeasurement.Weight);//param
                com.Parameters.AddWithValue("@", genMeasurement.Height);//param
                com.Parameters.AddWithValue("@", genMeasurement.BMI);//param
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


            #region CurrentMedications
            //HPT
           
            List<CurrentMedications> medsList = new List<CurrentMedications>();

            
            CurrentMedications curhpt = new CurrentMedications {
                ScreeningID = scrID,
                DiseaseID =1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic=false,
                ReferralNo= ""


            };

           // medsList.Add(curhpt);
            

            //epilepsy
            
            CurrentMedications curEpi = new CurrentMedications
            {
                ScreeningID = scrID,
                DiseaseID = 1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic = false,
                ReferralNo = ""


            };
          //  medsList.Add(curEpi);
            

            //Diabetes
           
            CurrentMedications curDiabetes = new CurrentMedications
            {
                ScreeningID = scrID,
                DiseaseID = 1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic = false,
                ReferralNo = ""


            };
           // medsList.Add(curDiabetes);
           
            //Asthma
          
            CurrentMedications curAsthma = new CurrentMedications
            {
                ScreeningID = scrID,
                DiseaseID = 1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic = false,
                ReferralNo = ""


            };
           // medsList.Add(curAsthma);
           

            //oral thrush
            
            CurrentMedications curOther = new CurrentMedications
            {
                ScreeningID = scrID,
                DiseaseID = 1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic = false,
                ReferralNo = ""


            };

            #region commented
            //if (medsList.Count >1)
            //{
            //    foreach (var meds in medsList)
            //    {
            //        try
            //        {
            //            storedProcedure = "";// name of sp
            //            conn.Open();
            //            SqlCommand com = new SqlCommand(storedProcedure, conn);
            //            com.CommandType = CommandType.StoredProcedure;
            //            com.Parameters.AddWithValue("@", meds.ScreeningID);//param
            //            com.Parameters.AddWithValue("@", meds.DiseaseID);//param
            //            com.Parameters.AddWithValue("@", meds.StartDate);//param
            //            com.Parameters.AddWithValue("@", meds.Defaulting);//param
            //            com.Parameters.AddWithValue("@", meds.ReferToClinic);//param
            //            com.Parameters.AddWithValue("@", meds.ReferralID);//param
                       
            //            com.ExecuteNonQuery();//execute command
            //        }
            //        catch (Exception ex)
            //        {

            //            MessageBox.Show(ex.Message.ToString());
            //        }
            //        finally
            //        {
            //            conn.Close();
            //        }
            //    }
            //}

            //sp place
            //connection
            #endregion

            #endregion

            #region CurrentConditons
            //BP Reading
            BPReading bpr = new BPReading {
                ScreeningID = scrID,
                OnMeds = (redCurMedsBP.IsChecked == true) ? true : false,
                Systolic = ((ComboBoxItem)cbosys.SelectedItem).Content.ToString(),
                Diastolic = ((ComboBoxItem)cboDiasto.SelectedItem).Content.ToString(),
                ReferToCHOWs = (radrefCHowBP.IsChecked == true) ? true : false,
                ReferToClinic= (radrefBP.IsChecked == true) ? true : false,
                ReferralNo=txtrefBP.Text,
            };
            //sp place
            //connection
            //try
            //{
            //    storedProcedure = "";// name of sp
            //    conn.Open();
            //    SqlCommand com = new SqlCommand(storedProcedure, conn);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@ScreeningID", bpr.ScreeningID);//param
            //    com.Parameters.AddWithValue("@", bpr.Systolic);//param
            //    com.Parameters.AddWithValue("@", bpr.Diastolic);//param
            //    com.Parameters.AddWithValue("@", bpr.ReferToCHOWs);//param
            //    com.Parameters.AddWithValue("@", bpr.ReferToClinic);//param
            //    com.Parameters.AddWithValue("@", bpr.ReferralNo);//param
            //    com.ExecuteNonQuery();//execute command
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}

            //Blood sugar
            BloodSuger bs = new BloodSuger {
                ScreeningID = scrID,
                OnMeds = (redCurMedsBs.IsChecked == true) ? true : false,
                NotOnMedsBSReadings = ((ComboBoxItem)cboBSreading.SelectedItem).Content.ToString(),
               
                ReferToCHOWs = (radrefCHowBS.IsChecked == true) ? true : false,
                ReferToClinic = (radrefBS.IsChecked == true) ? true : false,
                ReferralNo = txtrefBS.Text,
            
            };
            //sp place
            //connection
            //try
            //{
            //    storedProcedure = "";// name of sp
            //    conn.Open();
            //    SqlCommand com = new SqlCommand(storedProcedure, conn);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@ScreeningID", bs.ScreeningID);//param
            //    com.Parameters.AddWithValue("@", bs.OnMeds);//param
            //    com.Parameters.AddWithValue("@", bs.NotOnMedsBSReadings);//param
            //    com.Parameters.AddWithValue("@", bs.ReferToCHOWs);//param
            //    com.Parameters.AddWithValue("@", bs.ReferToClinic);//param
            //    com.Parameters.AddWithValue("@", bs.ReferralNo);//param
            //    com.ExecuteNonQuery();//execute command
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}

            //Epilepsy
            Epilepsy ep = new Epilepsy {
                ScreeningID = scrID,
                FitsInLastMonth = (radlastfit.IsChecked == true) ? true : false,
                ReferToClinic = (radrefepi.IsChecked == true) ? true : false,
                ReferralNo = txtrefEpi.Text,
            };

            //sp place
            //connection
            //try
            //{
            //    storedProcedure = "";// name of sp
            //    conn.Open();
            //    SqlCommand com = new SqlCommand(storedProcedure, conn);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@ScreeningID", ep.ScreeningID);//param
            //    com.Parameters.AddWithValue("@", ep.FitsInLastMonth);//param
            //    com.Parameters.AddWithValue("@", ep.ReferToClinic);//param
            //    com.Parameters.AddWithValue("@", ep.ReferralNo);//param
            //    com.ExecuteNonQuery();//execute command
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}

            //HIV
            HIV hiv = new HIV {
                ScreeningID = scrID,
                KnownHIVPosStatus = (radhivknown.IsChecked == true) ? true : false,
                HIVTestDone = (radhivtest.IsChecked == true) ? true : false,
                Result = ((ComboBoxItem)cboHIVres.SelectedItem).Content.ToString(),
                ReferToClinic = (radrefHIV.IsChecked == true) ? true : false,
                ReferralNo = txtrefHIV.Text,
            };

            //sp place
            //connection
            //try
            //{
            //    storedProcedure = "AddHIV";// name of sp
            //    conn.Open();
            //    SqlCommand com = new SqlCommand(storedProcedure, conn);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@ScreeningID", hiv.ScreeningID);//param
            //    com.Parameters.AddWithValue("@KnownHIVPosStatus", hiv.KnownHIVPosStatus);//param
            //    com.Parameters.AddWithValue("@HIVTestDone", hiv.HIVTestDone);//param
            //    com.Parameters.AddWithValue("@Result", hiv.Result);//param
            //    com.Parameters.AddWithValue("@ReferToClinic", hiv.ReferToClinic);//param
            //    com.Parameters.AddWithValue("@ReferralNo", hiv.ReferralNo);//param
            //    com.ExecuteNonQuery();//execute command
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}

            //Pregnancy
            Pregnancy preg = new Pregnancy {
                ScreeningID = scrID,
               CurrentlyPregnant = (radpregpos.IsChecked == true) ? true : false,
                PregnancyTestDone = (radpretest.IsChecked == true) ? true : false,
                Results = ((ComboBoxItem)cboPreres.SelectedItem).Content.ToString(),
                ReferToClinic = (radrefHIV.IsChecked == true) ? true : false,
                ReferralNo = txtrefHIV.Text,
            };

            //sp place
            //connection
            //try
            //{
            //    storedProcedure = "";// name of sp
            //    conn.Open();
            //    SqlCommand com = new SqlCommand(storedProcedure, conn);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@ScreeningID", preg.ScreeningID);//param
            //    com.Parameters.AddWithValue("@", preg.CurrentlyPregnant);//param
            //    com.Parameters.AddWithValue("@", preg.PregnancyTestDone);//param
            //    com.Parameters.AddWithValue("@", preg.Results);//param
            //    com.Parameters.AddWithValue("@", preg.ReferToClinic);//param
            //    com.Parameters.AddWithValue("@", preg.ReferralNo);//param
               
            //    com.ExecuteNonQuery();//execute command
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}

            #endregion

            #region Tuberculosis

            Tubercolosis tb = new Tubercolosis {
                ScreeningID = scrID,
                HaveTubercolosis = (radhasTB.IsChecked == true) ? true : false,
                LossWeight = (radhasTB.IsChecked == true) ? true : false,
                SweatingAtNight = (radhasTB.IsChecked == true) ? true : false,
                Defaulting = (radDefaulting.IsChecked == true) ? true : false,
                FeverOver2Weeks = (radFever2.IsChecked == true) ? true : false,
                CoughMoreThan2Weeks = (radCough2.IsChecked == true) ? true : false,
                LossOfApetite = (radlossapp.IsChecked == true) ? true : false,
               
                ReferToClinic = (radrefTBSymp.IsChecked == true) ? true : false,
                ReferralNo = txtRefTBsymp.Text,
            };
            tb.WhatMedsAreYouOn = "";

            TBContactTracing tbc = new TBContactTracing
            {
                ScreeningID = scrID,
                HouseHoldOnTBMeds = (radhouseTb.IsChecked == true) ? true : false,
                ReferToClinic = (radrefTbCon.IsChecked == true) ? true : false,
                 ReferralNo = txtTBContract.Text,
            };

            //sp place
            //connection
            //try
            //{
            //    storedProcedure = "";// name of sp
            //    conn.Open();
            //    SqlCommand com = new SqlCommand(storedProcedure, conn);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@ScreeningID", tb.ScreeningID);//param
            //    com.Parameters.AddWithValue("@", tb.HaveTubercolosis);//param
            //    com.Parameters.AddWithValue("@", tb.WhatMedsAreYouOn);//param
            //    com.Parameters.AddWithValue("@", tb.Defaulting);//param
            //    com.Parameters.AddWithValue("@", tb.LossWeight);//param
            //    com.Parameters.AddWithValue("@", tb.SweatingAtNight);//param
            //    com.Parameters.AddWithValue("@", tb.FeverOver2Weeks);//param
            //    com.Parameters.AddWithValue("@", tb.CoughMoreThan2Weeks);//param
            //    com.Parameters.AddWithValue("@", tb.LossOfApetite);//param
            //    com.Parameters.AddWithValue("@", tb.ReferToClinic);//param
            //    com.Parameters.AddWithValue("@", tb.ReferralNo);//param
            //    com.Parameters.AddWithValue("@", tb.HouseholdMemberONTBMeds);//param
                

            //    com.ExecuteNonQuery();//execute command
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}
            #endregion


            #region Other Conditon and eldery care

            OtherCondition otc = new OtherCondition {
                ScreeningID = scrID,
               BloodInUrine = (radelpassvision.IsChecked == true) ? true : false,
                ReferToClinic = (radotref.IsChecked == true) ? true : false,
                ReferralNo = txtRefOtc.Text,
                Smoking = (radotsmoke.IsChecked == true) ? true : false,
               Drinking = (radotdrink.IsChecked == true) ? true : false,
                DrinkAlchoholUnitsPerWeek = ((ComboBoxItem)cboBeerUnit.SelectedItem).Content.ToString(),
                DiarrhoeaOver3Days = (radotdiarhoea.IsChecked == true) ? true : false,
                ReferToClinic2 = (radotref2.IsChecked == true) ? true : false,
                AttendedInitiationSchool = (radotinit.IsChecked == true) ? true : false,
               LagCrampsOver2Weeks = (radotcramps.IsChecked == true) ? true : false,
               LagNumbnessOver2Weeks = (radotnumb.IsChecked == true) ? true : false,
                FootUlcer = (radotulcer.IsChecked == true) ? true : false,
                ReferToClinic3 = (radotref3.IsChecked == true) ? true : false,
               
                FamilyPlanningAdvice = (radotFam.IsChecked == true) ? true : false,
                ReferralNo2 = txtRefOtc2.Text,
                ReferalNo3 = txtRefOtc3.Text
             };

            //sp place
            //connection
            //try
            //{
            //    storedProcedure = "";// name of sp
            //    conn.Open();
            //    SqlCommand com = new SqlCommand(storedProcedure, conn);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@ScreeningID", otc.ScreeningID);//param
            //    com.Parameters.AddWithValue("@", otc.BloodInUrine);//param
            //    com.Parameters.AddWithValue("@", otc.ReferToClinic);//param
            //    com.Parameters.AddWithValue("@", otc.ReferralNo);//param
            //    com.Parameters.AddWithValue("@", otc.Smoking);//param
            //    com.Parameters.AddWithValue("@", otc.Drinking);//param
            //    com.Parameters.AddWithValue("@", otc.DrinkAlchoholUnitsPerWeek);//param
            //    com.Parameters.AddWithValue("@", otc.DiarrhoeaOver3Days);//param
            //    com.Parameters.AddWithValue("@", otc.ReferToClinic2);//param
            //    com.Parameters.AddWithValue("@", otc.AttendedInitiationSchool);//param
            //    com.Parameters.AddWithValue("@", otc.LagCrampsOver2Weeks);//param
            //    com.Parameters.AddWithValue("@", otc.LagNumbnessOver2Weeks);//param
            //    com.Parameters.AddWithValue("@", otc.FootUlcer);//param
            //    com.Parameters.AddWithValue("@", otc.ReferToClinic3);//param
            //    com.Parameters.AddWithValue("@", otc.FamilyPlanningAdvice);//param
            //    com.Parameters.AddWithValue("@", otc.ReferralNo2);//param

            //    com.Parameters.AddWithValue("@", otc.ReferalNo3);//param
                
            //    com.ExecuteNonQuery();//execute command
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}
            
            ElderlyCareAssessment eld = new ElderlyCareAssessment {
                 ScreeningID = scrID,
                LegFootArmHanAmputation = (radelamp.IsChecked == true) ? true : false,
                 PassVisionTest = (radelpassvision.IsChecked == true) ? true : false,
                 Bedridden = (radelBed.IsChecked == true) ? true : false,
                 UseAidToMove = (radelmove.IsChecked == true) ? true : false,
                 WashYourself = (radelwash.IsChecked == true) ? true : false,
                 FeedYourSelf = (radelfeed.IsChecked == true) ? true : false,
                 DressYourSelf = (radeldress.IsChecked == true) ? true : false,
                 ReferToClinic = (radelref.IsChecked == true) ? true : false,
                 ReferralNo = txtRefEld.Text

            };

            //sp place
            //connection
            //try
            //{
            //    storedProcedure = "";// name of sp
            //    conn.Open();
            //    SqlCommand com = new SqlCommand(storedProcedure, conn);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@", eld.ScreeningID);//param
            //    com.Parameters.AddWithValue("@", eld.LegFootArmHanAmputation);//param
            //    com.Parameters.AddWithValue("@", eld.Bedridden);//param
            //    com.Parameters.AddWithValue("@", eld.UseAidToMove);//param
            //    com.Parameters.AddWithValue("@", eld.WashYourself);//param
            //    com.Parameters.AddWithValue("@", eld.FeedYourSelf);//param
            //    com.Parameters.AddWithValue("@", eld.DressYourSelf);//param
            //    com.Parameters.AddWithValue("@", eld.ReferToClinic);//param
            //    com.Parameters.AddWithValue("@", eld.ReferralNo);//param
            //    com.ExecuteNonQuery();//execute command
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}

            #endregion




            //sp place this is a massive store pro cross finger
            //connection
            try
            {
                storedProcedure = "AddScreeningGeneral";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                //measurement+
                com.Parameters.AddWithValue("@EncounterID", genMeasurement.ScreeningID);//param
                com.Parameters.AddWithValue("@sgWeight", genMeasurement.Weight);//param
                com.Parameters.AddWithValue("@sgHeight", genMeasurement.Height);//param
                com.Parameters.AddWithValue("@sgBMI", genMeasurement.BMI);//param
             
                //Current Medication HPT+
                com.Parameters.AddWithValue("@sgOnMeds", curhpt.OnMeds);//param
                com.Parameters.AddWithValue("@sgNotOnMeds", curhpt.OnMeds);//param
                com.Parameters.AddWithValue("@sgHypertension", curhpt.IsSick);//param
                com.Parameters.AddWithValue("@sgHypertensionStartDate", curhpt.StartDate);//param
                com.Parameters.AddWithValue("@sgHypertensionDefaulting", curhpt.Defaulting);//param
                com.Parameters.AddWithValue("@sgHypertensionReferToClinic", curhpt.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgHypertensionRefNo", curhpt.ReferralNo);//param

                    //Diabetes+
                com.Parameters.AddWithValue("@sgDiabetes", curDiabetes.IsSick);//param
                com.Parameters.AddWithValue("@sgDiabetesStartDate", curDiabetes.StartDate);//param
                com.Parameters.AddWithValue("@sgDiabetesDefaulting", curDiabetes.Defaulting);//param
                com.Parameters.AddWithValue("@sgDiabetesReferToClinic", curDiabetes.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgDiabetesRefNo", curDiabetes.ReferralNo);//param

                    //Epilepsy+
                com.Parameters.AddWithValue("@sgEpilepsy", curEpi.IsSick);//param
                com.Parameters.AddWithValue("@sgEpilepsyStartDate", curEpi.StartDate);//param
                com.Parameters.AddWithValue("@sgEpilepsyDefaulting", curEpi.Defaulting);//param
                com.Parameters.AddWithValue("@sgEpilepsyReferToClinic", curEpi.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgEpilepsyRefNo", curEpi.ReferralNo);//param

                    //Asthma +   
                com.Parameters.AddWithValue("@sgAsthma", curAsthma.IsSick);//param
                com.Parameters.AddWithValue("@sgAsthmaStartDate", curAsthma.StartDate);//param
                com.Parameters.AddWithValue("@sgAsthmaDefaulting", curAsthma.Defaulting);//param
                com.Parameters.AddWithValue("@sgAsthmaReferToClinic", curAsthma.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgAsthmaRefNo", curAsthma.ReferralNo);//param

                     //Other   +
                com.Parameters.AddWithValue("@sgOther", curOther.IsSick);//param
                com.Parameters.AddWithValue("@sgOtherStartDate", curOther.StartDate);//param
                com.Parameters.AddWithValue("@sgOtherDefaulting", curOther.Defaulting);//param
                com.Parameters.AddWithValue("@sgOtherReferToClinic", curOther.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgOtherRefNo", curOther.ReferralNo);//param
              
                //Current Condition
                        //Bp Reading+-
                com.Parameters.AddWithValue("@sgBPOnMedsSystolic", bpr.Systolic);//param
                com.Parameters.AddWithValue("@sgBPOnMedsDiastolic", bpr.Diastolic);//param
                com.Parameters.AddWithValue("@sgBPNotOnMedsSystolic", bpr.Systolic);//param-
                com.Parameters.AddWithValue("@sgBPNotOnMedsDiastolic", bpr.Diastolic);//param-
                com.Parameters.AddWithValue("@sgBPReferToCHOW", bpr.ReferToCHOWs);//param
                com.Parameters.AddWithValue("@sgBPReferToClinic", bpr.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgBPRefNo", bpr.ReferralNo);//param
                        //Blood Sugar Reading+
                com.Parameters.AddWithValue("@sgBSOnMeds", bs.OnMeds);//param
                com.Parameters.AddWithValue("@sgBSNotOnMedsBSReading", bs.NotOnMedsBSReadings);//param
                com.Parameters.AddWithValue("@sgBSReferToChow", bs.ReferToCHOWs);//param
                com.Parameters.AddWithValue("@sgBSReferToClinic", bs.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgBSRefNo", bs.ReferralNo);//param
                        //Epilepsy+
                com.Parameters.AddWithValue("@sgEpilepsyFitsInLastMonth", ep.FitsInLastMonth);//param
                com.Parameters.AddWithValue("@sgEpilepsyReferToClinic2", ep.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgEpilepsyRefNo2", ep.ReferralNo);//param
                     //HIV+-
                com.Parameters.AddWithValue("@sgHIVKnownPosStatus", hiv.KnownHIVPosStatus);//param
                com.Parameters.AddWithValue("@sgHIVKnownNegStatus", hiv.KnownHIVPosStatus);//param-
                com.Parameters.AddWithValue("@sgHIVTestDone", hiv.HIVTestDone);//param
                com.Parameters.AddWithValue("@sgHIVResult", hiv.Result);//param
                com.Parameters.AddWithValue("@sgHIVReferToClinic", hiv.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgHIVRefNo", hiv.ReferralNo);//param
                    //Pregnacy+-
                com.Parameters.AddWithValue("@sgPregnancyCurrentlyPregnant", preg.CurrentlyPregnant);//param
                com.Parameters.AddWithValue("@sgPregnancyPossibleThatPregnant", preg.CurrentlyPregnant);//param-
                com.Parameters.AddWithValue("@sgPregnancyTestDone", preg.PregnancyTestDone);//param
                com.Parameters.AddWithValue("@sgPregnancyResult", preg.Results);//param
                com.Parameters.AddWithValue("@sgPregnancyReferToClinic", preg.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgPregancyRefNo", preg.ReferralNo);//param
                    //Tuberculosis+-
                com.Parameters.AddWithValue("@sgTBCurrentlyHaveTB", tb.HaveTubercolosis);//param
                //com.Parameters.AddWithValue("@", tb.WhatMedsAreYouOn);//param
                com.Parameters.AddWithValue("@sgTBDefaulting", tb.Defaulting);//param
                com.Parameters.AddWithValue("@sgTBRecentUnplannedWeightLoss", tb.LossWeight);//param
                com.Parameters.AddWithValue("@sgTBExcessiveSweatingAtNight", tb.SweatingAtNight);//param
                com.Parameters.AddWithValue("@sgTBFeverOver2Weeks", tb.FeverOver2Weeks);//param
                com.Parameters.AddWithValue("@sgTBCoughMoreThan2Weeks", tb.CoughMoreThan2Weeks);//param
                com.Parameters.AddWithValue("@sgTBLossOfApetite", tb.LossOfApetite);//param
                com.Parameters.AddWithValue("@sgTBReferToClinic", tb.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgTBRefNo", tb.ReferralNo);//param
              //  com.Parameters.AddWithValue("@", tb.HouseholdMemberONTBMeds);//param

                com.Parameters.AddWithValue("@sgTBContactTracingHouseholdMemberOnMeds", tbc.HouseHoldOnTBMeds);//param
                com.Parameters.AddWithValue("@sgTBContactTracingReferToClinic", tbc.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgTBContactTracingRefNo", tbc.ReferralNo);//param

                //other+-
                com.Parameters.AddWithValue("@sgOtherBloodInUrine", otc.BloodInUrine);//param
                com.Parameters.AddWithValue("@sgOtherReferToClinic2", otc.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgOtherRefNo2", otc.ReferralNo);//param
                com.Parameters.AddWithValue("@sgOtherSmoking", otc.Smoking);//param
              //  com.Parameters.AddWithValue("@sgOtherAlcoholUnitsPerWeek", otc.Drinking);//param
                com.Parameters.AddWithValue("@sgOtherAlcoholUnitsPerWeek", otc.DrinkAlchoholUnitsPerWeek);//param
                com.Parameters.AddWithValue("@sgOtherDiarrhoeaOver3Days", otc.DiarrhoeaOver3Days);//param
                com.Parameters.AddWithValue("@sgOtherReferToClinic3", otc.ReferToClinic2);//param
                com.Parameters.AddWithValue("@sgOtherRefNo3", otc.ReferralNo2);//param
                com.Parameters.AddWithValue("@sgOtherAttendedInitiationSchool", otc.AttendedInitiationSchool);//param
                com.Parameters.AddWithValue("@sgOtherLegCrampsOver2Weeks", otc.LagCrampsOver2Weeks);//param
                com.Parameters.AddWithValue("@sgOtherLegNumbnessOver2Weeks", otc.LagNumbnessOver2Weeks);//param
                com.Parameters.AddWithValue("@sgOtherFootUlcer", otc.FootUlcer);//param
                com.Parameters.AddWithValue("@sgOtherReferToClinic4", otc.ReferToClinic3);//param
                com.Parameters.AddWithValue("@sgFamilyPlanningAdviceGiven", otc.FamilyPlanningAdvice);//param
                com.Parameters.AddWithValue("@sgOtherRefNo4", otc.ReferalNo3);//param

                //Eldery+
                com.Parameters.AddWithValue("@sgElderlyAmputation", eld.LegFootArmHanAmputation);//param
                com.Parameters.AddWithValue("@sgElderlyBedridden", eld.Bedridden);//param
                com.Parameters.AddWithValue("@sgElderlyUseAidToMove", eld.UseAidToMove);//param
                com.Parameters.AddWithValue("@sgElderlyWashYourself", eld.WashYourself);//param
                com.Parameters.AddWithValue("@sgElderlyFeedYourself", eld.FeedYourSelf);//param
                com.Parameters.AddWithValue("@sgElderlyDressYourself", eld.DressYourSelf);//param
                com.Parameters.AddWithValue("@sgElderlyReferToClinic", eld.ReferToClinic);//param
                com.Parameters.AddWithValue("@sgElderlyRefNo", eld.ReferralNo);//param

                //Fiiirrred
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

            //end of general

            #region otherTab +

            Other_Tab ottab = new Other_Tab {
                ScreeningID = scrID,
                ReferralNo= txtOtherRef.Text,
                ReferToClinic = (radOtherRef.IsChecked == true) ? true : false,
                OtherConditionFound = txtOtherCon.Text
            
            };


            //sp place
            //connection
            try
            {
                storedProcedure = "AddScreeningOther";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EncounterID", ottab.ScreeningID);//param
                com.Parameters.AddWithValue("@schReferredToClinic", ottab.ReferToClinic);//param
                com.Parameters.AddWithValue("@schRefNo", ottab.ReferralNo);//param
                com.Parameters.AddWithValue("@schOtherConditionFoundThatRequiredReferral", ottab.OtherConditionFound);//param
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

            #region Environmental +-

            Environmental environment = new Environmental();
            environment.ScreeningId = scrID;
            environment.NoOfHouseholdCurrent = int.Parse(numCurrentHousehold.Text);
            environment.NoOfHouseholdAway = int.Parse(numAwayHousehold.Text);
            environment.ListWhere = ((ComboBoxItem)cboListWhere.SelectedItem).Content.ToString(); //----ListWhere missing from the db
            environment.WhenLastClinicVisit = ((ComboBoxItem)cboLastVisit.SelectedItem).Content.ToString();
            environment.WhichClinic = int.Parse(((ComboBoxItem)cboClinic.SelectedItem).Content.ToString());

           
            EnvironmentalExtra environmentExtra = new EnvironmentalExtra();
            environmentExtra.ScreeningID = scrID;
            environmentExtra.NoOfPeopleInOneRoomMainHut = int.Parse(((ComboBoxItem)cboNoPeople.SelectedItem).Content.ToString());
            environmentExtra.NoOfStructuresInHomeStead = int.Parse(((ComboBoxItem)cboNoStructure.SelectedItem).Content.ToString());
            environmentExtra.RainWaterCollection = (rdoYesRain.IsChecked == true) ? true : false;
            environmentExtra.WaterSupply = ((ComboBoxItem)cboWaterSupply.SelectedItem).Content.ToString();
           environmentExtra.WalkingDistanceFromWhaterSupply = ((ComboBoxItem)cboWalkingDistanceWater.SelectedItem).Content.ToString();
            environmentExtra.TreatWaterBeforeDrinking = (rdoTreatWater.IsChecked == true) ? true : false;
            environmentExtra.ElectricityInAnyHut = (rdoElectricityYes.IsChecked == true) ? true : false;
            environmentExtra.HaveWorkingFridge = (rdoYesFridge.IsChecked == true) ? true : false;
            environmentExtra.UseForCooking = ((ComboBoxItem)cboUseForCooking.SelectedItem).Content.ToString();
            environmentExtra.TypeOfToilet = ((ComboBoxItem)cboTypeOfToilet.SelectedItem).Content.ToString();
            environmentExtra.DisposeWaste = ((ComboBoxItem)cboDisposeWaste.SelectedItem).Content.ToString();
            environmentExtra.SourceOfIncome = ((ComboBoxItem)cboSourceOfIncome.SelectedItem).Content.ToString(); //----SourceOfIncome missing from the db
            environmentExtra.RecievedFoodPacelIn6Month = (cboFoodParcel.IsChecked == true) ? true : false;

            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@EncounterID", environment.ScreeningId);//param
                com.Parameters.AddWithValue("@seNoPeopleInHousehold", environment.NoOfHouseholdCurrent);//param
                com.Parameters.AddWithValue("@seNoLivingAwayFromHousehold", environment.NoOfHouseholdAway);//param
                com.Parameters.AddWithValue("@seWhenDidYouOrMemberLastVisit", environment.WhenLastClinicVisit);//param
                com.Parameters.AddWithValue("@cID", environment.WhichClinic);//param

                com.Parameters.AddWithValue("@seTotalNumberSleepingInOneRoomInMainHut", environmentExtra.NoOfPeopleInOneRoomMainHut);//param
                com.Parameters.AddWithValue("@seTotalNoStructures", environmentExtra.NoOfStructuresInHomeStead);//param
                com.Parameters.AddWithValue("@seRainWaterCollection", environmentExtra.RainWaterCollection);//param
                com.Parameters.AddWithValue("@seWaterSupply", environmentExtra.WaterSupply);//param
                com.Parameters.AddWithValue("@seWatlkingDistanceFromWaterSupply", environmentExtra.WalkingDistanceFromWhaterSupply);//param
                com.Parameters.AddWithValue("@seTreatWaterBeforeDrinking", environmentExtra.TreatWaterBeforeDrinking);//param
                com.Parameters.AddWithValue("@seElectricityInAnyHut", environmentExtra.ElectricityInAnyHut);//param
                com.Parameters.AddWithValue("@seWorkingFridge", environmentExtra.HaveWorkingFridge);//param
                com.Parameters.AddWithValue("@seCookingMethod", environmentExtra.UseForCooking);//param
                com.Parameters.AddWithValue("@seToiletType", environmentExtra.TypeOfToilet);//param
                com.Parameters.AddWithValue("@seWasteDisposalType", environmentExtra.DisposeWaste);//param
               // com.Parameters.AddWithValue("@seSourceOfIncome", environmentExtra.SourceOfIncome);//param
                com.Parameters.AddWithValue("@seFoodParcelInLast6Months", environmentExtra.RecievedFoodPacelIn6Month);//param


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

        #region SaveHut
        private void btnSaveHut_Click(object sender, RoutedEventArgs e)
        {
            string scrID = ""; // 
            string storedProcedure = "";
            //this thing is missing encouter id so it doesnt reflect to the client

            if (int.Parse(cboNoOfHuts.SelectedItem.ToString()) > 1)
            {
                int MaxNoHuts = int.Parse(cboNoOfHuts.SelectedItem.ToString());

                while (HutCounter <= MaxNoHuts)
                {
                    Hut hut = new Hut();
                    hut.HutId = HutCounter;
                    hut.HutStracture = ((ComboBoxItem)cboHutStructure.SelectedItem).Content.ToString();
                    hut.TypeOfRoof = ((ComboBoxItem)cboTypeOfRoof.SelectedItem).Content.ToString();
                    hut.Ventilation = ((ComboBoxItem)cboVentilation.SelectedItem).Content.ToString();
                    hut.TotalNoOfRooms = int.Parse(((ComboBoxItem)cboNoRooms.SelectedItem).Content.ToString());
                    hut.isMainHut = false;
                    try
                    {
                        storedProcedure = "AddScreeningEnvironmentHuts";// name of sp
                        conn.Open();
                        SqlCommand com = new SqlCommand(storedProcedure, conn);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@seID", hut.HutId);//param
                        com.Parameters.AddWithValue("@sehutStructure", hut.HutStracture);//param
                        com.Parameters.AddWithValue("@sehutTypeOfRoof", hut.TypeOfRoof);//param
                        com.Parameters.AddWithValue("@sehutVentilation", hut.Ventilation);//param
                        com.Parameters.AddWithValue("@sehutNumberOfRooms", hut.TotalNoOfRooms);//param
                        com.Parameters.AddWithValue("@sehutMain", hut.isMainHut);//param
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

                    HutCounter++;
                }
            }

            else
            {
                Hut hut = new Hut();
                hut.HutId = 1;
                hut.HutStracture = ((ComboBoxItem)cboHutStructure.SelectedItem).Content.ToString();
                hut.TypeOfRoof = ((ComboBoxItem)cboTypeOfRoof.SelectedItem).Content.ToString();
                hut.Ventilation = ((ComboBoxItem)cboVentilation.SelectedItem).Content.ToString();
                hut.TotalNoOfRooms = int.Parse(((ComboBoxItem)cboNoRooms.SelectedItem).Content.ToString());

                try
                {
                    storedProcedure = "";// name of sp
                    conn.Open();
                    SqlCommand com = new SqlCommand(storedProcedure, conn);

                    com.Parameters.AddWithValue("@", hut.HutId);//param

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
            }
        }

        #endregion
    }
}
