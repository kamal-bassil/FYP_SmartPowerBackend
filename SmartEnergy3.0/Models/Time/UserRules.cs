using MongoDB.Bson;
using Newtonsoft.Json;
using SmartPower2._0.Scripts.Controllers.DataBase;

namespace SmartPowerBackend.Models;

public class UserRules : MongoDBStorable
{
    [NonSerialized]
    public Load Device;
    public TimeWindow[] TimeWindows = new TimeWindow[7];
    public LoadStatus Status = LoadStatus.On;

    public UserRules(TimeWindow[] timeWindows, Load device)
    {
        TimeWindows = timeWindows;
        Device = device;
    }
    public UserRules(string encodedTimes)
    {
        try
        {
            int startTime;
            int endTime;
            Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(encodedTimes);
            Dictionary<string, object> dictionary2;
            foreach (string VARIABLE in dictionary.Keys)
            {
                Console.WriteLine(VARIABLE);
            }

            TimeWindow.DayOfTheWeek enumValue;
            Console.WriteLine(encodedTimes);
            foreach (int value in Enum.GetValues(typeof(TimeWindow.DayOfTheWeek))) {
                
                enumValue = (TimeWindow.DayOfTheWeek)value;
                string x = dictionary[$"{enumValue}"].ToString();
                Console.WriteLine($"{enumValue}");
                dictionary2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(x);
                
                if(dictionary2["Start"].ToString()!=""){                
                    startTime = int.Parse(dictionary2["Start"].ToString().Split(':')[0])*60+int.Parse(dictionary2["Start"].ToString().Split(':')[1]);
                    endTime = int.Parse(dictionary2["End"].ToString().Split(':')[0])*60+int.Parse(dictionary2["End"].ToString().Split(':')[1]);
                    TimeWindows[value] = new TimeWindow()
                    {
                        Day = enumValue,
                        Start = startTime,
                        End = endTime

                    };
                    
                }


            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    public (int Start, int End) GetToday()
    {
        int Start = 0;
        int End = 0;
        TimeWindow.DayOfTheWeek day = (TimeWindow.DayOfTheWeek)1;
        foreach (TimeWindow timeWindow in TimeWindows)
        {
            if (timeWindow.Day == day)
            {
                Start = timeWindow.Start;
                End = timeWindow.End;
                break;
            }
        }

        return (Start, End);
    }

    public ObjectId Id { get; set; }
}

public class TimeWindow
{
    public int Start;
    public int End;
    public DayOfTheWeek Day;
    
    public enum DayOfTheWeek
    {
        Sunday=0,
        Monday =1,
        Tuesday=2,
        Wednesday=3,
        Thursday=4,
        Friday=5,
        Saturday=6,
        
    }
}
