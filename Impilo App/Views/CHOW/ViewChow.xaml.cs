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

namespace Impilo_App.Views.CHOW
{
    /// <summary>
    /// Interaction logic for ViewChow.xaml
    /// </summary>
    public partial class ViewChow : UserControl
    {
        static string sconn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(sconn);

        public ViewChow()
        {
            InitializeComponent();
            BindMyData();
        }
        public void BindMyData()
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT FirstName, LastName, Gender, Email, CellNumber FROM Chow", conn);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
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
    }
}
