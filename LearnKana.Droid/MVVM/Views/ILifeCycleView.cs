namespace LearnKana.Droid.MVVM.Views
{
    public interface ILifeCycleView
    {
        public void OnStart();
        public void OnStop();
        public void OnDestroy();
    }
}
