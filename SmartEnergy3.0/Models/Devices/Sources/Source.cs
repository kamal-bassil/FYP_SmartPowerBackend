namespace SmartPowerBackend.Models
{
    public interface Source: Device
    {
        string Name {get;  set; }
        string ID { get; set; }
        SourceTypes Type { get; set; }
        SourceStatus CurrentStatus { get; set; }
        float CurrentPowerDrawn { get; set; }
        TimeLine TimeLine { get; set; }
        float PriceOfWatt{ get; set; }
    }

    public enum SourceTypes
    {
        Unsupported = -1,
        SolarPanel = 1,
        EDL = 2,
        Subscription = 3,
        Batteries = 4
    }

    public enum SourceStatus
    {
        Unknown = -1,
        Offline = 1,
        Online = 2
    }
}