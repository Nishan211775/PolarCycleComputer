using PolarCycleComputer.Action;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolarCycleComputer
{
  public partial class Form1 : Form
  {
    private Dictionary<string, List<string>> _hrData = new Dictionary<string, List<string>>();
    private Dictionary<string, string> _param = new Dictionary<string, string>();
    private int count = 0;
    private List<int> smode = new List<int>();
    private FileConvertor c = new FileConvertor();

    public Form1()
    {
      InitializeComponent();
      this.CenterToScreen();
      InitGrid();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DialogResult result = openFileDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        Cursor.Current = Cursors.WaitCursor;
        string text = File.ReadAllText(openFileDialog1.FileName);
        Dictionary<string, object> _hrData = new TableFiller().FillTable(text, dataGridView1);

        var param = _hrData["params"] as Dictionary<string, string>;
        var sMode = param["SMode"];
        for (int i = 0; i < sMode.Length; i++)
        {
          smode.Add((int)Char.GetNumericValue(param["SMode"][i]));
        }

        if (smode[0] == 0)
        {
          dataGridView1.Columns[0].Visible = false;
        }
        else if (smode[1] == 0)
        {
          dataGridView1.Columns[1].Visible = false;
        }
        else if (smode[2] == 0)
        {
          dataGridView1.Columns[2].Visible = false;
        }
        else if (smode[3] == 0)
        {
          dataGridView1.Columns[3].Visible = false;
        }
        else if (smode[4] == 0)
        {
          dataGridView1.Columns[4].Visible = false;
        }

        dataGridView2.Rows.Clear();
        dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(_hrData, _hrData["params"] as Dictionary<string, string>, _hrData["endTime"] as string));








        //_param = new Dictionary<string, string>();
        //_hrData = new Dictionary<string, List<string>>();
        //string text = File.ReadAllText(openFileDialog1.FileName);
        //var splittedString = c.SplitString(text);

        //var splittedParamsData = c.SplitStringByEnter(splittedString[0]);

        //foreach (var data in splittedParamsData)
        //{
        //  if (data != "\r")
        //  {
        //    string[] parts = data.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
        //    _param.Add(parts[0], parts[1]);
        //  }
        //}


        //lblStartTime.Text = "Start Time" + "= " + _param["StartTime"];
        //lblInterval.Text = "Interval" + "= " + Regex.Replace(_param["Interval"], @"\t|\n|\r", "") + " sec";
        //lblMonitor.Text = "Monitor" + "= " + _param["Monitor"];
        //lblSMode.Text = "SMode" + "= " + _param["SMode"];
        //lblDate.Text = "Date" + "= " + Summary.ConvertToDate(_param["Date"]);
        //lblLength.Text = "Length" + "= " + _param["Length"];
        //lblWeight.Text = "Weight" + "= " + Regex.Replace(_param["Weight"], @"\t|\n|\r", "") + " kg";

        //var sMode = _param["SMode"];
        //for (int i = 0; i < sMode.Length; i++)
        //{
        //  smode.Add((int)Char.GetNumericValue(_param["SMode"][i]));
        //}

        //List<string> cadence = new List<string>();
        //List<string> altitude = new List<string>();
        //List<string> heartRate = new List<string>();
        //List<string> watt = new List<string>();
        //List<string> speed = new List<string>();
        //List<string> time = new List<string>();

        ////adding data for datagrid
        //var splittedHrData = c.SplitStringByEnter(splittedString[11]);
        //DateTime dateTime = DateTime.Parse(_param["StartTime"]);

        //int temp = 0;
        //foreach (var data in splittedHrData)
        //{
        //  temp++;
        //  var value = c.SplitStringBySpace(data);

        //  if (value.Length >= 5)
        //  {
        //    cadence.Add(value[0]);
        //    altitude.Add(value[1]);
        //    heartRate.Add(value[2]);
        //    watt.Add(value[3]);
        //    speed.Add(value[4]);

        //    if (temp > 2) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
        //    endTime = dateTime.TimeOfDay.ToString();

        //    List<string> hrData = new List<string>();
        //    hrData.Add(value[0]);
        //    hrData.Add(value[1]);
        //    hrData.Add(value[2]);
        //    hrData.Add(value[3]);
        //    hrData.Add(value[4]);
        //    hrData.Add(dateTime.TimeOfDay.ToString());

        //    dataGridView1.Rows.Add(hrData.ToArray());
        //  }
        //}

        //_hrData.Add("cadence", cadence);
        //_hrData.Add("altitude", altitude);
        //_hrData.Add("heartRate", heartRate);
        //_hrData.Add("watt", watt);
        //_hrData.Add("speed", speed);

        //if (smode[0] == 0)
        //{
        //  dataGridView1.Columns[0].Visible = false;
        //}
        //else if (smode[1] == 0)
        //{
        //  dataGridView1.Columns[1].Visible = false;
        //}
        //else if (smode[2] == 0)
        //{
        //  dataGridView1.Columns[2].Visible = false;
        //}
        //else if (smode[3] == 0)
        //{
        //  dataGridView1.Columns[3].Visible = false;
        //}
        //else if (smode[4] == 0)
        //{
        //  dataGridView1.Columns[4].Visible = false;
        //}

        //dataGridView2.Rows.Clear();
        //dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(_hrData, _param, endTime));
      }
    }

    private void InitGrid()
    {
      dataGridView1.ColumnCount = 6;
      dataGridView1.Columns[0].Name = "Cadence(RPM)";
      dataGridView1.Columns[1].Name = "Altitude(m/ft)";
      dataGridView1.Columns[2].Name = "Heart rate(bpm)";
      dataGridView1.Columns[3].Name = "Power(watt)";
      dataGridView1.Columns[4].Name = "Speed(Mile/hr)";
      dataGridView1.Columns[5].Name = "Time";

      dataGridView2.ColumnCount = 10;
      dataGridView2.Columns[0].Name = "Total distance covered";
      dataGridView2.Columns[1].Name = "Average speed(km/hr)";
      dataGridView2.Columns[2].Name = "Maximum speed(km/hr)";
      dataGridView2.Columns[3].Name = "Average heart rate(bpm)";
      dataGridView2.Columns[4].Name = "Maximum heart rate(bpm)";
      dataGridView2.Columns[5].Name = "Minimum heart rate(bpm)";
      dataGridView2.Columns[6].Name = "Average power(watt)";
      dataGridView2.Columns[7].Name = "Maximum power(watt)";
      dataGridView2.Columns[8].Name = "Average altitude(RPM)";
      dataGridView2.Columns[9].Name = "Maximum altitude(RPM)";

    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (_hrData.Count < 1)
      {
        MessageBox.Show("Please select a file first");
      }
      else
      {
        GraphWindow._hrData = _hrData;
        new GraphWindow().Show();
      }
    }

    private void button3_Click_1(object sender, EventArgs e)
    {
      if (_hrData.Count < 1)
      {
        MessageBox.Show("Please select a file first");
      }
      else
      {
        PersonalGraph._hrData = _hrData;
        new PersonalGraph().Show();
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void CalculateSpeed(string type)
    {
      if (_hrData.Count > 0)
      {
        List<string> data = new List<string>();
        if (type == "mile")
        {
          dataGridView1.Columns[4].Name = "Speed(Mile/hr)";

          data.Clear();

          for (int i = 0; i < _hrData["cadence"].Count; i++)
          {
            string temp = (Convert.ToDouble(_hrData["speed"][i]) / 1.60934).ToString();
            data.Add(temp);
          }

          //_hrData["speed"].Clear();
          _hrData["speed"] = data;

          dataGridView1.Rows.Clear();
          DateTime dateTime = DateTime.Parse(_param["StartTime"]);
          for (int i = 0; i < _hrData["cadence"].Count; i++)
          {
            if (i > 0) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
            string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i], dateTime.TimeOfDay.ToString() };
            dataGridView1.Rows.Add(hrData);
          }
        }
        else
        {
          dataGridView1.Columns[4].Name = "Speed(km/hr)";

          data.Clear();
          for (int i = 0; i < _hrData["cadence"].Count; i++)
          {
            string temp = (Convert.ToDouble(_hrData["speed"][i]) * 1.60934).ToString();
            data.Add(temp);
          }

          //_hrData["speed"].Clear();
          _hrData["speed"] = data;

          dataGridView1.Rows.Clear();

          DateTime dateTime = DateTime.Parse(_param["StartTime"]);
          for (int i = 0; i < _hrData["cadence"].Count; i++)
          {
            if (i > 0) dateTime = dateTime.AddSeconds(Convert.ToInt32(_param["Interval"]));
            string[] hrData = new string[] { _hrData["cadence"][i], _hrData["altitude"][i], _hrData["heartRate"][i], _hrData["watt"][i], _hrData["speed"][i], dateTime.TimeOfDay.ToString() };
            dataGridView1.Rows.Add(hrData);
          }
        }
      }
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void button4_Click(object sender, EventArgs e)
    {
      count++;
      if (count > 1) CalculateSpeed("mile");
    }

    private void button5_Click(object sender, EventArgs e)
    {
      count++;
      CalculateSpeed("km");
    }

    private void button6_Click(object sender, EventArgs e)
    {
      new FileCompare().Show();
    }
  }
}
