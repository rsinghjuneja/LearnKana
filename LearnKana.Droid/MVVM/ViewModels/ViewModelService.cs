using AndroidX.Lifecycle;


namespace LearnKana.Droid.MVVM.ViewModels
{
    public class ViewModelService
    {
        private readonly IViewModelStoreOwner m_ViewModelStoreOwner;

        public ViewModelService(IViewModelStoreOwner? owner)
        {
            ArgumentNullException.ThrowIfNull(owner, nameof(owner));
            m_ViewModelStoreOwner = owner;
        }

        public T CreateViewModel<T>(ViewModelProvider.IFactory? factory) where T : ViewModel
        {
            ViewModel viewModel = CreateViewModel<T>(m_ViewModelStoreOwner, factory);
            return (T)viewModel;
        }
        public T CreateViewModel<T>(ViewModelProvider.IFactory? factory, string key) where T : ViewModel
        {
            ViewModel viewModel = CreateViewModel<T>(m_ViewModelStoreOwner, factory, key);
            return (T)viewModel;
        }
        public T GetViewModel<T>() where T : ViewModel
        {
            ViewModel viewModel = GetViewModel<T>(m_ViewModelStoreOwner);
            return (T)viewModel;
        }
        public T GetViewModel<T>(string key) where T : ViewModel
        {
            ViewModel viewModel = GetViewModel<T>(m_ViewModelStoreOwner, key);
            return (T)viewModel;
        }

        public static T CreateViewModel<T>(IViewModelStoreOwner? owner, ViewModelProvider.IFactory? factory) where T : ViewModel
        {
            ArgumentNullException.ThrowIfNull(owner, nameof(owner));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            return (T)new ViewModelProvider(owner, factory).Get(Java.Lang.Class.FromType(typeof(T)));
        }
        public static T CreateViewModel<T>(IViewModelStoreOwner? owner, ViewModelProvider.IFactory? factory, string key) where T : ViewModel
        {
            ArgumentNullException.ThrowIfNull(owner, nameof(owner));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            return (T)new ViewModelProvider(owner, factory).Get(key, Java.Lang.Class.FromType(typeof(T)));
        }
        public static T GetViewModel<T>(IViewModelStoreOwner? owner) where T : ViewModel
        {
            ArgumentNullException.ThrowIfNull(owner, nameof(owner));
            return (T)new ViewModelProvider(owner).Get(Java.Lang.Class.FromType(typeof(T)));
        }
        public static T GetViewModel<T>(IViewModelStoreOwner? owner, string key) where T : ViewModel
        {
            ArgumentNullException.ThrowIfNull(owner, nameof(owner));
            return (T)new ViewModelProvider(owner).Get(key, Java.Lang.Class.FromType(typeof(T)));
        }
    }
}