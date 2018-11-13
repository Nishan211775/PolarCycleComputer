using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace PolarCycleComputer
{
    public partial class GraphWindow : Form
    {
        public static List<string> _powerData;
        public GraphWindow()
        {
            InitializeComponent();
            //int[] data = _powerData.Select(int.Parse).ToList();
            //foreach (var data in _powerData)
            //{
            //    Console.WriteLine(data);
            //}
        }

        private void GraphWindow_Load(object sender, EventArgs e)
        {
            plotGraph();
            SetSize();
        }

        private int[] buildTeamAData()
        {
            int[] goalsScored = new int[10];
            for (int i = 0; i < 10; i++)
            {
                goalsScored[i] = (i + 1) * 10;
            }
            return goalsScored;
        }

        private int[] buildTeamBData()
        {
            int[] goalsScored = new int[10];
            for (int i = 0; i < 10; i++)
            {
                goalsScored[i] = (i + 10) * 11;
            }
            return goalsScored;
        }

        private void plotGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the Titles
            myPane.Title = "Team A vs Team B Goal Analysis for 2014/2015 Season";
            myPane.XAxis.Title = "Year";
            myPane.YAxis.Title = "No of Goals";
            /* myPane.XAxis.Scale.MajorStep = 50;
             myPane.YAxis.Scale.Mag = 0;
             myPane.XAxis.Scale.Max = 1000;*/

            PointPairList teamAPairList = new PointPairList();
            PointPairList teamBPairList = new PointPairList();

            //int[] teamAData = buildTeamAData();
            //int[] teamBData = buildTeamBData();
            //for (int i = 0; i < 10; i++)
            //{
            //    teamAPairList.Add(i, teamAData[i]);
            //    teamBPairList.Add(i, teamBData[i]);
            //}

            for (int i = 0; i < 10; i++)
            {
                teamAPairList.Add(i, Convert.ToInt16(_powerData.ElementAt(i)));
                teamBPairList.Add(i, Convert.ToInt16(_powerData.ElementAt(i)) + 12);
            }

            LineItem teamACurve = myPane.AddCurve("Team A",
                   teamAPairList, Color.Red, SymbolType.Diamond);

            LineItem teamBCurve = myPane.AddCurve("Team B",
                  teamBPairList, Color.Blue, SymbolType.Circle);

            zedGraphControl1.AxisChange();
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            zedGraphControl1.IsShowPointValues = true;
            zedGraphControl1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 50);

        }

        private void GraphWindow_Resize(object sender, EventArgs e)
        {
            SetSize();
        }
    }
}
