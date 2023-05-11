namespace FT
{
    partial class MainHubForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelChoose = new System.Windows.Forms.Label();
            this.checkMouse = new System.Windows.Forms.CheckBox();
            this.checkMicrophone = new System.Windows.Forms.CheckBox();
            this.checkKeyboard = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonAudioTest = new System.Windows.Forms.Button();
            this.labelSelectDevice = new System.Windows.Forms.Label();
            this.outputDeviceCombo = new System.Windows.Forms.ComboBox();
            this.labelWarningTestAudio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelChoose
            // 
            this.labelChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChoose.AutoSize = true;
            this.labelChoose.Location = new System.Drawing.Point(318, 9);
            this.labelChoose.Name = "labelChoose";
            this.labelChoose.Size = new System.Drawing.Size(110, 13);
            this.labelChoose.TabIndex = 3;
            this.labelChoose.Text = "Choose Track Events";
            // 
            // checkMouse
            // 
            this.checkMouse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkMouse.AutoSize = true;
            this.checkMouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.4F);
            this.checkMouse.Location = new System.Drawing.Point(364, 98);
            this.checkMouse.Name = "checkMouse";
            this.checkMouse.Size = new System.Drawing.Size(64, 19);
            this.checkMouse.TabIndex = 4;
            this.checkMouse.Text = "Mouse";
            this.checkMouse.UseVisualStyleBackColor = true;
            this.checkMouse.CheckedChanged += new System.EventHandler(this.checkMouseClick);
            // 
            // checkMicrophone
            // 
            this.checkMicrophone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkMicrophone.AutoSize = true;
            this.checkMicrophone.Location = new System.Drawing.Point(180, 98);
            this.checkMicrophone.Name = "checkMicrophone";
            this.checkMicrophone.Size = new System.Drawing.Size(82, 17);
            this.checkMicrophone.TabIndex = 5;
            this.checkMicrophone.Text = "Microphone";
            this.checkMicrophone.UseVisualStyleBackColor = true;
            this.checkMicrophone.CheckedChanged += new System.EventHandler(this.checkMicrophoneClick);
            // 
            // checkKeyboard
            // 
            this.checkKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkKeyboard.AutoSize = true;
            this.checkKeyboard.Location = new System.Drawing.Point(548, 100);
            this.checkKeyboard.Name = "checkKeyboard";
            this.checkKeyboard.Size = new System.Drawing.Size(71, 17);
            this.checkKeyboard.TabIndex = 6;
            this.checkKeyboard.Text = "Keyboard";
            this.checkKeyboard.UseVisualStyleBackColor = true;
            this.checkKeyboard.CheckedChanged += new System.EventHandler(this.checkKeyboardClick);
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(222, 257);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(134, 56);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStartClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Location = new System.Drawing.Point(485, 257);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(134, 56);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStopClick);
            // 
            // buttonAudioTest
            // 
            this.buttonAudioTest.Location = new System.Drawing.Point(268, 85);
            this.buttonAudioTest.Name = "buttonAudioTest";
            this.buttonAudioTest.Size = new System.Drawing.Size(46, 41);
            this.buttonAudioTest.TabIndex = 10;
            this.buttonAudioTest.Text = "Test";
            this.buttonAudioTest.UseVisualStyleBackColor = true;
            this.buttonAudioTest.Hide();
            this.buttonAudioTest.Click += new System.EventHandler(this.buttonAudioClick);
            // 
            // labelSelectDevice
            // 
            this.labelSelectDevice.AutoSize = true;
            this.labelSelectDevice.Location = new System.Drawing.Point(12, 98);
            this.labelSelectDevice.Name = "labelSelectDevice";
            this.labelSelectDevice.Size = new System.Drawing.Size(109, 13);
            this.labelSelectDevice.TabIndex = 11;
            this.labelSelectDevice.Hide();
            this.labelSelectDevice.Text = "Select Output Device";
            // 
            // outputDeviceCombo
            // 
            this.outputDeviceCombo.FormattingEnabled = true;
            this.outputDeviceCombo.Location = new System.Drawing.Point(13, 124);
            this.outputDeviceCombo.Name = "outputDeviceCombo";
            this.outputDeviceCombo.Size = new System.Drawing.Size(182, 21);
            this.outputDeviceCombo.TabIndex = 12;
            this.outputDeviceCombo.Hide();
            this.outputDeviceCombo.SelectedIndexChanged += new System.EventHandler(this.outputDeviceCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.labelWarningTestAudio.AutoSize = true;
            this.labelWarningTestAudio.Location = new System.Drawing.Point(265, 141);
            this.labelWarningTestAudio.Name = "labelWarningTestAudio";
            this.labelWarningTestAudio.Size = new System.Drawing.Size(35, 13);
            this.labelWarningTestAudio.TabIndex = 13;
            this.labelWarningTestAudio.Hide();
            this.labelWarningTestAudio.ForeColor = System.Drawing.Color.Red;
            this.labelWarningTestAudio.Text = "Warning: Test Audio";
            // 
            // MainHubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelWarningTestAudio);
            this.Controls.Add(this.outputDeviceCombo);
            this.Controls.Add(this.labelSelectDevice);
            this.Controls.Add(this.buttonAudioTest);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.checkKeyboard);
            this.Controls.Add(this.checkMicrophone);
            this.Controls.Add(this.checkMouse);
            this.Controls.Add(this.labelChoose);
            this.Name = "MainHubForm";
            this.Text = "Fear Tracking";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainHubForm_FormClosing);
            this.Load += new System.EventHandler(this.MainHubForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelChoose;
        private System.Windows.Forms.CheckBox checkMouse;
        private System.Windows.Forms.CheckBox checkMicrophone;
        private System.Windows.Forms.CheckBox checkKeyboard;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonAudioTest;
        private System.Windows.Forms.Label labelSelectDevice;
        private System.Windows.Forms.ComboBox outputDeviceCombo;
        private System.Windows.Forms.Label labelWarningTestAudio;
    }
}

