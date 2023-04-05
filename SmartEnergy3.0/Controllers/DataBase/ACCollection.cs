using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class ACCollection : CollectionInterface<AC>
{
    public ACCollection(IMongoDatabase dataBase)
    {
        CollectionName = "ACLoad";
        DataBase = dataBase;
        ConnectToCollection();
    }


}
