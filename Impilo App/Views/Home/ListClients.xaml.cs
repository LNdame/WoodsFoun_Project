using System;
using System.Collections.Generic;
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
using WpfPageTransitions;
using Impilo_App.Views.Screening;
using Impilo_App.Views.FollowUpVisit;
using Impilo_App.Views.ClinicData;
using System.Configuration;


namespace Impilo_App.Views.Home
{
    /// <summary>
    /// Interaction logic for ListClients.xaml
    /// </summary>
    public partial class ListClients : UserControl
    { 
        //DAL da = new DAL();
        static string sconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=impilo;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(sconn);
        public ListClients()
        {
            InitializeComponent();
            BindMyData();
        }

        int lflag;//1 for follow up 2 for clinic visit//
        public ListClients(int launcherFlag)
        {
            InitializeComponent();
            lflag = launcherFlag;
            BindMyData();
        }
        DataTable dt = new DataTable();
        public void BindMyData()
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Client", conn);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
                dt = ds.Tables[0];
                mydatagrid.ItemsSource = ds.Tables[0].DefaultView;
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

        private void mydatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = mydatagrid.SelectedIndex;

            if (i >= 0 && i < mydatagrid.Items.Count - 1)
            {
                var id = dt.Rows[i]["IDNo"].ToString();

                Impilo_App.LocalModels.Client selectClient = new Impilo_App.LocalModels.Client
                {
                    ClientID = dt.Rows[i]["ClientID"].ToString(),
                    HeadOfHousehold = dt.Rows[i]["HeadOfHousehold"].ToString(),
                    FirstName = dt.Rows[i]["FirstName"].ToString(),
                    LastName = dt.Rows[i]["LastName"].ToString(),
                    GPSLatitude = dt.Rows[i]["GPSLatitude"].ToString(),
                    GPSLongitude = dt.Rows[i]["GPSLongitude"].ToString(),
                    IDNo = dt.Rows[i]["IDNo"].ToString(),
                    ClinicUsed =int.Parse( dt.Rows[i]["ClinicID"].ToString()),
                   // DateOfBirth = DateTime.Parse( dt.Rows[i]["IDNo"].ToString()),
                    Gender = dt.Rows[i]["Gender"].ToString(),
                  //  AttendingSchool = dt.Rows[i]["AttendingSchool"].ToString(),
                    Grade = dt.Rows[i]["Grade"].ToString(),
                    NameofSchool = dt.Rows[i]["NameofSchool"].ToString()
                };



                switch (lflag)
                {
                    case 1: FollowUp newPage = new FollowUp(selectClient);
                        var a = Application.Current.MainWindow.FindName("pageTransitionControl") as PageTransition;
                        a.ShowPage(newPage); break;
                    case 2: ClinicVisit clinicV = new ClinicVisit(selectClient);
                        var c = Application.Current.MainWindow.FindName("pageTransitionControl") as PageTransition;
                        c.ShowPage(clinicV); break;

                    default: FollowUp defaultfup = new FollowUp(selectClient);
                        var d = Application.Current.MainWindow.FindName("pageTransitionControl") as PageTransition;
                        d.ShowPage(defaultfup); break;
                }

               
            }
        }
       
    }
}
