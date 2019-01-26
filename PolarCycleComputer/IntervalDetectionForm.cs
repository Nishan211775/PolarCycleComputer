using PolarCycleComputer.Action;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolarCycleComputer
{
  public partial class IntervalDetectionForm : Form
  {
    private Dictionary<string, object> _hrData;
    public IntervalDetectionForm(Dictionary<string, object> hrData)
    {
      InitializeComponent();
      _hrData = new IntervalDetection().GetIntervalDetectedData(hrData.ToDictionary(k => k.Key, k => k.Value as object));

      for (int i = 0; i < _hrData.Count; i++)
      {
        comboBox1.Items.Add("Interval " + (i + 1));
      }

      //dataGridView2.Rows.Add(new TableFiller().FillDataInSumaryTable(hrData, hrData["endTime"] as string, hrData["params"] as Dictionary<string, string>));
    }

    private void IntervalDetectionForm_Load(object sender, EventArgs e)
    {
      InitGrid();
    }

    private void InitGrid()
    {
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

    private void comboBox1_Click(object sender, EventArgs e)
    {
      
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      int selectedIndex = comboBox1.SelectedIndex + 1;
      
      dataGridView2.Rows.Clear();

      var a = _hrData["data" + selectedIndex] as Dictionary<string, List<string>>;
      var b = a.ToDictionary(k => k.Key, k => k.Value as object);


      var data = new TableFiller().FillDataInSumaryTable(b, "19:12:15", null);
      dataGridView2.Rows.Add(data);
    }
  }
}
