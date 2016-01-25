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
using System.Reflection;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using Impilo_App.ReportQueries.Format_2.Diabetes;
using Impilo_App.ReportQueries.Format_2.Hypertension;


namespace Impilo_App.Views.Reports
{
    /// <summary>
    /// Interaction logic for Format2Report.xaml
    /// </summary>
    public partial class Format2Report : UserControl
    {
        struct IndicatorSpot
        {
            public IndicatorSpot(int DiabetesRow, int DiabetesCell, int HypertensionRow, int HypertensionCell)
            {
                this.DiabetesRow = DiabetesRow;
                this.DiabetesCell = DiabetesCell;
                this.HypertensionRow = HypertensionRow;
                this.HypertensionCell = HypertensionCell;
            }

            public int DiabetesRow;
            public int DiabetesCell;
            public int HypertensionRow;
            public int HypertensionCell;
        }

        public static DateTime StartDate = new DateTime(2015, 1, 1);
        public static DateTime EndDate = DateTime.Now;

        List<IndicatorSpot> Spots = new List<IndicatorSpot>();
        public string [] IndicatorList = {"1","2","3","4","5","6","7","8","9","10","10a","10b","10c","10d","11","11a","11b","12","12a","12b","13","13a","13b","14","15","16","17","18","19","19a","19b","20","20a","20b","21","21a","21b","22","22a","22b","23","24","25","26","27","28","29","30","31","32","32a","32b","32c","32d","33","33a","33b","34","34a","34b","35","35a","35b","36","37","38","39","40","41","42","43","44","45","45a","45b","45c","45d","46","46a","46b","47","47a","47b","48","48a","48b","49","50","50a","50b","50c","51","52","10di","32di","19bi","51a","52a"};
        public Format2Report()
        {
            InitializeComponent();
            InitializeSpots();

            dpStartDate.DisplayDate = StartDate;
            dpEndDate.DisplayDate = EndDate;
            dpStartDate.SelectedDate = StartDate;
            dpEndDate.SelectedDate = EndDate;
        }

