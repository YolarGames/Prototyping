using System;

namespace EasyCS
{
	public readonly struct Result<TValue, TError>
	{
#nullable enable
		private readonly TValue? _value;
		private readonly TError? _error;
#nullable disable

		private Result(TValue value)
		{
			IsError = false;
			_value = value;
			_error = default;
		}

		public Result(TError error)
		{
			IsError = true;
			_value = default;
			_error = error;
		}

		public bool IsError { get; }
		public bool IsSuccess => !IsError;

		public static implicit operator Result<TValue, TError>(TValue value) => new(value);

		public static implicit operator Result<TValue, TError>(TError error) => new(error);

		public TResult Match<TResult>(Func<TValue, TResult> success, Func<TError, TResult> error) =>
			IsSuccess ? success(_value) : error(_error);

		public void Match(Action<TValue> success, Action<TError> error)
		{
			if (IsSuccess)
				success.Invoke(_value);
			else
				error.Invoke(_error);
		}
	}
}