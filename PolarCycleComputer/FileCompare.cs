using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PolarCycleComputer.Action;

namespace PolarCycleComputer
{
  public partial class FileCompare : Form
  {
    private string fileOneText = "";
    private string fileTwoText = "";

    public FileCompare()
    {
      InitializeComponent();
    }

    private void FileCompare_Load(object sender, EventArgs e)
    {
      InitGrid();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DialogResult result = openFileDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        fileOneText = File.ReadAllText(openFileDialog1.FileName);
        string path = Path.GetFullPath(openFileDialog1.FileName);
        textBox1.Text = path;
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DialogResult result = openFileDialog2.ShowDialog();
      if (result == DialogResult.OK)
      {
        fileTwoText = File.ReadAllText(openFileDialog2.FileName);
        string path = Path.GetFullPath(openFileDialog2.FileName);
        textBox2.Text = path;
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(fileOneText) || string.IsNullOrEmpty(fileTwoText))
      {
        MessageBox.Show("Both file are required");
        return;
      }

      var firstTableData = new TableFiller().FillTable(fileOneText, dataGridView1);
      var secondTableData = new TableFiller().FillTable(fileTwoText, dataGridView2);

      dataGridView3.Rows.Add(new TableFiller().FillDataInSumaryTable(firstTableData, firstTableData["params"] as Dictionary<string, string>, firstTableData["endTime"] as string));
      dataGridView3.Rows.Add(new TableFiller().FillDataInSumaryTable(secondTableData, secondTableData["params"] as Dictionary<string, string>, secondTableData["endTime"] as string));
    }

    private void InitGrid()
    {
      //try
      //{
      //  MessageBox.Show(dataGridView1.SelectedCells[0].Value.ToString());
      //} catch(Exception ex)
      //{
      //  Console.WriteLine(ex.Message);
      //}

      dataGridView1.ColumnCount = 6;
      dataGridView1.Columns[0].Name = "Cadence(RPM)";
      dataGridView1.Columns[1].Name = "Altitude(m/ft)";
      dataGridView1.Columns[2].Name = "Heart rate(bpm)";
      dataGridView1.Columns[3].Name = "Power(watt)";
      dataGridView1.Columns[4].Name = "Speed(Mile/hr)";
      dataGridView1.Columns[5].Name = "Time";

      dataGridView2.ColumnCount = 6;
      dataGridView2.Columns[0].Name = "Cadence(RPM)";
      dataGridView2.Columns[1].Name = "Altitude(m/ft)";
      dataGridView2.Columns[2].Name = "Heart rate(bpm)";
      dataGridView2.Columns[3].Name = "Power(watt)";
      dataGridView2.Columns[4].Name = "Speed(Mile/hr)";
      dataGridView2.Columns[5].Name = "Time";

      dataGridView3.ColumnCount = 10;
      dataGridView3.Columns[0].Name = "Total distance covered";
      dataGridView3.Columns[1].Name = "Average speed(km/hr)";
      dataGridView3.Columns[2].Name = "Maximum speed(km/hr)";
      dataGridView3.Columns[3].Name = "Average heart rate(bpm)";
      dataGridView3.Columns[4].Name = "Maximum heart rate(bpm)";
      dataGridView3.Columns[5].Name = "Minimum heart rate(bpm)";
      dataGridView3.Columns[6].Name = "Average power(watt)";
      dataGridView3.Columns[7].Name = "Maximum power(watt)";
      dataGridView3.Columns[8].Name = "Average altitude(RPM)";
      dataGridView3.Columns[9].Name = "Maximum altitude(RPM)";

    }

    private void button3_MouseHover(object sender, EventArgs e)
    {
      //ToolTip tt = new ToolTip();
      //Control o = (Control)sender;
      //tt.Show("Lorem ipsum dolor sit ame", o, 1000);
    }

    private void dataGridView3_MouseMove(object sender, MouseEventArgs e)
    {

    }

    private void dataGridView3_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dataGridView3_Click(object sender, EventArgs e)
    {

    }

    private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      var value = dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

      if (value != null)
      {
        if (e.RowIndex == 0)
        {
          var value1 = dataGridView3.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value;
          string result = "";
          if (Convert.ToInt32(value) > Convert.ToInt32(value1))
          {
            result = "+";
          }
          else
          {
            result = "-";
          }
          MessageBox.Show(result);
        }
        else if (e.RowIndex == 1)
        {
          var value1 = dataGridView3.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value;
          string result = "";
          if (Convert.ToInt32(value) > Convert.ToInt32(value1))
          {
            result = "+";
          }
          else
          {
            result = "-";
          }
          MessageBox.Show(result);
        }
      }
    }
  }
}
