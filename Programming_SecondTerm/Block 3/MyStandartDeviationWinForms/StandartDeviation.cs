using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStandartDeviationWinForms
{
    public partial class StandartDeviation : Form
    {
        public StandartDeviation()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var strArrey = textBox1.Text;
            double[] array = GetArrey(strArrey);
            int totalElements = array.Length;

            double variance = GetVariance(array, totalElements);
            double result = StandardDeviation(array, totalElements);

            label4.Text = result.ToString();
            label6.Text = variance.ToString();

        }

        static double[] GetArrey(string str)
        {
            string[] temp = str.Split(new Char[] { ' ' });
            double[] result = new double[temp.Length];

            int counter = 0;
            foreach (string i in temp)
            {
                try
                {
                    double n = double.Parse(i);
                    result[counter] = n;
                    counter++;
                }
                catch (FormatException e)
                {
                    return result;
                }

            }
            return result;
        }

        static double MeanCalculator(double[] array, int x) //Расчет среднего значения
        {
            double mean = 0;
            for (int i = 0; i < x; i++)
            {
                mean += array[i];
            }
            mean /= x;

            return mean;
        }

        static double GetVariance(double[] array, int x)
        {
            double mean = MeanCalculator(array, x);
            double variance = 0;

            for (int i = 0; i < x; i++)
            {
                variance = variance + Math.Pow((array[i] - mean), 2);
            }

            return variance / (x - 1);
        }

        static double StandardDeviation(double[] array, int x)
        {

            double standardDeviation = Math.Sqrt(GetVariance(array, x));

            return standardDeviation;
        }

    }
}
