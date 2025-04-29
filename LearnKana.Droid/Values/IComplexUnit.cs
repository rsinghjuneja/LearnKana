using Android.App;
using Android.Content;
using Android.Util;

namespace LearnKana.Droid.Values
{
    public interface IComplexUnit
    {
        public static float Convert(float value, ComplexUnitType unit) => Convert(Application.Context, value, unit);
        public static float Convert(Context? context, float value, ComplexUnitType unit)
            => TypedValue.ApplyDimension(unit, value, context?.Resources?.DisplayMetrics);
    }
}
