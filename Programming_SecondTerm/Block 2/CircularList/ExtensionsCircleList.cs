using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularList
{
    internal static class ExtensionsCircleList
    {
        // Any<TSource>(IEnumerable<TSource>) – Проверяет, содержит ли последовательность какие-либо элементы.
        public static bool Any<TSource>(this IEnumerable<TSource> list)
        {
            return list.Select(i => i != null).FirstOrDefault();
        }

        // Any<TSource>(IEnumerable<TSource>) – Проверяет, содержит ли последовательность какие-либо элементы.
        public static bool MyAny<TSource>(this IEnumerable<TSource> list, Func<TSource, Boolean> condition)
        {
            foreach (var i in list)
            {
                if (condition(i))
                {
                    return true;
                }
            }
            return false;
        }

        //Append<TSource>(IEnumerable<TSource>, TSource) – Добавляет значение в конец последовательности.
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> list, TSource data) where TSource : IComparable
        {
            var newList = ((CircleList<TSource>)list).MyMemberwiseClone();
            newList.AddLast(data);
            return newList;
        }

        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null || second == null)
            {
                throw new ArgumentNullException();
            }

            var newList = first;
            foreach (var i in second)
            {
                newList.Append(i);
            }
            return newList;
        }

        //Contains<TSource>(IEnumerable<TSource>, TSource) – Определяет, содержится ли указанный
        //элемент в последовательности,используя компаратор проверки на равенство по умолчанию.
        public static bool Contains<TSource>(this IEnumerable<TSource> list, TSource data) where TSource : IComparable
        {
            foreach (var i in list)
            {
                if (i.CompareTo(data) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        //Count<TSource>(IEnumerable<TSource>) – Возвращает количество элементов в последовательности.
        public static int Count<TSource>(this IEnumerable<TSource> list)
        {
            int counter = 0;
            foreach (var i in list)
            {
                if (i != null)
                {
                    counter++;
                }
            }
            return counter;
        }

        //Count<TSource>(IEnumerable<TSource>, Func<TSource,Boolean>) – Возвращает число,
        //представляющее количество элементов последовательности, удовлетворяющих заданному условию.
        public static int Count<TSource>(this IEnumerable<TSource> list, Func<TSource, Boolean> condition)
        {
            int count = 0;
            foreach (var i in list)
            {
                if (i != null && condition(i))
                {
                    count++;
                }
            }
            return count;
        }

        //ElementAt<TSource>(IEnumerable<TSource>, Int32) – Возвращает элемент по указанному индексу в последовательности.
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> list, int index)
        {
            int counter = 0;
            foreach (var i in list)
            {
                if (counter == index)
                {
                    return i;
                }
                counter++;
            }

            throw new ArgumentOutOfRangeException();
        }

        //ElementAt<TSource>(IEnumerable<TSource>, Index) – Возвращает элемент по указанному индексу в последовательности.
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> list, Index index)
        {
            if (index.IsFromEnd == true)
            {
                TSource item = list.ElementAt(index.Value);
                return item;
            }
            else
            {
                var newList = list.Reverse();
                TSource item = newList.ElementAt(index.Value);
                return item;
            }
        }

        //ElementAtOrDefault<TSource>(IEnumerable<TSource>, Int32) – Возвращает элемент последовательности по указанному
        //индексу  или значение по умолчанию, если индекс вне допустимого диапазона.
        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> list, int index)
        {
            int counter = 0;
            foreach (var i in list)
            {
                if (counter == index)
                {
                    return i;
                }
                counter++;
            }
            return default;
        }

        //ElementAtOrDefault<TSource>(IEnumerable<TSource>, Index) – Возвращает элемент последовательности
        //по указанному индексу  или значение по умолчанию, если индекс вне допустимого диапазона.
        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> list, Index index)
        {
            if (list.Count() >= index.Value)
            {
                return default;
            }
            else
            {
                return list.ElementAt(index);
            }
        }

        //First<TSource>(IEnumerable<TSource>) – Возвращает первый элемент последовательности.
        public static TSource First<TSource>(this IEnumerable<TSource> list)
        {
            foreach (var i in list)
            {
                return i;
            }

            throw new ArgumentNullException();
        }

        //FirstOrDefault<TSource>(IEnumerable<TSource>) – Возвращает первый элемент последовательности
        //или значение по умолчанию, если последовательность не содержит элементов.
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> list)
        {
            foreach (var i in list)
            {
                return i;
            }
            return default;
        }

        //Last<TSource>(IEnumerable<TSource>) – Возвращает последний элемент последовательности.
        public static TSource Last<TSource>(this IEnumerable<TSource> list)
        {
            TSource returnItem = default;
            var counter = 0;
            foreach (var i in list)
            {
                returnItem = i;
                counter++;
            }

            if (counter == 0)
            {
                throw new ArgumentNullException();
            }

            return returnItem;
        }

        //LastOrDefault<TSource>(IEnumerable<TSource>) – Возвращает последний элемент последовательности или значение
        //по умолчанию, если последовательность не содержит элементов.
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> list)
        {
            TSource returnItem = default;
            var count = 0;
            foreach (var i in list)
            {
                returnItem = i;
                count++;
            }

            if (count == 0)
            {
                return default;
            }

            return returnItem;
        }

        //Max<TSource>(IEnumerable<TSource>) – Возвращает максимальное значение, содержащееся в универсальной последовательности
        public static TSource Max<TSource>(this IEnumerable<TSource> list) where TSource : IComparable
        {
            if (list.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            var max = list.First();
            foreach (var i in list)
            {
                if (i.CompareTo(max) > 0)
                {
                    max = i;
                }
            }

            return max;
        }

        //Min<TSource>(IEnumerable<TSource>) – Возвращает минимальное значение, содержащееся в универсальной последовательности
        public static TSource Min<TSource>(this IEnumerable<TSource> list) where TSource : IComparable
        {
            if (list.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            var min = list.First();
            foreach (var i in list)
            {
                if (i.CompareTo(min) < 0)
                {
                    min = i;
                }
            }

            return min;
        }

        //OrderBy<TSource,TKey>(IEnumerable<TSource>, Func<TSource,TKey>) - Сортирует элементы последовательности в порядке возрастания ключа.
        public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> list, Func<TSource, TKey> k)
        {
            if (k == null || list == null)
            {
                throw new ArgumentNullException();
            }

            var items = list.ToArray();
            var keys = items.Select(k).ToArray();

            Array.Sort(keys, items);

            foreach (var i in items)
            {
                yield return i;
            }
        }

        //OrderByDescending<TSource,TKey>(IEnumerable<TSource>, Func<TSource,TKey>) – Сортирует элементы последовательности в порядке убывания ключа.
        public static IEnumerable<TSource> OrederByDescending<TSource, TKey>(this IEnumerable<TSource> list, Func<TSource, TKey> k)
        {
            if (k == null || list == null)
            {
                throw new ArgumentNullException();
            }

            var items = list.ToArray();
            var keys = items.Select(k).Reverse().ToArray();

            Array.Sort(keys, items);

            foreach (var i in items)
            {
                yield return i;
            }
        }

        //Prepend<TSource>(IEnumerable<TSource>, TSource) – Добавляет значение в начало последовательности.
        public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> list, TSource data) where TSource : IComparable
        {
            var newList = ((CircleList<TSource>)list).MyMemberwiseClone();
            newList.AddFirst(data);
            return newList;
        }

        //Reverse<TSource>(IEnumerable<TSource>) – Изменяет порядок элементов последовательности на противоположный.
        public static IEnumerable<TSource> MyReverse<TSource>(this IEnumerable<TSource> list) where TSource : IComparable
        {
            var returnList = new CircleList<TSource>();
            foreach (var i in list)
            {
                returnList.AddFirst(i);
            }
            return returnList;
        }

        //SequenceEqual<TSource>(IEnumerable<TSource>, IEnumerable<TSource>) – Определяет, совпадают ли две последовательности,
        //используя для сравнения элементов компаратор проверки на равенство по умолчанию, предназначенный для их типа.
        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second) where TSource : IComparable
        {
            if (first.Count() != second.Count())
            {
                return false;
            }

            var firstArray = new TSource[first.Count()];
            var counter = 0;

            foreach (var i in first)
            {
                firstArray[counter] = i;
            }

            var secondArray = new TSource[second.Count()];
            counter = 0;
            foreach (var i in second)
            {
                secondArray[counter] = i;
            }

            for (counter = 0; counter < second.Count(); counter++)
            {
                if (firstArray[counter].CompareTo(secondArray[counter]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        //Single<TSource>(IEnumerable<TSource>) –Возвращает единственный элемент последовательности и генерирует исключение, если число элементов последовательности отлично от 1.
        public static TSource Single<TSource>(this IEnumerable<TSource> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }
            else if (list.Count() != 1)
            {
                throw new InvalidOperationException();
            }
            else
            {
                foreach (var i in list)
                {
                    return i;
                }
            }
            throw new InvalidOperationException();
        }

        //Take<TSource>(IEnumerable<TSource>, Int32) – Возвращает указанное число подряд идущих элементов с начала последовательности.
        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> list, int number) where TSource : IComparable
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            int counter = 0;

            IEnumerable<TSource> newList = new CircleList<TSource>();

            if (number <= 0)
            {
                return newList;
            }

            foreach (var i in list)
            {
                counter++;
                newList.Append(i);
                if (counter == number)
                {
                    return newList;
                }
            }
            return newList;
        }

        //TakeLast<TSource>(IEnumerable<TSource>, Int32) – Возвращает новую перечислимую коллекцию, содержащую последние count элементов из source.
        public static IEnumerable<TSource> TakeLast<TSource>(this IEnumerable<TSource> list, int n)
        {
            var newList = list.Reverse();
            return newList.Take(n).Reverse();
        }

    }
}
