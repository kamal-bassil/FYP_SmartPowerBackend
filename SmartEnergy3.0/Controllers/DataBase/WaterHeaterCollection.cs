using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class WaterHeaterCollection : CollectionInterface<WaterHeater>
{
    public WaterHeaterCollection(IMongoDatabase dataBase)
    {
        CollectionName = "WaterHeaterLoad";
        DataBase = dataBase;
        ConnectToCollection();
    }


}