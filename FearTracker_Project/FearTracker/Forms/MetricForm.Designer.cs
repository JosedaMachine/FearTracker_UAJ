namespace FT
{
    partial class MetricForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        const int width = 1600, height = 900, offset = 10;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            
            if (shared_.trackerParams.MicTracking)
            {
                this.MicChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
                ((System.ComponentModel.ISupportInitialize)(this.MicChart)).BeginInit();
            }
            if (shared_.trackerParams.mouseTracking)
            {
                this.mouseChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
                ((System.ComponentModel.ISupportInitialize)(this.mouseChart)).BeginInit();
            }
            if (shared_.trackerParams.KeyboardTracking)
            {
                this.keyboardChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
                ((System.ComponentModel.ISupportInitialize)(this.keyboardChart)).BeginInit();
            }

            this.scareChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.scareChart)).BeginInit();

            this.SuspendLayout();

            // Tamaño de las graficas
            int chartWidth = shared_.trackerParams.trackingCount < 3 ? (width - (offset * 2)) : ((width / 2) - (offset * 3));
            int chartHeight = shared_.trackerParams.trackingCount < 2 ? (height - (offset * 2)) : ((height / 2) - (offset * 3));

            int posCont = 0;

            if (shared_.trackerParams.MicTracking)
            {
                // 
                // MicChart
                // 
                chartArea1.Name = "ChartArea1";
                this.MicChart.ChartAreas.Add(chartArea1);
                legend1.Name = "Legend1";
                this.MicChart.Legends.Add(legend1);
                this.MicChart.Location = new System.Drawing.Point(positionsX[posCont], positionsY[posCont]);
                posCont++;
                this.MicChart.Name = "MicChart";
                series1.ChartArea = "ChartArea1";
                series1.Legend = "Legend1";
                series1.Name = "Microphone";
                this.MicChart.Series.Add(series1);
                this.MicChart.Size = new System.Drawing.Size(chartWidth, chartHeight);
                this.MicChart.TabIndex = 1;
                this.MicChart.Text = "chart1";
            }
            if (shared_.trackerParams.mouseTracking)
            {
                // 
                // mouse
                // 
                chartArea2.Name = "ChartArea1";
                this.mouseChart.ChartAreas.Add(chartArea2);
                legend2.Name = "Legend1";
                this.mouseChart.Legends.Add(legend2);
                this.mouseChart.Location = new System.Drawing.Point(positionsX[posCont], positionsY[posCont]);
                posCont++;
                this.mouseChart.Name = "chart1";
                series2.ChartArea = "ChartArea1";
                series2.Legend = "Legend1";
                series2.Name = "mouse";
                this.mouseChart.Series.Add(series2);
                this.mouseChart.Size = new System.Drawing.Size(chartWidth, chartHeight);
                this.mouseChart.TabIndex = 2;
                this.mouseChart.Text = "chart1";
            }
            if (shared_.trackerParams.KeyboardTracking)
            {
                // 
                // keyboard
                // 
                chartArea3.Name = "ChartArea1";
                this.keyboardChart.ChartAreas.Add(chartArea3);
                legend3.Name = "Legend1";
                this.keyboardChart.Legends.Add(legend3);
                this.keyboardChart.Location = new System.Drawing.Point(positionsX[posCont], positionsY[posCont]);
                posCont++;
                this.keyboardChart.Name = "chart2";
                series3.ChartArea = "ChartArea1";
                series3.Legend = "Legend1";
                series3.Name = "Keyboard";
                this.keyboardChart.Series.Add(series3);
                this.keyboardChart.Size = new System.Drawing.Size(chartWidth, chartHeight);
                this.keyboardChart.TabIndex = 3;
                this.keyboardChart.Text = "chart2";
            }

            // 
            // Scare
            // 
            chartArea4.Name = "ChartArea4";
            this.scareChart.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.scareChart.Legends.Add(legend4);
            this.scareChart.Location = new System.Drawing.Point(positionsX[posCont], positionsY[posCont]);
            posCont++;
            this.scareChart.Name = "chart4";
            series4.ChartArea = "ChartArea4";
            series4.Legend = "Legend1";
            series4.Name = "Scare";
            this.scareChart.Series.Add(series4);
            this.scareChart.Size = new System.Drawing.Size(chartWidth, chartHeight);
            this.scareChart.TabIndex = 4;
            this.scareChart.Text = "chart4";

            // 
            // MetricForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(width, height);
            this.Controls.Add(this.keyboardChart);
            this.Controls.Add(this.mouseChart);
            this.Controls.Add(this.MicChart);
            this.Controls.Add(this.scareChart);
            this.Name = "MetricForm";
            this.Text = "MetricForm";
            this.Load += new System.EventHandler(this.MetricForm_Load);
            if (shared_.trackerParams.MicTracking)
                ((System.ComponentModel.ISupportInitialize)(this.MicChart)).EndInit();
            if (shared_.trackerParams.mouseTracking)
                ((System.ComponentModel.ISupportInitialize)(this.mouseChart)).EndInit();
            if (shared_.trackerParams.KeyboardTracking)
                ((System.ComponentModel.ISupportInitialize)(this.keyboardChart)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.scareChart)).EndInit();

            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart MicChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart mouseChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart keyboardChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart scareChart;

        // Posicion de las graficas
        private int[] positionsY = new int[] { offset, (height / 2) + (int)(offset * 1.5f), offset, (height / 2) + (int)(offset * 1.5f) };
        private int[] positionsX = new int[] { offset, offset, (width / 2) + (int)(offset * 1.5f), (width / 2) + (int)(offset * 1.5f) };
    }
}