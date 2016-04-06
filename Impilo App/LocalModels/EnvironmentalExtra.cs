using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impilo_App.LocalModels
{
    class EnvironmentalExtra
    {
        public string ScreeningID { get; set; }

        public int NoOfPeopleInOneRoomMainHut { get; set; }

        public int NoOfStructuresInHomeStead { get; set; }

        public bool RainWaterCollection { get; set; }


        public bool OwnorRentHome { get; set; }

        public string WaterSupply { get; set; }

        public string WalkingDistanceFromWhaterSupply { get; set; }

        public bool TreatWaterBeforeDrinking { get; set; }

        public bool ElectricityInAnyHut { get; set; }

        public bool HaveWorkingFridge { get; set; }

        public string UseForCooking { get; set; }

        public string TypeOfToilet { get; set; }

        public string DisposeWaste { get; set; }

        public List<String> SourceOfIncome { get; set; }

        public bool RecievedFoodPacelIn6Month { get; set; }
    }
}
