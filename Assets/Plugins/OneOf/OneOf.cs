using System;

namespace OneOf
{
	public readonly struct OneOf<T1, T2, T3>
	{
#nullable enable
		private readonly T1? _value1;
		private readonly T2? _value2;
		private readonly T3? _value3;

		private OneOf(T1 value)
		{
			_value1 = value;
			_value2 = default;
			_value3 = default;
		}

		public OneOf(T2 value)
		{
			_value1 = default;
			_value2 = value;
			_value3 = default;
		}

		public OneOf(T3 value)
		{
			_value1 = default;
			_value2 = default;
			_value3 = value;
		}

		public static implicit operator OneOf<T1, T2, T3>(T1 value) => new(value);

		public static implicit operator OneOf<T1, T2, T3>(T2 value) => new(value);

		public static implicit operator OneOf<T1, T2, T3>(T3 value) => new(value);

		public TResult Match<TResult>(Func<T1, TResult> value1, Func<T2, TResult> value2, Func<T3, TResult> value3)
		{
			if (_value1 != null)
				return value1.Invoke(_value1);
			if (_value2 != null)
				return value2.Invoke(_value2);

			return value3.Invoke(_value3);
		}

		public void Match(Action<T1> value1, Action<T2> value2, Action<T3> value3)
		{
			if (_value1 != null)
				value1.Invoke(_value1);
			else if (_value2 != null)
				value2.Invoke(_value2);
			else
				value3.Invoke(_value3);
		}
	}

	public readonly struct OneOf<T1, T2>
	{
		private readonly T1? _value1;
		private readonly T2? _value2;

		private OneOf(T1 value)
		{
			_value1 = value;
			_value2 = default;
		}

		public OneOf(T2 value)
		{
			_value1 = default;
			_value2 = value;
		}

		public static implicit operator OneOf<T1, T2>(T1 value) => new(value);

		public static implicit operator OneOf<T1, T2>(T2 value) => new(value);

		public TResult Match<TResult>(Func<T1, TResult> value1, Func<T2, TResult> value2) =>
			_value1 != null ? value1.Invoke(_value1) : value2.Invoke(_value2);

		public void Match(Action<T1> value1, Action<T2> value2)
		{
			if (_value1 != null)
				value1.Invoke(_value1);
			else if (_value2 != null)
				value2.Invoke(_value2);
		}
	}
}