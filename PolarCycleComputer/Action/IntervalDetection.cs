using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycleComputer.Action
{
  class IntervalDetection
  {

    public Dictionary<string, object> GetIntervalDetectedData(Dictionary<string, object> _hrData)
    {
      var splittingString = GetSplittedString(_hrData);

      splittingString.ForEach(res =>
      {

      });

      return null;
    }

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
