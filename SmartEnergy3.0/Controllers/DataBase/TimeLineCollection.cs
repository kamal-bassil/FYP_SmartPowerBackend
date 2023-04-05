using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class TimeLineCollection : CollectionInterface<TimeLine>
{
    public TimeLineCollection(IMongoDatabase dataBase)
    {
        CollectionName = "TimeLines";
        DataBase = dataBase;
        ConnectToCollection();
    }


}