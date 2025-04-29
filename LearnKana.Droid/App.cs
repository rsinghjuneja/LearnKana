using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;

using LearnKana.Droid.Persistence;
using LearnKana.Droid.Repositories;
using LearnKana.Droid.Services;
using LearnKana.Droid.Utilities;
using LearnKana.Provider;

namespace LearnKana.Droid
{
    [Application]
    public class App(nint javaReference, JniHandleOwnership transfer) : Application(javaReference, transfer)
    {
        public static App Instance { get; private set; } = null!;

        public static Prefs Prefs { get; private set; } = null!;

        public static FileManager FileManager { get; private set; } = null!;
        public static ApplicationRepository ApplicationRepository { get; private set; } = null!;
        public static QuizRepository QuizRepository { get; private set; } = null!;

        public static KanaService KanaService { get; private set; } = null!;
        public static KanaAudioPlayer KanaAudioPlayer { get; private set; } = null!;
        public static KanaImageDatabase KanaImageDatabase { get; private set; } = null!;

        public static string AppDataDirectory
        {
            get
            {
                string? path = Context.FilesDir?.AbsolutePath;
                return path.ThrowIfNull();
            }
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Instance = this;
            InitializeAndroidServices();
        }
        private void InitializeAndroidServices()
        {
            Prefs = new Prefs(this);
        }

        public static async Task OnCreateAsync()
        {
            FileManager = await FileManager.CreateAsync(AppDataDirectory, Keys.AppDatabaseFileName);
            ApplicationRepository = new ApplicationRepository(FileManager);
            QuizRepository = new QuizRepository(FileManager);

            KanaService = new KanaService(new DataProvider());
            KanaAudioPlayer = new KanaAudioPlayer(KanaService);
            KanaImageDatabase = new KanaImageDatabase();
        }


        public static void ShowKeyboard(View? view, ShowFlags flags = ShowFlags.Implicit)
        {
            if (view?.Context?.GetSystemService<InputMethodManager>(InputMethodService) is InputMethodManager input)
                input.ShowSoftInput(view, flags);

        }
        public static void HideKeyboard(View? view, HideSoftInputFlags flags = HideSoftInputFlags.None)
        {
            if (view?.Context?.GetSystemService<InputMethodManager>(InputMethodService) is InputMethodManager input)
                input.HideSoftInputFromWindow(view?.WindowToken, flags);
        }

        public static LayoutInflater GetLayoutInflater(Context? context) =>
            context?.GetSystemService(LayoutInflaterService) as LayoutInflater
            ?? throw new ArgumentNullException(nameof(context));
    }
}