        public void InitializeSpots()
        {
            Spots.Add(new IndicatorSpot(10,1,11,1)); //1
            Spots.Add(new IndicatorSpot(10, 3, 11, 3)); //2
            Spots.Add(new IndicatorSpot(10, 4, 11, 4)); //3
            Spots.Add(new IndicatorSpot(10, 5, 11, 5)); //4
            Spots.Add(new IndicatorSpot(10, 6, 11, 6)); //5
            Spots.Add(new IndicatorSpot(10, 7, 11, 7)); //6
            Spots.Add(new IndicatorSpot(10, 8, 11, 8)); //7
            Spots.Add(new IndicatorSpot(10, 9, 11, 9)); //8
            Spots.Add(new IndicatorSpot(10, 11, 11, 11)); //9
            Spots.Add(new IndicatorSpot(10, 12, 11, 12)); //10
            Spots.Add(new IndicatorSpot(13, 12, 14, 12)); //10a
            Spots.Add(new IndicatorSpot(13, 13, 14, 13)); //10b
            Spots.Add(new IndicatorSpot(13, 14, 14, 14)); //10c
            Spots.Add(new IndicatorSpot(13, 15, 14, 15)); //10d
            Spots.Add(new IndicatorSpot(10, 16, 11, 16)); //11
            Spots.Add(new IndicatorSpot(13, 16, 14, 16)); //11a
            Spots.Add(new IndicatorSpot(13, 17, 14, 17)); //11b
            Spots.Add(new IndicatorSpot(10, 18, 11, 18)); //12
            Spots.Add(new IndicatorSpot(13, 18, 14, 18)); //12a
            Spots.Add(new IndicatorSpot(13, 19, 14, 19)); //12b
            Spots.Add(new IndicatorSpot(10, 20, 11, 20)); //13
            Spots.Add(new IndicatorSpot(13, 20, 14, 20)); //13a
            Spots.Add(new IndicatorSpot(13, 21, 14, 21)); //13b
            Spots.Add(new IndicatorSpot(10, 22, 11, 22)); //14
            Spots.Add(new IndicatorSpot(17, 3, 18, 3)); //15
            Spots.Add(new IndicatorSpot(17, 4, 18, 4)); //16
            Spots.Add(new IndicatorSpot(17, 7, 18, 7)); //17
            Spots.Add(new IndicatorSpot(17, 11, 18, 11)); //18
            Spots.Add(new IndicatorSpot(17, 12, 18, 12)); //19
            Spots.Add(new IndicatorSpot(20, 12, 21, 12)); //19a
            Spots.Add(new IndicatorSpot(20, 13, 21, 13)); //19b
            Spots.Add(new IndicatorSpot(17, 16, 18, 16)); //20
            Spots.Add(new IndicatorSpot(20, 16, 21, 16)); //20a
            Spots.Add(new IndicatorSpot(20, 17, 21, 17)); //20b
            Spots.Add(new IndicatorSpot(17, 18, 18, 18)); //21
            Spots.Add(new IndicatorSpot(20, 18, 21, 18)); //21a
            Spots.Add(new IndicatorSpot(20, 19, 21, 19)); //21b
            Spots.Add(new IndicatorSpot(17, 20, 18, 20)); //22
            Spots.Add(new IndicatorSpot(20, 20, 21, 20)); //22a
            Spots.Add(new IndicatorSpot(20, 21, 21, 21)); //22b
            Spots.Add(new IndicatorSpot(17, 22, 18, 22)); //23
            Spots.Add(new IndicatorSpot(24, 3, 25, 3)); //24
            Spots.Add(new IndicatorSpot(24, 4, 25, 4)); //25
            Spots.Add(new IndicatorSpot(24, 5, 25, 5)); //26
            Spots.Add(new IndicatorSpot(24, 6, 25, 6)); //27
            Spots.Add(new IndicatorSpot(24, 7, 25, 7)); //28
            Spots.Add(new IndicatorSpot(24, 8, 25, 8)); //29
            Spots.Add(new IndicatorSpot(24, 9, 25, 9)); //30
            Spots.Add(new IndicatorSpot(24, 11, 25, 11)); //31
            Spots.Add(new IndicatorSpot(24, 12, 25, 12)); //32
            Spots.Add(new IndicatorSpot(27, 12, 28, 12)); //32a
            Spots.Add(new IndicatorSpot(27, 13, 28, 13)); //32b
            Spots.Add(new IndicatorSpot(27, 14, 28, 14)); //32c
            Spots.Add(new IndicatorSpot(27, 15, 28, 15)); //32d
            Spots.Add(new IndicatorSpot(24, 16, 25, 16)); //33
            Spots.Add(new IndicatorSpot(27, 16, 28, 16)); //33a
            Spots.Add(new IndicatorSpot(27, 17, 28, 17)); //33b
            Spots.Add(new IndicatorSpot(24, 18, 25, 18)); //34
            Spots.Add(new IndicatorSpot(27, 18, 28, 18)); //34a
            Spots.Add(new IndicatorSpot(27, 19, 28, 19)); //34b
            Spots.Add(new IndicatorSpot(24, 20, 25, 20)); //35
            Spots.Add(new IndicatorSpot(27, 20, 28, 20)); //35a
            Spots.Add(new IndicatorSpot(27, 21, 28, 21)); //35b
            Spots.Add(new IndicatorSpot(24, 22, 25, 22)); //36
            Spots.Add(new IndicatorSpot(31, 3, 32, 3)); //37
            Spots.Add(new IndicatorSpot(31, 4, 32, 4)); //38
            Spots.Add(new IndicatorSpot(31, 5, 32, 5)); //39
            Spots.Add(new IndicatorSpot(31, 6, 32, 6)); //40
            Spots.Add(new IndicatorSpot(31, 7, 32, 7)); //41
            Spots.Add(new IndicatorSpot(31, 8, 32, 8)); //42
            Spots.Add(new IndicatorSpot(31, 9, 32, 9)); //43
            Spots.Add(new IndicatorSpot(31, 11, 32, 11)); //44
            Spots.Add(new IndicatorSpot(31, 12, 32, 12)); //45
            Spots.Add(new IndicatorSpot(34, 12, 35, 12)); //45a
            Spots.Add(new IndicatorSpot(34, 13, 35, 13)); //45b
            Spots.Add(new IndicatorSpot(34, 14, 35, 14)); //45c
            Spots.Add(new IndicatorSpot(34, 15, 35, 15)); //45d
            Spots.Add(new IndicatorSpot(31, 16, 32, 16)); //46
            Spots.Add(new IndicatorSpot(34, 16, 35, 16)); //46a
            Spots.Add(new IndicatorSpot(34, 17, 35, 17)); //46b
            Spots.Add(new IndicatorSpot(31, 18, 32, 18)); //47
            Spots.Add(new IndicatorSpot(34, 18, 35, 18)); //47a
            Spots.Add(new IndicatorSpot(34, 19, 35, 19)); //47b
            Spots.Add(new IndicatorSpot(31, 20, 32, 20)); //48
            Spots.Add(new IndicatorSpot(34, 20, 35, 20)); //48a
            Spots.Add(new IndicatorSpot(34, 21, 35, 21)); //48b
            Spots.Add(new IndicatorSpot(31, 22, 32, 22)); //49
            Spots.Add(new IndicatorSpot(39, 3, 40, 3)); //50
            Spots.Add(new IndicatorSpot(39, 4, 40, 4)); //50a
            Spots.Add(new IndicatorSpot(39, 5, 40, 5)); //50b
            Spots.Add(new IndicatorSpot(39, 6, 40, 6)); //50c
            Spots.Add(new IndicatorSpot(39, 7, 40, 7)); //51
            Spots.Add(new IndicatorSpot(39, 8, 40, 8)); //52
            Spots.Add(new IndicatorSpot(42, 4, 43, 4)); //10di
            Spots.Add(new IndicatorSpot(42, 5, 43, 5)); //32di
            Spots.Add(new IndicatorSpot(42, 6, 43, 6)); //19bi
            Spots.Add(new IndicatorSpot(42, 7, 43, 7)); //51a
            Spots.Add(new IndicatorSpot(42, 8, 43, 8)); //52a
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            StartDate = (DateTime)dpStartDate.SelectedDate;
            EndDate = (DateTime)dpEndDate.SelectedDate;

            Cursor = Cursors.Wait;
            XSSFWorkbook MyWorkbook = null;
           
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx";

            using (FileStream file = new FileStream(Directory.GetCurrentDirectory() + "\\Templates\\ReportFormat2.xlsx", FileMode.Open, FileAccess.Read))
            {
                MyWorkbook = new XSSFWorkbook(file);
            }

            dlg.FileName = "Format 2 Report - " + DateTime.Now.ToShortDateString();

            List<string> Report = new List<string>();
            Report.Add("Indicator Number, Diabetes, Hypertension");

            //foreach (string CurrentIndicator in IndicatorList)
            for (int i = 0; i < IndicatorList.Length; i++)
            {
                string CurrentIndicator = IndicatorList[i];
                int DiabetesRow = -1;
                int DiabetesCell = -1;
                int HypertensionRow = -1;
                int HypertensionCell = -1;

                try
                {
                    DiabetesRow = Spots[i].DiabetesRow;
                    DiabetesCell = Spots[i].DiabetesCell;
                    HypertensionRow = Spots[i].HypertensionRow;
                    HypertensionCell = Spots[i].HypertensionCell;
                }
                catch { }

                if (DiabetesRow > -1)
                {
                    string Diabetes = "";
                    string Hypertension = "";

                    Type thisType = this.GetType();
                    MethodInfo theMethod = thisType.GetMethod("CallClassMethod");
                    Diabetes = (string)theMethod.Invoke(this, new object[] { "Diabetes", CurrentIndicator });
                    Hypertension = (string)theMethod.Invoke(this, new object[] { "Hypertension", CurrentIndicator });
                    Report.Add(string.Format("{0},{1},{2}", CurrentIndicator, Diabetes, Hypertension));
                    MyWorkbook.GetSheetAt(0).GetRow(DiabetesRow).GetCell(DiabetesCell).SetCellValue(Diabetes);
                    MyWorkbook.GetSheetAt(0).GetRow(HypertensionRow).GetCell(HypertensionCell).SetCellValue(Hypertension);
                }
            }

            Cursor = Cursors.Arrow;
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                FileStream xfile = new FileStream(dlg.FileName, FileMode.Create, System.IO.FileAccess.Write);
                MyWorkbook.Write(xfile);
                xfile.Close();

                //File.WriteAllLines(dlg.FileName, Report);

                MessageBox.Show("The report has been generated successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);           

                System.Diagnostics.Process.Start(dlg.FileName);
            }
        }

