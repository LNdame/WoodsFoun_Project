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

                #region Top Group 1 - 19
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(2).SetCellValue(Hypertension1());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(3).SetCellValue(Hypertension2());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(4).SetCellValue(Hypertension3());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(5).SetCellValue(Hypertension4());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(6).SetCellValue(Hypertension5());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(7).SetCellValue(Hypertension6());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(8).SetCellValue(Hypertension7());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(8).SetCellValue(Hypertension7a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(9).SetCellValue(Hypertension7a());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(10).SetCellValue(Hypertension8());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(11).SetCellValue(Hypertension9());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(11).SetCellValue(Hypertension9a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(12).SetCellValue(Hypertension9b());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(13).SetCellValue(Hypertension9c());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(14).SetCellValue(Hypertension9d());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(15).SetCellValue(Hypertension10());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(15).SetCellValue(Hypertension10a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(16).SetCellValue(Hypertension10b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(17).SetCellValue(Hypertension11());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(17).SetCellValue(Hypertension11a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(18).SetCellValue(Hypertension11b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(19).SetCellValue(Hypertension12());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(19).SetCellValue(Hypertension12a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(20).SetCellValue(Hypertension12b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(21).SetCellValue(Hypertension13());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(22).SetCellValue(Hypertension14());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(22).SetCellValue(Hypertension14a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(23).SetCellValue(Hypertension14b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(24).SetCellValue(Hypertension15());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(24).SetCellValue(Hypertension15a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(25).SetCellValue(Hypertension15b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(26).SetCellValue(Hypertension16());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(26).SetCellValue(Hypertension16a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(27).SetCellValue(Hypertension16b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(28).SetCellValue(Hypertension17());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(28).SetCellValue(Hypertension17a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(29).SetCellValue(Hypertension17b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(30).SetCellValue(Hypertension18());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(30).SetCellValue(Hypertension18a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(31).SetCellValue(Hypertension18b());
                MyWorkbook.GetSheetAt(0).GetRow(4).GetCell(32).SetCellValue(Hypertension19());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(32).SetCellValue(Hypertension19a());
                MyWorkbook.GetSheetAt(0).GetRow(6).GetCell(33).SetCellValue(Hypertension19b());
                #endregion

                #region Middle Group 20 - 33

                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(2).SetCellValue(Hypertension13());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(6).SetCellValue(Hypertension20());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(7).SetCellValue(Hypertension21());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(8).SetCellValue(Hypertension22());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(8).SetCellValue(Hypertension22a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(9).SetCellValue(Hypertension22b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(10).SetCellValue(Hypertension23());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(11).SetCellValue(Hypertension24());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(11).SetCellValue(Hypertension24a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(12).SetCellValue(Hypertension24b());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(13).SetCellValue(Hypertension24c());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(14).SetCellValue(Hypertension24d());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(15).SetCellValue(Hypertension25());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(15).SetCellValue(Hypertension25a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(16).SetCellValue(Hypertension25b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(17).SetCellValue(Hypertension26());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(17).SetCellValue(Hypertension26a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(18).SetCellValue(Hypertension26b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(19).SetCellValue(Hypertension27());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(19).SetCellValue(Hypertension27a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(20).SetCellValue(Hypertension27b());

                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(22).SetCellValue(Hypertension28());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(22).SetCellValue(Hypertension28a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(23).SetCellValue(Hypertension28b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(24).SetCellValue(Hypertension29());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(24).SetCellValue(Hypertension29a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(25).SetCellValue(Hypertension29b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(26).SetCellValue(Hypertension30());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(26).SetCellValue(Hypertension30a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(27).SetCellValue(Hypertension30b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(28).SetCellValue(Hypertension31());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(28).SetCellValue(Hypertension31a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(29).SetCellValue(Hypertension31b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(30).SetCellValue(Hypertension32());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(30).SetCellValue(Hypertension32a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(31).SetCellValue(Hypertension32b());
                MyWorkbook.GetSheetAt(0).GetRow(11).GetCell(32).SetCellValue(Hypertension33());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(32).SetCellValue(Hypertension33a());
                MyWorkbook.GetSheetAt(0).GetRow(13).GetCell(33).SetCellValue(Hypertension33b());
                #endregion

                #region Bottom Group 34 - 48

                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(2).SetCellValue(Hypertension34());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(3).SetCellValue(Hypertension35());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(6).SetCellValue(Hypertension36());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(10).SetCellValue(Hypertension37());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(11).SetCellValue(Hypertension38());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(11).SetCellValue(Hypertension38a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(12).SetCellValue(Hypertension38b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(15).SetCellValue(Hypertension39());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(15).SetCellValue(Hypertension39a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(16).SetCellValue(Hypertension39b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(17).SetCellValue(Hypertension40());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(17).SetCellValue(Hypertension40a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(18).SetCellValue(Hypertension40b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(19).SetCellValue(Hypertension41());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(19).SetCellValue(Hypertension41a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(20).SetCellValue(Hypertension41b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(21).SetCellValue(Hypertension42());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(22).SetCellValue(Hypertension43());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(22).SetCellValue(Hypertension43a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(23).SetCellValue(Hypertension43b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(24).SetCellValue(Hypertension44());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(24).SetCellValue(Hypertension44a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(25).SetCellValue(Hypertension44b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(26).SetCellValue(Hypertension45());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(26).SetCellValue(Hypertension45a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(27).SetCellValue(Hypertension45b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(28).SetCellValue(Hypertension46());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(28).SetCellValue(Hypertension46a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(29).SetCellValue(Hypertension46b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(30).SetCellValue(Hypertension47());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(30).SetCellValue(Hypertension47a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(31).SetCellValue(Hypertension47b());
                MyWorkbook.GetSheetAt(0).GetRow(17).GetCell(32).SetCellValue(Hypertension48());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(32).SetCellValue(Hypertension48a());
                MyWorkbook.GetSheetAt(0).GetRow(19).GetCell(33).SetCellValue(Hypertension48b());

                #endregion

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
            return Diabetes2Q.Query();
        }

        public string Diabetes3()
        {
            return Diabetes3Q.Query();
        }

        public string Diabetes4()
        {
            return Diabetes4Q.Query();
        }

        public string Diabetes5()
        {
            return Diabetes5Q.Query();
        }

        public string Diabetes6()
        {
            return Diabetes6Q.Query();
        }

        public string Diabetes7()
        {
            return Diabetes7Q.Query();
        }

        public string Diabetes7a()
        {
            return Diabetes7aQ.Query();
        }

        public string Diabetes7b()
        {
            return Diabetes7bQ.Query();
        }

        public string Diabetes8()
        {
            return Diabetes8Q.Query();
        }
        public string Diabetes9()
        {
            return Hyper1Q.Query();
        }
        public string Diabetes9a()
        {
            return Diabetes9aQ.Query();
        }
        public string Diabetes9b()
        {
            return Diabetes9bQ.Query();
        }
        public string Diabetes9c()
        {
            return Diabetes9cQ.Query();
        }
        public string Diabetes9d()
        {
            return Diabetes9dQ.Query();
        }

        public string Diabetes10()
        {
            return Diabetes10Q.Query();
        }
        public string Diabetes10a()
        {
            return Diabetes10aQ.Query();
        }
        public string Diabetes10b()
        {
            return Diabetes10bQ.Query();
        }

        public string Diabetes11()
        {
            return Diabetes11Q.Query();
        }
        public string Diabetes11a()
        {
            return Diabetes11aQ.Query();
        }
        public string Diabetes11b()
        {
            return Diabetes11bQ.Query();
        }

        public string Diabetes12()
        {
            return Diabetes12Q.Query();
        }
        public string Diabetes12a()
        {
            return Diabetes12aQ.Query();
        }
        public string Diabetes12b()
        {
            return Diabetes12bQ.Query();
        }

        public string Diabetes13()
        {
            return Diabetes13Q.Query();
        }

        public string Diabetes14()
        {
            return Diabetes14Q.Query();
        }

        public string Diabetes14a()
        {
            return Diabetes14aQ.Query();
        }
        public string Diabetes14b()
        {
            return Diabetes14bQ.Query();
        }

        public string Diabetes15()
        {
            return Diabetes15Q.Query();
        }
        public string Diabetes15a()
        {
            return Diabetes15aQ.Query();
        }
        public string Diabetes15b()
        {
            return Diabetes15bQ.Query();
        }

        public string Diabetes16()
        {
            return Diabetes16Q.Query();
        }
        public string Diabetes16a()
        {
            return Diabetes16aQ.Query();
        }
        public string Diabetes16b()
        {
            return Diabetes16bQ.Query();
        }

        public string Diabetes17()
        {
            return Diabetes17Q.Query();
        }
        public string Diabetes17a()
        {
            return Diabetes17aQ.Query();
        }
        public string Diabetes17b()
        {
            return Diabetes17bQ.Query();
        }

        public string Diabetes18()
        {
            return Diabetes18Q.Query();
        }
        public string Diabetes18a()
        {
            return Diabetes18aQ.Query();
        }
        public string Diabetes18b()
        {
            return Diabetes18bQ.Query();
        }

        public string Diabetes19()
        {
            return Diabetes19Q.Query();
        }
        public string Diabetes19a()
        {
            return Diabetes19aQ.Query();
        }
        public string Diabetes19b()
        {
            return Diabetes19bQ.Query();
        }

        public string Diabetes20()
        {
            return Diabetes20Q.Query();
        }

        public string Diabetes21()
        {
            return Diabetes21Q.Query();
        }

        public string Diabetes22()
        {
            return Diabetes22Q.Query();
        }

        public string Diabetes22a()
        {
            return Diabetes22aQ.Query();
        }
        public string Diabetes22b()
        {
            return Diabetes22bQ.Query();
        }

        public string Diabetes23()
        {
            return Diabetes23Q.Query();
        }

        public string Diabetes24()
        {
            return Diabetes24Q.Query();
        }

        public string Diabetes24a()
        {
            return Diabetes24aQ.Query();
        }
        public string Diabetes24b()
        {
            return Diabetes24bQ.Query();
        }
        public string Diabetes24c()
        {
            return Diabetes24cQ.Query();
        }
        public string Diabetes24d()
        {
            return Diabetes24dQ.Query();
        }

        public string Diabetes25()
        {
            return Diabetes25Q.Query();
        }

        public string Diabetes25a()
        {
            return Diabetes25aQ.Query();
        }

        public string Diabetes25b()
        {
            return Diabetes25bQ.Query();
        }

        public string Diabetes26()
        {
            return Diabetes26Q.Query();
        }

        public string Diabetes26a()
        {
            return Diabetes26aQ.Query();
        }

        public string Diabetes26b()
        {
            return Diabetes26bQ.Query();
        }

        public string Diabetes27()
        {
            return Diabetes27Q.Query();
        }

        public string Diabetes27a()
        {
            return Diabetes27aQ.Query();
        }

        public string Diabetes27b()
        {
            return Diabetes27bQ.Query();
        }

        public string Diabetes28()
        {
            return Diabetes28Q.Query();
        }

        public string Diabetes28a()
        {
            return Diabetes28aQ.Query();
        }

        public string Diabetes28b()
        {
            return Diabetes28bQ.Query();
        }

        public string Diabetes29()
        {
            return Diabetes29Q.Query();
        }

        public string Diabetes29a()
        {
            return Diabetes29aQ.Query();
        }

        public string Diabetes29b()
        {
            return Diabetes29bQ.Query();
        }

        public string Diabetes30()
        {
            return Diabetes30Q.Query();
        }

        public string Diabetes30a()
        {
            return Diabetes30aQ.Query();
        }

        public string Diabetes30b()
        {
            return Diabetes30bQ.Query();
        }

        public string Diabetes31()
        {
            return Diabetes31Q.Query();
        }

        public string Diabetes31a()
        {
            return Diabetes31aQ.Query();
        }

        public string Diabetes31b()
        {
            return Diabetes31bQ.Query();
        }

        public string Diabetes32()
        {
            return Diabetes32Q.Query();
        }

        public string Diabetes32a()
        {
            return Diabetes32aQ.Query();
        }

        public string Diabetes32b()
        {
            return Diabetes32bQ.Query();
        }

        public string Diabetes33()
        {
            return Diabetes33Q.Query();
        }

        public string Diabetes33a()
        {
            return Diabetes33aQ.Query();
        }

        public string Diabetes33b()
        {
            return Diabetes33bQ.Query();
        }

        public string Diabetes34()
        {
            return Diabetes34Q.Query();
        }

        public string Diabetes35()
        {
            return Diabetes35Q.Query();
        }
        public string Diabetes36()
        {
            return Diabetes36Q.Query();
        }
        public string Diabetes37()
        {
            return Diabetes37Q.Query();
        }

        public string Diabetes38()
        {
            return Diabetes38Q.Query();
        }

        public string Diabetes38a()
        {
            return Diabetes38aQ.Query();
        }

        public string Diabetes38b()
        {
            return Diabetes38bQ.Query();
        }
        public string Diabetes39()
        {
            return Diabetes39Q.Query();
        }

        public string Diabetes39a()
        {
            return Diabetes39aQ.Query();
        }

        public string Diabetes39b()
        {
            return Diabetes39bQ.Query();
        }
        public string Diabetes40()
        {
            return Diabetes40Q.Query();
        }
        public string Diabetes40a()
        {
            return Diabetes40aQ.Query();
        }

        public string Diabetes40b()
        {
            return Diabetes40bQ.Query();
        }

        public string Diabetes41()
        {
            return Diabetes41Q.Query();
        }

        public string Diabetes41a()
        {
            return Diabetes41aQ.Query();
        }

        public string Diabetes41b()
        {
            return Diabetes41bQ.Query();
        }

        public string Diabetes42()
        {
            return Diabetes42Q.Query();
        }
        public string Diabetes43()
        {
            return Diabetes43Q.Query();
        }

        public string Diabetes43a()
        {
            return Diabetes43aQ.Query();
        }

        public string Diabetes43b()
        {
            return Diabetes43bQ.Query();
        }

        public string Diabetes44()
        {
            return Diabetes44Q.Query();
        }

        public string Diabetes44a()
        {
            return Diabetes44aQ.Query();
        }

        public string Diabetes44b()
        {
            return Diabetes44bQ.Query();
        }
        public string Diabetes45()
        {
            return Diabetes45Q.Query();
        }
        public string Diabetes45a()
        {
            return Diabetes45aQ.Query();
        }

        public string Diabetes45b()
        {
            return Diabetes45bQ.Query();
        }
        public string Diabetes46()
        {
            return Diabetes46Q.Query();
        }

        public string Diabetes46a()
        {
            return Diabetes46aQ.Query();
        }

        public string Diabetes46b()
        {
            return Diabetes46bQ.Query();
        }
        public string Diabetes47()
        {
            return Diabetes47Q.Query();
        }

        public string Diabetes47a()
        {
            return Diabetes47aQ.Query();
        }

        public string Diabetes47b()
        {
            return Diabetes47bQ.Query();
        }
        public string Diabetes48()
        {
            return Diabetes48Q.Query();
        }
        public string Diabetes48a()
        {
            return Diabetes48aQ.Query();
        }

        public string Diabetes48b()
        {
            return Diabetes48bQ.Query();
        }

        #endregion

        #region Hypertension
        public string Hypertension1()
        {
            return Hypertension1Q.Query();
        }
        public string Hypertension2()
        {
            return Hypertension2Q.Query();
        }

        public string Hypertension3()
        {
            return Hypertension3Q.Query();
        }

        public string Hypertension4()
        {
            return Hypertension4Q.Query();
        }

        public string Hypertension5()
        {
            return Hypertension5Q.Query();
        }

        public string Hypertension6()
        {
            return Hypertension6Q.Query();
        }

        public string Hypertension7()
        {
            return Hypertension7Q.Query();
        }

        public string Hypertension7a()
        {
            return Hypertension7aQ.Query();
        }

        public string Hypertension7b()
        {
            return Hypertension7bQ.Query();
        }

        public string Hypertension8()
        {
            return Hypertension8Q.Query();
        }
        public string Hypertension9()
        {
            return Hypertension9Q.Query();
        }
        public string Hypertension9a()
        {
            return Hypertension9aQ.Query();
        }
        public string Hypertension9b()
        {
            return Hypertension9bQ.Query();
        }
        public string Hypertension9c()
        {
            return Hypertension9cQ.Query();
        }
        public string Hypertension9d()
        {
            return Hypertension9dQ.Query();
        }

        public string Hypertension10()
        {
            return Hypertension10Q.Query();
        }
        public string Hypertension10a()
        {
            return Hypertension10aQ.Query();
        }
        public string Hypertension10b()
        {
            return Hypertension10bQ.Query();
        }

        public string Hypertension11()
        {
            return Hypertension11Q.Query();
        }
        public string Hypertension11a()
        {
            return Hypertension11aQ.Query();
        }
        public string Hypertension11b()
        {
            return Hypertension11bQ.Query();
        }

        public string Hypertension12()
        {
            return Hypertension12Q.Query();
        }
        public string Hypertension12a()
        {
            return Hypertension12aQ.Query();
        }
        public string Hypertension12b()
        {
            return Hypertension12bQ.Query();
        }

        public string Hypertension13()
        {
            return Hypertension13Q.Query();
        }

        public string Hypertension14()
        {
            return Hypertension14Q.Query();
        }

        public string Hypertension14a()
        {
            return Hypertension14aQ.Query();
        }
        public string Hypertension14b()
        {
            return Hypertension14bQ.Query();
        }

        public string Hypertension15()
        {
            return Hypertension15Q.Query();
        }
        public string Hypertension15a()
        {
            return Hypertension15aQ.Query();
        }
        public string Hypertension15b()
        {
            return Hypertension15bQ.Query();
        }

        public string Hypertension16()
        {
            return Hypertension16Q.Query();
        }
        public string Hypertension16a()
        {
            return Hypertension16aQ.Query();
        }
        public string Hypertension16b()
        {
            return Hypertension16bQ.Query();
        }

        public string Hypertension17()
        {
            return Hypertension17Q.Query();
        }
        public string Hypertension17a()
        {
            return Hypertension17aQ.Query();
        }
        public string Hypertension17b()
        {
            return Hypertension17bQ.Query();
        }

        public string Hypertension18()
        {
            return Hypertension18Q.Query();
        }
        public string Hypertension18a()
        {
            return Hypertension18aQ.Query();
        }
        public string Hypertension18b()
        {
            return Hypertension18bQ.Query();
        }

        public string Hypertension19()
        {
            return Hypertension19Q.Query();
        }
        public string Hypertension19a()
        {
            return Hypertension19aQ.Query();
        }
        public string Hypertension19b()
        {
            return Hypertension19bQ.Query();
        }

        public string Hypertension20()
        {
            return Hypertension20Q.Query();
        }

        public string Hypertension21()
        {
            return Hypertension21Q.Query();
        }

        public string Hypertension22()
        {
            return Hypertension22Q.Query();
        }

        public string Hypertension22a()
        {
            return Hypertension22aQ.Query();
        }
        public string Hypertension22b()
        {
            return Hypertension22bQ.Query();
        }

        public string Hypertension23()
        {
            return Hypertension23Q.Query();
        }

        public string Hypertension24()
        {
            return Hypertension24Q.Query();
        }

        public string Hypertension24a()
        {
            return Hypertension24aQ.Query();
        }
        public string Hypertension24b()
        {
            return Hypertension24bQ.Query();
        }
        public string Hypertension24c()
        {
            return Hypertension24cQ.Query();
        }
        public string Hypertension24d()
        {
            return Hypertension24dQ.Query();
        }

        public string Hypertension25()
        {
            return Hypertension25Q.Query();
        }

        public string Hypertension25a()
        {
            return Hypertension25aQ.Query();
        }

        public string Hypertension25b()
        {
            return Hypertension25bQ.Query();
        }

        public string Hypertension26()
        {
            return Hypertension26Q.Query();
        }

        public string Hypertension26a()
        {
            return Hypertension26aQ.Query();
        }

        public string Hypertension26b()
        {
            return Hypertension26bQ.Query();
        }

        public string Hypertension27()
        {
            return Hypertension27Q.Query();
        }

        public string Hypertension27a()
        {
            return Hypertension27aQ.Query();
        }

        public string Hypertension27b()
        {
            return Hypertension27bQ.Query();
        }

        public string Hypertension28()
        {
            return Hypertension28Q.Query();
        }

        public string Hypertension28a()
        {
            return Hypertension28aQ.Query();
        }

        public string Hypertension28b()
        {
            return Hypertension28bQ.Query();
        }

        public string Hypertension29()
        {
            return Hypertension29Q.Query();
        }

        public string Hypertension29a()
        {
            return Hypertension29aQ.Query();
        }

        public string Hypertension29b()
        {
            return Hypertension29bQ.Query();
        }

        public string Hypertension30()
        {
            return Hypertension30Q.Query();
        }

        public string Hypertension30a()
        {
            return Hypertension30aQ.Query();
        }

        public string Hypertension30b()
        {
            return Hypertension30bQ.Query();
        }

        public string Hypertension31()
        {
            return Hypertension31Q.Query();
        }

        public string Hypertension31a()
        {
            return Hypertension31aQ.Query();
        }

        public string Hypertension31b()
        {
            return Hypertension31bQ.Query();
        }

        public string Hypertension32()
        {
            return Hypertension32Q.Query();
        }

        public string Hypertension32a()
        {
            return Hypertension32aQ.Query();
        }

        public string Hypertension32b()
        {
            return Hypertension32bQ.Query();
        }

        public string Hypertension33()
        {
            return Hypertension33Q.Query();
        }

        public string Hypertension33a()
        {
            return Hypertension33aQ.Query();
        }

        public string Hypertension33b()
        {
            return Hypertension33bQ.Query();
        }

        public string Hypertension34()
        {
            return Hypertension34Q.Query();
        }

        public string Hypertension35()
        {
            return Hypertension35Q.Query();
        }
        public string Hypertension36()
        {
            return Hypertension36Q.Query();
        }
        public string Hypertension37()
        {
            return Hypertension37Q.Query();
        }

        public string Hypertension38()
        {
            return Hypertension38Q.Query();
        }

        public string Hypertension38a()
        {
            return Hypertension38aQ.Query();
        }

        public string Hypertension38b()
        {
            return Hypertension38bQ.Query();
        }
        public string Hypertension39()
        {
            return Hypertension39Q.Query();
        }

        public string Hypertension39a()
        {
            return Hypertension39aQ.Query();
        }

        public string Hypertension39b()
        {
            return Hypertension39bQ.Query();
        }
        public string Hypertension40()
        {
            return Hypertension40Q.Query();
        }
        public string Hypertension40a()
        {
            return Hypertension40aQ.Query();
        }

        public string Hypertension40b()
        {
            return Hypertension40bQ.Query();
        }

        public string Hypertension41()
        {
            return Hypertension41Q.Query();
        }

        public string Hypertension41a()
        {
            return Hypertension41aQ.Query();
        }

        public string Hypertension41b()
        {
            return Hypertension41bQ.Query();
        }

        public string Hypertension42()
        {
            return Hypertension42Q.Query();
        }
        public string Hypertension43()
        {
            return Hypertension43Q.Query();
        }

        public string Hypertension43a()
        {
            return Hypertension43aQ.Query();
        }

        public string Hypertension43b()
        {
            return Hypertension43bQ.Query();
        }

        public string Hypertension44()
        {
            return Hypertension44Q.Query();
        }

        public string Hypertension44a()
        {
            return Hypertension44aQ.Query();
        }

        public string Hypertension44b()
        {
            return Hypertension44bQ.Query();
        }
        public string Hypertension45()
        {
            return Hypertension45Q.Query();
        }
        public string Hypertension45a()
        {
            return Hypertension45aQ.Query();
        }

        public string Hypertension45b()
        {
            return Hypertension45bQ.Query();
        }
        public string Hypertension46()
        {
            return Hypertension46Q.Query();
        }

        public string Hypertension46a()
        {
            return Hypertension46aQ.Query();
        }

        public string Hypertension46b()
        {
            return Hypertension46bQ.Query();
        }
        public string Hypertension47()
        {
            return Hypertension47Q.Query();
        }

        public string Hypertension47a()
        {
            return Hypertension47aQ.Query();
        }

        public string Hypertension47b()
        {
            return Hypertension47bQ.Query();
        }
        public string Hypertension48()
        {
            return Hypertension48Q.Query();
        }
        public string Hypertension48a()
        {
            return Hypertension48aQ.Query();
        }

        public string Hypertension48b()
        {
            return Hypertension48bQ.Query();
        }

        #endregion
    }
}
