using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularList
{
    internal class CircleList<T> : IEnumerable<T> where T : IComparable
    {
        public CircleListNode<T> First { get; private set; }    //First – Получает первый узел объекта CircleList<T>.
        public CircleListNode<T> Last { get; private set; }     //Last – Получает последний узел объекта CircleList<T>.

        //Count – Получает число узлов, которое в действительности хранится в CircleList<T>.
        private int count;
        public int Count  
        { 
            get { return count; } 
        }

        public bool IsEmpty 
        { 
            get { return count == 0; }
        }

        public CircleList() //CircleList<T>() – Инициализирует новый экземпляр пустого класса CircleList<T>
        { 
        }

        //CircleList<T>(IEnumerable<T>) – Инициализирует новый экземпляр класса CircleList<T>, содержащий элементы,
        //скопированные из указанного класса IEnumerable, и обладающий емкостью, достаточной для того,
        //чтобы вместить количество скопированных элементов.
        public CircleList(IEnumerable<T> list)  
        {
            count = 0;
            foreach (var i in list)
            {
                AddLast(i);
            }
        }

        //AddAfter(CircleListNode<T>, CircleListNode<T>) – Добавляет заданный новый узел
        //после заданного существующего узла в CircleList<T>
        public void AddAfter(CircleListNode<T> newNode, CircleListNode<T> current)
        {
            if (Count == 0)
            {
                throw new ArgumentNullException();
            }
            var next = current.Next;
            current.Next = newNode;
            newNode.Next = next;
            count += 1;
        }

        //AddAfter(CircleListNode<T>, T) – Добавляет новый узел, содержащий
        // заданное значение, после заданного существующего узла в CircleList<T>
        public void AddAfter(CircleListNode<T> current, T data)
        {
            var newNode = new CircleListNode<T>(data);
            AddAfter(current, newNode);
        }

        //AddBefore(CircleListNode<T>, CircleListNode<T>) – Добавляет заданный 
        //новый узел перед заданным существующим узлом в CircleList<T>
        public void AddBefore(CircleListNode<T> newNode, CircleListNode<T> current)
        {
            if (count == 1)
            {
                Last = newNode;
                Last.Next = First;
                First.Next = Last;
                count++;
                return;
            }

            if (current == First)
            {
                Last.Next = newNode;
                Last = newNode;
                Last.Next = First;
                count++;
                return;
            }

            var previous = current;
            for (var i = 0; i < count; i++)
            {
                previous = current;
                current = current.Next;
            }
            previous.Next = newNode;
            newNode.Next = current;
            count++;
        }

        //AddBefore(CircleListNode<T>, T) – Добавляет новый узел, содержащий заданное
        //значение, перед заданным существующим узлом в CircleList<T>
        public void AddBefore(CircleListNode<T> current, T data)
        {
            var newNode = new CircleListNode<T>(data);
            AddBefore(current, newNode);
        }

        //AddFirst(CircleListNode<T>) – Добавляет заданный новый узел в начало CircleList<T>
        public void AddFirst(CircleListNode<T> newNode)
        {
            if (First == null)
            {
                First = newNode;
                Last = newNode;
                Last.Next = First;
            }
            else
            {
                newNode.Next = First;
                First = newNode;
                Last.Next = newNode;
            }
            count++;
        }

        //AddFirst(T) – Добавляет новый узел, содержащий заданное значение, в начало CircleList<T>
        public void AddFirst(T data)
        {
            var newNode = new CircleListNode<T>(data);
            AddFirst(newNode);
        }

        //AddLast(CircleListNode<T>) – Добавляет заданный новый узел в конец CircleList<T>
        public void AddLast(CircleListNode<T> newNode)
        {
            if (First == null)
            {
                First = newNode;
                Last = newNode;
                Last.Next = First;
            }
            else
            {
                newNode.Next = First;
                Last.Next = newNode;
                Last = newNode;
            }
            count++;
        }

        //AddLast(T) – Добавляет новый узел, содержащий заданное значение, в конец CircleList<T>
        public void AddLast(T data)
        {
            var newNode = new CircleListNode<T>(data);
            AddLast(newNode);
        }

        //Clear() – Удаляет все узлы из CircleList<T>
        public void Clear()
        {
            First = null;
            Last = First;
            count = 0;
        }

        //Contains(T) – Определяет, принадлежит ли значение объекту CircleList<T>
        public bool Contains(T data)
        {
            var current = First;

            if (null == current)
            {
                return false;
            }

            do
            {
                if (current.Data.Equals(data))
                {
                    return true;
                }
                current = current.Next;
            } while (current != First);

            return false;
        }

        //Equals(Object) – Определяет, равен ли указанный объект текущему объекту. (Унаследовано от Object) 
        public bool Equals(Object obj)
        {
            CircleList<T> consideredList;

            consideredList = (CircleList<T>)obj;

            var current = consideredList.First;

            foreach (var i in this)
            {
                if (current.Data.CompareTo(i) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        //Find(T) – Находит первый узел, содержащий указанное значение.
        public CircleListNode<T> Find(T data)
        {
            if (First == null)
            {
                return null;
            }

            var current = First;

            if (current.Data.CompareTo(data) == 0)
            {
                return First;
            }

            while (current.Next != First)
            {
                current = current.Next;
                if (current.Data.CompareTo(data) == 0)
                {
                    return current;
                }
            }

            return null;
        }

        //FindLast(T) – Находит последний узел, содержащий указанное значение.
        public CircleListNode<T> FindLast(T data)
        {
            if (First == null)
            {
                return null;
            }

            var current = First;
            CircleListNode<T> returnNode = null;

            while (current != Last)
            {
                if (current.Data.CompareTo(data) == 0)
                {
                    returnNode = current;
                }

                current = current.Next;
            }

            if (current.Data.CompareTo(data) == 0)
            {
                returnNode = current;
            }

            return returnNode;
        }

        //GetType() - Возвращает объект Type для текущего экземпляра.
        public Type ListGetType()
        {
            return this.GetType();
        }

        //MemberwiseClone() – Создает неполную копию текущего объекта Object. (Унаследовано от Object)
        public CircleList<T> MyMemberwiseClone()
        {
            var list = (object)this;
            return (CircleList<T>)list;
        }

        //Remove(CircleListNode<T>) – Удаляет заданный узел из объекта CircleList<T>
        public void Remove(CircleListNode<T> node)
        {
            var previous = Last;
            var current = First;
            while (current != node)
            {
                if (current.Next == First)
                {
                    return;
                }

                previous = current;
                current = current.Next;
            }
            previous.Next = current.Next;
            count--;
        }

        //Remove(T) – Удаляет первое вхождение заданного значения из CircleList<T>.
        public bool Remove(T data)
        {
            var current = First;
            CircleListNode<T> previous = null;

            if (IsEmpty)
            {
                return false;
            }

            do
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current == Last)
                        {
                            Last = previous;
                        }
                    }
                    else
                    {
                        if (count == 1)
                        {
                            First = Last = null;
                        }
                        else
                        {
                            First = current.Next;
                            Last.Next = current.Next;
                        }
                    }

                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            } while (current != First);

            return false;
        }

        //RemoveFirst()	– Удаляет узел в начале CircleList<T>
        public void RemoveFirst()
        {
            if (First == null)
            {
                return;
            }

            else
            {
                var savedNext = First.Next;
                Last.Next = savedNext;
                First = savedNext;
                count--;
            }
        }

        //RemoveLast()	– Удаляет узел в конце CircleList<T>
        public void RemoveLast()
        {
            
            var current = First;
            CircleListNode<T> previous = null;

            if (First == null)
            {
                return;
            }
            else
            {
                while (current != Last)
                {
                    previous = current;
                    current = current.Next;
                }
                previous.Next = First;
                Last = previous;
                count--;
            }
        }

        















        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            CircleListNode<T> current = First;
            do
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
            while (current != First);
        }
    }
}
