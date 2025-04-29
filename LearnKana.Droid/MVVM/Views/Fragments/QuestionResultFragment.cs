using Android.Graphics;
using Android.Views;
using Android.Widget;

using LearnKana.Domain.Kana;
using LearnKana.Domain.Study;
using LearnKana.Droid.Text;
using LearnKana.Droid.Utilities;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Fragments
{
    public class QuestionResultFragment : QuestionFragment
    {
        public static QuestionResultFragment CreateInstance(int questionNumber)
        {
            Bundle bundle = new Bundle();
            bundle.PutInt(Keys.Question, questionNumber);
            QuestionResultFragment fragment = new QuestionResultFragment
            {
                Arguments = bundle
            };
            return fragment;
        }

        private TextView? m_TextViewAnswer;

        public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate<View>(Resource.Layout.fragment_question_result, container);
            return view;
        }

        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            m_TextViewAnswer = view.RequireViewById<TextView>(Resource.Id.textview_answer);

            Arguments arguments = new Arguments(Arguments);
            int questionNumber = arguments.GetInt(Keys.Question);

            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);

            Question question = ViewModel.Quiz.GetQuestion(questionNumber);
            UpdateQuestionView(question);

            QuestionResult result = ViewModel.Quiz.GetResult(question);

            switch (result.Status)
            {
                case QuestionStatus.Correct:
                case QuestionStatus.Incorrect:
                    ResultAnswered(result);
                    break;
                case QuestionStatus.Skipped:
                    ResultSkipped(result);
                    break;
                default:
                    ResultNotAnswered(result);
                    break;

            }
        }

        private void ResultAnswered(QuestionResult result)
        {
            if (result.Answer.TryGetValue(out KanaSet answer))
                m_TextViewAnswer?.SetText(new SpanBuilder()
                    .Append("You Answered:")
                    .NewLine()
                    .SetForegroundColor(answer.Character.Romaji, result.Status == QuestionStatus.Correct ? Color.Green : Color.Red).Build());
            else
                m_TextViewAnswer?.SetText("?");
        }

        private void ResultSkipped(QuestionResult _)
        {
            m_TextViewAnswer?.SetText("Skipped");
        }
        private void ResultNotAnswered(QuestionResult _)
        {
            m_TextViewAnswer?.SetText("Not Answered");
        }
    }
}