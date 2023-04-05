using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class BatteriesCollection : CollectionInterface<Batteries>
{
    public BatteriesCollection(IMongoDatabase dataBase)
    {
        CollectionName = "BatteriesSource";
        DataBase = dataBase;
        ConnectToCollection();
    }


}