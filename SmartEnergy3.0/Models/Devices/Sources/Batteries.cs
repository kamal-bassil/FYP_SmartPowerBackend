using System;
using MongoDB.Bson;

namespace SmartPowerBackend.Models
{
    public class Batteries: Source
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
            UpdateType = typeof(BatteriesMessage);
        }
        

        public StatusUpdate GenerateUpdate(string time)
        {
            return new BatteriesMessage(time, ID, this);
            
        }


        public float PriceOfWatt { get; set; }
        public float MaxEnergy { get; set; }
        public float CurrentLevel { get; set; }//percentage
        
        
        public Batteries(string name, float maxEnergy)
        {
            Name = name;
            MaxEnergy = maxEnergy;
            PriceOfWatt = 0f;
            ID = Guid.NewGuid().ToString();
            Type = SourceTypes.Batteries;
            TimeLine = new TimeLine(this);
            SetMessageType();
            
        }

        public ObjectId Id { get; set; }
    }

    public class BatteriesMessage : StatusUpdate
    {
        public string Time { get; set; }
        public string ID { get; set; }
        public float BatteryLevel { get; set; }
        public float CurrentPower { get; set; }
        public SourceStatus Status { get; set; }
        

        public BatteriesMessage(string time,string iD,Batteries batteries)
        {
            Time = time;
            ID = iD;
            BatteryLevel = batteries.CurrentLevel;
            CurrentPower = batteries.CurrentPowerDrawn;
            Status = batteries.CurrentStatus;
            


        }
    }
}