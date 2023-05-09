using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Gravedad
{
    public class Dato
    {
        public double Tiempo { get; set; }
        public double Velocidad { get; set; }
    }
    internal static class Program
    {
        public static DateTime initDateTime, endDateTime;
        public static float currentTime;
        static Stopwatch stopwatch;
        static bool quit = false;
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
            Thread newThread = new Thread(StartTracker);
            newThread.Start(shared);

            //Iniciar app para indicar parametros de tracking
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(ref shared));
            
            
            //Esperar a que acabe el hilo
            newThread.Join();
            //InputTracker inputTracker = new InputTracker();
            //MouseTracker mouseTracker = new MouseTracker();


            //while (!processToTrack.HasExit    ed)
            //{
            //    currentTime = DateTime.Now;

            //    if (trackerParams.mouseTracking)
            //    {
            //        mouseTracker.readInput(currentTime);
            //    }
            //    if(trackerParams.KeyboardTracking)
            //        inputTracker.readInput();
            //    // Espera a que el proceso termine
            //}

            //Application.Run(new MetricForm());
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
            //Init();
            //Start();

            while (!parameters.canStop) { };
            //Update();

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
            //Carga de procesos e hilos
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2); // Uses the second Core or Processor for the Test
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;      // Prevents "Normal" processes 
                                                                                        // from interrupting Threads
            Thread.CurrentThread.Priority = ThreadPriority.Highest;     // Prevents "Normal" Threads 
                                                                        // from interrupting this thread
                                                                        //Creación de variables e instancias
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

        static void Update()
        {
            float deltaTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
            currentTime += deltaTime;
            stopwatch.Reset();
            stopwatch.Start();

            //MessageBox.Show("Time since start app: " + currentTime + " ms\n");
            //MessageBox.Show("DeltaTime: " + deltaTime + " ms\n");
        }
        #endregion

    }
}
