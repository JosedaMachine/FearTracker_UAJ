﻿using Newtonsoft.Json;
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
        SharedObject shared_;
        public MetricForm(ref SharedObject shared)
        {
            shared_ = shared;
            InitializeComponent();

        }

        private void MetricForm_Load(object sender, EventArgs e)
        {
            // Leer el archivo JSON
            string json = File.ReadAllText("datos.json");
            List<Dato> datos = JsonConvert.DeserializeObject<List<Dato>>(json);

            Series[] series = new Series[3];

            Console.WriteLine();

            // Crear la serie de los gráficos
            if (shared_.Parameters.MicTracking)
            {
                //Mic
                series[0] = createSeries(ref MicChart);
                series[0].Color = Color.Violet;
            }

            if (shared_.Parameters.mouseTracking)
            {
                //mouse
                series[1] = createSeries(ref mouseChart);
                series[1].Color = Color.Tomato;
            }

            if (shared_.Parameters.KeyboardTracking)
                //keyboard
                series[2] = createSeries(ref keyboardChart);
            

            // Agregar los puntos de datos a las series
            foreach (Dato dato in datos)
            {
                DataPoint punto = new DataPoint(dato.time, dato.y);
                series[dato.typeId].Points.Add(punto);
            }

            // Configurar los ejes del gráfico
            if (shared_.Parameters.MicTracking)
                configureAxis(ref MicChart, "Sonido (db)");
            if (shared_.Parameters.mouseTracking)
                configureAxis(ref mouseChart, "Velocidad (m/s)");
            if (shared_.Parameters.KeyboardTracking)
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
