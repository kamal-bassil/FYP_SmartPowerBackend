using Newtonsoft.Json;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers;

using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


[ApiController]
[Route("Loads")]
public class LoadsEndpoint : ControllerBase
{

    [HttpGet]
    public string Get()//used for testing
    {
        return JsonConvert.SerializeObject(DeviceTracker.Tracker.Loads);
    }

}
[ApiController]
[Route("Loads/Add")]
public class LoadsAddEndpoint : ControllerBase
{
    

    [HttpPost]
    public bool Post([FromBody]dynamic objer)
    {
        Console.WriteLine(objer.ToString());
        return DeviceTracker.Tracker.AddLoad(objer.ToString());
         
          

    }

    

     
     
}
[ApiController]
[Route("Loads/Remove")]
public class LoadsRemoveEndpoint : ControllerBase
{
    

    [HttpPost]
    public bool Post([FromBody]dynamic objer)
    {
        Console.WriteLine(objer.ToString());
        return DeviceTracker.Tracker.RemoveLoad(objer.ToString());
         
          

    }
     
}
/*public void SwitchONScheduledDevices()
{
    foreach (TimeLine timeLine in Schedule.Instance.SubscribedTimelines)
        if (IsBetweenWorkingHours(timeLine.UserRules.getToday().Start, timeLine.UserRules.getToday().End))
            console.writeLine(timeLine.DeviceID + " should be ON");
}

public bool IsBetweenWorkingHours(int Start, int End)
{
    // make now into an int of the minute of the day
    int now = (int)DateTime.Now.TimeOfDay.TotalMinutes;

    if (now >= Start && now <= End)
    {
        return true;
    }
    else
    {
        return false;
    }
}*/






