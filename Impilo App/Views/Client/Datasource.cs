using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace Impilo_App.Views.Client
{
    class Datasource : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region school name

        private ObservableCollection<string> _schoolname = new ObservableCollection<string> { "Elliotdale Tech SSS", "Ngonyama JP", "Elliotdale Village JS",
            "Kalalo JS", "Bacela PJS", "Mzomhle JS", "Futye JS", "Melithafa JS", "Other", "Not applicable" };
        

        public ObservableCollection<string> SchoolName
        {
            get { return _schoolname; }
        }

        private ObservableCollection<string> _selectedSchool;

        public ObservableCollection<string> SelectedSchool
        {
            get {
                if (_selectedSchool == null)
                {
                    _selectedSchool = new ObservableCollection<string> { "Elliotdale Tech SSS" };
                    _selectedSchool.CollectionChanged += (s, e) => { OnPropertyChanged("SelectedSchool"); };
                }
                return _selectedSchool;
            }
            set { _selectedSchool = value; }
        }

        #endregion

        #region Grade

        private ObservableCollection<string> _grade = new ObservableCollection<string> {"Grade R", "Grade 1","Grade 2","Grade 3","Grade 4","Grade 5",
            "Grade 6","Grade 7","Grade 8","Grade 9","Grade 10","Grade 11","Grade 12","Other ","Not applicable",};
      
        public ObservableCollection<string> Grade
        {
            get { return _grade; }
        }

        #endregion



    }
}
