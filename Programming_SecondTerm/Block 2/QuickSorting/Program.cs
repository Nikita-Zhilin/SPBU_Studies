using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralizedSorting
{
    class Program
    {
        private static void GeneralizedSorting<T>(T[] items, int left, int right) where T : IComparable
        {
            int i, j;
            i = left; j = right;
            IComparable partition = items[left];

            while (i <= j)
            {
                for (; (items[i].CompareTo(partition) < 0) && (i.CompareTo(right) < 0); i++) ;
                for (; (partition.CompareTo(items[j]) < 0) && (j.CompareTo(left) > 0); j--) ;

                if (i <= j)
                    swap(ref items[i++], ref items[j--]);

            }
            if (left < j) GeneralizedSorting<T>(items, left, j);
            if (i < right) GeneralizedSorting<T>(items, i, right);
        }
        static void swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }

        static void Main(string[] args)
        {
            IComparable[] array1 = { 3, 5, 7, 8, 1, 2 };


            foreach (int s in array1)
            {
                Console.WriteLine(" {0} ", s);
            }

            Console.ReadKey();
            Console.WriteLine("Отсортированная версия:");
            foreach (int x in array1)
            {
                GeneralizedSorting(array1, 0, array1.Length - 1);
                Console.WriteLine(" {0} ", x);
            }
            Console.ReadKey();
        }


    }
}

