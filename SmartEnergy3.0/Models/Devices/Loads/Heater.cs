using System;
using System.Data;
using MongoDB.Bson;

namespace SmartPowerBackend.Models
{
    public class Heater: Load
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
            UpdateType = typeof(HeaterMessage);
        }

        public StatusUpdate GenerateUpdate(string time)
        {
            return new HeaterMessage(time, ID, this);
        }

        public WifiSwitch Switch { get; set; }
        public LoadTypes Type { get; set; }
        public int Priority { get; set; }

        public Heater(string IP, string name,int priority,float power)
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
            Type = LoadTypes.Heater;
            CurrentPowerDrawn = -1f;
            SetMessageType();
        }

        public ObjectId Id { get; set; }
    }
    public class HeaterMessage: StatusUpdate
    {
        public string Time { get; set; }
        public string ID { get; set; }
        public float WeatherTempreature { get; set; }
        public float CurrentPower { get; set; }
        public LoadStatus Status { get; set; }
                
        
        public HeaterMessage(string time,string iD,Heater heater)
        {
            Time = time;
            ID = iD;
            CurrentPower = heater.CurrentPowerDrawn;
            Status = heater.CurrentStatus;
        }
    }
}