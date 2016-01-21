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



        #region listofMeds Hyperten

        #endregion

        #region listofMeds Diabetes

        #endregion

        #region listofMeds Asthma

        #endregion

    }
}