using MongoDB.Bson;
using MongoDB.Driver;

namespace SmartPower2._0.Scripts.Controllers.DataBase;

public abstract class CollectionInterface<T> where T : MongoDBStorable
{
    public string CollectionName { get; set; }
    public IMongoDatabase DataBase { get; set; }
    
    public IMongoCollection<T> Collection;

    public void ConnectToCollection()
    {
        try
        {
            var collectionExists = DataBase.ListCollections(new ListCollectionsOptions
            {
                Filter = Builders<BsonDocument>.Filter.Eq("name", CollectionName)
            }).Any();

            if (collectionExists)
            {
                Console.WriteLine("The collection exists.");
                Collection= DataBase.GetCollection<T>(CollectionName);
            }
            else
            {
                Console.WriteLine("Could not find collection creating it...");
                DataBase.CreateCollection(CollectionName);
                Collection = DataBase.GetCollection<T>(CollectionName);
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            
        }
    }

    public void InsertElement(T element)
    {
        Collection.InsertOne(element);
    }

    public T GetElementByID(string ID)
    {
        var objectId = new ObjectId(ID);
        var filter = Builders<T>.Filter.Eq(x => x.Id, objectId);
        return Collection.Find(filter).FirstOrDefault();
    }

}