using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarCycleComputer.Action
{
  class AdvanceMetricsCalculation
  {
    public double CalculateNormalizedPower(Dictionary<string, object> list)
    {
      List<double> powerSumList = new List<double>();

      var powerList = list["watt"] as List<string>;
      int count = 0;
      double powerSum = 0;

      for (int i = 0; i < powerList.Count; i++)
      {
        count++;
        double power = Convert.ToDouble(powerList[i]);
        powerSum += power;

        if (count == 30)
        {
          powerSumList.Add(NthRoot(powerSum, 4) * 0.5);
          count = 0;
        }
      }

      var result = Summary.FindSum(powerSumList.Select(p => p.ToString()).ToList());

      return result;
    }

    public double CalculateFunctionalThresholdPower(Dictionary<string, object> list) => Summary.FindAverage((List<string>)list["watt"]) * 0.95;

    public double CalculateIntensityFactor(Dictionary<string, object> list) => CalculateNormalizedPower(list) / CalculateFunctionalThresholdPower(list);

    public double CalculateTss(double ftp, double avgPower) => (avgPower / ftp) * 100;

    public double CalculatePowerBalance(Dictionary<string, object> list) => Summary.FindAverage(list["speed"] as List<string>);
    public static double NthRoot(double A, int N) => Math.Pow(A, 1.0 / N);
  }
}
