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

namespace ReproductorMultimedia3
{
    public partial class Form1 : Form
    {
        DirectoryInfo directory;
        FileInfo[] files;
        private int i = 0;
        private bool resume;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            timer1.Interval= Int32.Parse(comboBox1.SelectedItem.ToString()) *1000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                directory = new DirectoryInfo(folderBrowserDialog1.SelectedPath);

                files = directory.GetFiles();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            reproductorMultimedia1.Segundos++;
            
            try
            {

                if (i > files.Length - 1)
                {
                    i = 0;

                    pictureBox1.Image = Image.FromFile(files[0].FullName);
                    timer1.Stop();
                }
                if (resume)
                {

                    pictureBox1.Image = Image.FromFile(files[i].FullName);
                    i++;
                }
            }
            catch (OutOfMemoryException e1)
            {
                return;
            }
        }

        private void reproductorMultimedia1_PlayClick(object sender, EventArgs e)
        {
            timer1.Start();
            _ = resume == true ? resume = false : resume = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = Int32.Parse(comboBox1.SelectedItem.ToString()) * 1000;

        }

        private void reproductorMultimedia1_DesbordaTiempo(object sender, EventArgs e)
        {
            reproductorMultimedia1.Minutos++;
        }
    }
}
