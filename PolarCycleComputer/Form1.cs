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
        private Dictionary<string, string> _hrData = new Dictionary<string, string>();
        private Dictionary<string, string> _param = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
        }
        
        private static string[] SplitString(string text)
        {
            var splitString = new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};

            var splittedText = text.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            return splittedText;
        }

        private static string[] SplitStringByEnter(string text)
        {
            return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                _param = new Dictionary<string, string>();
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

                foreach (var a in _param)
                {
                    Console.WriteLine(a.Key);
                }

                lblStartTime.Text = lblStartTime.Text + "= " + _param["StartTime"];
                lblInterval.Text = lblInterval.Text + "= " + _param["Interval"];
                lblMonitor.Text = lblMonitor.Text + "= " + _param["Monitor"];
                lblSMode.Text = lblSMode.Text + "= " + _param["SMode"];
                lblDate.Text = lblDate.Text + "= " + _param["Date"];
                lblLength.Text = lblLength.Text + "= " + _param["Length"];
                lblWeight.Text = lblWeight.Text + "= " + _param["Weight"];
                
            }
        }
        
    }
}
