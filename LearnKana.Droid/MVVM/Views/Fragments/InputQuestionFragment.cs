using System.Collections.Generic;
using System.Linq;

using Android.Content.Res;
using Android.Graphics;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;

using LearnKana.Domain.Kana;
using LearnKana.Domain.Study;
using LearnKana.Droid.MVVM.Listeners;
using LearnKana.Droid.MVVM.Views.Widgets;

namespace LearnKana.Droid.MVVM.Views.Fragments
{
    public class InputQuestionFragment : QuestionFragment
    {
        public static InputQuestionFragment CreateInstance()
        {
            InputQuestionFragment fragment = new InputQuestionFragment
            {

            };
            return fragment;
        }

        private MaterialTextInputView? m_TextInputView;
        private WordValidatorTextWatcher? m_TextWatcher;

        public override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            HashSet<string> set = KanaService.KanaSyllabary.Values
                .Select(x => x.Romaji)
                .ToHashSet();

            m_TextWatcher = new WordValidatorTextWatcher(set, EditText_TextWatcher);
        }

        public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate<View>(Resource.Layout.fragment_question_input, container);
            return view;
        }
        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            m_TextViewQuestion?.SetText(Resource.String.quiz_question_input_explanation);

            m_TextInputView = view.RequireViewById<MaterialTextInputView>(Resource.Id.text_input_view);

            m_TextInputView.TextInputLayout.SetHint(Resource.String.hint_input_answer);
            m_TextInputView.TextInputLayout.CounterEnabled = true;
            m_TextInputView.TextInputLayout.CounterMaxLength = 3;

            m_TextInputView.TextInputEditText.InputType = InputTypes.ClassText;
            m_TextInputView.TextInputEditText.ImeOptions = ImeAction.Done;

            m_TextInputView.SetButtonText(Android.Resource.String.Ok);
            m_TextInputView.SetButtonEnabled(false);

            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);
            Question question = ViewModel.Quiz.GetCurrentQuestion();
            UpdateQuestionView(question);

            ShowKeyboard();
        }
        public override void OnStart()
        {
            base.OnStart();
            m_TextInputView?.SetButtonOnClickListener(this);
            m_TextInputView?.TextInputEditText.AddTextChangedListener(m_TextWatcher);
        }

        public override void OnStop()
        {
            base.OnStop();
            m_TextInputView?.SetButtonOnClickListener(null);
            m_TextInputView?.TextInputEditText.RemoveTextChangedListener(m_TextWatcher);
        }
        public override void OnHiddenChanged(bool hidden)
        {
            base.OnHiddenChanged(hidden);
            if (hidden)
                App.HideKeyboard(View);
            else
                App.ShowKeyboard(m_TextInputView?.TextInputEditText);
        }

        public override void UpdateQuestionView(Question question)
        {
            base.UpdateQuestionView(question);

            if (m_TextInputView == null)
                return;

            m_TextInputView.StrokeWidth = 0;
            m_TextInputView.SetStrokeColor(ColorStateList.ValueOf(Color.Transparent));

            m_TextInputView.TextInputEditText?.ClearText();
            m_TextInputView.TextInputEditText?.RequestFocus();
        }

        private void ShowKeyboard() =>
            Activity?.RunOnUiThread(() => m_TextInputView?.TextInputEditText?.Post(() =>
                m_TextInputView.TextInputEditText?.ShowKeyboard()));

        public override void OnClick(View? view)
        {
            if (view?.Id == Resource.Id.button_ok)
                ButtonOk_OnClick();
            else
                base.OnClick(view);
        }

        private void EditText_TextWatcher(bool valid)
        {
            m_TextInputView?.SetButtonEnabled(valid);
        }

        private void ButtonOk_OnClick()
        {
            ArgumentNullException.ThrowIfNull(m_TextInputView);
            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);
            Question question = ViewModel.Quiz.GetCurrentQuestion();
            string? text = m_TextInputView.TextInputEditText.Text ?? string.Empty;
            bool isCorrect = text == question.Answer.Romaji;
            if (isCorrect)
            {
                m_TextInputView.StrokeWidth = 1;
                m_TextInputView.SetStrokeColor(ColorStateList.ValueOf(Color.Green));
            }
            else
            {
                m_TextInputView.StrokeWidth = 1;
                m_TextInputView.SetStrokeColor(ColorStateList.ValueOf(Color.Red));
            }

            QuestionStatus status = QuestionResult.DetermineQuestionStatus(isCorrect);

            KanaSet? answer = null;
            if (KanaService.KanaSyllabary.TryGetValue(text, out KanaCharacter character))
                answer = new KanaSet(character, question.KanaScript);

            ViewModel.Quiz.SetResult(question.QuestionNumber, new QuestionResult(answer, status));
            SetQuestionResult(question, status);
        }
    }
}