using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gravedad
{
    internal static class Program
    {
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


            Console.WriteLine("Hola");

            //Tracker
            Application.Run(new Form1());


        }
    }


    //public partial class Form1 : Form
    //{
    //    private const double GravedadDeLaTierra = 9.81;

    //    public Form1()
    //    {
    //        InitializeComponent();
    //    }

    //    private void btnCalcular_Click(object sender, EventArgs e)
    //    {
    //        if (double.TryParse(txtPeso.Text, out double peso))
    //        {
    //            double pesoEnTierra = peso * GravedadDeLaTierra;
    //            lblResultado.Text = $"El peso en la tierra es: {pesoEnTierra:F2} N";
    //        }
    //        else
    //        {
    //            MessageBox.Show("Ingrese un número válido para el peso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //        }
    //    }
    //}
}
