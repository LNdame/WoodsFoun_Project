using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
using WpfPageTransitions;
using Impilo_App.Views.Screening;

namespace Impilo_App.Views.Client
{
    /// <summary>
    /// Interaction logic for AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : UserControl
    {
        //SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\MyDatabase.mdf;Integrated Security=True;User Instance=True");
       // DAL da = new DAL();

        static string sconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
       Impilo_App.Views.Client.Datasource datasource = null;
        SqlConnection conn = new SqlConnection(sconn);
        public AddNewClient()
        {
            InitializeComponent();

            datasource = new Datasource();
            this.DataContext = datasource;
            bindClinicCBO();
        }

        public void bindClinicCBO()
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Clinic", conn);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
              //  dt = ds.Tables[0];
                ClinicUsed.ItemsSource = ds.Tables[0].DefaultView;
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


        private void btnAddCountry_Click(object sender, RoutedEventArgs e)
        {
            Impilo_App.LocalModels.Client newClient = new LocalModels.Client();

            try
            {
                newClient.ClientID = Utilities.GenerateClientID();
                newClient.FirstName = txtFirstName.Text;
                newClient.LastName = txtLastName.Text;
                newClient.HeadOfHousehold = (radioHHYes.IsChecked == true) ? "yes" : "no";
                newClient.GPSLatitude = txtLatitude.Text;
                newClient.GPSLongitude = txtLongitude.Text;
                newClient.IDNo = txtIDNo.Text;
                newClient.ClinicUsed = int.Parse(ClinicUsed.SelectedValue.ToString());
                newClient.DateOfBirth = DateTime.Parse(txtDateofBirth.Text);
                newClient.NameofSchool = ((ComboBoxItem)NameofSchool.SelectedItem).Content.ToString();
                newClient.Gender = (radioMale.IsChecked == true) ? "Male" : "Female";
                newClient.AttendingSchool = (radioAttYes.IsChecked == true) ? true : false;

                newClient.Grade = ((ComboBoxItem)Grade.SelectedItem).Content.ToString();

                JumptoScreening(newClient);

              //  MessageBox.Show(newClient.ClinicUsed.ToString());

            }
            catch (Exception)
            {
                MessageBox.Show("Some fields are missing data or were filled with incorrect data", "New Client", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; throw;
            }
            
            



            //string storedProcedure = "AddClient";

           
          
           
            //Users user = new Users();
            //user.FirstName = txtFirstName.Text;
            //user.LastName = txtLastName.Text;
            //user.HeadOfHousehold = (radioHHYes.IsChecked == true) ? "yes" : "no";
            //user.GPSLatitude = txtLatitude.Text;
            //user.GPSLongitude = txtLongitude.Text;
            //user.IdentityNo = txtIDNo.Text;
            //user.ClinicUsed = ((ComboBoxItem)ClinicUsed.SelectedItem).Content.ToString();
            //user.DateOfBirth = DateTime.Parse(txtDateofBirth.Text);
            //user.NameofSchool = ((ComboBoxItem)NameofSchool.SelectedItem).Content.ToString();

            //string gender;
            //if(radioMale.IsChecked == true)
            //{
            //    gender = "male";
            //}
            //else
            //{
            //    gender = "female";
            //}
            //user.Gender = gender;

           
            //user.AttendingSchool = (radioAttYes.IsChecked == true) ? "yes" : "no";
            //user.Grade = ((ComboBoxItem)Grade.SelectedItem).Content.ToString();
            

            //da.RegisterUser(user);
            //MessageBoxResult result = MessageBox.Show("A client is successful added", "Confirmation");

            //txtFirstName.Text = "";
            //txtLastName.Text = "";
           
            //txtLatitude.Text = "";
            //txtLongitude.Text = "";
            //txtIDNo.Text = "";
            
        }


        private void JumptoScreening(LocalModels.Client addedClient)
        {
            ScreeningHome newPage = new ScreeningHome(addedClient.IDNo, addedClient);
            var a = Application.Current.MainWindow.FindName("pageTransitionControl") as PageTransition;
            a.ShowPage(newPage);
        }

        private void radioAttNo_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            if (rad.Name == "radioAttNo") //radioAttYes  radioAttNo
            {
                if (rad.IsChecked == true)
                {
                    NameofSchool.IsEnabled = false;
                }

            }
            else {
                NameofSchool.IsEnabled = true;
            }
               
            
        }
       
    }
}
