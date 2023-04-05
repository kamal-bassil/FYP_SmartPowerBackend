using System.Collections.Generic;
using MongoDB.Bson;
using SmartPower2._0.Scripts.Controllers.DataBase;

namespace SmartPowerBackend.Models
{
    public class TimeLine : MongoDBStorable
    {
        public List<StatusUpdate> UpdateTimeline = new List<StatusUpdate>();
        public StatusUpdate CurrentUpdate;
        public string DeviceID;
        public UserRules UserRules;
        
        
        [NonSerialized]
        private Device Device;

        public Device GetDevice()
        {
            return Device;
        }
        public TimeLine(Device device)
        {
            Device = device;
            DeviceID = device.ID;
            Schedule.Instance.Subscribe(this);
        }

        public void UpdateTimeLine(string time)
        {
            CurrentUpdate = Device.GenerateUpdate(time);
            UpdateTimeline.Add(CurrentUpdate);
        }
        public void updateUserRules(UserRules rule)
        {
            UserRules = rule;
        }

        public ObjectId Id { get; set; }
    }
}