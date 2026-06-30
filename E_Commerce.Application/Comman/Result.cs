using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Comman
{
    public class Result
    {
        public bool IsSuccess { get; }
        public IReadOnlyList<Error> Errors { get; } 

        protected Result(bool isSuccess,IReadOnlyList<Error> errors) 
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }
        public static Result Ok() => new(true, Array.Empty<Error>());
        public static Result Fail(Error errors) => new(false, new[] {errors});
        public static Result Fail(IReadOnlyList<Error> errors) => new(false, errors);
    }
    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public TValue Data => IsSuccess ? _value : throw new InvalidOperationException("Can not access value of failed result");
        private Result(TValue value) : base(true,Array.Empty<Error>())
        {
            _value = value;
        }
        private Result(Error error) : base(true, new[] {error })
        {
            _value = default!;
        }
        private Result(IReadOnlyList<Error> error) : base(true, error)
        {
            _value = default!;
        }
        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static Result<TValue> Fail(Error errors) => new Result<TValue>(errors);
        public static Result<TValue> Fail(IReadOnlyList<Error> errors) => new Result<TValue>(errors);

        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);

    }
}
