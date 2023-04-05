using System;
using MongoDB.Bson;

namespace SmartPowerBackend.Models
{
    public class AC: Load
    {
        private Load _loadImplementation;
        public string Name { get; set; }
        public string ID { get; set; }
        public LoadRating Rating { get; set; }
        public LoadStatus CurrentStatus { get; set; }
        public float CurrentPowerDrawn { get; set; }
        public TimeLine TimeLine { get; set; }
        public Type UpdateType { get; set; }
        public void SetMessageType()
        {
            UpdateType = typeof(ACMessage);
        }

        public StatusUpdate GenerateUpdate(string time)
        {
            return new ACMessage(time, ID, this);
        }

        public WifiSwitch Switch { get; set; }
        public LoadTypes Type { get; set; }
        public int Priority { get; set; }

        public float WeatherTempreature { get; set; }
        
        public AC(string IP, string DeviceName,int priority,float power)
        {
            Name = DeviceName;
            Priority = priority;
            ID = Guid.NewGuid().ToString();
            Switch = new WifiSwitch(IP);
            SetMessageType();
            Rating = new LoadRating()
            {
                MaxPower = power,
                SteadyStatePower = power
            };
            CurrentStatus = LoadStatus.Unkown;
            TimeLine = new TimeLine(this);
            Type = LoadTypes.AC;
            CurrentPowerDrawn = -1f;
        }

        public ObjectId Id { get; set; }
    }

    public class ACMessage : StatusUpdate
    {
        public string Time { get; set; }
        public string ID { get; set; }
        public float WeatherTempreature { get; set; }
        public float CurrentPower { get; set; }
        public LoadStatus Status { get; set; }
        

        public ACMessage(string time,string iD,AC aC)
        {
            Time = time;
            ID = iD;
            WeatherTempreature = aC.WeatherTempreature;
            CurrentPower = aC.CurrentPowerDrawn;
            Status = aC.CurrentStatus;
            


        }
    }
}