using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers;

[ApiController]
[Route("Sources")]
public class SourceController
{
    [HttpGet]
    public string Get()
    {
        return JsonConvert.SerializeObject(DeviceTracker.Tracker.Sources);
    }
    
}