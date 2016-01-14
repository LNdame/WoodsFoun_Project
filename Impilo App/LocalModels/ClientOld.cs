using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClientOld
    {
        public string ClientID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public char IDNumber { get; set; }

        public char Gender { get; set; }

        public bool isHeadOfHousehold { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool isAttendingSchool { get; set; }

        public char Grade { get; set; }

        public int NameOfSchool { get; set; }

        public DateTime LastSyncDateFromServer { get; set; }

        public DateTime LastSyncDateToServer { get; set; }

        public DateTime LastDateUpdated { get; set; }

        public bool isActive { get; set; }
    }
}
