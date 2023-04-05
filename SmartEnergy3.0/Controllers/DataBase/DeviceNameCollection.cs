using MongoDB.Bson;
using MongoDB.Driver;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class DeviceNameCollection : CollectionInterface<DeviceNameID>
{
    public DeviceNameCollection(IMongoDatabase dataBase)
    {
        CollectionName = "DeviceNameID";
        DataBase = dataBase;
        ConnectToCollection();
    }
    public void AddDevicweNameID(DeviceNameID deviceNameId)
    {
        
        Collection.InsertOne(deviceNameId);
        
    }


}
public class DeviceNameID : MongoDBStorable
{
    public string Name { get; set; }
    public string ID { get; set; }
    public ObjectId Id { get; set; }
}