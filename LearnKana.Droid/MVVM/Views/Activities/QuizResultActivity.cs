using System.Linq;

using Android.App;
using Android.Content;
using Android.Graphics;

using AndroidX.Lifecycle;
using AndroidX.ViewPager2.Widget;

using Google.Android.Material.Tabs;

using LearnKana.Domain.Study;
using LearnKana.Droid.MVVM.Pager;
using LearnKana.Droid.MVVM.Tabs;
using LearnKana.Droid.MVVM.ViewModels;
using LearnKana.Droid.MVVM.Views.Fragments;
using LearnKana.Droid.MVVM.Views.Widgets;
using LearnKana.Droid.Text;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    [Activity]
    public class QuizResultActivity : ViewModelActivity<QuizViewModel>
    {
        public static void StartActivity(Context? context)
        {
            Intent intent = CreateIntent<QuizResultActivity>(context);
            context?.StartActivity(intent);
        }

        private CircularProgressView? m_ProgressViewAnswered;
        private CircularProgressView? m_ProgressViewCorrect;
        private TabService? m_TabService;
        private TabLayout? m_TabLayout;
        private ViewPager2? m_ViewPager;

        protected override ViewModelProvider.IFactory GetViewModelFactory() => new QuizViewModel.Factory(App.QuizRepository);
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_quiz_results);
            SetToolbarTitle($"Quiz Results");

            Initialize();
        }

        private void Initialize()
        {
            m_ProgressViewAnswered = RequireViewById<CircularProgressView>(Resource.Id.progress_view_answered);
            m_ProgressViewCorrect = RequireViewById<CircularProgressView>(Resource.Id.progress_view_correct);

            m_TabLayout = RequireViewById<TabLayout>(Resource.Id.tab_layout);
            m_ViewPager = RequireViewById<ViewPager2>(Resource.Id.view_pager);

            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);

            Quiz quiz = ViewModel.Quiz;

            int answered = quiz.Results.Where(x => x.Value.Status != QuestionStatus.NotAnswered).Count();
            int correctCount = quiz.Results.Where(x => x.Value.Status == QuestionStatus.Correct).Count();
            int incorrectCount = quiz.Results.Where(x => x.Value.Status == QuestionStatus.Incorrect).Count();

            m_ProgressViewAnswered.SetTitle("Questions\nAnswered");
            m_ProgressViewCorrect.SetTitle("Correct\nAnswers");

            m_ProgressViewAnswered.SetProgress(0, quiz.QuestionCount, answered);
            m_ProgressViewAnswered.SetProgressLabel(answered, quiz.QuestionCount);

            m_ProgressViewCorrect.SetProgress(0, answered, correctCount.ToPercent(answered));
            m_ProgressViewCorrect.SetProgressLabel(correctCount.ToPercentString(answered));

            CreateResultsView(quiz);
        }

        private void CreateResultsView(Quiz quiz)
        {
            ViewPagerAdapter adapter = new ViewPagerAdapter(this);
            m_TabService = new TabService { HideSingleTab = false };

            for (int i = 0; i < quiz.QuestionCount; i++)
            {
                Question question = quiz.Questions[i];
                QuestionResult result = quiz.GetResult(question);

                if (result.Status == QuestionStatus.NotAnswered)
                    continue;

                string tabTitle = $"#{question.QuestionNumber} ({question.Answer.KanaFromScript(question.KanaScript)})";
                if (result.Status == QuestionStatus.Correct)
                    m_TabService.AddTab(new Tab(new SpanBuilder().SetForegroundColor(tabTitle, Color.Green).Build(), 0, false));
                else
                    m_TabService.AddTab(new Tab(new SpanBuilder().SetForegroundColor(tabTitle, Color.Red).Build(), 0, false));

                adapter.AddFragment(() => QuestionResultFragment.CreateInstance(question.QuestionNumber));
            }

            ArgumentNullException.ThrowIfNull(m_TabLayout);
            ArgumentNullException.ThrowIfNull(m_ViewPager);

            m_ViewPager.Adapter = adapter;
            m_TabService.Mediate(m_TabLayout, m_ViewPager, m_TabService.Count > 5 ? TabMode.Scrollable : TabMode.Fixed);
        }

        protected override void OnDestroy()
        {
            SetResult(Result.Ok, null);
            m_TabService?.Dispose();
            base.OnDestroy();
        }
    }
}