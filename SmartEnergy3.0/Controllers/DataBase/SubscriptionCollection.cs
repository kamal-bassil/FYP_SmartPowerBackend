using MongoDB.Bson;
using MongoDB.Driver;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public class SubscriptionCollection : CollectionInterface<Subscription>
{
    public SubscriptionCollection(IMongoDatabase dataBase)
    {
        CollectionName = "SubscriptionSource";
        DataBase = dataBase;
        ConnectToCollection();
    }


}