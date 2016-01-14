
namespace Models.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class EnvironmentalExtra
    {
        public string ScreeningID { get; set; }
        public Nullable<int> NoOfPeopleInOneRoomMainHut { get; set; }
        public Nullable<int> NoOfStructuresInHomeStead { get; set; }
        public Nullable<bool> RainWaterCollection { get; set; }
        public string WaterSupply { get; set; }
        public Nullable<bool> WalkingDistanceFromWhaterSupply { get; set; }
        public Nullable<bool> TreatWaterBeforeDrinking { get; set; }
        public Nullable<bool> ElectricityInAnyHut { get; set; }
        public Nullable<bool> HaveWorkingFridge { get; set; }
        public string UseForCooking { get; set; }
        public string TypeOfToilet { get; set; }
        public string DisposeWaste { get; set; }
        public string SourceOfIncome { get; set; }
        public Nullable<bool> RecievedFoodPacelIn6Month { get; set; }
    }
}
