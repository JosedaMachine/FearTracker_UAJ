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
    public struct TrackerParams
    {
        public bool mouseTracking, 
            MicTracking, 
            KeyboardTracking, 
            cameraTracking;
        //public TrackerParams() { params_ = new bool[4]; }
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
            
            Process processToTrack = new Process();
            TrackerParams trackerParams = new TrackerParams();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Interface to choose program to track
            Application.Run(new Form1(ref processToTrack, ref trackerParams));

            //Thread formsEntry = new Thread(new ThreadStart(StartUI));
            //Thread tracker = new Thread(new ThreadStart(StartTracker));

            //formsEntry.Start();

            DateTime currentTime;

            #region MouseVariables
            Point oldMousePosition = Cursor.Position;   //Position of the mouse in the previous iteration
            int mousePositionDifference;    //Difference in position between previous iteration and current one
            int offsetMouseDifference = 20;  //Offset
            double averageDifference = 0;  //Average movement, this variable changes during execution and decides if a movement is sporadic or not
            long numberOfMovements = 0;     //Number of times the average has been determined
            float scaredMouseMultiplier = 1.7f; //Multiplier applied to the average to determine if the movement of the mouse was sporadic or not
                                                //Decrease to increase sensitivity

            DateTime lastMouseScareTime = DateTime.Now; //Saves when was the last mouse scare to avoid repeating the same input
            int mouseScareTimeOffset = 1;   //Minimum offset between scares in seconds
            #endregion

            while (!processToTrack.HasExited)
            {
                currentTime = DateTime.Now;

                if (trackerParams.mouseTracking)
                {
                    #region MouseTracking
                    Point currentMousePosition = Cursor.Position;

                    //We calculate how much the muse has moved in this iteration
                    int mouseDifferenceX = Math.Abs(oldMousePosition.X - currentMousePosition.X);
                    int mouseDifferenceY = Math.Abs(oldMousePosition.Y - currentMousePosition.Y);
                    mousePositionDifference = mouseDifferenceX + mouseDifferenceY;

                    //We only determine the average and register the input if the difference surpasses the offset
                    if (mousePositionDifference >= offsetMouseDifference)
                    {
                        //We avoid getting various mouseScareEvents if one has already ocurred recently
                        if ((currentTime - lastMouseScareTime).Seconds > mouseScareTimeOffset)
                        {
                            //Determining if the user was scared or not
                            if (mousePositionDifference > averageDifference * scaredMouseMultiplier)
                            {
                                lastMouseScareTime = DateTime.Now;
                                Console.WriteLine("SUSTO RATON");
                            }
                        }

                        //We determine the new average
                        numberOfMovements++;
                        averageDifference = (double)((averageDifference * (numberOfMovements - 1) + mousePositionDifference)) / (double)numberOfMovements;
                        oldMousePosition = currentMousePosition;
                        //Console.WriteLine("Diferencia de movimiento: X={0}", mousePositionDifference);

                    }
                    #endregion
                }


                // Espera a que el proceso termine
            }

            Application.Run(new Form1(ref processToTrack, ref trackerParams));

            //Tracker
            //Application.Run(new Form1());

            //if()

            //tracker.Start();

        }

        static void StartUI()
        {
            //Interface to choose program to track
            //Application.Run(new Form1());
        }

        static void StartTracker()
        {
            TrackerProcces();
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
