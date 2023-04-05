using SmartPower2._0.Scripts.Controllers.DataBase;
using SmartPowerBackend.Models;

namespace SmartPower2._0.Scripts.Controllers;

public class MainController
{
    public DeviceTracker DeviceTracker;
    public Schedule Schedule;
    public UserScheduleController UserScheduleController;
    public DataBaseController DataBaseController;
    public MainController()
    {
        Schedule =new Schedule();
        DataBaseController = new DataBaseController();
        DeviceTracker=new DeviceTracker();
        //UserScheduleController=new UserScheduleController();
        



    }
}