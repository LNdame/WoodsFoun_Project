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
        public ScreeningHome()
        {
            InitializeComponent();
        }
        //DAL dataAccess;
        public ScreeningHome(string ID)
        {
           
            InitializeComponent();

            //fill client being used




            //dataAccess = new DAL();
            //var client = dataAccess.GetClient(ID);
            //txtName.Text = client.FirstName;
            //txtLastName.Text = client.LastName;
            //txtIDNumber.Text = client.IDNo;
            //txtDateOfScreening.Text = DateTime.Now.ToString("dd MMMM yyyy h:mm");

            //if (client.Gender == "Male")
            //{
            //    rdoMale.IsChecked = true;
            //}
            //else
            //{
            //    rdoFemale.IsChecked = true;
            //}
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Repository.Screening screen = new Repository.Screening();
            //screen.ScreeningID = "";
            string name = "", surname = ""; 
            string scrID = Utilities.GenerateScreeningID(name, surname); // 
            string storedProcedure = "";
            #region Environemental

            #endregion

            #region Hypertension
            Hypertension hyper = new Hypertension();
            hyper.ScreeningID = scrID;
            hyper.YearOfDiagnosis = ((ComboBoxItem)cboDiaYear.SelectedItem).Content.ToString();
            hyper.Headache =  (headYes.IsChecked == true) ? true : false;
            hyper.BlurredVision = (headYes.IsChecked == true) ? true : false; ;
            hyper.ChestPain = (headYes.IsChecked == true) ? true : false; ;
            hyper.ReferralToClinic = (chestYes.IsChecked == true) ? true : false; ;
            hyper.ReferalNo = txthyperRef.Text;
            hyper.EverHadStroke = (stroYes.IsChecked == true) ? true : false; ;
            hyper.YearOfStroke = ((ComboBoxItem)cboStrokeYear.SelectedItem).Content.ToString();
            hyper.AnyOneInFamilyHadStroke = (anyYes.IsChecked == true) ? true : false; ;

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


            #region Diabetes
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
                storedProcedure = "AddDiabetes";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.AddWithValue("@ScreeningID", dia.ScreeningID);//param
                com.Parameters.AddWithValue("@YearOfDiagnosis", dia.YearOfDiagnosis);//param
                com.Parameters.AddWithValue("@regularlyThirsty", dia.regularlyThirsty);//param
                com.Parameters.AddWithValue("@WeightLoss", dia.WeightLoss);//param
                com.Parameters.AddWithValue("@UrinatingMore", dia.UrinatingMore);//param
                com.Parameters.AddWithValue("@NauseaOrVomitting", dia.NauseaOrVomitting);//param
                com.Parameters.AddWithValue("@FootExamResult", dia.FootExamResult);//param
                com.Parameters.AddWithValue("@ReferralToClinic", dia.ReferralToClinic);//param
                com.Parameters.AddWithValue("@ReferralNo", dia.ReferralNo);//param
                com.Parameters.AddWithValue("@FamilyMemberWith", dia.FamilyMemberWith);//param

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
                ReferToClinic=(hivref.IsChecked == true) ? true : false,
                ReferralNo = txtHIVRef.Text,
                ARVFileNo=txtARVFile.Text,

            };

            //missing the list of meds

            //sp place
            //connection
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@", hivTab.ScreeningID);//param

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

            #region Maternal Health
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
                storedProcedure = "AddMaternalHealth";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ScreeningID", materHealth.ScreeningID);//param
                com.Parameters.AddWithValue("@PregnantBefore", materHealth.PregnantBefore);//param
                com.Parameters.AddWithValue("@HowManySuccessful", materHealth.HowManySuccessful);//param
                com.Parameters.AddWithValue("@WhereDeliveredLasBaby", materHealth.WhereDeliveredLasBaby);//param
                com.Parameters.AddWithValue("@Caesarian", materHealth.Caesarian);//param
                com.Parameters.AddWithValue("@BabyUnder2_5Kgs", materHealth.BabyUnder2_5Kgs);//param
                com.Parameters.AddWithValue("@ChildrenDiedUnder1Year", materHealth.ChildrenDiedUnder1Year);//param
                com.Parameters.AddWithValue("@ChildrenDiedBetween1to5Years", materHealth.ChildrenDiedBetween1to5Years);//param
                com.Parameters.AddWithValue("@PAPSmearInLast5Years", materHealth.PAPSmearInLast5Years);//param
                com.Parameters.AddWithValue("@LastBloodTestResult", materHealth.LastBloodTestResult);//param
                com.Parameters.AddWithValue("@DateOfFirstANC", materHealth.DateOfFirstANC);//param
                com.Parameters.AddWithValue("@DateOfLastANC", materHealth.DateOfLastANC);//param
                com.Parameters.AddWithValue("@ReferredToClinic", materHealth.ReferredToClinic);//param
                com.Parameters.AddWithValue("@ReferralNo", materHealth.ReferralNo);//param
                com.Parameters.AddWithValue("@ExpectedDateOfDelivery", materHealth.ExpectedDateOfDelivery);//param
                com.Parameters.AddWithValue("@IntendFormulaFeed", materHealth.IntendFormulaFeed);//param
             //   com.Parameters.AddWithValue("@IntendBreastFeed", materHealth.IntendBreastFeed);//param this could superfluu
                com.Parameters.AddWithValue("@RegisteredOnMomConnect", materHealth.RegisteredOnMomConnect);//param
               

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
                storedProcedure = "AddChildHealth";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ScreeningID", childh.ScreeningID);//param
                com.Parameters.AddWithValue("@NameOfMother", childh.NameOfMother);//param
                com.Parameters.AddWithValue("@ChildWithRTHC", childh.ChildWithRTHC);//param
                com.Parameters.AddWithValue("@ReferToClinic", childh.ReferToClinic);//param
                com.Parameters.AddWithValue("@ReferalNo", childh.ReferalNo);//param
                com.Parameters.AddWithValue("@ListConcernsReChild", childh.ListConcernsReChild);//param
                com.Parameters.AddWithValue("@ReferToClinic2", childh.ReferToClinic2);//param
                com.Parameters.AddWithValue("@ReferToOVC", childh.ReferToOVC);//param
                com.Parameters.AddWithValue("@ReferralNo2", childh.ReferralNo2);//param
                com.Parameters.AddWithValue("@MotherHIVPlus", childh.MotherHIVPlus);//param
                com.Parameters.AddWithValue("@ChildBreastFed", childh.ChildBreastFed);//param
                com.Parameters.AddWithValue("@Howlong", childh.Howlong);//param
                com.Parameters.AddWithValue("@ChildEverOnNevirapine", childh.ChildEverOnNevirapine);//param
                com.Parameters.AddWithValue("@PCRDone", childh.PCRDone);//param
                com.Parameters.AddWithValue("@PCRResults", childh.PCRResults);//param
                com.Parameters.AddWithValue("@ReferToClinic3", childh.ReferToClinic3);//param
                com.Parameters.AddWithValue("@ReferalNo3", childh.ReferalNo3);//param
                com.Parameters.AddWithValue("@ImmunisationUpToDate", childh.ImmunisationUpToDate);//param
                com.Parameters.AddWithValue("@WhichImmunisatationsOutStanding", childh.WhichImmunisatationsOutStanding);//param
                com.Parameters.AddWithValue("@ReferToClinic4", childh.ReferToClinic4);//param
                com.Parameters.AddWithValue("@ReferralNo4", childh.ReferralNo4);//param
                com.Parameters.AddWithValue("@VITAandWarmMedsGivenEachMonth", childh.VITAandWarmMedsGivenEachMonth);//param
                com.Parameters.AddWithValue("@WalkAppropriateForAge", childh.WalkAppropriateForAge);//param
                com.Parameters.AddWithValue("@TalkAppropriateForAge", childh.TalkAppropriateForAge);//param
                

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
            #region General

            #region Measurement
            General genMeasurement = new General
            {
                ScreeningID = scrID,
                Weight= int.Parse( txtWeight.Text),
                Height = int.Parse(txtheight.Text)

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
            CurrentMedications curhpt = new CurrentMedications {
                ScreeningID = scrID,
                DiseaseID =1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic=false,
                ReferralID= ""


            };

            //epilepsy
            CurrentMedications curEpi = new CurrentMedications
            {
                ScreeningID = scrID,
                DiseaseID = 1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic = false,
                ReferralID = ""


            };


            //Diabetes
            CurrentMedications curDiabetes = new CurrentMedications
            {
                ScreeningID = scrID,
                DiseaseID = 1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic = false,
                ReferralID = ""


            };

            //Asthma
            CurrentMedications curAsthma = new CurrentMedications
            {
                ScreeningID = scrID,
                DiseaseID = 1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic = false,
                ReferralID = ""


            };

            //oral thrush
            CurrentMedications curOther = new CurrentMedications
            {
                ScreeningID = scrID,
                DiseaseID = 1,
                StartDate = (DateTime)txtDateNextANC.SelectedDate,
                Defaulting = false,
                ReferToClinic = false,
                ReferralID = ""


            };

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


            //Blood sugar
            BloodSuger bs = new BloodSuger {
                ScreeningID = scrID,
                OnMeds = (redCurMedsBs.IsChecked == true) ? true : false,
                NotOnMedsBSReadings = ((ComboBoxItem)cboBSreading.SelectedItem).Content.ToString(),
               
                ReferToCHOWs = (radrefCHowBS.IsChecked == true) ? true : false,
                ReferToClinic = (radrefBS.IsChecked == true) ? true : false,
                ReferralNo = txtrefBS.Text,
            
            };
            //Epilepsy
            Epilepsy ep = new Epilepsy {
                ScreeningID = scrID,
                FitsInLastMonth = (radlastfit.IsChecked == true) ? true : false,
                ReferToClinic = (radrefepi.IsChecked == true) ? true : false,
                ReferralNo = txtrefEpi.Text,
            };
            //HIV
            HIV hiv = new HIV {
                ScreeningID = scrID,
                KnownHIVPosStatus = (radhivknown.IsChecked == true) ? true : false,
                HIVTestDone = (radhivtest.IsChecked == true) ? true : false,
                Result = ((ComboBoxItem)cboHIVres.SelectedItem).Content.ToString(),
                ReferToClinic = (radrefHIV.IsChecked == true) ? true : false,
                ReferralNo = txtrefHIV.Text,
            };
            //Pregnancy
            Pregnancy preg = new Pregnancy {
                ScreeningID = scrID,
               CurrentlyPregnant = (radpregpos.IsChecked == true) ? true : false,
                PregnancyTestDone = (radpretest.IsChecked == true) ? true : false,
                Results = ((ComboBoxItem)cboPreres.SelectedItem).Content.ToString(),
                ReferToClinic = (radrefHIV.IsChecked == true) ? true : false,
                ReferralNo = txtrefHIV.Text,
            };


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
            try
            {
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

                com.Parameters.AddWithValue("@", hyper.ScreeningID);//param

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
            
            #endregion

            #endregion

            //end of general

            #region otherTab

            Other_Tab ottab = new Other_Tab {
                ScreeningID = scrID,
                ReferralNo= txtOtherRef.Text,
                ReferToClinic = (radOtherRef.IsChecked == true) ? true : false,
                OtherConditionFound = txtOtherCon.Text
            
            };

            #endregion

            #region Environmental

            #endregion



        }
      
    }
}
