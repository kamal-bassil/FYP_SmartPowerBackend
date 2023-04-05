using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartPower2._0.Scripts.Controllers.DataBase;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers;


[ApiController]
[Route("Schedule")]
public class ScheduleController
{
    [HttpGet]
    public string Get()
    {
        return JsonConvert.SerializeObject(Schedule.Instance.SubscribedTimelines);
    }

}
[ApiController]
[Route("LatestSourcesUpdate")]
public class LatestSourcesUpdate
{
    [HttpGet]
    public string Get()
    {
        return JsonConvert.SerializeObject(Schedule.Instance.GetLatestSourcesUpdates());
    }

}
[ApiController]
[Route("LatestLoadsUpdate")]
public class LatestLoadsUpdate
{
    [HttpGet]
    public string Get()
    {
        return JsonConvert.SerializeObject(Schedule.Instance.GetLatestLoadsUpdates());
    }

}

[ApiController]
[Route("AddUserRule")]
public class AddNewScheduleRule : ControllerBase
{
    

    [HttpPost]
    public bool Post([FromBody]dynamic objer)
    {
        
        Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(objer.ToString());
        while (dictionary["ID"] == null){
            Console.WriteLine("waiting cause no ID");
        }
        string ID = dictionary["ID"].ToString();
        string encodedSchedule = dictionary["Schedule"].ToString();
        UserRules rule = new UserRules(encodedSchedule);
        DeviceTracker.Tracker.FindDeviceByID(ID).TimeLine.UserRules=rule;
        return true;


    }



     
     
}
[ApiController]
[Route("GenerateUserPreferences")]
public class GenerateUserPreferences : ControllerBase
{
    

    [HttpGet]
    public bool Get()
    {

        new UserRuleSimulator(Schedule.Instance.SubscribedTimelines.Select(x => x.UserRules).ToList());
        return true;


    }



     
     
}
[ApiController]
[Route("test")]
public class Test
{
    [HttpGet]
    public string Get()
    {
        DataBaseController.Instance.DeviceNameCollection.AddDevicweNameID(new DeviceNameID(){ID = "2323123", Name = "Heater"});
        return "badboi";
    }

}
