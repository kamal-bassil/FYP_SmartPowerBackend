namespace SmartPower2._0.Scripts.Controllers.DataBase;

using MongoDB.Driver;

public class DataBaseController
{
    public static DataBaseController Instance;
    public string URL;
    public string DBName;
    public MongoClient DBClient;
    public IMongoDatabase Database;
    
    public bool Connected = false;
    public DeviceNameCollection DeviceNameCollection;
    public ECCollection ECCollection;
    public ACCollection ACCollection;
    public WaterHeaterCollection WHCollection;
    public HeaterCollection HeaterCollection;


    public DataBaseController(string dataBaseURL = "mongodb://localhost:27017",string dBName = "SmartEnergy")
    {
        Instance = this;
        URL = dataBaseURL;
        DBName = dBName;
        Connected= _ConnectToDataBase();
        Console.WriteLine(Connected);
        _InitiateCollectionControllers();
    }

    private bool _ConnectToDataBase()
    {
        try
        {
            DBClient = new MongoClient(URL);
            Database = DBClient.GetDatabase(DBName);
            Console.WriteLine("Connected successfully to DB @ " + URL);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not connect to database server endpoint" + e);
            return false;
        }
    }

    private bool _InitiateCollectionControllers()
    {
        try
        {
            DeviceNameCollection = new DeviceNameCollection(Database);
            ECCollection = new ECCollection(Database);
            ACCollection = new ACCollection(Database);
            WHCollection = new WaterHeaterCollection(Database);
            HeaterCollection = new HeaterCollection(Database);
            //ToDo : instentiate the collections
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    
}