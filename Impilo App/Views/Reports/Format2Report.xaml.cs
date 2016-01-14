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
using Impilo_App.ReportQueries.Format_2.Diabetes;


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
            XSSFWorkbook MyWorkbook = null;
           
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx";
            
            if (radDiabetes.IsChecked == true)
            {
                using (FileStream file = new FileStream(Directory.GetCurrentDirectory() + "\\Templates\\Format2Diabetes.xlsx", FileMode.Open, FileAccess.Read))
                {
                    MyWorkbook = new XSSFWorkbook(file);
                }

                dlg.FileName = "Format2Diabetes - " + DateTime.Now.ToShortDateString();

                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(2).SetCellValue(Diabetes1());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(3).SetCellValue(Diabetes2());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(4).SetCellValue(Diabetes3());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(5).SetCellValue(Diabetes4());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(6).SetCellValue(Diabetes5());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(7).SetCellValue(Diabetes6());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(8).SetCellValue(Diabetes7());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(8).SetCellValue(Diabetes7a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(9).SetCellValue(Diabetes7a());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(10).SetCellValue(Diabetes8());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(11).SetCellValue(Diabetes9());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(11).SetCellValue(Diabetes9a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(12).SetCellValue(Diabetes9b());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(13).SetCellValue(Diabetes9c());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(14).SetCellValue(Diabetes9d());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(15).SetCellValue(Diabetes10());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(15).SetCellValue(Diabetes10a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(16).SetCellValue(Diabetes10b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(17).SetCellValue(Diabetes11());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(17).SetCellValue(Diabetes11a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(18).SetCellValue(Diabetes11b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(19).SetCellValue(Diabetes12());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(19).SetCellValue(Diabetes12a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(20).SetCellValue(Diabetes12b());
            }

            if (radHypertension.IsChecked == true)
            {
                using (FileStream file = new FileStream(Directory.GetCurrentDirectory() + "\\Templates\\Format2Hypertension.xlsx", FileMode.Open, FileAccess.Read))
                {
                    MyWorkbook = new XSSFWorkbook(file);
                }

                dlg.FileName = "Format2Hypertension - " + DateTime.Now.ToShortDateString();
            }  

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FileStream xfile = new FileStream(dlg.FileName, FileMode.Create, System.IO.FileAccess.Write);
                MyWorkbook.Write(xfile);
                xfile.Close();
            }

            MessageBox.Show("The report has been generated successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);           
        }

        public string Diabetes1()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes2()
        {
            return "x";
        }

        public string Diabetes3()
        {
            return "x";
        }

        public string Diabetes4()
        {
            return "x";
        }

        public string Diabetes5()
        {
            return "x";
        }

        public string Diabetes6()
        {
            return "x";
        }

        public string Diabetes7()
        {
            return "x";
        }

        public string Diabetes7a()
        {
            return "x";
        }

        public string Diabetes7b()
        {
            return "x";
        }

        public string Diabetes8()
        {
            return "x";
        }
        public string Diabetes9()
        {
            return "x";
        }
        public string Diabetes9a()
        {
            return "x";
        }
        public string Diabetes9b()
        {
            return "x";
        }
        public string Diabetes9c()
        {
            return "x";
        }
        public string Diabetes9d()
        {
            return "x";
        }

        public string Diabetes10()
        {
            return "x";
        }
        public string Diabetes10a()
        {
            return "x";
        }
        public string Diabetes10b()
        {
            return "x";
        }

        public string Diabetes11()
        {
            return "x";
        }
        public string Diabetes11a()
        {
            return "x";
        }
        public string Diabetes11b()
        {
            return "x";
        }

        public string Diabetes12()
        {
            return "x";
        }
        public string Diabetes12a()
        {
            return "x";
        }
        public string Diabetes12b()
        {
            return "x";
        }
    }
}
