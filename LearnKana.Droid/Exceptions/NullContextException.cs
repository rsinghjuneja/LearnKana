using System;

namespace LearnKana.Droid.Exceptions
{
    public class NullContextException : Exception
    {
        public NullContextException() : this("The context being used was null.")
        {

        }

        public NullContextException(string? message) : base(message)
        {

        }

        public NullContextException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}