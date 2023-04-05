using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class HeaterCollection : CollectionInterface<Heater>
{
    public HeaterCollection(IMongoDatabase dataBase)
    {
        CollectionName = "HeaterLoad";
        DataBase = dataBase;
        ConnectToCollection();
    }


}