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
        private bool micInTesting = false;
        private bool devicesLoaded = false;

        SharedObject shared_;

        public MainHubForm(ref SharedObject shared)
        {   
            InitializeComponent();
            shared_ = shared;

            // Componentes escondidos de inicio
            labelWarningTestAudio.Hide();
            labelSelectDevice.Hide();
            outputDeviceCombo.Hide();
            buttonAudioTest.Hide();
            progressBarAudio.Hide();
        }

        // Busca y añade al ComboBox los posibles dispositivos
        private void LoadDevices()
        {
            if (devicesLoaded)
                return;

            AudioTracker tracker = shared_.trackerParams.audioTracker;
            var devices = tracker.GetDevices();

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
                ShowAudioLabel("Warning: Test Audio", Color.Red);
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
            // Prepara e inicia el testeo de micro
            micInTesting = true;

            AudioTracker tracker = shared_.trackerParams.audioTracker;
            
            timer1.Enabled = true;
            tracker.ResetMicTesting();
            tracker.SetSelectedDevice(outputDeviceCombo.SelectedItem);
            progressBarAudio.Show();
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
            tracker.SetSelectedDevice(outputDeviceCombo.SelectedItem);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!micInTesting)
                return;

            AudioTracker tracker = shared_.trackerParams.audioTracker;

            if (!tracker.IsBackgroundNoiseRecordingFinished())
            {
                ShowAudioLabel("Recording background noise...", Color.DarkGray);
                tracker.GetBackgroundNoise();
                return;
            }

            ShowAudioLabel("Speak", Color.Blue);

            float progressValue;
            if (tracker.VoiceTest(out progressValue)) { 
                ShowAudioLabel("Silence!", Color.OrangeRed);
            };

            progressBarAudio.Value = (int)(progressValue * 100);

            if (tracker.IsVoiceTestOver())
            {
                micTested = true;
                micInTesting = false;
                progressBarAudio.Hide();
                ShowAudioLabel("Audio Calibrated", Color.Green);
            }
        }

        private void ShowAudioLabel(string text, Color color)
        {
            labelWarningTestAudio.ForeColor = color;
            labelWarningTestAudio.Text = text;
            labelWarningTestAudio.Show();
        }

        private void labelWarningTestAudio_Click(object sender, EventArgs e)
        {

        }
    }
}
