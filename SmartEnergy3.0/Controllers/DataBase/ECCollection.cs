using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class ECCollection : CollectionInterface<ElectricCar>
{
    public ECCollection(IMongoDatabase dataBase)
    {
        CollectionName = "ECLoad";
        DataBase = dataBase;
        ConnectToCollection();
    }


}