using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class ClinicClientBiographical
    {
        public int ccbioID { get; set; }

        public int ClinicID { get; set; }

        public string ClientID { get; set; }

        public string ccbioContactNo { get; set; }

        public string ccbioFileNo { get; set; }

        public string ccbioNextOfKinRelationship { get; set; }

        public string ccbioNextOfKinName { get; set; }

        public string ccbioNextOfKinTelNo { get; set; }

        public DateTime ccbioDoDHypertension { get; set; }

        public DateTime ccbioDoDDiabetes { get; set; }

        public DateTime ccbioDoDEpilepsy { get; set; }

        public DateTime ccbioDoDAsthma { get; set; }

        public DateTime ccbioDoDHIV { get; set; }

        public DateTime ccbioDoDTB { get; set; }

        public DateTime ccbioDoDMaternalHealth { get; set; }

        public DateTime ccbioDoDChildHealth { get; set; }

        public DateTime ccbioOther { get; set; }
    }
}
