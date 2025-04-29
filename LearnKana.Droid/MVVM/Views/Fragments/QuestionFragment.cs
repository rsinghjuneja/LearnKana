using Android.Views;
using Android.Widget;

using LearnKana.Domain.Study;
using LearnKana.Droid.MVVM.ViewModels;
using LearnKana.Droid.MVVM.Views.Widgets;
using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Views.Fragments
{
    public abstract class QuestionFragment : BaseFragment
    {
        protected TextView? m_TextViewQuestionType;
        protected KanaCharacterView? m_KanaCharacterView;
        protected TextView? m_TextViewQuestion;

        private QuizViewModel? m_ViewModel;
        protected QuizViewModel ViewModel => m_ViewModel ??= ViewModelService.GetViewModel<QuizViewModel>(Activity);

        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            m_TextViewQuestionType = view.RequireViewById<TextView>(Resource.Id.textview_question_type);
            m_KanaCharacterView = view.RequireViewById<KanaCharacterView>(Resource.Id.kana_character_view);
            m_TextViewQuestion = view.RequireViewById<TextView>(Resource.Id.textview_question);

            m_KanaCharacterView.SetKanaTextSize(50);
            m_KanaCharacterView.SetRomajiVisible(false);
            m_KanaCharacterView.SetAlternateKanaVisible(false);
        }

        public virtual void UpdateQuestionView(Question question)
        {
            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);
            m_TextViewQuestionType?.SetText(question.KanaScript.ToString());
            m_KanaCharacterView?.SetKanaCharacter(question.Answer, question.KanaScript);
        }

        protected void SetQuestionResult(Question question, QuestionStatus status)
        {
            QuestionResultBundle bundle = new QuestionResultBundle(question.QuestionNumber, status);
            ResultData.PutBundle(Keys.Answer, bundle);
            SetResult(Keys.Quiz, ResultData);
        }
    }
}
