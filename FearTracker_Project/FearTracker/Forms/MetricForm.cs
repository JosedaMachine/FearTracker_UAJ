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

            Console.WriteLine();

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
                configureAxis(ref mouseChart, "Velocidad (m/s)");
            }

            if (shared_.trackerParams.KeyboardTracking)
            {
                //keyboard
                series[2] = createSeries(ref keyboardChart);
                configureAxis(ref keyboardChart, "Num inputs?");
            }
            
            // Agregar los puntos de datos a las series
            foreach (jsonData dato in datos)
            {
                DataPoint punto = new DataPoint(dato.time, dato.y);
                series[dato.typeId].Points.Add(punto);
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

            return serie;
        }
    }
}