        public string CallClassMethod(string Condition, string Number)
        {
            string ReturnMe = "x";

            try
            {
                Type QueryType = System.Type.GetType("Impilo_App.ReportQueries.Format_2." + Condition + "." + Condition + Number + "Q");
                MethodInfo theMethod = QueryType.GetMethod("Query");
                ReturnMe = (string)theMethod.Invoke(this, null);
            }
            catch { }

            return ReturnMe;
        }

        //#region Diabetes
        //public string Diabetes1()
        //{
        //    return Diabetes1Q.Query();
        //}
        //public string Diabetes2()
        //{
        //    return Diabetes2Q.Query();
        //}

        //public string Diabetes3()
        //{
        //    return Diabetes3Q.Query();
        //}

        //public string Diabetes4()
        //{
        //    return Diabetes4Q.Query();
        //}

        //public string Diabetes5()
        //{
        //    return Diabetes5Q.Query();
        //}

        //public string Diabetes6()
        //{
        //    return Diabetes6Q.Query();
        //}

        //public string Diabetes7()
        //{
        //    return Diabetes7Q.Query();
        //}

        //public string Diabetes7a()
        //{
        //    return Diabetes7aQ.Query();
        //}

        //public string Diabetes7b()
        //{
        //    return Diabetes7bQ.Query();
        //}

