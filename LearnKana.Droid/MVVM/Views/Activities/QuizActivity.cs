using System.Linq;
using System.Threading;

using Android.App;
using Android.Content;
using Android.Views;

using AndroidX.Activity.Result;
using AndroidX.Lifecycle;

using LearnKana.Domain.Kana;
using LearnKana.Domain.Study;
using LearnKana.Droid.MVVM.ViewModels;
using LearnKana.Droid.MVVM.Views.Dialogs;
using LearnKana.Droid.MVVM.Views.Fragments;
using LearnKana.Droid.MVVM.Views.Widgets;
using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustResize)]
    public class QuizActivity : ViewModelActivity<QuizViewModel>
    {
        public static void StartActivity(Context? context, KanaScript script)
        {
            ArgumentNullException.ThrowIfNull(context);
            Intent intent = new Intent(context, typeof(QuizActivity));
            intent.PutExtra(Keys.KanaScript, (int)script);
            context.StartActivity(intent);
        }

        private ProgressView? m_ProgressView;
        private TimerProgressBar? m_TimerProgressBar;
        private FragmentService<Type>? m_FragmentService;

        private ActivityResultLauncher? m_ActivityResultLauncher;
        private CancellationTokenSource? m_CancellationTokenSource;

        protected override ViewModelProvider.IFactory GetViewModelFactory() => new QuizViewModel.Factory(App.QuizRepository);
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            m_ActivityResultLauncher = RegisterForActivityResult();

            SetContentView(Resource.Layout.activity_quiz);
            SetToolbarTitle(Resource.String.activity_quiz);

            SupportFragmentManager.SetFragmentResultListener(BaseFragment.FragmentViewCreated, this, this);
            SupportFragmentManager.SetFragmentResultListener(Keys.Quiz, this, this);
            m_FragmentService = new FragmentService<Type>(this, RequireViewById<View>(Resource.Id.fragment_container_view))
                .AddFragmentFactory(typeof(MultiChoiceQuestionFragment), MultiChoiceQuestionFragment.CreateInstance)
                .AddFragmentFactory(typeof(InputQuestionFragment), InputQuestionFragment.CreateInstance);

            m_ProgressView = RequireViewById<ProgressView>(Resource.Id.progress_view);
            m_TimerProgressBar = RequireViewById<TimerProgressBar>(Resource.Id.timer_progress_bar);

            InitializeQuiz();
        }

        public override bool OnCreateOptionsMenu(IMenu? menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_quiz, menu);
            return menu != null;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_skip:
                    Action_Skip();
                    return true;
                case Resource.Id.action_end:
                    Action_End();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        private void Action_Skip()
        {
            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);
            Question question = ViewModel.Quiz.GetCurrentQuestion();
            ViewModel.Quiz.SetResult(question.QuestionNumber, new QuestionResult(null, QuestionStatus.Skipped));

            OnQuestionAnswered();
        }
        private void Action_End()
        {
            if (ViewModel.Quiz?.CurrentQuestion == 1)
            {
                ClearQuizAndFinish();
            }
            else
            {
                ConfirmationDialog.Parameters parameters = new(0, "End Quiz", "Are you sure you want to end the quiz?", "OK", "Cancel");
                ConfirmationDialog.ShowDialog(this, parameters, (result) =>
                {
                    switch (result)
                    {
                        case DialogResult.Yes:
                            StartQuizResultsActivity();
                            break;
                        case DialogResult.No:
                        case DialogResult.Cancel:
                        default:
                            break;
                    }
                });
            }
        }

        private void InitializeQuiz()
        {
            m_CancellationTokenSource = new CancellationTokenSource();

            ArgumentNullException.ThrowIfNull(Intent);
            KanaScript script = (KanaScript)Intent.GetIntExtra(Keys.KanaScript, (int)KanaScript.Hiragana);

            if (ViewModel.Quiz == null)
                ViewModel.Quiz = new QuizFactory(new QuizSettings()).GenerateQuiz(10, script, App.KanaService.GetKanaCharacters().ToList());

            m_ProgressView?.SetProgress(0, ViewModel.Quiz.QuestionCount, 0);
            m_ProgressView?.SetProgressLabel(0, ViewModel.Quiz.QuestionCount);

            m_TimerProgressBar?.SetDuration(TimeSpan.FromSeconds(1));
            Question question = ViewModel.Quiz.GetCurrentQuestion();
            UpdateView(question);
        }

        private void UpdateView(Question question)
        {
            ArgumentNullException.ThrowIfNull(m_FragmentService);
            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);
            ArgumentNullException.ThrowIfNull(m_ProgressView);

            m_ProgressView.SetProgress(question.QuestionNumber);
            m_ProgressView.SetProgressLabel(question.QuestionNumber, ViewModel.Quiz.QuestionCount);

            switch (question.QuestionType)
            {
                case QuestionType.MultipleChoice:
                    m_FragmentService.ShowFragment(typeof(MultiChoiceQuestionFragment));
                    break;
                case QuestionType.Input:
                    m_FragmentService.ShowFragment(typeof(InputQuestionFragment));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        protected void OnQuestionAnswered()
        {
            ArgumentNullException.ThrowIfNull(m_FragmentService);
            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);

            m_FragmentService.FragmentContainer.SetEnabled(false);
            m_TimerProgressBar?.Start(() =>
            {
                if (ViewModel.Quiz.QuestionsRemaining > 0)
                {
                    ViewModel.Quiz.NextQuestion();
                    Question question = ViewModel.Quiz.GetCurrentQuestion();
                    UpdateView(question);
                    QuestionFragment? fragment = m_FragmentService.GetCurrentFragment<QuestionFragment>();
                    if (fragment == null)
                        return;
                    fragment.UpdateQuestionView(question);
                    m_FragmentService.FragmentContainer.SetEnabled(true);
                }
                else
                    StartQuizResultsActivity();
            });
        }

        private void StartQuizResultsActivity()
            => m_ActivityResultLauncher?.Launch(CreateIntent<QuizResultActivity>());

        protected override void OnActivityResult(Intent? intent, Result result)
            => ClearQuizAndFinish();

        private void ClearQuizAndFinish()
        {
            ViewModel.Quiz = null;
            Finish();
        }

        public override void OnFragmentResult(string key, Bundle data)
        {
            if (key != Keys.Quiz)
                throw new NotImplementedException(key);

            data.Clear();
            OnQuestionAnswered();
        }

        protected override void OnBackArrowPressed()
        {
            if (BackPressedService.OnBackPressed(Keys.Quiz, TimeSpan.FromSeconds(1)))
                base.OnBackArrowPressed();
            else
                this.ShowToast(Resource.String.quiz_quit_warning);
        }
        public override void OnBackPressed()
        {
            if (BackPressedService.OnBackPressed(Keys.Quiz, TimeSpan.FromSeconds(1)))
                base.OnBackPressed();
            else
                this.ShowToast(Resource.String.quiz_quit_warning);
        }
        protected override void OnDestroy()
        {
            m_CancellationTokenSource?.Cancel();
            SupportFragmentManager.ClearFragmentResultListener(Keys.Quiz);

            base.OnDestroy();
        }
    }
}