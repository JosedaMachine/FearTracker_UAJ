using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gravedad
{
    public partial class Form1 : Form
    {

        private const double GravedadDeLaTierra = 9.81;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void calcWeight_Click(object sender, EventArgs e)
        {
            if (double.TryParse(massEntry.Text, out double peso))
            {
                double pesoEnTierra = peso * GravedadDeLaTierra;
                weightResult.Text = $"Weight in earth is: {pesoEnTierra:F2} N";
            }
            else
            {
                MessageBox.Show("Ingrese un número válido para el peso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
