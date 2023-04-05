using System.Data;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using SmartPower2._0.Scripts.Controllers.DataBase;

namespace SmartPowerBackend.Models
{
    public interface Device : MongoDBStorable
    {
        string ID { get; set; }
        string Name { get; set; }
        
        [JsonIgnore][BsonIgnore]
        TimeLine TimeLine { get; set; }
        
        Type UpdateType { get; set; }
        
        void SetMessageType();
        StatusUpdate GenerateUpdate(string time);


    }
}