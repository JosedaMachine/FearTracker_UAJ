using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Gravedad
{
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //Interface to choose program to track
            Application.Run(new Form1());


            TrackerProcces();

            //Tracker
            Application.Run(new Form1());


            
        }
        
        static void TrackerProcces()
        {

            Init();
            Start();
            while (!quit)
            {
                Update();
            }
            EndProgram();
        }

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

           MessageBox.Show("Time since start app: " + currentTime + " ms\n");
            MessageBox.Show("DeltaTime: " + deltaTime + " ms\n");
        }

        static void EndProgram()
        {

        }
    }
}
