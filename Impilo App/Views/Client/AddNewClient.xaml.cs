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
       
        SqlConnection conn = new SqlConnection(sconn);
        public AddNewClient()
        {
            InitializeComponent();
        }

        private void btnAddCountry_Click(object sender, RoutedEventArgs e)
        {
            Impilo_App.LocalModels.Client newClient = new LocalModels.Client();

            newClient.FirstName = txtFirstName.Text;
            newClient.LastName = txtLastName.Text;
            newClient.HeadOfHousehold = (radioHHYes.IsChecked == true) ? "yes" : "no";
            newClient.GPSLatitude = txtLatitude.Text;
            newClient.GPSLongitude = txtLongitude.Text;
            newClient.IDNo = txtIDNo.Text;
            newClient.ClinicUsed = ((ComboBoxItem)ClinicUsed.SelectedItem).Content.ToString();
            newClient.DateOfBirth = DateTime.Parse(txtDateofBirth.Text);
            newClient.NameofSchool = ((ComboBoxItem)NameofSchool.SelectedItem).Content.ToString();
            newClient.Gender = (radioMale.IsChecked == true) ? "Male" : "Female";
            newClient.AttendingSchool = (radioAttYes.IsChecked == true) ? "yes" : "no";
           
            newClient.Grade = ((ComboBoxItem)Grade.SelectedItem).Content.ToString();



            string storedProcedure = "RegisterUser";


              try
            {
                storedProcedure = "RegisterUser";// name of sp
                conn.Open();
                SqlCommand com = new SqlCommand(storedProcedure, conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@FirstName", newClient.FirstName);//param
                com.Parameters.AddWithValue("@LastName", newClient.LastName);//param
                com.Parameters.AddWithValue("@HeadOfHousehold", newClient.HeadOfHousehold);//param
                com.Parameters.AddWithValue("@GPSLatitude", newClient.GPSLatitude);//param
                com.Parameters.AddWithValue("@GPSLongitude", newClient.GPSLongitude);//param
                com.Parameters.AddWithValue("@IDNo", newClient.IDNo);//param
                com.Parameters.AddWithValue("@ClinicUsed", newClient.ClinicUsed);//param
                com.Parameters.AddWithValue("@DateOfBirth", newClient.DateOfBirth);//param
                com.Parameters.AddWithValue("@Gender", newClient.Gender);//param
                com.Parameters.AddWithValue("@AttendingSchool", newClient.AttendingSchool);//param
                com.Parameters.AddWithValue("@Grade", newClient.Grade);//param
                com.Parameters.AddWithValue("@NameofSchool", newClient.NameofSchool);//param

              int i =  com.ExecuteNonQuery();//execute command
              if (i>=1)
              {
                  MessageBox.Show("New Client Added Successfully");
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

       

       
    }
}
