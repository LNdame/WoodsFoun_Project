using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
   public class Client
    {
        public string ClientID { get; set; }

        public string HeadOfHousehold { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GPSLatitude { get; set; }

        public string GPSLongitude { get; set; }

        public string IDNo { get; set; }

        public int ClinicUsed { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public bool AttendingSchool { get; set; }

        public string Grade { get; set; }

        public string NameofSchool { get; set; }

    }
}
