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

                #region Top Group 1 - 19
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
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(21).SetCellValue(Diabetes13());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(22).SetCellValue(Diabetes14());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(22).SetCellValue(Diabetes14a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(23).SetCellValue(Diabetes14b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(24).SetCellValue(Diabetes15());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(24).SetCellValue(Diabetes15a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(25).SetCellValue(Diabetes15b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(26).SetCellValue(Diabetes16());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(26).SetCellValue(Diabetes16a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(27).SetCellValue(Diabetes16b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(28).SetCellValue(Diabetes17());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(28).SetCellValue(Diabetes17a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(29).SetCellValue(Diabetes17b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(30).SetCellValue(Diabetes18());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(30).SetCellValue(Diabetes18a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(31).SetCellValue(Diabetes18b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(32).SetCellValue(Diabetes19());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(32).SetCellValue(Diabetes19a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(33).SetCellValue(Diabetes19b());
                #endregion

                #region Middle Group 20 - 33

                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(2).SetCellValue(Diabetes13());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(6).SetCellValue(Diabetes20());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(7).SetCellValue(Diabetes21());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(8).SetCellValue(Diabetes22());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(8).SetCellValue(Diabetes22a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(9).SetCellValue(Diabetes22b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(10).SetCellValue(Diabetes23());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(11).SetCellValue(Diabetes24());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(11).SetCellValue(Diabetes24a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(12).SetCellValue(Diabetes24b());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(13).SetCellValue(Diabetes24c());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(14).SetCellValue(Diabetes24d());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(15).SetCellValue(Diabetes25());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(15).SetCellValue(Diabetes25a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(16).SetCellValue(Diabetes25b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(17).SetCellValue(Diabetes26());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(17).SetCellValue(Diabetes26a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(18).SetCellValue(Diabetes26b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(19).SetCellValue(Diabetes27());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(19).SetCellValue(Diabetes27a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(20).SetCellValue(Diabetes27b());

                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(22).SetCellValue(Diabetes28());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(22).SetCellValue(Diabetes28a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(23).SetCellValue(Diabetes28b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(24).SetCellValue(Diabetes29());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(24).SetCellValue(Diabetes29a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(25).SetCellValue(Diabetes29b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(26).SetCellValue(Diabetes30());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(26).SetCellValue(Diabetes30a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(27).SetCellValue(Diabetes30b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(28).SetCellValue(Diabetes31());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(28).SetCellValue(Diabetes31a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(29).SetCellValue(Diabetes31b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(30).SetCellValue(Diabetes32());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(30).SetCellValue(Diabetes32a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(31).SetCellValue(Diabetes32b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(32).SetCellValue(Diabetes33());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(32).SetCellValue(Diabetes33a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(33).SetCellValue(Diabetes33b());
                #endregion

                #region Bottom Group 34 - 48

                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(2).SetCellValue(Diabetes34());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(3).SetCellValue(Diabetes35());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(6).SetCellValue(Diabetes36());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(10).SetCellValue(Diabetes37());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(11).SetCellValue(Diabetes38());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(11).SetCellValue(Diabetes38a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(12).SetCellValue(Diabetes38b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(15).SetCellValue(Diabetes39());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(15).SetCellValue(Diabetes39a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(16).SetCellValue(Diabetes39b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(17).SetCellValue(Diabetes40());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(17).SetCellValue(Diabetes40a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(18).SetCellValue(Diabetes40b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(19).SetCellValue(Diabetes41());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(19).SetCellValue(Diabetes41a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(20).SetCellValue(Diabetes41b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(21).SetCellValue(Diabetes42());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(22).SetCellValue(Diabetes43());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(22).SetCellValue(Diabetes43a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(23).SetCellValue(Diabetes43b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(24).SetCellValue(Diabetes44());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(24).SetCellValue(Diabetes44a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(25).SetCellValue(Diabetes44b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(26).SetCellValue(Diabetes45());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(26).SetCellValue(Diabetes45a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(27).SetCellValue(Diabetes45b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(28).SetCellValue(Diabetes46());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(28).SetCellValue(Diabetes46a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(29).SetCellValue(Diabetes46b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(30).SetCellValue(Diabetes47());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(30).SetCellValue(Diabetes47a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(31).SetCellValue(Diabetes47b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(32).SetCellValue(Diabetes48());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(32).SetCellValue(Diabetes48a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(33).SetCellValue(Diabetes48b());

                #endregion
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

        #region Diabetes
        public string Diabetes1()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes2()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes3()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes4()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes5()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes6()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes7()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes7a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes7b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes8()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes9()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes9a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes9b()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes9c()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes9d()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes10()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes10a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes10b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes11()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes11a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes11b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes12()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes12a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes12b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes13()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes14()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes14a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes14b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes15()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes15a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes15b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes16()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes16a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes16b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes17()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes17a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes17b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes18()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes18a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes18b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes19()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes19a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes19b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes20()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes21()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes22()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes22a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes22b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes23()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes24()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes24a()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes24b()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes24c()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes24d()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes25()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes25a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes25b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes26()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes26a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes26b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes27()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes27a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes27b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes28()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes28a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes28b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes29()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes29a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes29b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes30()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes30a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes30b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes31()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes31a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes31b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes32()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes32a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes32b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes33()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes33a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes33b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes34()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes35()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes36()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes37()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes38()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes38a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes38b()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes39()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes39a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes39b()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes40()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes40a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes40b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes41()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes41a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes41b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes42()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes43()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes43a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes43b()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes44()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes44a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes44b()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes45()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes45a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes45b()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes46()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes46a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes46b()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes47()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes47a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes47b()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes48()
        {
            return Diabetes1Q.Query();
        }
        public string Diabetes48a()
        {
            return Diabetes1Q.Query();
        }

        public string Diabetes48b()
        {
            return Diabetes1Q.Query();
        }

        #endregion
    }
}
