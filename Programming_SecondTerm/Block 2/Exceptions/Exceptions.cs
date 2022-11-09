using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class Class1
    {
        public int Number;
    }

    class Exceptions
    {
        public bool DivideByZeroException(int divisible, int divider) //1) Обрабатываем исключение при делении на ноль
        {
            Console.WriteLine("1)Исключение DivideByZeroException: проверяем, делится ли одно число на другое нацело.\n");
            Console.WriteLine("Исключение при делителе равном 0.\n");

            try
            {
                var tryDivide = divisible % divider;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e);
                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine($"В качестве делителя был введен 0.");
                Console.WriteLine();
                return false;
            }

            if (divisible % divider == 0)
            {
                Console.WriteLine($"Число {divisible} делится нацело на число {divider}.");
                return true;
            }

            Console.WriteLine($"Число {divisible} не делится нацело на число {divider}.");

            return false;
        }

        public int OverflowException(int number)   //2) Обрабатываем сключение при выходе за границы Int32 (при исключении результат -1)
        {
            Console.WriteLine("2)Исключение OverflowException: принимаем целое число и вычисляем его факториал.\n");
            Console.WriteLine("Исключение при факториале вышедшем за границы допустимых значений Int32.\n");

            int result = 1;
            try
            {
                for (var i = 2; i <= number; i++)
                {
                    result = checked(result * i);
                    Console.WriteLine(result);
                }
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e);
                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine($"Факториал числа вышел за границы допустимых значений Int32.");
                Console.WriteLine();
                result = -1;
            }

            Console.WriteLine($"Факториал числа {number}: {result}");
            Console.WriteLine();
            return result;
        }

        public int FormatException(string str)  //3) Обрабатываем исключение при нейдачной попытке сконвертировать строку в число (при исключении результат -1)
        {
            Console.WriteLine("3)Исключение FormatException: принимаем строку и конвертируем ее в целое число.\n");
            Console.WriteLine("Исключение если в строке не число, или оно не целое.\n");

            int result = 0;

            try
            {
                result = Convert.ToInt32(str);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine($"В строке содержалось не целое число число");
                Console.WriteLine();
                result = -1;
            }

            Console.WriteLine($"Результат конвертирования строки: {result}");
            Console.WriteLine();
            return result;
        }

        public bool IndexOutOfRangeException(int[] array, int index)    //4) Обрабатываем исключение при выходе за границы массива
        {
            Console.WriteLine("4)Исключение IndexOutOfRangeException: принимаем массив целых чисел и индекс, выводим значение элемента массива по индексу.\n");
            Console.WriteLine("Исключение если вышли за границы массива.\n");

            int result = 0;
            try
            {
                result = array[index];
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine($"Выход за границы массива.");
                Console.WriteLine();
                return false;
            }

            Console.WriteLine($"Значение по указанному индексу: {result}");
            Console.WriteLine();
            return true;
        }

        public bool ArrayTypeMismatchException(Object[] array, Object obj) // 5) Обрабатываем исключени при попытке сохранить в массиве элемент неподходящего типа,
                                                                           // выводя сообщение на консоль и возвращая false.
        {
            Console.WriteLine("5)Исключение ArrayTypeMismatchException: принимаем массив  и объект, выводим подтверждение успешного сорханения в массиве.\n");
            Console.WriteLine("Исключение при попыте сохранить объект неподходящего типа.\n");

            try
            {
                array[0] = obj;
            }
            catch (ArrayTypeMismatchException e)
            {
                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine("Попытка сохранить в массиве элемент неподходящего типа.");
                Console.WriteLine();
                return false;
            }

            Console.WriteLine("Сохранили элемент в массиве.");
            Console.WriteLine();
            return true;
        }

        
        public Class1 NullReferenceException(Class1 class1) // 6) Обрабатываем исключение при попытке обратиться к объекту, равному null, производя инициализацию.
        {
            Console.WriteLine("6)Исключение NullReferenceException: принимаем объект класса, присваиваем в Number число 10.\n");
            Console.WriteLine("Исключение при попытке обратиться к объекту, равному null.\n");

            try
            {
                class1.Number = 10;
            }
            catch (NullReferenceException e)
            {

                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine("Попытка обратиться к объекту, равному null.");
                Console.WriteLine();
                class1 = new Class1 
                { 
                    Number = 10 
                };
            }
            Console.WriteLine($"Number в Class1 равен {class1.Number}");
            Console.WriteLine();
            return class1;
        }

        public void DirectoryNotFoundException(string dir) // 7) Обрабатываем исключение при попытке перейти в несуществующую директорию.
        {
            Console.WriteLine("7)Исключение DirectoryNotFoundException: принимаем строку - директорию  и пытаемся в нее перейти.\n");
            Console.WriteLine("Исключение если директории не существует.\n");

            try
            {
                Directory.SetCurrentDirectory(dir);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine("Попытка перейти в несуществующую директорию.");
                Console.WriteLine();
                return;
            }

        }

        public void InvalidOperationExceptionFixed(List<int> numbers) //8) Обрабатываем исключение при попытке добывить число в коллекцию во время итерации. 
        {
            Console.WriteLine("8)Исключение InvalidOperationException: выполняем итерации коллекции целых чисел и пытаемся добавить квадрат каждого целого числа в коллекцию.\n");
            Console.WriteLine("Исключение при попытке добывить число в коллецию во время итерации.\n");

            try
            {
                foreach (var i in numbers)
                {
                    int result = (int)Math.Pow(i, 2);
                    numbers.Add(result);
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine("При итерации коллекции пытались в нее что-то добавить.");
                Console.WriteLine();
                return;
            }

            foreach (var i in numbers)
            {
                int result = (int)Math.Pow(i, 2);
                numbers.Add(result);
            }
        }

        public FileStream FileNotFoundException(string path) // 9) Обрабатываем исключение при некорректном открытии файла, создавая новый.
        {
            Console.WriteLine("9)Исключение FileNotFoundException: открываем файл по переданому пути и возвращаем его.\n");
            Console.WriteLine("Исключение при попытке открыть файл, который не существует.\n");

            FileStream text;
            try
            {
                text = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"\nИсключение: {e.Message}\n");
                Console.WriteLine($"Метод, вызвавший исключение: {e.TargetSite}");
                Console.WriteLine("Файл не существует, поэтому создаем новый.");
                Console.WriteLine();

                text = File.Create(Path.GetTempFileName());
                return text;
            }

            Console.WriteLine("Файл открыт успешно.");
            Console.WriteLine();
            return text;
        }
    }
}
