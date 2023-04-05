namespace SmartPowerBackend.Models
{
    public interface Load: Device
    {
        string Name { get; set; }
        string ID { get; set; }
        LoadRating Rating { get; set; }
        LoadStatus CurrentStatus { get; set; }
        float CurrentPowerDrawn { get; set; }
        TimeLine TimeLine { get; set; }
        WifiSwitch Switch { get; set; }
        LoadTypes Type { get; set; }
        int Priority { get; set; }
        
    }

    public class LoadRating
    {
        public float SteadyStatePower;
        public float MaxPower;
    }

    public enum LoadTypes
    {
        Unsupported = -1,
        Heater = 1,
        WaterHeater = 2,
        AC = 3,
        ElectricCar = 4
    }

    public enum LoadStatus
    {
        Unkown = -1,
        On = 1,
        Off =2
    }
}