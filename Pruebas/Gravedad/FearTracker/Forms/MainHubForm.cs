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

using AudioTracking;

namespace FT
{
    public partial class MainHubForm : Form
    {
        private bool micTested = false;
        private bool devicesLoaded = false;

        SharedObject shared_;

        public MainHubForm(ref SharedObject shared)
        {   
            InitializeComponent();
            shared_ = shared;
        }

        // Busca y añade al ComboBox los posibles dispositivos
        private void LoadDevices()
        {
            if (devicesLoaded)
                return;

            AudioTracker tracker = shared_.Parameters.audioTracker;
            var devices = tracker.getDevices();
            outputDeviceCombo.Items.AddRange(devices.ToArray());
            outputDeviceCombo.SelectedIndex = 0;
            devicesLoaded = true;
        }

        //mouse
        private void checkMouseClick(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            shared_.Parameters.mouseTracking = checkBox.Checked;

            if (checkBox.Checked)
                shared_.Parameters.trackingCount += 1;
            else
                shared_.Parameters.trackingCount -= 1;
        }

        //Micro
        private void checkMicrophoneClick(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            shared_.Parameters.MicTracking = checkBox.Checked;

            if (checkBox.Checked)
            {
                shared_.Parameters.trackingCount += 1;
                buttonAudioTest.Show();
            }
            else
            {
                shared_.Parameters.trackingCount -= 1;
                buttonAudioTest.Hide();
            }
        }
        //Keyboard
        private void checkKeyboardClick(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            shared_.Parameters.KeyboardTracking = checkBox.Checked;

            if (checkBox.Checked)
                shared_.Parameters.trackingCount += 1;
            else
                shared_.Parameters.trackingCount -= 1;
        }

        private void buttonStartClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                string extension = Path.GetExtension(filePath); 



                if (extension == ".exe" || extension == ".mp4")
                {

                    MessageBox.Show("Your applications is going to be tracked. This window will be closed.");

                    shared_.Parameters.process.StartInfo.FileName = filePath;

                    shared_.Parameters.canStart = true;

                    //matar
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Men ?");
                }
            }

        }

        private void buttonStopClick(object sender, EventArgs e)
        {
            shared_.Parameters.canStop = true;
            this.Close();
        }

        private void buttonAudioClick(object sender, EventArgs e)
        {

        }

        private void outputDeviceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
