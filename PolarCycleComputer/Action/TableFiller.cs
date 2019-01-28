using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolarCycleComputer.Action
{
  public class TableFiller
  {
    /// <summary>
    /// used to dispaly data in summary table
    /// </summary>
    /// <param name="_hrData"></param>
    /// <param name="endTime"></param>
    /// <param name="_param"></param>
    /// <returns></returns>
    public string[] FillDataInSumaryTable(Dictionary<string, object> _hrData, string endTime, Dictionary<string, string> _param = null)
    {
      //double startDate = TimeSpan.Parse(_param["StartTime"]).TotalSeconds;
      double startDate = 0;

      try
      {
        startDate = TimeSpan.Parse(_param["StartTime"]).TotalSeconds;
      }
      catch (Exception e)
      {
        startDate = 0;
      }

      double endDate = !string.IsNullOrEmpty(endTime) ? TimeSpan.Parse(endTime).TotalSeconds : 0;
      double totalTime = endDate - startDate;

      string averageSpeed = Summary.FindAverage(_hrData["speed"] as List<string>).ToString();
      string totalDistanceCovered = ((Convert.ToDouble(averageSpeed) * totalTime) / 360).ToString();
      string maxSpeed = Summary.FindMax(_hrData["speed"] as List<string>).ToString();

      string averageHeartRate = Summary.FindAverage(_hrData["heartRate"] as List<string>).ToString();
      string maximumHeartRate = Summary.FindMax(_hrData["heartRate"] as List<string> as List<string>).ToString();
      string minHeartRate = Summary.FindMin(_hrData["heartRate"] as List<string>).ToString();

      string averagePower = Summary.FindAverage(_hrData["watt"] as List<string>).ToString();
      string maxPower = Summary.FindMax(_hrData["watt"] as List<string>).ToString();

      string averageAltitude = Summary.FindAverage(_hrData["altitude"] as List<string>).ToString();
      string maximumAltitude = Summary.FindAverage(_hrData["altitude"] as List<string>).ToString();

      string[] summarydata = new string[] { totalDistanceCovered, averageSpeed, maxSpeed, averageHeartRate, maximumHeartRate, minHeartRate, averagePower, maxPower, averageAltitude, maximumAltitude };

      return summarydata;
    }

    /// <summary>
    /// used to display data into table
    /// </summary>
    /// <param name="text"></param>
    /// <param name="dataGridView"></param>
    /// <returns></returns>
    public Dictionary<string, object> FillTable(string text, DataGridView dataGridView = null)
    {
      FileConvertor c = new FileConvertor();
      List<string> list = new List<string>();
      Dictionary<string, object> _hrData = new Dictionary<string, object>();
      Dictionary<string, string> param = new Dictionary<string, string>();

      var splittedString = c.SplitString(text);

      var splittedParamsData = c.SplitStringByEnter(splittedString[0]);

      foreach (var data in splittedParamsData)
      {
        if (data != "\r")
        {
          string[] parts = data.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
          param.Add(parts[0], parts[1]);
        }
      }

      var splittedHrData = c.SplitStringByEnter(splittedString[11]);

      List<string> cadence = new List<string>();
      List<string> altitude = new List<string>();
      List<string> heartRate = new List<string>();
      List<string> watt = new List<string>();
      List<string> speed = new List<string>();
      List<string> time = new List<string>();
      string endTime = "";

      DateTime dateTime = DateTime.Parse(param["StartTime"]);


      foreach (var data in splittedHrData)
      {
        var value = c.SplitStringBySpace(data);
        if (value.Length >= 5)
        {
          cadence.Add(value[0]);
          altitude.Add(value[1]);
          heartRate.Add(value[2]);
          watt.Add(value[3]);
          speed.Add(value[4]);
          int interval = Convert.ToInt32(param["Interval"]);
          dateTime = dateTime.AddSeconds(Convert.ToInt32(param["Interval"]));
          endTime = dateTime.TimeOfDay.ToString();
          time.Add(endTime);

          List<string> tableData = new List<string>();
          tableData.Add(value[0]);
          tableData.Add(value[1]);
          tableData.Add(value[2]);
          tableData.Add(value[3]);
          tableData.Add(value[4]);
          tableData.Add(dateTime.TimeOfDay.ToString());

          if (dataGridView != null) dataGridView.Rows.Add(tableData.ToArray());
        }
      }

      _hrData.Add("cadence", cadence);
      _hrData.Add("altitude", altitude);
      _hrData.Add("heartRate", heartRate);
      _hrData.Add("watt", watt);
      _hrData.Add("speed", speed);
      _hrData.Add("time", time);
      _hrData.Add("dateTime", dateTime);
      _hrData.Add("endTime", endTime);
      _hrData.Add("params", param);

      return _hrData;
    }

  }
}
