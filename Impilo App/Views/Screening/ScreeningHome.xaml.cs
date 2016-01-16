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
            string scrID = ""; // 
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
                storedProcedure = "";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);

                com.Parameters.AddWithValue("@",dia.ScreeningID);//param

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

            #region Maternal Health
            MentalHealth materHealth = new MentalHealth();
            #endregion

            #region Child Health

            #endregion



        }
      
    }
}