        //public string Diabetes8()
        //{
        //    return Diabetes8Q.Query();
        //}
        //public string Diabetes9()
        //{
        //   return Diabetes9aQ.Query(); // Flagged for review  Please LEon Check
        //}
        //public string Diabetes9a()
        //{
        //    return Diabetes9aQ.Query();
        //}
        //public string Diabetes9b()
        //{
        //    return Diabetes9bQ.Query();
        //}
        //public string Diabetes9c()
        //{
        //    return Diabetes9cQ.Query();
        //}
        //public string Diabetes9d()
        //{
        //    return Diabetes9dQ.Query();
        //}

        //public string Diabetes10()
        //{
        //    return Diabetes10Q.Query();
        //}
        //public string Diabetes10a()
        //{
        //    return Diabetes10aQ.Query();
        //}
        //public string Diabetes10b()
        //{
        //    return Diabetes10bQ.Query();
        //}

        //public string Diabetes11()
        //{
        //    return Diabetes11Q.Query();
        //}
        //public string Diabetes11a()
        //{
        //    return Diabetes11aQ.Query();
        //}
        //public string Diabetes11b()
        //{
        //    return Diabetes11bQ.Query();
        //}

        //public string Diabetes12()
        //{
        //    return Diabetes12Q.Query();
        //}
        //public string Diabetes12a()
        //{
        //    return Diabetes12aQ.Query();
        //}
        //public string Diabetes12b()
        //{
        //    return Diabetes12bQ.Query();
        //}

        //public string Diabetes13()
        //{
        //    return Diabetes13Q.Query();
        //}

        //public string Diabetes14()
        //{
        //    return Diabetes14Q.Query();
        //}

        //public string Diabetes14a()
        //{
        //    return Diabetes14aQ.Query();
        //}
        //public string Diabetes14b()
        //{
        //    return Diabetes14bQ.Query();
        //}

        //public string Diabetes15()
        //{
        //    return Diabetes15Q.Query();
        //}
        //public string Diabetes15a()
        //{
        //    return Diabetes15aQ.Query();
        //}
        //public string Diabetes15b()
        //{
        //    return Diabetes15bQ.Query();
        //}

        //public string Diabetes16()
        //{
        //    return Diabetes16Q.Query();
        //}
        //public string Diabetes16a()
        //{
        //    return Diabetes16aQ.Query();
        //}
        //public string Diabetes16b()
        //{
        //    return Diabetes16bQ.Query();
        //}

        //public string Diabetes17()
        //{
        //    return Diabetes17Q.Query();
        //}
        //public string Diabetes17a()
        //{
        //    return Diabetes17aQ.Query();
        //}
        //public string Diabetes17b()
        //{
        //    return Diabetes17bQ.Query();
        //}

        //public string Diabetes18()
        //{
        //    return Diabetes18Q.Query();
        //}
        //public string Diabetes18a()
        //{
        //    return Diabetes18aQ.Query();
        //}
        //public string Diabetes18b()
        //{
        //    return Diabetes18bQ.Query();
        //}

        //public string Diabetes19()
        //{
        //    return Diabetes19Q.Query();
        //}
        //public string Diabetes19a()
        //{
        //    return Diabetes19aQ.Query();
        //}
        //public string Diabetes19b()
        //{
        //    return Diabetes19bQ.Query();
        //}

        //public string Diabetes20()
        //{
        //    return Diabetes20Q.Query();
        //}

        //public string Diabetes21()
        //{
        //    return Diabetes21Q.Query();
        //}

        //public string Diabetes22()
        //{
        //    return Diabetes22Q.Query();
        //}

