using System.Diagnostics;

namespace LearnKana.Droid.Values
{
    [DebuggerStepThrough]
    public struct Margin
    {
        public const int SIZE_ZERO = 0;
        public const int SIZE_SMALL = 5;
        public const int SIZE_MEDIUM = 10;
        public const int SIZE_LARGE = 20;

        public static Margin Zero => new Margin(SIZE_ZERO);
        public static Margin Small => new Margin(SIZE_SMALL);
        public static Margin Medium => new Margin(SIZE_MEDIUM);
        public static Margin Large => new Margin(SIZE_LARGE);
        public static Margin HorizontalSmall => new Margin(SIZE_SMALL, SIZE_ZERO, SIZE_SMALL, SIZE_ZERO);
        public static Margin HorizontalMedium => new Margin(SIZE_MEDIUM, SIZE_ZERO, SIZE_MEDIUM, SIZE_ZERO);
        public static Margin VerticalSmall => new Margin(SIZE_ZERO, SIZE_SMALL, SIZE_ZERO, SIZE_SMALL);
        public static Margin VerticalMedium => new Margin(SIZE_ZERO, SIZE_MEDIUM, SIZE_ZERO, SIZE_MEDIUM);

        public Margin(DensityPixel left, DensityPixel top, DensityPixel right, DensityPixel bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Margin(DensityPixel all)
        {
            Left = all;
            Top = all;
            Right = all;
            Bottom = all;
        }

        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }
    }
}