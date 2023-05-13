using System;
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

            AudioTracker tracker = shared_.trackerParams.audioTracker;
            var devices = tracker.getDevices();

            if (devices == null)
                return;

            outputDeviceCombo.Items.AddRange(devices.ToArray());
            outputDeviceCombo.SelectedIndex = 0;
            devicesLoaded = true;
        }

        //mouse
        private void checkMouseClick(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            shared_.trackerParams.mouseTracking = checkBox.Checked;

            if (checkBox.Checked)
                shared_.trackerParams.trackingCount += 1;
            else
                shared_.trackerParams.trackingCount -= 1;
        }

        //Micro
        private void checkMicrophoneClick(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            shared_.trackerParams.MicTracking = checkBox.Checked;

            if (checkBox.Checked)
            {
                shared_.trackerParams.trackingCount += 1;
                buttonAudioTest.Show();
                labelSelectDevice.Show();
                outputDeviceCombo.Show();
                LoadDevices();
            }
            else
            {
                shared_.trackerParams.trackingCount -= 1;
                buttonAudioTest.Hide();
                labelSelectDevice.Hide();
                outputDeviceCombo.Hide();
            }
        }
        //Keyboard
        private void checkKeyboardClick(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            shared_.trackerParams.KeyboardTracking = checkBox.Checked;

            if (checkBox.Checked)
                shared_.trackerParams.trackingCount += 1;
            else
                shared_.trackerParams.trackingCount -= 1;
        }

        private void buttonStartClick(object sender, EventArgs e)
        {
            if (shared_.trackerParams.MicTracking && !micTested)
            {
                labelWarningTestAudio.Show();
                return;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                string extension = Path.GetExtension(filePath); 



                if (extension == ".exe" || extension == ".mp4")
                {

                    MessageBox.Show("Your applications is going to be tracked. This window will be closed.");

                    shared_.trackerParams.process.StartInfo.FileName = filePath;

                    shared_.trackerParams.canStart = true;

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
            if (!shared_.trackerParams.canStart)
                return;

            shared_.trackerParams.canStop = true;
            this.Close();
        }

        private void buttonAudioClick(object sender, EventArgs e)
        {
            // Esconde warning en caso de estar visible
            labelWarningTestAudio.Hide();

            // Coge el tracker e inicia el voicetest
            AudioTracker tracker = shared_.trackerParams.audioTracker;
            tracker.setSelectedDevice(outputDeviceCombo.SelectedItem);
            tracker.getBackgroundNoise();
            tracker.voiceTest();
            micTested = true;
        }
        

        private void MainHubForm_Load(object sender, EventArgs e)
        {

        }

        private void MainHubForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!shared_.trackerParams.canStop)
            {
                shared_.trackerParams.canStop = true;
            }
        }

        private void outputDeviceCombo_SelectedIndexChanged(object sender, EventArgs e) 
        {
            AudioTracker tracker = shared_.trackerParams.audioTracker;
            tracker.setSelectedDevice(outputDeviceCombo.SelectedItem);
        }

    }
}
