using System;
using MongoDB.Bson;

namespace SmartPowerBackend.Models
{
    public class ElectricCar: Load
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public LoadRating Rating { get; set; }
        public LoadStatus CurrentStatus { get; set; }
        public float CurrentPowerDrawn { get; set; }
        public TimeLine TimeLine { get; set; }
        public Type UpdateType { get; set; }
        public void SetMessageType()
        {
            UpdateType = typeof(ElectricCarMessage);
        }

        public StatusUpdate GenerateUpdate(string time)
        {
            return new ElectricCarMessage(time, ID, this);
        }

        public WifiSwitch Switch { get; set; }
        public LoadTypes Type { get; set; }
        public int Priority { get; set; }

        public ElectricCar(string IP, string name,int priority,float power)
        {
            Name = name;
            Priority = priority;
            ID = Guid.NewGuid().ToString();
            Switch = new WifiSwitch(IP);
            Rating = new LoadRating()
            {
                MaxPower = power,
                SteadyStatePower = power
            };
            CurrentStatus = LoadStatus.Unkown;
            TimeLine = new TimeLine(this);
            Type = LoadTypes.ElectricCar;
            CurrentPowerDrawn = -1f;
            SetMessageType();
        }

        public ObjectId Id { get; set; }
    }

    public class  ElectricCarMessage: StatusUpdate
    {
        public string Time { get; set; }
        public string ID { get; set; }
        
        public float CurrentPower { get; set; }
        public LoadStatus Status { get; set; }
        

        public ElectricCarMessage(string time,string iD,ElectricCar eC)
        {
            Time = time;
            ID = iD;
            CurrentPower = eC.CurrentPowerDrawn;
            Status = eC.CurrentStatus;
        }
    }
}