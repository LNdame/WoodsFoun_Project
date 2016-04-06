using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

namespace Impilo_App.Views.Screening
{
    class DataSource: INotifyPropertyChanged
    {
        
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #endregion


        #region HIV Meds Datasource

        private ObservableCollection<string> _hivmeds = new ObservableCollection<string> {"3TC","d4T","EFV","STocrin","TDF","NVP","Lvp/r, Kelatra,Aluvia","ABC","FDC,TFE","Tribuss Atroiza, Odimune" };

        public ObservableCollection<string> HIVMeds
        {
            get { return _hivmeds; }
        }

        private ObservableCollection<string> _selectedHivmeds;

        public ObservableCollection<string> SelectedHIVMeds
        {
            get
            {
                if (_selectedHivmeds== null)
                {
                    _selectedHivmeds = new ObservableCollection<string> { "3TC" };
                    SelectedHivMedsText = WriteSelectedHIVMedsString(_selectedHivmeds);
                    _selectedHivmeds.CollectionChanged += (s, e) => {
                        SelectedHivMedsText = WriteSelectedHIVMedsString(_selectedHivmeds);
                        OnPropertyChanged("SelectedHIVMeds");
                    };
                }
                return _selectedHivmeds;
            }
            set { _selectedHivmeds = value; }
        }

         
        public string SelectedHivMedsText
        {
            get { return _selectedHivMedsText; }
            set
            {
                _selectedHivMedsText = value;
                OnPropertyChanged("SelectedHivMedsText");
            }
        } string _selectedHivMedsText;


        private static string WriteSelectedHIVMedsString(IList<string> list)
        {
            if (list.Count == 0)
                return String.Empty;

            StringBuilder builder = new StringBuilder(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                builder.Append(", ");
                builder.Append(list[i]);
            }

            return builder.ToString();
        }
        #endregion



        #region income source

        private ObservableCollection<string> _incomesource = new ObservableCollection<string> { "No Income", "Wages earned locally", "Social Grant", "Own Business", "Wages earned by relative working away" };

        public ObservableCollection<string> IncomeSource
        {
            get { return _incomesource; }
        }


        private ObservableCollection<string> _selectedIncomeSource;

        public ObservableCollection<string> SelectedIncomeSource
        {
            get
            {
                if (_selectedIncomeSource == null)
                {
                    _selectedIncomeSource = new ObservableCollection<string> { "No Income" };
                    SelectedIncomeSourceText = WriteSelectedIncomeSourceString(_selectedIncomeSource);
                    _selectedIncomeSource.CollectionChanged += (s, e) =>
                    {
                        SelectedIncomeSourceText = WriteSelectedIncomeSourceString(_selectedIncomeSource);
                        OnPropertyChanged("SelectedIncomeSource");
                    };
                }
                return _selectedIncomeSource;
            }
            set { _selectedIncomeSource = value; }
        }


        public string SelectedIncomeSourceText
        {
            get { return _selectedIncomeSourceText; }
            set
            {
                _selectedIncomeSourceText = value;
                OnPropertyChanged("SelectedIncomeSourceText");
            }
        } string _selectedIncomeSourceText;


        private static string WriteSelectedIncomeSourceString(IList<string> list)
        {
            if (list.Count == 0)
                return String.Empty;

            StringBuilder builder = new StringBuilder(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                builder.Append(", ");
                builder.Append(list[i]);
            }

            return builder.ToString();
        }

        #endregion

        #region environmental

        #region roof struture
        private ObservableCollection<string> _roofstruc = new ObservableCollection<string> { "Reed/Grass", "Tiles", "Zinc", "Other" };

        public ObservableCollection<string> RoofStruc
        {
            get { return _roofstruc; }
        }

        #endregion


      

       

        #region single

        private ObservableCollection<string> _distancewater = new ObservableCollection<string> { "5 min", "10 min", "15 min", "20 min", "25 min", "30 min", "1 hr", "2 hrs", "3 hrs" };

