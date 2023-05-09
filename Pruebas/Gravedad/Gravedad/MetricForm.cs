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

        private void button1_Click(object sender, EventArgs e)
        {
            //// Leer el archivo JSON
            //string json = File.ReadAllText("datos.json");
            //List<Dato> datos = JsonConvert.DeserializeObject<List<Dato>>(json);

            //// Crear la serie del gráfico
            //Series serie = chart1.Series[0];
            //serie.Points.Clear();

            //// Configurar el tipo de serie
            //serie.ChartType = SeriesChartType.Line;

            //// Agregar los puntos de datos a la serie
            //foreach (var dato in datos)
            //{
            //    DataPoint punto = new DataPoint(dato.Tiempo, dato.Velocidad);
            //    serie.Points.Add(punto);
            //}

            //// Configurar los ejes del gráfico
            //chart1.ChartAreas[0].AxisX.Title = "Tiempo (s)";
            //chart1.ChartAreas[0].AxisY.Title = "Velocidad (m/s)";
            //chart1.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.ChartAreas[0].AxisY.Minimum = 0;
        }

        private void MetricForm_Load(object sender, EventArgs e)
        {
            // Leer el archivo JSON
            string json = File.ReadAllText("datos.json");
            List<Dato> datos = JsonConvert.DeserializeObject<List<Dato>>(json);

            // Crear la serie del gráfico
            Series serie = chart1.Series[0];
            serie.Points.Clear();

            // Configurar el tipo de serie
            serie.ChartType = SeriesChartType.Line;

            // Agregar los puntos de datos a la serie
            foreach (var dato in datos)
            {
                DataPoint punto = new DataPoint(dato.Tiempo, dato.Velocidad);
                serie.Points.Add(punto);
            }

            // Configurar los ejes del gráfico
            chart1.ChartAreas[0].AxisX.Title = "Tiempo (s)";
            chart1.ChartAreas[0].AxisY.Title = "Velocidad (m/s)";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
        }
    }
}
