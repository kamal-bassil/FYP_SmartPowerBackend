using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class EDLCollection : CollectionInterface<EDL>
{
    public EDLCollection(IMongoDatabase dataBase)
    {
        CollectionName = "EDLSource";
        DataBase = dataBase;
        ConnectToCollection();
    }


}