        //public string Diabetes22a()
        //{
        //    return Impilo_App.ReportQueries.Format_2.Diabetes.Diabete22aQ.Query();
        //}
        //public string Diabetes22b()
        //{
        //    return Diabetes22bQ.Query();
        //}

        //public string Diabetes23()
        //{
        //    return Diabetes23Q.Query();
        //}

        //public string Diabetes24()
        //{
        //    return Diabetes24Q.Query();
        //}

        //public string Diabetes24a()
        //{
        //    return Diabetes24aQ.Query();
        //}
        //public string Diabetes24b()
        //{
        //    return Diabetes24bQ.Query();
        //}
        //public string Diabetes24c()
        //{
        //    return Diabetes24cQ.Query();
        //}
        //public string Diabetes24d()
        //{
        //    return Diabetes24dQ.Query();
        //}

        //public string Diabetes25()
        //{
        //    return Diabetes25Q.Query();
        //}

        //public string Diabetes25a()
        //{
        //    return Diabetes25aQ.Query();
        //}

        //public string Diabetes25b()
        //{
        //    return Diabetes25bQ.Query();
        //}

        //public string Diabetes26()
        //{
        //    return Diabetes26Q.Query();
        //}

        //public string Diabetes26a()
        //{
        //    return Diabetes26aQ.Query();
        //}

        //public string Diabetes26b()
        //{
        //    return Diabetes26bQ.Query();
        //}

        //public string Diabetes27()
        //{
        //    return Diabetes27Q.Query();
        //}

        //public string Diabetes27a()
        //{
        //    return Diabetes27aQ.Query();
        //}

        //public string Diabetes27b()
        //{
        //    return Diabetes27bQ.Query();
        //}

        //public string Diabetes28()
        //{
        //    return Diabetes28Q.Query();
        //}

        //public string Diabetes28a()
        //{
        //    return Diabetes28aQ.Query();
        //}

        //public string Diabetes28b()
        //{
        //    return Diabetes28bQ.Query();
        //}

        //public string Diabetes29()
        //{
        //    return Diabetes29Q.Query();
        //}

        //public string Diabetes29a()
        //{
        //    return Diabetes29aQ.Query();
        //}

        //public string Diabetes29b()
        //{
        //    return Diabetes29bQ.Query();
        //}

        //public string Diabetes30()
        //{
        //    return Diabetes30Q.Query();
        //}

        //public string Diabetes30a()
        //{
        //    return Diabetes30aQ.Query();
        //}

        //public string Diabetes30b()
        //{
        //    return Impilo_App.ReportQueries.Format_2.Diabetes.Diabetes130bQ.Query();
        //}

        //public string Diabetes31()
        //{
        //    return Diabetes31Q.Query();
        //}

        //public string Diabetes31a()
        //{
        //    return Diabetes31aQ.Query();
        //}

        //public string Diabetes31b()
        //{
        //    return Diabetes31bQ.Query();
        //}

        //public string Diabetes32()
        //{
        //    return Diabetes32Q.Query();
        //}

        //public string Diabetes32a()
        //{
        //    return Diabetes32aQ.Query();
        //}

        //public string Diabetes32b()
        //{
        //    return Diabetes32bQ.Query();
        //}

        //public string Diabetes33()
        //{
        //    return Diabetes33Q.Query();
        //}

        //public string Diabetes33a()
        //{
        //    return Diabetes33aQ.Query();
        //}

        //public string Diabetes33b()
        //{
        //    return Diabetes33bQ.Query();
        //}

        //public string Diabetes34()
        //{
        //    return Diabetes34Q.Query();
        //}

        //public string Diabetes35()
        //{
        //    return Diabetes35Q.Query();
        //}
        //public string Diabetes36()
        //{
        //    return Diabetes36Q.Query();
        //}
        //public string Diabetes37()
        //{
        //    return Diabetes37Q.Query();
        //}

        //public string Diabetes38()
        //{
        //    return Diabetes38Q.Query();
        //}

        //public string Diabetes38a()
        //{
        //    return Diabetes38aQ.Query();
        //}

        //public string Diabetes38b()
        //{
        //    return Diabetes38bQ.Query();
        //}
        //public string Diabetes39()
        //{
        //    return Diabetes39Q.Query();
        //}

        //public string Diabetes39a()
        //{
        //    return Diabetes39aQ.Query();
        //}

        //public string Diabetes39b()
        //{
        //    return Diabetes39bQ.Query();
        //}
        //public string Diabetes40()
        //{
        //    return Diabetes40Q.Query();
        //}
        //public string Diabetes40a()
        //{
        //    return Diabetes40aQ.Query();
        //}

