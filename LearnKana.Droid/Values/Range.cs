using System.Numerics;

namespace LearnKana.Droid.Values
{
    public interface IRange
    {

    }
    public interface IRange<T> : IRange where T : INumber<T>
    {
        public T Start { get; }
        public T End { get; }
        public bool Contains(T value);
        public bool UnderFlowed(T value);
        public bool OverFlowed(T value);
        public T Clamp(T value);
    }

    public readonly struct Range<T> : IRange<T> where T : INumber<T>
    {
        /// <summary>
        /// The range is Inclusive-Inclusive
        /// </summary>
        /// <param name="start">Inclusive</param>
        /// <param name="end">Inclusive</param>
        public Range(T start, T end)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(start, end);
            Start = start;
            End = end;
        }

        public T Start { get; }
        public T End { get; }

        public bool Contains(T value) => value > Start && value < End;
        public bool UnderFlowed(T value) => value < Start;
        public bool OverFlowed(T value) => value > End;
        public T Clamp(T value) => T.Clamp(value, Start, End);
    }
}
