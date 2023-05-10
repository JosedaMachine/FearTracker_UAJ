using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace FT
{
    public class jsonData
    {
        public int typeId { get; set; }
        public double time { get; set; }
        public double y { get; set; }
    }
    internal static class main
    {
        public static DateTime initDateTime, endDateTime;
        public static float currentTime;
        static Stopwatch stopwatch;
        static bool quit = false;

        static MouseTracker mouseTracker;
        static InputTracker inputTracker;
        

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
            shared.Parameters =  trackerParams;

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
            TrackerParams parameters = shared.Parameters;
           
            //Esperar a que la aplicación permita iniciarse.
            while (!parameters.canStart) {};

            //Iniciar programa
            parameters.process.Start();

            //Empezar a trackear
            Init();
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
            };

            Console.WriteLine("Deja");

            // Realiza el trabajo con los argumentos

            //TrackerProcces();

            //Point oldPosicion = Cursor.Position;
            //while (!processToTrack.HasExited)
            //{
            //    Point posicion = Cursor.Position;
            //    if (posicion != oldPosicion)
            //    {
            //        oldPosicion = posicion;
            //        Console.WriteLine("Posición del ratón: X={0}, Y={1}", posicion.X, posicion.Y);
            //    }
            //    // Espera a que el proceso termine
            //}
        }

        #region Tracker
        static void Init()
        {
            //Trackers
            inputTracker = new InputTracker();
            mouseTracker = new MouseTracker();

            stopwatch = new Stopwatch();
        }

        static void Start()
        {
            //Inicializacón de variables
            stopwatch.Start();
            initDateTime = DateTime.Now;
            currentTime = 0.0f;

            //Inicializacion de procesos
        }

        #endregion

    }
}
