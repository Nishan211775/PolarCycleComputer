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
        private string[] _breaks = new string[100];
        private string[] _hrData = new string[100];
        private string[] _param = new string[100];

        public Form1()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                string[] line = File.ReadAllLines(file);






























                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == "[Params]")
                    {
                        //_param[i] = line[i];
                        _param[i] = "Hello";
                    }
                    else if (line[i] == "[HRData]")
                    {
                        //_hrData[i] = line[i];
                        _param[i] = "Hello";
                    }
                }


                foreach (var data in _param)
                {
                    Console.WriteLine(data);
                }

                foreach (var data in _hrData)
                {
                    Console.WriteLine(data);
                }
                //var index = text.IndexOf("\n");
                //string[] param = text.Split('param');



                //string[] parts = text.Split(new[] { "\n", "\r", "\r\n"}, StringSplitOptions.None);

                //for (int i = 0; i < parts.Length; i++)
                //{
                //    if (parts[i] != "")
                //    {

                //        Console.WriteLine("Index {0}, {1}", i, parts[i]);
                //    }
                //}
            }
        }
        
    }
}
