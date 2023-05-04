using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;


//Esto es de ejemplo para leerlo sin forms con la cosa esta de C# 9.0

//[DllImport("user32.dll")]
//static extern bool GetCursorPos(out POINT lpPoint);

//[StructLayout(LayoutKind.Sequential)]
//public struct POINT
//{
//    public int X;
//    public int Y;

//    public static implicit operator System.Drawing.Point(POINT point)
//    {
//        return new System.Drawing.Point(point.X, point.Y);
//    }
//}

//public void Example()
//{
//    POINT lpPoint;
//    if (GetCursorPos(out lpPoint))
//    {
//        Console.WriteLine(lpPoint);
//    }
//}

namespace Gravedad
{

    public class MouseReading
    {

        static void main()
        {
            


        }

        //Esto es para leerlo con forms
        //public void OnMouseMove(object sender, MouseEventArgs e)
        //{
        //    Console.WriteLine("Mouse moved by X: " + e.X + ", Y: " + e.Y);
        //}
    }
}
