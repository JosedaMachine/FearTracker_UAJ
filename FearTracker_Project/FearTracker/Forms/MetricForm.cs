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
using AudioTracking;

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

            Series[] series = new Series[4];

            // Crear la serie de los gráficos
            if (shared_.trackerParams.MicTracking)
            {
                //Mic
                series[0] = createSeries(ref MicChart);
                series[0].Color = Color.Violet;
                configureAxis(ref MicChart, "Sound (db)");
            }

            if (shared_.trackerParams.mouseTracking)
            {
                //mouse
                series[1] = createSeries(ref mouseChart);
                series[1].Color = Color.Tomato;
                configureAxis(ref mouseChart, "Movement (points)");
            }

            if (shared_.trackerParams.KeyboardTracking)
            {
                //keyboard
                series[2] = createSeries(ref keyboardChart);
                configureAxis(ref keyboardChart, "Number of inputs");
            }

            //Scares
            series[3] = createSeries(ref scareChart);
            configureAxis(ref scareChart, "Scare");

            //Umbrales de susto
            float audioThreshold = AudioTracker.GetInstance().scareThreshold();
            double mouseThreshold = MouseTracker.GetInstance().scareThreshold();
            int inputThreshold = InputTracker.GetInstance().scareThreshold();

            bool userScared = false,unsavedPoint = true;//Se ha asustado ª

            // Agregar los puntos de datos a las series
            foreach (jsonData dato in datos)
            {
                int evType = (int)(dato.EventType[0]) - (int)'0';

                if (evType >= 0 && evType < 3)
                {
                    // Escribir puntos en graficas
                    float elapsedTime = (dato.TimeStamp - shared_.trackerParams.startTime)/1000.0f;
                    DataPoint punto = new DataPoint(elapsedTime, dato.y);
                    series[evType].Points.Add(punto);
                    if (!userScared)//Comprobar si el usuario ha superado algun umbral
                    {
                        switch (evType)
                        {
                            case 0://umbral mic
                                userScared = (dato.y > audioThreshold)? true: false;
                                break;
                            case 1://Umbral mouse
                                userScared = (dato.y > mouseThreshold)? true: false;
                                break;
                            case 2://umbral input
                                userScared = (dato.y > inputThreshold)? true: false;
                                break;
                            default:
                                break;
                        }
                    }

                    if(userScared && unsavedPoint){
                        DataPoint scarePoint = new DataPoint(elapsedTime, 1);//Es binario la coordenada y
                        series[3].Points.Add(scarePoint);
                        unsavedPoint = false;
                    }
                }
            }
        }
        private void configureAxis(ref Chart chart, string y)
        {
            chart.ChartAreas[0].AxisX.Title = "Time (s)";
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
