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
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Impilo_App.Views.Reports
{
    /// <summary>
    /// Interaction logic for Format2Report.xaml
    /// </summary>
    public partial class Format2Report : UserControl
    {
        public Format2Report()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            XSSFWorkbook MyWorkbook;
            
            if (radDiabetes.IsChecked == true)
            {
                using (FileStream file = new FileStream(Directory.GetCurrentDirectory() + "\\Templates\\Format2Diabetes.xlsx", FileMode.Open, FileAccess.Read))
                {
                    MyWorkbook = new XSSFWorkbook(file);
                }
            }

            if (radHypertension.IsChecked == true)
            {
                using (FileStream file = new FileStream(Directory.GetCurrentDirectory() + "\\Templates\\Format2Hypertension.xlsx", FileMode.Open, FileAccess.Read))
                {
                    MyWorkbook = new XSSFWorkbook(file);
                }
            }
        }
    }
}
