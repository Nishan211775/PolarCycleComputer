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
        public static Dictionary<string, List<string>> _hrData;
        public GraphWindow()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void GraphWindow_Load(object sender, EventArgs e)
        {
            plotGraph();
            SetSize();
        }
        
        private void plotGraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the Titles
            myPane.Title = "Overview";
            myPane.XAxis.Title = "Time in second";
            myPane.YAxis.Title = "Data";
            
            /* myPane.XAxis.Scale.MajorStep = 50;
             myPane.YAxis.Scale.Mag = 0;
             myPane.XAxis.Scale.Max = 1000;*/

            PointPairList cadencePairList = new PointPairList();
            PointPairList altitudePairList = new PointPairList();
            PointPairList heartPairList = new PointPairList();
            PointPairList powerPairList = new PointPairList();

            //int[] teamAData = buildTeamAData();
            //int[] teamBData = buildTeamBData();
            //for (int i = 0; i < 10; i++)
            //{
            //    teamAPairList.Add(i, teamAData[i]);
            //    teamBPairList.Add(i, teamBData[i]);
            //}

            for (int i = 0; i < _hrData["cadence"].Count; i++)
            {
                cadencePairList.Add(i, Convert.ToInt16(_hrData["cadence"][i]));
            }

            for (int i = 0; i < _hrData["altitude"].Count; i++)
            {
                altitudePairList.Add(i, Convert.ToInt16(_hrData["altitude"][i]));
            }

            for (int i = 0; i < _hrData["heartRate"].Count; i++)
            {
                heartPairList.Add(i, Convert.ToInt16(_hrData["heartRate"][i]));
            }

            for (int i = 0; i < _hrData["watt"].Count; i++)
            {
                powerPairList.Add(i, Convert.ToInt16(_hrData["watt"][i]));
            }

            LineItem cadence = myPane.AddCurve("Cadence",
                   cadencePairList, Color.Red, SymbolType.None);

            LineItem altitude = myPane.AddCurve("Altitude",
                  altitudePairList, Color.Blue, SymbolType.None);

            LineItem heart = myPane.AddCurve("Heart",
                   heartPairList, Color.Black, SymbolType.None);

            LineItem power = myPane.AddCurve("Power",
                  powerPairList, Color.Orange, SymbolType.None);

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
