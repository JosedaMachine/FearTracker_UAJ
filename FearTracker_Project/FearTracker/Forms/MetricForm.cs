using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FT
{
    public partial class MetricForm : Form
    {
        SharedObject shared_;
        public MetricForm(ref SharedObject shared)
        {
            shared_ = shared;
            InitializeComponent();

        }

        private void MetricForm_Load(object sender, EventArgs e)
        {
            // Leer el archivo JSON
            string json = File.ReadAllText("data.json");
            List<jsonData> datos = JsonConvert.DeserializeObject<List<jsonData>>(json);

            Series[] series = new Series[3];

            // Crear la serie de los gráficos
            if (shared_.trackerParams.MicTracking)
            {
                //Mic
                series[0] = createSeries(ref MicChart);
                series[0].Color = Color.Violet;
                configureAxis(ref MicChart, "Sonido (db)");
            }

            if (shared_.trackerParams.mouseTracking)
            {
                //mouse
                series[1] = createSeries(ref mouseChart);
                series[1].Color = Color.Tomato;
                configureAxis(ref mouseChart, "Desplazamiento (point)");
            }

            if (shared_.trackerParams.KeyboardTracking)
            {
                //keyboard
                series[2] = createSeries(ref keyboardChart);
                configureAxis(ref keyboardChart, "Número de inputs");
            }
            
            // Agregar los puntos de datos a las series
            foreach (jsonData dato in datos)
            {
                int evType = (int)(dato.EventType[0]) - (int)'0';


                if (evType >= 0 && evType < 3)
                {
                    float elapsedTime = (dato.TimeStamp - shared_.trackerParams.startTime)/1000.0f;
                    DataPoint punto = new DataPoint(elapsedTime, dato.y);
                    series[evType].Points.Add(punto);

                    Console.WriteLine(evType + " " + (dato.TimeStamp - shared_.trackerParams.startTime) + " " + dato.y);
                }
            }
        }
        private void configureAxis(ref Chart chart, string y)
        {
            chart.ChartAreas[0].AxisX.Title = "Tiempo (s)";
            chart.ChartAreas[0].AxisY.Title = y;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisY.Minimum = 0;
        }
        private Series createSeries(ref Chart chart)
        {
            Series serie = chart.Series[0];
            serie.Points.Clear();
            serie.ChartType = SeriesChartType.Line;

            DataPoint iniPoint = new DataPoint(0,0);
            serie.Points.Add(iniPoint);

            return serie;
        }
    }
}
