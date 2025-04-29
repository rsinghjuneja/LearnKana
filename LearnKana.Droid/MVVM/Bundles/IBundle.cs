namespace LearnKana.Droid.MVVM.Bundles
{
    public interface IBundle
    {
        public const int DefaultIntValue = int.MinValue;
        public const float DefaultFloatValue = float.MinValue;

        public Bundle ToBundle() => ToBundle(new Bundle());
        public Bundle ToBundle(Bundle bundle);
    }
    public interface IBundle<T> : IBundle where T : notnull
    {
        public static abstract T FromBundle(Bundle? bundle);
    }
}