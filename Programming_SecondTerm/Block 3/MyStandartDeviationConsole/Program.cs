using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStandardDeviation
{
    class Program
    {
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
                    Console.WriteLine(e);
                    Console.WriteLine($"\nЧисла были заданы в неверном формате.\n");
                    Console.WriteLine();
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

        static void Main(string[] args)
        {
            Console.WriteLine("Программа для нахождения Стандартного Отклонения и Дисперсии по заданому набору чисел: \n");
            Console.WriteLine("Введите набор числел через пробел и нажмите Enter: \n");
            var strArrey = Console.ReadLine();
            Console.WriteLine("");

            double[] array = GetArrey(strArrey);
            int totalElements = array.Length;

            double mean = MeanCalculator(array, totalElements);
            double variance = GetVariance(array, totalElements);
            double result = StandardDeviation(array, totalElements);

            Console.WriteLine("Стандартное отклонение: {0:F3}", result);
            Console.WriteLine("Дисперсия: {0:F3}", variance);
            Console.WriteLine("Минимальная CO: {0:F3}", mean - result);
            Console.WriteLine("Максимальная CO: {0:F3}", mean + result);
            Console.ReadLine();
        }
    }
}
