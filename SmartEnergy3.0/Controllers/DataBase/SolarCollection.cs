using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class SolarCollection : CollectionInterface<Solar>
{
    public SolarCollection(IMongoDatabase dataBase)
    {
        CollectionName = "SolarSource";
        DataBase = dataBase;
        ConnectToCollection();
    }


}