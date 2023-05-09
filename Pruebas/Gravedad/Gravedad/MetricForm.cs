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

namespace Gravedad
{
    public partial class MetricForm : Form
    {
        public MetricForm()
        {
            InitializeComponent();
        }

        private void MetricForm_Load(object sender, EventArgs e)
        {
            // Leer el archivo JSON
            string json = File.ReadAllText("datos.json");
            List<Dato> datos = JsonConvert.DeserializeObject<List<Dato>>(json);

            // Crear la serie de los gráficos
            Series micSerie = createSeries(ref MicChart);
            Series ratonSerie = createSeries(ref mouseChart);
            Series tecladoSerie = createSeries(ref keyboardChart);

            // Agregar los puntos de datos a la serie
            foreach (Dato dato in datos)
            {
                DataPoint punto = new DataPoint(dato.time, dato.y);
                switch (dato.type)
                {
                    case "microphone":
                        micSerie.Points.Add(punto);
                        break;
                    case "mouse":
                        ratonSerie.Points.Add(punto);
                        break;
                    case "keyboard":
                        tecladoSerie.Points.Add(punto);
                        break;
                }          
            }

            // Configurar los ejes del gráfico
            configureAxis(ref MicChart, "Sonido (db)");
            configureAxis(ref mouseChart, "Velocidad (m/s)");
            configureAxis(ref keyboardChart, "Num inputs?");

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
