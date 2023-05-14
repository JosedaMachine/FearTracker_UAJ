using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using AudioTracking;

using GameTracker;

namespace FT
{
    public class jsonData
    {
        //0:Microphone, 1:Mouse, 2:Keyboard
        public int typeId { get; set; }
        public double time { get; set; }
        public double y { get; set; }
    }
    internal static class main
    {
        public static DateTime initDateTime, endDateTime;   //Para los timers 
        public static float currentTime;    
        static Stopwatch stopwatch;
        static bool quit = false;   //Para salir de la aplicación

        //Los distintos trackers
        static MouseTracker mouseTracker;
        static InputTracker inputTracker;
        static AudioTracker audioTracker;


        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Parametros de tracking
            TrackerParams trackerParams = new TrackerParams();
            trackerParams.process = new Process();

            SharedObject shared = new SharedObject();
            shared.trackerParams =  trackerParams;

            //Hilo para realizar el tracking
            Thread trackerThread = new Thread(StartTracker);
            trackerThread.Start(shared);

            //Iniciar app para indicar parametros de tracking
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainHubForm(ref shared));
            
            
            //Esperar a que acabe el hilo
            trackerThread.Join();
            Application.Run(new MetricForm(ref shared));
        }

        static void StartTracker(object arg)
        {
            SharedObject shared = (SharedObject)arg;
            TrackerParams parameters = shared.trackerParams;

            //Lo he movido aquí para que exista el AudioTracker en el menu
            parameters.audioTracker = new AudioTracker();
            audioTracker = parameters.audioTracker;

            //Esperar a que la aplicación permita iniciarse.
            while (!parameters.canStart) { };

            //Iniciar programa
            parameters.process.Start();

            //Empezar a trackear
            Init(ref parameters);
            Start();

            while (!parameters.canStop) {

                float deltaTime = stopwatch.ElapsedMilliseconds;
                stopwatch.Stop();
                currentTime += deltaTime;
                stopwatch.Reset();
                stopwatch.Start();

                if (parameters.mouseTracking)
                {
                    mouseTracker.readInput(DateTime.Now);
                }
                if (parameters.KeyboardTracking)
                    inputTracker.readInput();
                if (parameters.MicTracking)
                    audioTracker.ReadInput();
            };

            Stop();
        }

        #region Tracker
        static void Init(ref TrackerParams trackerParams)
        {
            //Input Trackers
            inputTracker = new InputTracker();
            mouseTracker = new MouseTracker();

            //trackerParams.audioTracker = new AudioTracker();
            //audioTracker = trackerParams.audioTracker;


            //Main tracker events
            ISerializer serializerCSV = new CSVSerializer();
            IPersistence filePersistence = new FilePersistence(ref serializerCSV);

            string nameApp = trackerParams.process.ProcessName;

            TrackerSystem.Init(nameApp, "2", Environment.UserName, ref filePersistence);

            TrackerSystem tracker = TrackerSystem.GetInstance();

            ISerializer serializerJSON = new JsonSerializer();
            IPersistence filePersistenceCopy = new FilePersistence(ref serializerJSON);

            tracker.AddPersistence(ref filePersistenceCopy);

            tracker.setFrecuencyPersistanceTimeSeconds(3);

            stopwatch = new Stopwatch();
        }

        static void Start()
        {
            //Iniciar el tracker
            TrackerSystem.GetInstance().Start();

            //Inicializacón de variables
            stopwatch.Start();
            initDateTime = DateTime.Now;
            currentTime = 0.0f;
        }
        static void Stop()
        {
            TrackerSystem tracker = TrackerSystem.GetInstance();
            
            tracker.Stop();

            tracker.Persist();
        }

        #endregion

    }
}