        public ObservableCollection<string> DistanceWater
        {
            get { return _distancewater; }
        }


        #region Hut struture
        private ObservableCollection<string> _hutstruc = new ObservableCollection<string> { "Wood", "Cement", "Brick (mud/cement)", "Brick (cement)", "Clay/Mud", "", "Other" };

        public ObservableCollection<string> HutStruc
        {
            get { return _hutstruc; }
        }

        #endregion

        #region childhealth
        private ObservableCollection<string> _commonynd = new ObservableCollection<string> { "Yes", "No", "Don't know" };

        public ObservableCollection<string> CommonYND
        {
            get { return _commonynd; }
        }
        #endregion

        private ObservableCollection<string> _yeardiagnosis = new ObservableCollection<string> {"2008","2009","2010","2011","2012","2013","2014","2015","Can not remember", "Not applicable" };
        

        public ObservableCollection<string> YearDiagnosis
        {
            get { return _yeardiagnosis; }
        }



        private ObservableCollection<string> _ventilation = new ObservableCollection<string> { "Door", "Door/Windows" };


        public ObservableCollection<string> Ventilation
        {
            get { return _ventilation; }
        }


        private ObservableCollection<string> _muaccolour = new ObservableCollection<string> { "Red", "Orange","Green" };


        public ObservableCollection<string> MUAC
        {
            get { return _muaccolour; }
        }


        private ObservableCollection<string> _ccresult = new ObservableCollection<string> { "Reactive", "Non-reactive", "Not clear","Not applicable" };


        public ObservableCollection<string> CCResult
        {
            get { return _ccresult; }
        }


        private ObservableCollection<string> _breastfeedlength = new ObservableCollection<string> { "1 month", "2 months", "3 months","4 months","5 months","6 months"
            ,"7 months","8 months","9 months","10 months","11 months","12 months","2 years","3 years","4 years","Not applicable" };

       
        public ObservableCollection<string> BreastFeedLength
        {
            get { return _breastfeedlength; }
        }


        private ObservableCollection<string> _pcrresult = new ObservableCollection<string> { "Positive", "Negative", "Not sure" };


        public ObservableCollection<string> PCRResult
        {
            get { return _pcrresult; }
        }

        private ObservableCollection<string> _deliveryplace = new ObservableCollection<string> { "Clinic", "Hospital","Home","Private Doctor","Not applicable" };
         
        public ObservableCollection<string> DeliveryPlace
        {
            get { return _deliveryplace; }
        }

        private ObservableCollection<string> _bloodtest = new ObservableCollection<string> { "Normal", "Abnormal", "Not Sure" };

        public ObservableCollection<string> BloodTest
        {
            get { return _bloodtest; }
        }

        private ObservableCollection<string> _schoolname = new ObservableCollection<string> { "Elliotdale Tech SSS", "Ngonyama JP", "Elliotdale Village JS",
            "Kalalo JS", "Bacela PJS", "Mzomhle JS", "Futye JS", "Melithafa JS", "Other", "Not applicable" };


        public ObservableCollection<string> SchoolName
        {
            get { return _schoolname; }
        }


        private ObservableCollection<string> _grade = new ObservableCollection<string> {"Grade R", "Grade 1","Grade 2","Grade 3","Grade 4","Grade 5",
            "Grade 6","Grade 7","Grade 8","Grade 9","Grade 10","Grade 11","Grade 12","Other ","Not applicable",};

        public ObservableCollection<string> Grade
        {
            get { return _grade; }
        }

        private ObservableCollection<string> _chowname = new ObservableCollection<string> {"Linette Mawela","Vuyelwa Breakfast","Bulelwa Tsaliphike","Bongeka Dikilokhwe","Phatheka Zanayi","Thozama Nzima",
        "Nomvula Rila","Nosisa Mthunywa","Noluthembiso Nqwiliso","Vuyokazi Mjebeza","Nonkoliseko Phathakubi","Phindiwe Fudumele","Somikazi Mabhodla","Silverton Qubathi","Nosisa Gagula","Faniswa Mdlutha",
       "Zodwa Qebeyi","Phila Tyhopho", "Mandisa Phaliso","Nomampondomise Gabada","Nobesuthu Ngalwa","Ntomboxolo Sabulala","Nontembeko Sicaphuko"};

