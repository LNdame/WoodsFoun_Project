using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class HealthWorker
    {
        public string UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public char Gender { get; set; }

        public DateTime DateLastUpated { get; set; }

        public DateTime DateLastSnycFromServer { get; set; }

        public DateTime DateLastSnycToSeverServer { get; set; }

        public bool IsActive { get; set; }
    }
}
