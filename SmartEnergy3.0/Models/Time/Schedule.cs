using System.Data;

namespace SmartPowerBackend.Models;

public class Schedule
{
    public static Schedule Instance;
    public List<TimeLine> SubscribedTimelines = new List<TimeLine>();
    public int DeltaTimeInSeconds = 10;
    public bool Updatechedule = true;

    public Schedule()
    {
        Instance = this;
        StartCounter();

    }

    public List<StatusUpdate> GetLatestUpdates()
    {
        List<StatusUpdate> statuses = new List<StatusUpdate>();
        foreach (TimeLine timeLine in SubscribedTimelines)
        {
            statuses.Add(timeLine.CurrentUpdate);
        }

        return statuses;
    }
    public List<StatusUpdate> GetLatestLoadsUpdates()
    {
        List<StatusUpdate> statuses = new List<StatusUpdate>();
        foreach (TimeLine timeLine in SubscribedTimelines)
        {
            if (timeLine.GetDevice() is Load)
            {
                statuses.Add(timeLine.CurrentUpdate);
            }
            
        }

        return statuses;
    }
    public List<StatusUpdate> GetLatestSourcesUpdates()
    {
        List<StatusUpdate> statuses = new List<StatusUpdate>();
        foreach (TimeLine timeLine in SubscribedTimelines)
        {
            if (timeLine.GetDevice() is Source)
            {
                statuses.Add(timeLine.CurrentUpdate);
            }
            
        }

        return statuses;
    }

    public void StopTicking()
    {
        Updatechedule = false;
    }
    

    private async Task StartCounter()
    {
        while (Updatechedule)
        {
            UpdateTimeLines();
            await Task.Delay(DeltaTimeInSeconds * 1000);
        }

    }

    public void Subscribe(TimeLine timeLine)
    {
        SubscribedTimelines.Add(timeLine);
    }

    public void UpdateTimeLines()
    {
        foreach (TimeLine timeLine in SubscribedTimelines)
        {
            timeLine.UpdateTimeLine(DateTime.Now.ToString("HH:mm:ss.fff"));
        }  
    }
    
    
}