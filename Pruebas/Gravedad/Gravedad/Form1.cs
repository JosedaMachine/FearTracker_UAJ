﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gravedad
{
    public partial class Form1 : Form
    {

        private const double GravedadDeLaTierra = 9.81;

        private Process process;
        public Form1(ref Process processToTrack, ref TrackerParams tParams)
        {   
            InitializeComponent();
            Console.WriteLine("hola");

            process = processToTrack; 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                string extension = Path.GetExtension(filePath); 



                if (extension == ".exe") {

                    MessageBox.Show("Your applications is going to be tracked. This window will be closed.");

                    process.StartInfo.FileName = filePath;

                    process.Start();


                    //matar
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Men ?");
                }
            }


            

        }

        
    }
}
