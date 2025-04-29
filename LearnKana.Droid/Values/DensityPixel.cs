using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Android.App;
using Android.Content;
using Android.Util;

namespace LearnKana.Droid.Values
{
    [DebuggerStepThrough]
    public readonly struct DensityPixel
    {
        /// <summary>
        /// 24dp
        /// </summary>
        public static DensityPixel IconSize => new DensityPixel(24);
        /// <summary>
        /// 30dp
        /// </summary>
        public static DensityPixel IconSizeSmall => new DensityPixel(30);
        /// <summary>
        /// 40dp
        /// </summary>
        public static DensityPixel IconSizeMediumSmall => new DensityPixel(40);
        /// <summary>
        /// 50p
        /// </summary>
        public static DensityPixel IconSizeMediumBig => new DensityPixel(50);
        /// <summary>
        /// 60dp
        /// </summary>
        public static DensityPixel IconSizeBig => new DensityPixel(60);
        /// <summary>
        /// 80dp
        /// </summary>
        public static DensityPixel IconSizeExtra => new DensityPixel(80);

        [SetsRequiredMembers]
        public DensityPixel(int pixels) => Value = ConvertToDP(Application.Context, pixels);
        [SetsRequiredMembers]
        public DensityPixel(float pixels) => Value = ConvertToDP(Application.Context, pixels);
        public DensityPixel(Context context, int pixels) => Value = ConvertToDP(context, pixels);
        public DensityPixel(Context context, float pixels) => Value = ConvertToDP(context, pixels);

        public required float Value { get; init; }

        public static DensityPixel From(int dp)
        {
            return new DensityPixel { Value = dp };
        }
        public static DensityPixel From(float dp)
        {
            return new DensityPixel { Value = dp };
        }
        public static float ConvertToDP(Context context, float pixels)
        {
            if (pixels == 0 || pixels == -1 || pixels == -2) { return pixels; }
            float converted = IComplexUnit.Convert(context, pixels, ComplexUnitType.Dip);
            return converted;
        }

        public static implicit operator DensityPixel(int pixels) => new DensityPixel(pixels);
        public static implicit operator DensityPixel(float pixels) => new DensityPixel(pixels);

        public static implicit operator int(DensityPixel dp) => (int)dp.Value;
        public static implicit operator float(DensityPixel dp) => dp.Value;
    }
}