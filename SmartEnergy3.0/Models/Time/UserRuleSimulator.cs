using MathNet.Numerics.Distributions;
namespace SmartPowerBackend.Models;

public class UserRuleSimulator
{
    public static List<UserRuleSimulator> Generations = new List<UserRuleSimulator>();
    private List<UserRules> BaseRules = new List<UserRules>();
    private List<float> RuleVariance = new List<float>();

    #region LoadVariances
    private static double _ACVar=100f;
    private static double _ECVar=200f;
    private static double _HeaterVar=50f;
    private static double _WaterHeaterVar=0.9f;
    private static double _ACVarDuration=2f;
    private static double _ECVarDuration=1f;
    private static double _HeaterVarDuration=0.8f;
    private static double _WaterHeaterVarDuration=0.5f;
    #endregion
    
    private List<List<UserRules>> SimulatedRules;

    public UserRuleSimulator(List<UserRules> baseRules,int samples =100)
    {
        Generations.Add(this);
        BaseRules = baseRules;
        _GenerateVarianceForLoads(samples);

    }

    private void _GenerateVarianceForLoads(int samples)
    {
        List<List<UserRules>> output = new List<List<UserRules>>();
        List<UserRules> tempRuleBundle;
        TimeWindow[] tempWindowWeek;
        TimeWindow TempWindow;
        foreach (UserRules rule in BaseRules)
        {
            double durationVar;
            double translationVar;
            tempRuleBundle = new List<UserRules>(samples);
            if (rule.Device.Type == LoadTypes.WaterHeater)
            {
                durationVar = _WaterHeaterVarDuration;
                translationVar = _WaterHeaterVar;
            }else if (rule.Device.Type == LoadTypes.AC)
            {
                durationVar = _ACVarDuration;
                translationVar = _ACVar;
            }else if (rule.Device.Type == LoadTypes.Heater)
            {
                durationVar = _HeaterVarDuration;
                translationVar = _HeaterVar;
            }
            else if (rule.Device.Type ==LoadTypes.ElectricCar)
            {
                durationVar = _ECVarDuration;
                translationVar = _ECVar;
            }
            else
            {
                Console.WriteLine("ERROR");
                return;
            }
            double k = Math.Pow(1, 2) / durationVar;
            double theta = durationVar;
            var gammaDist = new Gamma(k, theta);
            double[] sampleDuration;
            var normalDist = new Normal(0, translationVar);

            double[] sampleTranslation;

            
            for (int i = 0; i < samples; i++)
            {
                sampleDuration = gammaDist.Samples().Take(7).ToArray();
                sampleTranslation = normalDist.Samples().Take(7).ToArray();
                tempWindowWeek = new TimeWindow[7];
                for (int j = 0; j < 7; j++)
                {
                    TempWindow = tempWindowWeek[j];
                    TempWindow.Day = rule.TimeWindows[j].Day;
                    TempWindow.Start = rule.TimeWindows[j].Start + (int)sampleTranslation[j];
                    if (TempWindow.Start<0)
                    {
                        TempWindow.Start = 0;
                    }

                    TempWindow.End =  (int)((rule.TimeWindows[j].End - rule.TimeWindows[j].Start) * sampleDuration[j] +
                                            rule.TimeWindows[j].Start);
                    if (TempWindow.End>1440)
                    {
                        TempWindow.End = 1440;
                    }
                }
                tempRuleBundle.Add(new UserRules(tempWindowWeek,rule.Device));
            }
            output.Add(tempRuleBundle);
            
            
        }

        SimulatedRules = output;
    }
    
    
    
}