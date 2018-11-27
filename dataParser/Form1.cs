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

namespace dataParser
{
    public partial class Form1 : Form
    {
        StreamReader sr;
        string lines, path;
        List<Data> result;

        public Form1()
        {
            InitializeComponent();

            result = new List<Data>();

            readFile();
        }

        private void readFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string[] items;
           if(ofd.ShowDialog() == DialogResult.OK)
            {
                sr = new StreamReader(ofd.FileName);
                while((lines=sr.ReadLine()) != null)
                {
                    items = lines.Split('\t');
                    result.Add(new Data(double.Parse(items[0]), double.Parse(items[1]), double.Parse(items[2])));
                }

                label4.Text = ofd.FileName;
                label1.Text = "Total lines: " + result.Count;
                path = Path.GetDirectoryName(ofd.FileName);
           }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int start, end;

            start = int.Parse(textBox1.Text);
            end = int.Parse(textBox2.Text);

            if(end > start && end < result.Count)
            {
                drawLine(start, end);
            }
        }

        private void drawLine(int start, int end)
        {
            chart1.Series["Series1"].Points.Clear();
            for(int i=start;i<end;i++)
            {
                chart1.Series["Series1"].Points.AddXY(result[i].time, result[i].displacement);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int start, end;

            start = int.Parse(textBox1.Text);
            end = int.Parse(textBox2.Text);

            if(end > start && end <= result.Count)
            {
                exportData(start, end);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void exportData(int start, int end)
        {
            string filename = path + "\\" + start.ToString() + '-' + end.ToString() + ".txt";
            StreamWriter sw = new StreamWriter(filename, true);

            for(int i=start;i<end;i++)
            {
                string t_ = result[i].time.ToString();
                string d_ = result[i].displacement.ToString();
                string f_ = result[i].force.ToString();

                sw.WriteLine(t_ + '\t' + d_ + '\t' + f_);
            }

            sw.Close();
        }
    }
    public class Data
    {
        public Data(double time, double displacement, double force)
        {
            this.time = time;
            this.displacement = displacement;
            this.force = force;
        }
        public double time;
        public double displacement;
        public double force;
    }
}
