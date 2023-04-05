using System;
using MongoDB.Bson;

namespace SmartPowerBackend.Models
{
    public class Subscription: Source
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
            UpdateType = typeof(SubscriptionMessage);
        }

        public StatusUpdate GenerateUpdate(string time)
        {
            return new SubscriptionMessage(time, ID, this);
        }

        public float PriceOfWatt { get; set; }
        
        public Subscription(string name)
        {
            Name = name;
            ID = Guid.NewGuid().ToString();
            CurrentStatus = SourceStatus.Unknown;
            TimeLine = new TimeLine(this);
            Type = SourceTypes.Subscription;
            PriceOfWatt = 5f;
            SetMessageType();

        }

        public ObjectId Id { get; set; }
    }
    public class SubscriptionMessage : StatusUpdate
    {
        public string Time { get; set; }
        public string ID { get; set; }
        public float CurrentPower { get; set; }
        public SourceStatus Status { get; set; }
        

        public SubscriptionMessage(string time,string iD,Subscription sub)
        {
            Time = time;
            ID = iD;
            CurrentPower = sub.CurrentPowerDrawn;
            Status = sub.CurrentStatus;
        }
    }
}