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
using Repository;

namespace Impilo_App.Views.Client
{
    /// <summary>
    /// Interaction logic for AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : UserControl
    {
        //SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\MyDatabase.mdf;Integrated Security=True;User Instance=True");
        DAL da = new DAL();
        public AddNewClient()
        {
            InitializeComponent();
        }

        private void btnAddCountry_Click(object sender, RoutedEventArgs e)
        {
            Users user = new Users();
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.HeadOfHousehold = (radioHHYes.IsChecked == true) ? "yes" : "no";
            user.GPSLatitude = txtLatitude.Text;
            user.GPSLongitude = txtLongitude.Text;
            user.IdentityNo = txtIDNo.Text;
            user.ClinicUsed = ((ComboBoxItem)ClinicUsed.SelectedItem).Content.ToString();
            user.DateOfBirth = DateTime.Parse(txtDateofBirth.Text);
            user.NameofSchool = ((ComboBoxItem)NameofSchool.SelectedItem).Content.ToString();

            string gender;
            if(radioMale.IsChecked == true)
            {
                gender = "male";
            }
            else
            {
                gender = "female";
            }
            user.Gender = gender;

           
            user.AttendingSchool = (radioAttYes.IsChecked == true) ? "yes" : "no";
            user.Grade = ((ComboBoxItem)Grade.SelectedItem).Content.ToString();
            

            da.RegisterUser(user);
            MessageBoxResult result = MessageBox.Show("A client is successful added", "Confirmation");

            txtFirstName.Text = "";
            txtLastName.Text = "";
           
            txtLatitude.Text = "";
            txtLongitude.Text = "";
            txtIDNo.Text = "";
            
        }

       

       
    }
}
