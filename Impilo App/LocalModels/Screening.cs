using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class Screening
    {
        public string ScreeningID { get; set; }

        public DateTime ScreeningDate { get; set; }

        public string ClientId { get; set; }

        public string EncounterCapturedBy { get; set; }
    }
}
