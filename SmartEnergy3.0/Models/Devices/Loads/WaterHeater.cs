using System;
using System.Drawing;
using MongoDB.Bson;

namespace SmartPowerBackend.Models
{
    public class WaterHeater: Load
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
            UpdateType = typeof(WaterHeater);
        }

        public StatusUpdate GenerateUpdate(string time)
        {
            return new WaterHeaterMessage(time, ID, this);
        }

        public WifiSwitch Switch { get; set; }
        public LoadTypes Type { get; set; }
        public int Priority { get; set; }

        public WaterHeater(string IP, string DeviceName,int priority,float power)
        {
            Name = DeviceName;
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
            Type = LoadTypes.WaterHeater;
            SetMessageType();
            CurrentPowerDrawn = -1f;


        }

        public ObjectId Id { get; set; }
    }

    public class WaterHeaterMessage : StatusUpdate
    {
        public string Time { get; set; }
        public string ID { get; set; }
        
        public float CurrentPower { get; set; }
        public LoadStatus Status { get; set; }
                
        
        public WaterHeaterMessage(string time,string iD,WaterHeater waterheater)
        {
            Time = time;
            ID = iD;
            CurrentPower = waterheater.CurrentPowerDrawn;
            Status = waterheater.CurrentStatus;
        }
    }
    
}