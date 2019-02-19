using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Example
{
    public class MyList<T> : IEnumerable<T>
    {
        public int Size { get; set; }
        private T[] _arr;
        private int _i;

        public MyList(int size)
        {
            Size = size;
            _arr = new T[size];
            _i = -1;
        }

        public void Add(T obj)
        {
            _i++;
            if (_arr[_i] == null)
                _arr[_i] = obj;
        }

        public T this[int index]
        {
            get { if (_i >= Size || _i < 0) throw new IndexOutOfRangeException(); return _arr[index]; }
            set { if (_i >= Size || _i < 0) throw new IndexOutOfRangeException(); _arr[index] = value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T el in this._arr)
            {
                yield return el;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


    }
}
