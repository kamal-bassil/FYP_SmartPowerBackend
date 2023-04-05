namespace SmartPower2._0.Scripts.Controllers.DataBase;
using MongoDB.Bson;

public interface MongoDBStorable
{
    public ObjectId Id { get; set; }
}
