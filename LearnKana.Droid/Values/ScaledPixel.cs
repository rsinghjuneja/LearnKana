using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.Util;

namespace LearnKana.Droid.Values
{
    [DebuggerStepThrough]
    public readonly struct ScaledPixel : IComplexUnit
    {
        public float Value { get; }

        public ScaledPixel(int pixels)
        {
            Value = ConvertToSP(Application.Context, pixels);
        }

        public ScaledPixel(Context context, int pixels)
        {
            Value = ConvertToSP(context, pixels);
        }

        public static float ConvertToSP(Context context, float value)
        {
            if (value == 0) { return (int)value; }
            float converted = IComplexUnit.Convert(context, value, ComplexUnitType.Sp);
            return converted;
        }

        public static implicit operator ScaledPixel(int pixels) => new ScaledPixel(pixels);

        public static implicit operator int(ScaledPixel sp) => (int)sp.Value;
    }
}