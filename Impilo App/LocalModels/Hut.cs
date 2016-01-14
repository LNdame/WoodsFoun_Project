using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Hut
    {
        public int HutId { get; set; }

        public string ClientId { get; set; }

        public string HutStracture { get; set; }

        public string TypeOfRoof { get; set; }

        public string Ventilation { get; set; }

        public int TotalNoOfRooms { get; set; }

        public bool isMainHut { get; set; }

        public DateTime DateLastSnycFromServer { get; set; }

        public DateTime DateLastSnycToServer { get; set; }

        public DateTime LastDateUpdated { get; set; }

        public bool isActive { get; set; }
    }
}
