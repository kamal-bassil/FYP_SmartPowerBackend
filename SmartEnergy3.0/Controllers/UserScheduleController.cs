using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;


public class UserScheduleController
{
    public static UserScheduleController Instance;
    public int DeltaTimeInSeconds = 10;

    public UserScheduleController()
    {
        Instance = this;
        StartCounter();

    }
    public async Task ConsoleWeatherAPIAsync(){
        string city = "Beirut";
        string apiKey = "5c10e3430b0969694d65271aceb78b1a";
        string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(apiUrl);
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(json);

        var weather = new Weather
        {
            Description = data.weather[0].description,
            Temperature = data.main.temp
        };

        //Console.WriteLine($"Current weather in {city}: {weather.Description}, {weather.Temperature}°C");


        //Solar information down    
        // string apiKeySolar = "9c601e63d1bb16227b1e7a6515a96a73";
        // double latitude = 33.9803;
        // double longitude = 35.6529;
        // string apiUrlSolar = $"https://api.openweathermap.org/data/2.5/solar_radiation?/forecast?lat={latitude}&lon={longitude}&appid={apiKeySolar}";
        
        // var httpClientSolar = new HttpClient();
        // var responseSolar = await httpClientSolar.GetAsync(apiUrlSolar);
        // var jsonSolar = await responseSolar.Content.ReadAsStringAsync();
        // var dataSolar = JsonConvert.DeserializeObject<dynamic>(jsonSolar);

        // try{
        //     if(dataSolar != null){
        //         Console.WriteLine($"dataSolar {dataSolar}");
        //         var solarData = new SolarData
        //         {
        //             Sunrise = DateTimeOffset.FromUnixTimeSeconds(dataSolar.current.sunrise).LocalDateTime,
        //             Sunset = DateTimeOffset.FromUnixTimeSeconds(dataSolar.current.sunset).LocalDateTime,
        //             SolarNoon = DateTimeOffset.FromUnixTimeSeconds(dataSolar.current.solar_noon).LocalDateTime,
        //             SolarRadiation = dataSolar.current.solar_radiation
        //         };

        //         Console.WriteLine($"Sunrise: {solarData.Sunrise}");
        //         Console.WriteLine($"Sunset: {solarData.Sunset}");
        //         Console.WriteLine($"Solar Noon: {solarData.SolarNoon}");
        //         Console.WriteLine($"Solar Radiation: {solarData.SolarRadiation}");
        //     }else{
        //         Console.WriteLine("datasolar has a null reference");
        //     }
        // }catch (Exception ex)
        //     {
        //         // code to handle the exception
        //         Console.WriteLine("An error occurred: " + ex.Message);
        //     }
        


    
    }
    public void SwitchONScheduledDevices()
    {
        //Console.WriteLine("There are " + Schedule.Instance.SubscribedTimelines.Count + " devices for this user");
        int start,end;
        foreach (TimeLine timeLine in Schedule.Instance.SubscribedTimelines)
        {
            // if (IsBetweenWorkingHours(timeLine.UserRules.getToday().Start, timeLine.UserRules.getToday().End))
            // {
            //     Console.WriteLine(timeLine.DeviceID + " should be ON");
            // }
            try
            {
                // code that might throw an exception
                if (timeLine.UserRules != null){
                    if(timeLine.UserRules.TimeWindows[(int)DateTime.Now.DayOfWeek] != null){
                        start = timeLine.UserRules.TimeWindows[(int)DateTime.Now.DayOfWeek].Start;
                        end = timeLine.UserRules.TimeWindows[(int)DateTime.Now.DayOfWeek].End;
                        Console.WriteLine("Today is: " + (int)DateTime.Now.DayOfWeek);
                        Console.WriteLine("Device ID: " + timeLine.DeviceID);
                        Console.WriteLine("Start of device is: " + start);
                        Console.WriteLine("End of device is: " + end);
                        Console.WriteLine("Should I be on ?: " + IsBetweenWorkingHours(start,end));
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                // code to handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            
        }
    }

    public  bool IsBetweenWorkingHours(int Start, int End)
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
    }
    private async Task StartCounter()
    {
        while (true)
        {
            SwitchONScheduledDevices();
            ConsoleWeatherAPIAsync();
            await Task.Delay(DeltaTimeInSeconds * 1000);
        }

    }
}

public class Weather
{
    public string Description { get; set; }
    public double Temperature { get; set; }
}
public class SolarData
{
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public DateTime SolarNoon { get; set; }
    public double SolarRadiation { get; set; }
}
