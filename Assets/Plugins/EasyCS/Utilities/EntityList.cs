using System;

namespace EasyCS.Utilities
{
	public class EntityList<T> where T : struct
	{
		private T[] _items;
		public int Count { get; private set; }

		public T this[int index]
		{
			get
			{
				CheckArrayBoundaries(index);
				return _items[index];
			}
			set
			{
				CheckArrayBoundaries(index);
				_items[index] = value;
			}
		}

		public EntityList(int initialCapacity = 10)
		{
			_items = new T[initialCapacity];
			Count = initialCapacity;
		}

		public void PopulateWith(T item)
		{
			for (var i = 0; i < _items.Length; i++)
				_items[i] = item;
		}

		public void Resize()
		{
			Array.Resize(ref _items, _items.Length * 2);
			Count = _items.Length;
		}

		private void CheckArrayBoundaries(int index)
		{
			if (index < 0 || index >= Count)
				throw new IndexOutOfRangeException();
		}
	}
}