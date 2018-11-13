using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolarCycleComputer
{
    public partial class Form1 : Form
    {
        private Dictionary<string, List<string>> _hrData = new Dictionary<string, List<string>>();
        private Dictionary<string, string> _param = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            InitGrid();
        }

        private string[] SplitString(string text)
        {
            var splitString = new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};

            var splittedText = text.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            return splittedText;
        }

        private string[] SplitStringByEnter(string text)
        {
            return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] SplitStringBySpace(string text)
        {
            var formattedText = string.Join(" ", text.Split().Where(x => x != ""));
            return formattedText.Split(' ');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                _param = new Dictionary<string, string>();
                _hrData = new Dictionary<string, List<string>>();
                string text = File.ReadAllText(openFileDialog1.FileName);
                var splittedString = SplitString(text);

                var splittedParamsData = SplitStringByEnter(splittedString[0]);

                foreach (var data in splittedParamsData)
                {
                    if (data != "\r")
                    {
                        string[] parts = data.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                        _param.Add(parts[0], parts[1]);
                    }
                }


                lblStartTime.Text = lblStartTime.Text + "= " + _param["StartTime"];
                lblInterval.Text = lblInterval.Text + "= " + _param["Interval"];
                lblMonitor.Text = lblMonitor.Text + "= " + _param["Monitor"];
                lblSMode.Text = lblSMode.Text + "= " + _param["SMode"];
                lblDate.Text = lblDate.Text + "= " + _param["Date"];
                lblLength.Text = lblLength.Text + "= " + _param["Length"];
                lblWeight.Text = lblWeight.Text + "= " + _param["Weight"];

                List<string> cadence = new List<string>();
                List<string> altitude = new List<string>();
                List<string> heartRate = new List<string>();
                List<string> watt = new List<string>();

                //adding data for datagrid
                var splittedHrData = SplitStringByEnter(splittedString[11]);
                foreach (var data in splittedHrData)
                {
                    var value = SplitStringBySpace(data);

                    if (value.Length >= 4)
                    {
                        cadence.Add(value[0]);
                        altitude.Add(value[1]);
                        heartRate.Add(value[2]);
                        watt.Add(value[3]);

                        string[] hrData = new string[] { value[0], value[1], value[2], value[3] };
                        dataGridView1.Rows.Add(hrData);
                    }
                }

                _hrData.Add("cadence", cadence);
                _hrData.Add("altitude", altitude);
                _hrData.Add("heartRate", heartRate);
                _hrData.Add("watt", watt);

                string totalDistanceCovered = Summary.FindSum(_hrData["cadence"]).ToString();
                string averageSpeed = Summary.FindAverage(_hrData["cadence"]).ToString();
                string maxSpeed = Summary.FindMax(_hrData["cadence"]).ToString();

                string averageHeartRate = Summary.FindAverage(_hrData["heartRate"]).ToString();
                string maximumHeartRate = Summary.FindMax(_hrData["heartRate"]).ToString();
                string minHeartRate = Summary.FindMin(_hrData["heartRate"]).ToString();

                string averagePower = Summary.FindAverage(_hrData["watt"]).ToString();
                string maxPower = Summary.FindMax(_hrData["watt"]).ToString();

                string averageAltitude = Summary.FindAverage(_hrData["altitude"]).ToString();
                string maximumAltitude = Summary.FindAverage(_hrData["altitude"]).ToString();

                string[] summarydata = new string[] { totalDistanceCovered, averageSpeed, maxSpeed, averageHeartRate, maximumHeartRate, minHeartRate, averagePower, maxPower, averageAltitude, maximumAltitude };
                dataGridView2.Rows.Add(summarydata);
            }
        }

        private void InitGrid()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Cadence";
            dataGridView1.Columns[1].Name = "Altitude";
            dataGridView1.Columns[2].Name = "Heart rate";
            dataGridView1.Columns[3].Name = "Power in watts";

            dataGridView2.ColumnCount = 10;
            dataGridView2.Columns[0].Name = "Total distance covered";
            dataGridView2.Columns[1].Name = "Average speed";
            dataGridView2.Columns[2].Name = "Maximum speed";
            dataGridView2.Columns[3].Name = "Average heart rate";
            dataGridView2.Columns[4].Name = "Maximum heart rate";
            dataGridView2.Columns[5].Name = "Minimum heart rate";
            dataGridView2.Columns[6].Name = "Average power";
            dataGridView2.Columns[7].Name = "Maximum power";
            dataGridView2.Columns[8].Name = "Average altitude";
            dataGridView2.Columns[9].Name = "Maximum altitude";
        }
    }
}
