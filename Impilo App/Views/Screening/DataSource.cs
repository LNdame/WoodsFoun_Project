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



        private ObservableCollection<string> _animals = new ObservableCollection<string> 
        { "Cat", "Dog", "Bear", "Lion", "Mouse", "Horse", "Rat", "Elephant", "Kangaroo", "Lizard", "Snake", "Frog", "Fish", "Butterfly", "Human", "Cow", "Bumble Bee" };
        
        public ObservableCollection<string> Animals
        {
            get { return _animals; }
        }
        
        private string _selectedAnimal = "Cat";
        public string SelectedAnimal
        {
            get { return _selectedAnimal; }
            set 
            { 
                _selectedAnimal = value;
                OnPropertyChanged("SelectedAnimal");
            }
        }

        private ObservableCollection<string> _selectedAnimals;
        public ObservableCollection<string> SelectedAnimals
        {
            get
            {
                if (_selectedAnimals == null)
                {
                    _selectedAnimals = new ObservableCollection<string> { "Dog", "Lion", "Lizard" };
                    SelectedAnimalsText = WriteSelectedAnimalsString(_selectedAnimals);
                    _selectedAnimals.CollectionChanged +=
                        (s, e) =>
                        {
                            SelectedAnimalsText = WriteSelectedAnimalsString(_selectedAnimals);
                            OnPropertyChanged("SelectedAnimals");
                        };
                }
                return _selectedAnimals;
            }
            set
            {
                _selectedAnimals = value;
            }
        }

        public string SelectedAnimalsText
        {
            get { return _selectedAnimalsText; }
            set 
            { 
                _selectedAnimalsText = value;
                OnPropertyChanged("SelectedAnimalsText");
            }
        } string _selectedAnimalsText;


        private static string WriteSelectedAnimalsString(IList<string> list)
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
    }
}