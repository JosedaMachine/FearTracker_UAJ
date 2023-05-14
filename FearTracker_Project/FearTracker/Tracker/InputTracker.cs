using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using System.Timers;
using GameTracker;

namespace FT
{
    internal class InputTracker
    {
        private List<Tuple<string, int>> keyRepetitions = new List<Tuple<string, int>>();
        private List<Tuple<string, System.Timers.Timer>> keyTimers = new List<Tuple<string, System.Timers.Timer>>();
        private Dictionary<int, bool> previousKeyStates = new Dictionary<int, bool>();

        private List<System.Timers.Timer> timerList = new List<System.Timers.Timer>();

        //---Asignar valores---
        private int msKeyTimer = 1000; //Tiempo en ms que tiene una tecla para ver su nº de repeticiones
        private int minRepetitions = 3;
        //------------



        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        public void readInput()
        {
            //Comprobar si se ha pulsado
            for (int i = 0; i < 255; i++)
            {
                int state = GetAsyncKeyState(i);
                bool isPressed = (state & 0x8000) != 0;

                if (isPressed && !previousKeyStates.ContainsKey(i))
                {
                    string pressedKey = ((System.Windows.Forms.Keys)i).ToString();

                    //Console.WriteLine("You have pressed: " + pressedKey);

                    registerRecurrentInput(pressedKey);

                    previousKeyStates[i] = true;
                }
            }

            //Comprobar si se ha soltado
            for (int i = 0; i < 255; i++)
            {
                int state = GetAsyncKeyState(i);
                bool isPressed = (state & 0x8000) != 0;

                if (!isPressed && previousKeyStates.ContainsKey(i) && previousKeyStates[i] == true)
                {
                    string pressedKey = ((System.Windows.Forms.Keys)i).ToString();

                   //Console.WriteLine("You have pressed: " + pressedKey);

                    previousKeyStates.Remove(i);//Se quita para contar la siguiente vez que haya pulsacion
                }
            }
        }

        public void registerRecurrentInput(string keyName)
        {
            //Comprobar la repeticion de input
            int i = 0;
            while (i < keyRepetitions.Count && keyName != keyRepetitions[i].Item1 ) i++;
            if(i < keyRepetitions.Count)    //Se ha encontrado esa repeticion
            {
                //Se suma 1
                Tuple<string, int> tuple = new Tuple<string, int>(keyName, keyRepetitions[i].Item2 + 1);
                keyRepetitions[i] = tuple;
            }
            else                            //Se añade a la lista
            {
                //Console.WriteLine("Timer started with key: " + keyName);
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = msKeyTimer;
                timer.Enabled = true;
                timer.Elapsed += TimerElapsedEventHandler;

                Tuple<string, System.Timers.Timer> keyTimer = new Tuple<string, System.Timers.Timer>(keyName, timer);
                keyTimers.Add(keyTimer);

                Tuple<string, int> keyRepetition = new Tuple<string, int>(keyName, 1);
                keyRepetitions.Add(keyRepetition);
            }
        }

        private void TimerElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            //Si el timer asignado a cada key pulsada se acaba, se quita de la lista
            System.Timers.Timer timer = sender as System.Timers.Timer;
            string keyName = "";
            int i = 0;
            while (i < keyTimers.Count && keyTimers[i].Item1 != null && timer != keyTimers[i].Item2) i++;
            if (i < keyTimers.Count)
            {
                keyName = keyTimers[i].Item1;

                //Eliminar los elementos de cada lista
                keyTimers.RemoveAt(i);//Se borra de los timers
            }

            i = 0;
            while (i < keyRepetitions.Count && keyRepetitions[i].Item1 != null && keyName != keyRepetitions[i].Item1) i++;
            if (i < keyRepetitions.Count)
            {
                //Enviar evento si cumple los requisitos
                if (keyRepetitions[i].Item2 > minRepetitions)
                {
                    eventInputHandler(keyRepetitions[i].Item1, keyRepetitions[i].Item2); //Mandar evento de teclado
                }

                keyRepetitions.RemoveAt(i);//Se borra de la lista de repeticiones
            }
        }

        public void eventInputHandler(string keyName, int repetitions)
        {
            TrackerSystem ts = TrackerSystem.GetInstance();
            KeyboardEvent scare = ts.CreateEvent<KeyboardEvent>();
            scare.setNumInputs((short)repetitions);
            ts.trackEvent(scare);

            Console.WriteLine("Key {0} was pressed {1} times", keyName, repetitions);
        }

    }
}
