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
        private List<Tuple<ConsoleKey, float>> keysRead = new List<Tuple<ConsoleKey, float>>();
        private bool read = false;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        public InputTracker()
        {
            read = false;
        }

        public void readInput()
        {
            for (int i = 0; i < 255; i++)
            {
                int state = GetAsyncKeyState(i);
                if (state != 0)
                {
                    string pressedKey = ((System.Windows.Forms.Keys)i).ToString();
                    switch (pressedKey)
                    {
                        default:
                            Console.WriteLine("You have pressed: " + pressedKey);
                            break;
                    }
                }
            }
        }

        public void StopReadingInput()
        {
            read = false;
        }

    }
}
