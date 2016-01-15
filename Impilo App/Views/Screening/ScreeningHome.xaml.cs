using System;
using System.Collections.Generic;
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
//using Models;
namespace Impilo_App.Views.Screening
{
    /// <summary>
    /// Interaction logic for ScreeningHome.xaml
    /// </summary>
    public partial class ScreeningHome : UserControl
    {
        public ScreeningHome()
        {
            InitializeComponent();
        }
        //DAL dataAccess;
        public ScreeningHome(string ID)
        {
           
            InitializeComponent();
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

           
        }
      
    }
}
