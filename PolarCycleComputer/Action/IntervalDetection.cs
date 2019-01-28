using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycleComputer.Action
{
  class IntervalDetection
  {
    /// <summary>
    /// detects the total no of interval and return as a form of array
    /// </summary>
    /// <param name="_hrData"></param>
    /// <returns></returns>
    public Dictionary<string, object> GetIntervalDetectedData(Dictionary<string, object> _hrData)
    {
      var splittingString = GetSplittedString(_hrData);
      var list = new Dictionary<string, object>();

      var cadenceData = _hrData["cadence"] as List<string>;
      var altitudeData = _hrData["altitude"] as List<string>;
      var heartRateData = _hrData["heartRate"] as List<string>;
      var wattData = _hrData["watt"] as List<string>;
      var speedData = _hrData["speed"] as List<string>;

      var newCadenceData = new List<string>();
      var newAltitudeData = new List<string>();
      var newHeartRateData = new List<string>();
      var newWattData = new List<string>();
      var newSpeedData = new List<string>();

      int num = 0;

      for (int i = 0; i < splittingString.Count; i++)
      {
        num++;
        string[] parts = splittingString[i].Split('-');
        int index = Convert.ToInt32(parts[0]);
        int count = Convert.ToInt32(parts[1]);
        int diff = count - index;

        for (int j = index; j < count; j++)
        {
          newCadenceData.Add(cadenceData[j]);
          newAltitudeData.Add(altitudeData[j]);
          newHeartRateData.Add(heartRateData[j]);
          newWattData.Add(wattData[j]);
          newSpeedData.Add(speedData[j]);
        }

        var listData = new Dictionary<string, List<string>>();
        listData.Add("cadence", newCadenceData);
        listData.Add("altitude", newAltitudeData);
        listData.Add("heartRate", newHeartRateData);
        listData.Add("watt", newWattData);
        listData.Add("speed", newSpeedData);

        list.Add("data" + num, listData);

        newCadenceData = new List<string>();
        newAltitudeData = new List<string>();
        newHeartRateData = new List<string>();
        newWattData = new List<string>();
        newSpeedData = new List<string>();
      }
      
      return list;
    }


    /// <summary>
    /// detects interval from file
    /// </summary>
    /// <param name="_hrData"></param>
    /// <returns></returns>
    public List<string> GetSplittedString(Dictionary<string, object> _hrData)
    {
      var speedData = _hrData["speed"] as List<string>;
      var index = new List<int>();
      var splittingInt = new List<string>();

      for (int i = 0; i < speedData.Count; i++)
      {
        if (speedData[i] == "0")
        {
          index.Add(i);
        }
      }

      if (index[0] != 0)
      {
        splittingInt.Add("0-" + (index[0] - 1).ToString());
      }

      for (int i = 0; i < index.Count; i++)
      {
        string splitString = "";

        try
        {
          if (index[i + 1] - index[i] != 1)
          {
            splitString = (index[i] + 1).ToString() + "-" + (index[i + 1] - 1).ToString();
            splittingInt.Add(splitString);
          }
        } catch(ArgumentOutOfRangeException e)
        {
          Console.WriteLine(e.Message);
        }
      }
      
      return splittingInt;
    }
  }
}
