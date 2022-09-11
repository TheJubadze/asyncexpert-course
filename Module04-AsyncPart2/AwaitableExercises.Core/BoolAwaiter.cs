using System;
using System.Runtime.CompilerServices;

namespace AwaitableExercises.Core
{
    public static class BoolExtensions
    {
        public static BoolAwaiter GetAwaiter(this bool arg)
        {
            return new BoolAwaiter(arg);
        }
    }

    public class BoolAwaiter : INotifyCompletion
    {
        private readonly bool _value;

        public BoolAwaiter(bool value)
        {
            _value = value;
        }

        public void OnCompleted(Action continuation)
        {
        }

        public bool GetResult() => _value;

        public bool IsCompleted => true;
    }
}