        //public string Diabetes40b()
        //{
        //    return Diabetes40bQ.Query();
        //}

        //public string Diabetes41()
        //{
        //    return Diabetes41Q.Query();
        //}

        //public string Diabetes41a()
        //{
        //    return Diabetes41aQ.Query();
        //}

        //public string Diabetes41b()
        //{
        //    return Diabetes41bQ.Query();
        //}

        //public string Diabetes42()
        //{
        //    return Diabetes42Q.Query();
        //}
        //public string Diabetes43()
        //{
        //    return Diabetes43Q.Query();
        //}

        //public string Diabetes43a()
        //{
        //    return Diabetes43aQ.Query();
        //}

        //public string Diabetes43b()
        //{
        //    return Diabetes43bQ.Query();
        //}

        //public string Diabetes44()
        //{
        //    return Diabetes44Q.Query();
        //}

        //public string Diabetes44a()
        //{
        //    return Diabetes44aQ.Query();
        //}

        //public string Diabetes44b()
        //{
        //    return Diabetes44bQ.Query();
        //}
        //public string Diabetes45()
        //{
        //    return Diabetes45Q.Query();
        //}
        //public string Diabetes45a()
        //{
        //    return Diabetes45aQ.Query();
        //}

        //public string Diabetes45b()
        //{
        //    return Diabetes45bQ.Query();
        //}
        //public string Diabetes46()
        //{
        //    return Diabetes46Q.Query();
        //}

        //public string Diabetes46a()
        //{
        //    return Diabetes46aQ.Query();
        //}

        //public string Diabetes46b()
        //{
        //    return Diabetes46bQ.Query();
        //}
        //public string Diabetes47()
        //{
        //    return Diabetes47Q.Query();
        //}

        //public string Diabetes47a()
        //{
        //    return Diabetes47aQ.Query();
        //}

        //public string Diabetes47b()
        //{
        //    return Diabetes47bQ.Query();
        //}
        //public string Diabetes48()
        //{
        //    return Diabetes48Q.Query();
        //}
        //public string Diabetes48a()
        //{
        //    return Diabetes48aQ.Query();
        //}

        //public string Diabetes48b()
        //{
        //    return Diabetes48bQ.Query();
        //}

        //#endregion

        //#region Hypertension
        //public string Hypertension1()
        //{
        //    return Hypertension1Q.Query();
        //}
        //public string Hypertension2()
        //{
        //    return Hypertension2Q.Query();
        //}

        //public string Hypertension3()
        //{
        //    return Hypertension3Q.Query();
        //}

        //public string Hypertension4()
        //{
        //    return Hypertension4Q.Query();
        //}

        //public string Hypertension5()
        //{
        //    return Hypertension5Q.Query();
        //}

        //public string Hypertension6()
        //{
        //    return Hypertension6Q.Query();
        //}

        //public string Hypertension7()
        //{
        //    return Hypertension7Q.Query();
        //}

        //public string Hypertension7a()
        //{
        //    return Hypertension7aQ.Query();
        //}

        //public string Hypertension7b()
        //{
        //    return Hypertension7bQ.Query();
        //}

        //public string Hypertension8()
        //{
        //    return Hypertension8Q.Query();
        //}
        //public string Hypertension9()
        //{
        //    return Hypertension9Q.Query();
        //}
        //public string Hypertension9a()
        //{
        //    return Hypertension9aQ.Query();
        //}
        //public string Hypertension9b()
        //{
        //    return Hypertension9bQ.Query();
        //}
        //public string Hypertension9c()
        //{
        //    return Hypertension9cQ.Query();
        //}
        //public string Hypertension9d()
        //{
        //    return Hypertension9dQ.Query();
        //}

        //public string Hypertension10()
        //{
        //    return Hypertension10Q.Query();
        //}
        //public string Hypertension10a()
        //{
        //    return Hypertension10aQ.Query();
        //}
        //public string Hypertension10b()
        //{
        //    return Hypertension10bQ.Query();
        //}

        //public string Hypertension11()
        //{
        //    return Hypertension11Q.Query();
        //}
        //public string Hypertension11a()
        //{
        //    return Hypertension11aQ.Query();
        //}
        //public string Hypertension11b()
        //{
        //    return Hypertension11bQ.Query();
        //}

        //public string Hypertension12()
        //{
        //    return Hypertension12Q.Query();
        //}
        //public string Hypertension12a()
        //{
        //    return Hypertension12aQ.Query();
        //}
        //public string Hypertension12b()
        //{
        //    return Hypertension12bQ.Query();
        //}

