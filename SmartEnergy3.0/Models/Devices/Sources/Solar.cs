using System;
using System.Globalization;
using MongoDB.Bson;

namespace SmartPowerBackend.Models
{
    public class Solar: Source
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public SourceTypes Type { get; set; }
        public SourceStatus CurrentStatus { get; set; }
        public float CurrentPowerDrawn { get; set; }
        public TimeLine TimeLine { get; set; }
        public Type UpdateType { get; set; }
        public void SetMessageType()
        {
            UpdateType = typeof(Solar);
        }

        public StatusUpdate GenerateUpdate(string time)
        {
            return new SolarMessage(time, ID, this);
        }

        public float PriceOfWatt { get; set; }
        public Inverter Inverter { get; set; }

        public Solar(string name, string IP)
        {
            Name = name;
            ID = Guid.NewGuid().ToString();
            CurrentStatus = SourceStatus.Unknown;
            TimeLine = new TimeLine(this);
            Inverter = new Inverter(IP);
            Type = SourceTypes.SolarPanel;
            PriceOfWatt = 0;
            SetMessageType();

        }
        public ObjectId Id { get; set; }
    }
    public class SolarMessage : StatusUpdate
    {
        public string Time { get; set; }
        public string ID { get; set; }
        public float CurrentPower { get; set; }
        public SourceStatus Status { get; set; }
        

        public SolarMessage(string time,string iD,Solar solar)
        {
            Time = time;
            ID = iD;
            CurrentPower = solar.CurrentPowerDrawn;
            Status = solar.CurrentStatus;
            


        }
    }
}