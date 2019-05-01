using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DotLiquid;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public class LiquidList<T> : LiquidAdapter, IList<T>
    {
        private readonly List<T> _items;

        public LiquidList()
        {
            _items = new List<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array,arrayIndex);
        }

        public bool Remove(T item)
        {
          return  _items.Remove(item);
        }

        public int Count => _items.Count;
        public bool IsReadOnly => false;
        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _items.Insert(index,item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }
    }
}