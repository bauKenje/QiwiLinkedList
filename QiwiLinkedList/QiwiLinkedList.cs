using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QiwiLinkedList
{
    public class QiwiLinkedList<T> : IEnumerable<T>, IQueryable<T>
    {
        #region Свойства
        public T Value { get; set; }
        public QiwiLinkedList<T> Next { get; set; }
        #endregion

        #region Enumerator

        public IEnumerator GetEnumerator()
        {
            return new QiwiLinkedListEnumerator<T>(this);
        }

        public Type ElementType
        {
            get
            {
                IQueryable<T> l = ((IEnumerable<T>)this).Select(x => x).AsQueryable();
                return l.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                IQueryable<T> l = ((IEnumerable<T>)this).Select(x => x).AsQueryable();
                return l.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                IQueryable<T> l = ((IEnumerable<T>)this).Select(x => x).AsQueryable();
                return l.Provider;
            }
        }
        #endregion

        #region Конструктор
        public QiwiLinkedList()
        {

        }

        public QiwiLinkedList(T Value)
        {
            this.Value = Value;
        }

        public QiwiLinkedList(T Value, QiwiLinkedList<T> Next)
        {
            this.Value = Value;
            this.Next = Next;
        }
        #endregion

        public void Add(T Value)
        {
            QiwiLinkedList<T> current = this;

            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = new QiwiLinkedList<T>(Value);
        }

        public void Add(T Value, uint index)
        {
            QiwiLinkedList<T> current = this;

            uint counter = 0;
            while (counter < index)
            {
                if (current == null)
                    throw new IndexOutOfRangeException();
                current = current.Next;
                counter++;
            }
            current.Next = new QiwiLinkedList<T>(Value, current.Next);
        }

        public string Get()
        {
            QiwiLinkedList<T> current = this.Next;

            StringBuilder result = new StringBuilder();
            int counter = 0;
            while (current != null)
            {
                result.Append(counter++);
                result.Append(" : ");
                result.Append(current.Value.ToString());
                result.Append("\n");
                current = current.Next;
            }
            return counter == 0 ? "Список пуст" : result.ToString();
        }

        public T Get(uint index)
        {
            QiwiLinkedList<T> current = this.Next;

            int counter = 0;
            while (counter < index)
            {
                if (current == null)
                    throw new IndexOutOfRangeException();
                current = current.Next;
                counter++;
            }
            return current.Value;
        }

        public void Delete(uint index)
        {
            QiwiLinkedList<T> current = this;

            int counter = 0;
            while (counter < index)
            {
                if (current == null)
                    throw new IndexOutOfRangeException();
                current = current.Next;
                counter++;
            }
            current.Next = current.Next?.Next;
        }

        public int Count()
        {
            QiwiLinkedList<T> current = this.Next;

            int counter = 0;
            while (current != null)
            {
                current = current.Next;
                counter++;
            }
            return counter;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new QiwiLinkedListEnumerator<T>(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new QiwiLinkedListEnumerator<T>(this);
        }
    }

    public class QiwiLinkedListEnumerator<T> : IEnumerator<T>
    {
        public QiwiLinkedList<T> list { get; set; }
        public QiwiLinkedList<T> position { get; set; }

        public QiwiLinkedListEnumerator(QiwiLinkedList<T> list)
        {
            this.list = list;
            this.position = list;
        }

        public object Current
        {
            get
            {
                return position.Value;
            }
        }

        T IEnumerator<T>.Current => position.Value;

        public bool MoveNext()
        {
            if (position.Next != null)
            {
                position = position.Next;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            position = list;
        }
        public void Dispose()
        {
        }
    }
}