        //public string Hypertension13()
        //{
        //    return Hypertension13Q.Query();
        //}

        //public string Hypertension14()
        //{
        //    return Hypertension14Q.Query();
        //}

        //public string Hypertension14a()
        //{
        //    return Hypertension14aQ.Query();
        //}
        //public string Hypertension14b()
        //{
        //    return Hypertension14bQ.Query();
        //}

        //public string Hypertension15()
        //{
        //    return Hypertension15Q.Query();
        //}
        //public string Hypertension15a()
        //{
        //    return Hypertension15aQ.Query();
        //}
        //public string Hypertension15b()
        //{
        //    return Hypertension15bQ.Query();
        //}

        //public string Hypertension16()
        //{
        //    return Hypertension16Q.Query();
        //}
        //public string Hypertension16a()
        //{
        //    return Hypertension16aQ.Query();
        //}
        //public string Hypertension16b()
        //{
        //    return Hypertension16bQ.Query();
        //}

        //public string Hypertension17()
        //{
        //    return Hypertension17Q.Query();
        //}
        //public string Hypertension17a()
        //{
        //    return Hypertension17aQ.Query();
        //}
        //public string Hypertension17b()
        //{
        //    return Hypertension17bQ.Query();
        //}

        //public string Hypertension18()
        //{
        //    return Hypertension18Q.Query();
        //}
        //public string Hypertension18a()
        //{
        //    return Hypertension18aQ.Query();
        //}
        //public string Hypertension18b()
        //{
        //    return Hypertension18bQ.Query();
        //}

        //public string Hypertension19()
        //{
        //    return Hypertension19Q.Query();
        //}
        //public string Hypertension19a()
        //{
        //    return Hypertension19aQ.Query();
        //}
        //public string Hypertension19b()
        //{
        //    return Hypertension19bQ.Query();
        //}

        //public string Hypertension20()
        //{
        //    return Hypertension20Q.Query();
        //}

        //public string Hypertension21()
        //{
        //    return Hypertension21Q.Query();
        //}

        //public string Hypertension22()
        //{
        //    return Hypertension22Q.Query();
        //}

        //public string Hypertension22a()
        //{
        //    return Hypertension22aQ.Query();
        //}
        //public string Hypertension22b()
        //{
        //    return Hypertension22bQ.Query();
        //}

        //public string Hypertension23()
        //{
        //    return Hypertension23Q.Query();
        //}

        //public string Hypertension24()
        //{
        //    return Hypertension24Q.Query();
        //}

        //public string Hypertension24a()
        //{
        //    return Hypertension24aQ.Query();
        //}
        //public string Hypertension24b()
        //{
        //    return Hypertension24bQ.Query();
        //}
        //public string Hypertension24c()
        //{
        //    return Hypertension24cQ.Query();
        //}
        //public string Hypertension24d()
        //{
        //    return Hypertension24dQ.Query();
        //}

        //public string Hypertension25()
        //{
        //    return Hypertension25Q.Query();
        //}

        //public string Hypertension25a()
        //{
        //    return Hypertension25aQ.Query();
        //}

        //public string Hypertension25b()
        //{
        //    return Hypertension25bQ.Query();
        //}

        //public string Hypertension26()
        //{
        //    return Hypertension26Q.Query();
        //}

        //public string Hypertension26a()
        //{
        //    return Hypertension26aQ.Query();
        //}

        //public string Hypertension26b()
        //{
        //    return Hypertension26bQ.Query();
        //}

        //public string Hypertension27()
        //{
        //    return Hypertension27Q.Query();
        //}

        //public string Hypertension27a()
        //{
        //    return Hypertension27aQ.Query();
        //}

        //public string Hypertension27b()
        //{
        //    return Hypertension27bQ.Query();
        //}

        //public string Hypertension28()
        //{
        //    return Hypertension28Q.Query();
        //}

        //public string Hypertension28a()
        //{
        //    return Hypertension28aQ.Query();
        //}

        //public string Hypertension28b()
        //{
        //    return Hypertension28bQ.Query();
        //}

        //public string Hypertension29()
        //{
        //    return Hypertension29Q.Query();
        //}

        //public string Hypertension29a()
        //{
        //    return Hypertension29aQ.Query();
        //}

        //public string Hypertension29b()
        //{
        //    return Hypertension29bQ.Query();
        //}

        //public string Hypertension30()
        //{
        //    return Hypertension30Q.Query();
        //}

