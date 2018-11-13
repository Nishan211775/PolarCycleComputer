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
            //InitGrid();
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

                //adding column to datagrid
                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[0].Name = "Cadence";
                dataGridView1.Columns[1].Name = "Altitude";
                dataGridView1.Columns[2].Name = "Heart rate";
                dataGridView1.Columns[2].Name = "Power in watts";

                //adding data for datagrid
                var splittedHrData = SplitStringByEnter(splittedString[11]);
                foreach (var data in splittedHrData)
                {
                    var value = SplitStringBySpace(data);
                    if (value.Length >= 4)
                    {
                        //_hrData.Add("", "");

                        string[] hrData = new string[] { value[0], value[1], value[2], value[3] };
                        dataGridView1.Rows.Add(hrData);

                        Console.WriteLine("................................");
                    }
                }


                lblStartTime.Text = lblStartTime.Text + "= " + _param["StartTime"];
                lblInterval.Text = lblInterval.Text + "= " + _param["Interval"];
                lblMonitor.Text = lblMonitor.Text + "= " + _param["Monitor"];
                lblSMode.Text = lblSMode.Text + "= " + _param["SMode"];
                lblDate.Text = lblDate.Text + "= " + _param["Date"];
                lblLength.Text = lblLength.Text + "= " + _param["Length"];
                lblWeight.Text = lblWeight.Text + "= " + _param["Weight"];
                

                //string[] row = new string[] { "1", "Product 1", "1000" };
                //dataGridView1.Rows.Add(row);
                //row = new string[] { "2", "Product 2", "2000" };
                //dataGridView1.Rows.Add(row);
                //row = new string[] { "3", "Product 3", "3000" };
                //dataGridView1.Rows.Add(row);
                //row = new string[] { "4", "Product 4", "4000" };
                //dataGridView1.Rows.Add(row);
                //row = new string[] { "4", "Product 4", "4000" };
                //dataGridView1.Rows.Add(row);
            }
        }

        private void InitGrid()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Cadence"));
            table.Columns.Add(new DataColumn("Altitude"));
            table.Columns.Add(new DataColumn("Heart rate"));
            table.Columns.Add(new DataColumn("Power in watts"));
            dataGridView1.DataSource = table;
        }
    }
}
