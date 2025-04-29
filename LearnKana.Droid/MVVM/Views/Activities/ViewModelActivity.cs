using AndroidX.Lifecycle;

using LearnKana.Droid.MVVM.ViewModels;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    public abstract class ViewModelActivity<T> : ViewModelActivity where T : ViewModel
    {
        private T? m_ViewModel;
        public T ViewModel => m_ViewModel ??= CreateViewModel();

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            Debug.WriteLine($"{GetType().Name}: OnCreate");
            base.OnCreate(savedInstanceState);
            m_ViewModel = CreateViewModel();
        }
        protected T CreateViewModel() => ViewModelService.CreateViewModel<T>(GetViewModelFactory());
    }

    public abstract class ViewModelActivity : BaseActivity
    {
        public static ViewModelProvider.NewInstanceFactory NewInstanceViewModelFactory => new ViewModelProvider.NewInstanceFactory();
        public static SavedStateViewModelFactory SavedStateViewModelFactory => new SavedStateViewModelFactory();


        private ViewModelService? m_ViewModelService;
        protected ViewModelService ViewModelService => m_ViewModelService ??= new ViewModelService(this);

        protected abstract ViewModelProvider.IFactory GetViewModelFactory();
        protected T CreateViewModel<T>() where T : ViewModel => ViewModelService.CreateViewModel<T>(GetViewModelFactory());
        protected T GetViewModel<T>() where T : ViewModel => ViewModelService.GetViewModel<T>();
    }
}
