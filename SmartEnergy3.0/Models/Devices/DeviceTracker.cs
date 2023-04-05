using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using SmartPower2._0.Scripts.Controllers.DataBase;

namespace SmartPowerBackend.Models
{
    public class DeviceTracker
    {
        public static DeviceTracker Tracker;
        public List<Source> Sources = new List<Source>();
        public List<Load> Loads = new List<Load>();
        public List<Device> Devices = new List<Device>();

        public DeviceTracker()
        {
            //Replace with stuff from database
            Tracker = this;
            _TrackDevice(new ElectricCar("69","electricBroz",4,3000f));
            _TrackDevice(new EDL("edl"));
            _TrackDevice(new Subscription("SUbs"));
            _TrackDevice(new Solar("sunnyBoi","69.69.69.69"));
            _TrackDevice(new AC("12121","coldboi",1,3500f));
            _TrackDevice(new Heater("12121","hotboi",1,1500f));
            _TrackDevice(new WaterHeater("12121","coldboi",1,4000f));
            _TrackDevice(new Batteries("sunnyBoi",10000f));
            
            
        }
        private void _TrackDevice(Device x)
        {
            if(x is Source)
                Sources.Add((Source)x);
            if(x is Load)
                Loads.Add((Load)x);
            Devices.Add(x);
        }

        private void _TrackLoad(Load x)
        {
            Loads.Add(x);
        }

        public bool RemoveLoad(string encguid)
        {
            try
            {
                Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(encguid);
                Console.WriteLine(dictionary["ID"]);
                Console.WriteLine(JsonConvert.SerializeObject(Loads));
                return Loads.Remove(Loads.FirstOrDefault(x => x.ID == dictionary["ID"].ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
            
        }

        public Device FindDeviceByID(string ID)
        {
            return Devices.FirstOrDefault(x => x.ID == ID);
        }
        

        public bool AddLoad(string encLoad)
        {
            try
            {
                Load load;
                Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(encLoad);
                string type = dictionary["Type"].ToString();
                string ip = dictionary["IP"].ToString();
                string name = dictionary["Name"].ToString();
                int priority= int.Parse(dictionary["Priority"].ToString());
                float power= float.Parse(dictionary["Power"].ToString());
                
                switch (type)
                {
                    case "-1":
                        Console.WriteLine("Unsupported load type Error");
                        
                        return false;
                    case "1":
                        load = new Heater(ip,name,priority,power);
                        DataBaseController.Instance.HeaterCollection.InsertElement((Heater) load);
                        Console.WriteLine("adding heater");
                        break;
                    case "2":
                        load = new WaterHeater(ip,name,priority,power);
                        DataBaseController.Instance.WHCollection.InsertElement((WaterHeater)load);
                        Console.WriteLine("adding waterheater");
                        break;
                    case "3":
                        load = new AC(ip,name,priority,power);
                        DataBaseController.Instance.ACCollection.InsertElement((AC)load);
                        Console.WriteLine("adding AC");
                        break;
                    case "4":
                        load = new ElectricCar(ip,name,priority,power);
                        DataBaseController.Instance.ECCollection.InsertElement((ElectricCar)load);
                        Console.WriteLine("adding Electric car");
                        break;
                    default:
                        return false;
                }
                _TrackDevice(load);
                
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to decode Load" + e);
                return false;
                
                
            }
            
            
        }
        
    }
}