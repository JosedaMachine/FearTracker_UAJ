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

            // Crear la serie de los graficos
            if (shared_.trackerParams.MicTracking)
            {
                //Mic
                series[0] = createSeries(ref MicChart);
                series[0].Color = Color.Violet;
                configureAxis(ref MicChart, "Sound (micrpohone audio level percentage)");
            }

            if (shared_.trackerParams.mouseTracking)
            {
                //mouse
                series[1] = createSeries(ref mouseChart);
                series[1].Color = Color.Tomato;
                configureAxis(ref mouseChart, "Movement (pixels per iteration)");
            }

            if (shared_.trackerParams.KeyboardTracking)
            {
                //keyboard
                series[2] = createSeries(ref keyboardChart);
                series[2].Color = Color.YellowGreen;
                configureAxis(ref keyboardChart, "Number of inputs");
            }

            //Scares
            series[3] = createSeries(ref scareChart);
            configureAxis(ref scareChart, "Scare");

            //Umbrales de susto
            float audioThreshold = AudioTracker.GetInstance().scareThreshold();
            double mouseThreshold = MouseTracker.GetInstance().ScareThreshold();
            int inputThreshold = InputTracker.GetInstance().GetScareThreshold();

            bool userScared = false;//Se ha asustado ª

            //Ultimo intervalo de grbacion de seguimiento
            float lastIntervalRecorded = 0;

            // Agregar los puntos de datos a las series
            foreach (jsonData dato in datos)
            {
                int evType = (int)(dato.EventType[0]) - (int)'0';
                float elapsedTime = (dato.TimeStamp - shared_.trackerParams.startTime) / 1000.0f;

                if (evType >= 0 && evType < 3)
                {
                    // Escribir puntos en graficas
                    DataPoint punto = new DataPoint(elapsedTime, dato.y);
                    series[evType].Points.Add(punto);
                }
                //Comprobar si ha habido algun susto
                if (!userScared)//Comprobar si el usuario ha superado algun umbral
                {
                    switch (evType)
                    {
                        case 0://umbral mic
                            userScared = (dato.y > audioThreshold) ? true : false;
                            break;
                        case 1://Umbral mouse
                            userScared = (dato.y > mouseThreshold) ? true : false;
                            break;
                        case 2://umbral input
                            userScared = (dato.y > inputThreshold) ? true : false;
                            break;
                        default:
                            break;
                    }
                }

                if (lastIntervalRecorded < elapsedTime)//Si ha pasado el intervalo de tiempo gestiona sustos
                {
                    int y = (userScared) ? 1 : 0;
                    DataPoint scarePoint = new DataPoint(elapsedTime, y);//Es binario la coordenada y
                    series[3].Points.Add(scarePoint);
                    //Reset
                    lastIntervalRecorded = elapsedTime + (float)(shared_.trackerParams.recordingTimeMilliseconds/1000);
                    userScared = false;
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
