using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace Gravedad
{
    internal class InputTracker
    {
        private List<Tuple<string, float>> keysRead = new List<Tuple<string, float>>();
        private Dictionary<int, bool> previousKeyStates = new Dictionary<int, bool>();

        private bool read = false;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        public InputTracker()
        {
            read = false;
        }

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
                    switch (pressedKey)
                    {
                        default:
                            Console.WriteLine("You have pressed: " + pressedKey);
                            break;
                    }

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
                    switch (pressedKey)
                    {
                        default:
                            Console.WriteLine("You have released: " + pressedKey);
                            break;
                    }
                    previousKeyStates.Remove(i);
                }
            }
        }

        public void StopReadingInput()
        {
            read = false;
        }

    }
}
