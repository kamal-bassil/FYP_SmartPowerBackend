using System;
using MongoDB.Bson;

namespace SmartPowerBackend.Models
{
    public class EDL: Source
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
            UpdateType = typeof(EDLMessage);
        }

        public StatusUpdate GenerateUpdate(string time)
        {
            return new EDLMessage(time, ID, this);
        }

        public float PriceOfWatt { get; set; }

        public EDL(string name)
        {
            Name = name;
            ID = Guid.NewGuid().ToString();
            CurrentStatus = SourceStatus.Unknown;
            TimeLine = new TimeLine(this);
            Type = SourceTypes.EDL;
            PriceOfWatt = 0.1f;
            SetMessageType();

        }

        public ObjectId Id { get; set; }
    }
    public class EDLMessage : StatusUpdate
    {
        public string Time { get; set; }
        public string ID { get; set; }
        public float CurrentPower { get; set; }
        public SourceStatus Status { get; set; }
        

        public EDLMessage(string time,string iD,EDL batteries)
        {
            Time = time;
            ID = iD;
            CurrentPower = batteries.CurrentPowerDrawn;
            Status = batteries.CurrentStatus;
            


        }
    }
}