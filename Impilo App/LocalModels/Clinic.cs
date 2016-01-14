using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Clinic
    {
        public int ClinicID { get; set; }

        public string ClinicName { get; set; }

        public string Logitude { get; set; }

        public string Latitude { get; set; }

        public DateTime DateLastSnycFromServer { get; set; }

        public DateTime DateLastSnycToServer { get; set; }

        public DateTime LastDayUpdated { get; set; }
    }
}
