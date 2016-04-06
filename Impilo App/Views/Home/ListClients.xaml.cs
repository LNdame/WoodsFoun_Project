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
                string SQL = "Select ClientID as 'Unique Identifier',HeadOfHousehold as 'Head of Household',FirstName as 'First Name',LastName as 'Last Name',CONVERT(VARCHAR(11),DateOfBirth,106) as 'Date of Birth',GPSLatitude as 'GPS Latitude',GPSLongitude as 'GPS Longitude',IDNo as 'RSA ID Number',Clinic.ClinicID as 'Clinic ID',Clinic.ClinicName as 'Clinic Name',Gender,Grade as 'Grade in School',NameOfSchool as 'Name of School',AttendingSchool as 'Attending School'";
                SQL += ",(Select Count(EncounterID) from Encounters where ClientID = Client.ClientID and etID='1') as Screenings";
                SQL += ",(Select Count(EncounterID) from Encounters where ClientID = Client.ClientID and etID='2') as 'Follow-Ups'";
                SQL += ",(Select Count(EncounterID) from Encounters where ClientID = Client.ClientID and etID='3') as 'Clinic Visits'";
                SQL += " FROM Client";
                SQL += " Inner Join Clinic on Client.ClinicID = Clinic.ClinicID";

                if (txtSearch.Text.Trim() != "")
                {
                    SQL += " where " + (string)((ComboBoxItem)ddlSearch.SelectedValue).Tag + " like '%" + txtSearch.Text + "%'";
                }

                SQL += " Order by 'Unique Identifier', 'Last Name', 'First Name'";

                SqlCommand comm = new SqlCommand(SQL, conn);
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
                var id = dt.Rows[i]["RSA ID Number"].ToString();              

                Impilo_App.LocalModels.Client selectClient = new Impilo_App.LocalModels.Client
                {
                    ClientID = dt.Rows[i]["Unique Identifier"].ToString(),
                    HeadOfHousehold = dt.Rows[i]["Head of Household"].ToString(),
                    FirstName = dt.Rows[i]["First Name"].ToString(),
                    LastName = dt.Rows[i]["Last Name"].ToString(),
                    GPSLatitude = dt.Rows[i]["GPS Latitude"].ToString(),
                    GPSLongitude = dt.Rows[i]["GPS Longitude"].ToString(),
                    IDNo = dt.Rows[i]["RSA ID Number"].ToString(),
                    ClinicUsed =int.Parse( dt.Rows[i]["Clinic ID"].ToString()),
                    DateOfBirth = DateTime.Parse( dt.Rows[i]["Date of Birth"].ToString()),
                    Gender = dt.Rows[i]["Gender"].ToString(),
                    AttendingSchool = dt.Rows[i]["Attending School"].ToString() == "1"?true:false,
                    Grade = dt.Rows[i]["Grade in School"].ToString(),
                    NameofSchool = dt.Rows[i]["Name of School"].ToString()
                };



                switch (lflag)
                {
                    case 1: FollowUp newPage = new FollowUp(selectClient);
                        var a = Application.Current.MainWindow.FindName("pageTransitionControl") as PageTransition; 
                        a.ShowPage(newPage);break;
                    case 2: ClinicVisit clinicV = new ClinicVisit(selectClient);
                        var c = Application.Current.MainWindow.FindName("pageTransitionControl") as PageTransition;
                        c.ShowPage(clinicV);break;

                    default: FollowUp defaultfup= new FollowUp(selectClient);
                        var d = Application.Current.MainWindow.FindName("pageTransitionControl") as PageTransition;
                        d.ShowPage(defaultfup);break;
                }

               
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindMyData();
        }

        private void ddlSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindMyData();
        }
    }
}