        //public string Hypertension30a()
        //{
        //    return Hypertension30aQ.Query();
        //}

        //public string Hypertension30b()
        //{
        //    return Hypertension30bQ.Query();
        //}

        //public string Hypertension31()
        //{
        //    return Hypertension31Q.Query();
        //}

        //public string Hypertension31a()
        //{
        //    return Hypertension31aQ.Query();
        //}

        //public string Hypertension31b()
        //{
        //    return Hypertension31bQ.Query();
        //}

        //public string Hypertension32()
        //{
        //    return Hypertension32Q.Query();
        //}

        //public string Hypertension32a()
        //{
        //    return Hypertension32aQ.Query();
        //}

        //public string Hypertension32b()
        //{
        //    return Hypertension32bQ.Query();
        //}

        //public string Hypertension33()
        //{
        //    return Hypertension33Q.Query();
        //}

        //public string Hypertension33a()
        //{
        //    return Hypertension33aQ.Query();
        //}

        //public string Hypertension33b()
        //{
        //    return Hypertension33bQ.Query();
        //}

        //public string Hypertension34()
        //{
        //    return Hypertension34Q.Query();
        //}

        //public string Hypertension35()
        //{
        //    return Hypertension35Q.Query();
        //}
        //public string Hypertension36()
        //{
        //    return Hypertension36Q.Query();
        //}
        //public string Hypertension37()
        //{
        //    return Hypertension37Q.Query();
        //}

        //public string Hypertension38()
        //{
        //    return Hypertension38Q.Query();
        //}

        //public string Hypertension38a()
        //{
        //    return Hypertension38aQ.Query();
        //}

        //public string Hypertension38b()
        //{
        //    return Hypertension38bQ.Query();
        //}
        //public string Hypertension39()
        //{
        //    return Hypertension39Q.Query();
        //}

        //public string Hypertension39a()
        //{
        //    return Hypertension39aQ.Query();
        //}

        //public string Hypertension39b()
        //{
        //    return Hypertension39bQ.Query();
        //}
        //public string Hypertension40()
        //{
        //    return Hypertension40Q.Query();
        //}
        //public string Hypertension40a()
        //{
        //    return Hypertension40aQ.Query();
        //}

        //public string Hypertension40b()
        //{
        //    return Hypertension40bQ.Query();
        //}

        //public string Hypertension41()
        //{
        //    return Hypertension41Q.Query();
        //}

        //public string Hypertension41a()
        //{
        //    return Hypertension41aQ.Query();
        //}

        //public string Hypertension41b()
        //{
        //    return Hypertension41bQ.Query();
        //}

        //public string Hypertension42()
        //{
        //    return Hypertension42Q.Query();
        //}
        //public string Hypertension43()
        //{
        //    return Hypertension43Q.Query();
        //}

        //public string Hypertension43a()
        //{
        //    return Hypertension43aQ.Query();
        //}

        //public string Hypertension43b()
        //{
        //    return Hypertension43bQ.Query();
        //}

        //public string Hypertension44()
        //{
        //    return Hypertension44Q.Query();
        //}

        //public string Hypertension44a()
        //{
        //    return Hypertension44aQ.Query();
        //}

        //public string Hypertension44b()
        //{
        //    return Hypertension44bQ.Query();
        //}
        //public string Hypertension45()
        //{
        //    return Hypertension45Q.Query();
        //}
        //public string Hypertension45a()
        //{
        //    return Hypertension45aQ.Query();
        //}

        //public string Hypertension45b()
        //{
        //    return Hypertension45bQ.Query();
        //}
        //public string Hypertension46()
        //{
        //    return Hypertension46Q.Query();
        //}

        //public string Hypertension46a()
        //{
        //    return Hypertension46aQ.Query();
        //}

        //public string Hypertension46b()
        //{
        //    return Hypertension46bQ.Query();
        //}
        //public string Hypertension47()
        //{
        //    return Hypertension47Q.Query();
        //}

        //public string Hypertension47a()
        //{
        //    return Hypertension47aQ.Query();
        //}

        //public string Hypertension47b()
        //{
        //    return Hypertension47bQ.Query();
        //}
        //public string Hypertension48()
        //{
        //    return Hypertension48Q.Query();
        //}
        //public string Hypertension48a()
        //{
        //    return Hypertension48aQ.Query();
        //}

        //public string Hypertension48b()
        //{
        //    return Hypertension48bQ.Query();
        //}

        //#endregion
    }
}