        public ObservableCollection<string> CHOWName
        {
            get { return _chowname; }
        }


        #endregion



        #region ChildImmunisation
        private ObservableCollection<string> _immunisationout = new ObservableCollection<string> { "BCG", "Polio 1", "Polio 2", "Polio 3", "Polio 4", "Polio 5", "DPT1", "DPT2", "DPT3", "DPT4", "Hib 1", "Hib 2", "Hib 3", "Hep B1", "Hep B 2", "Hep B 3", "Measles 1", "Measles 2", "DPT1/Hip 1", "DPT2/Hip 2", "DPT3/Hip 3" };

        public ObservableCollection<string> ImmunisationOut
        {
            get { return _immunisationout; }
        }

        private ObservableCollection<string> _selectedImmunisationout;

        public ObservableCollection<string> SelectedImmunisationOut
        {
            get
            {
                if (_selectedImmunisationout == null)
                {
                    _selectedImmunisationout = new ObservableCollection<string> { "BCG" };
                    SelectedImmunisationOutText = WriteSelectedImmunisationOutString(_selectedImmunisationout);
                    _selectedImmunisationout.CollectionChanged += (s, e) =>
                    {
                        SelectedImmunisationOutText = WriteSelectedImmunisationOutString(_selectedImmunisationout);
                        OnPropertyChanged("SelectedImmunisationOut");
                    };
                }
                return _selectedImmunisationout;
            }
            set { _selectedImmunisationout = value; }
        }


        public string SelectedImmunisationOutText
        {
            get { return _selectedImmunisationOutText; }
            set
            {
                _selectedImmunisationOutText = value;
                OnPropertyChanged("SelectedImmunisationOutText");
            }
        } string _selectedImmunisationOutText;


        private static string WriteSelectedImmunisationOutString(IList<string> list)
        {
            if (list.Count == 0)
                return String.Empty;

            StringBuilder builder = new StringBuilder(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                builder.Append(", ");
                builder.Append(list[i]);
            }

            return builder.ToString();
        }


        #endregion



      

        #endregion

        #region listofMeds Diabetes
        private ObservableCollection<string> _tbmeds = new ObservableCollection<string> { "INH", "Ritib", "Rifafour", "Rifinah", "RH" };
       
        public ObservableCollection<string> TBMeds
        {
            get { return _tbmeds; }
        }

        private ObservableCollection<string> _selectedTBmeds;

        public ObservableCollection<string> SelectedTBMeds
        {
            get
            {
                if (_selectedTBmeds == null)
                {
                    _selectedTBmeds = new ObservableCollection<string> { "INH" };
                    SelectedTBMedsText = WriteSelectedTBMedsString(_selectedTBmeds);
                    _selectedTBmeds.CollectionChanged += (s, e) =>
                    {
                        SelectedTBMedsText = WriteSelectedTBMedsString(_selectedTBmeds);
                        OnPropertyChanged("SelectedTBMeds");
                    };
                }
                return _selectedTBmeds;
            }
            set { _selectedTBmeds = value; }
        }


        public string SelectedTBMedsText
        {
            get { return _selectedtbMedsText; }
            set
            {
                _selectedtbMedsText = value;
                OnPropertyChanged("SelectedTBMedsText");
            }
        } string _selectedtbMedsText;


        private static string WriteSelectedTBMedsString(IList<string> list)
        {
            if (list.Count == 0)
                return String.Empty;

            StringBuilder builder = new StringBuilder(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                builder.Append(", ");
                builder.Append(list[i]);
            }

            return builder.ToString();
        }
        #endregion

        #region listofMeds Asthma

        #endregion

    }
}