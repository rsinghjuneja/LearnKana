using System.Collections.Generic;

using Android.App;

using AndroidX.Lifecycle;

using LearnKana.Domain.Study;
using LearnKana.Droid.MVVM.Listeners;
using LearnKana.Droid.MVVM.ViewModels;
using LearnKana.Droid.MVVM.Views.Containers;
using LearnKana.Droid.MVVM.Views.Widgets;
using LearnKana.Droid.Values;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    [Activity]
    public class QuizSettingsActivity : ViewModelActivity<QuizSettingViewModel>, MaterialSwitchSettingView.IOnSwitchCheckedChangeListener
    {
        private List<MaterialSwitchSettingView>? m_SwitchSettingViews;
        private MaterialEditTextSettingView? m_MaterialEditTextSettingView;

        private NumberRangeTextWatcher? m_TextWatcher;

        protected override ViewModelProvider.IFactory GetViewModelFactory()
            => new QuizSettingViewModel.Factory(App.QuizRepository);
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_quiz_settings);
            SetToolbarTitle(Resource.String.activity_quiz_settings);
            InitializeViews();
        }

        private void InitializeViews()
        {
            m_TextWatcher = new NumberRangeTextWatcher(new Range<int>(Quiz.MinQuestions, Quiz.MaxQuestions), EditText_TextWatcher);
            m_SwitchSettingViews = new()
            {
                RequireViewById<MaterialSwitchSettingView>(Resource.Id.setting_view_question_romaji)
                    .SetTitle("Ask Questions in Romaji")
                    .SetSubtitle("Questions will be asked in Romaji and require an answer in Kana.", "Questions will not be asked in Romaji.")
                    .SetSwitchChecked(ViewModel.Settings.AskQuestionsInRomaji),
                RequireViewById<MaterialSwitchSettingView>(Resource.Id.setting_view_question_hiragana)
                    .SetTitle("Ask Questions in Hiragana")
                    .SetSubtitle("Questions will be asked in Hiragana and require an answer in Romaji.", "Questions will not be asked in Hiragana")
                    .SetSwitchChecked(ViewModel.Settings.AskQuestionsInHiragana),
                RequireViewById<MaterialSwitchSettingView>(Resource.Id.setting_view_question_katakana)
                    .SetTitle("Ask Questions in Katakana")
                    .SetSubtitle("Questions will be asked in Katakana and require an answer in Romaji", "Questions will not be asked in Katakana")
                    .SetSwitchChecked(ViewModel.Settings.AskQuestionsInKatakana),

                RequireViewById<MaterialSwitchSettingView>(Resource.Id.setting_view_multichoice)
                    .SetTitle("Allow Questions with Multiple-Choice")
                    .SetSubtitle("There will be Multiple-Choice questions", "There will not be any Multiple-Choice questions.")
                    .SetSwitchChecked(ViewModel.Settings.AnswerUsingMultiChoice),
                RequireViewById<MaterialSwitchSettingView>(Resource.Id.setting_view_answer_romaji)
                    .SetTitle("Answer Questions in Romaji")
                    .SetSubtitle("Multiple-Choice options will contain Romaji.", "There will be no Multiple-Choice options with Romaji.")
                    .SetSwitchChecked(ViewModel.Settings.MultiChoiceIncludesRomaji),
                RequireViewById<MaterialSwitchSettingView>(Resource.Id.setting_view_answer_hiragana)
                    .SetTitle("Answer Questions in Hiragana")
                    .SetSubtitle("Multiple-Choice options will contain Hiragana.", "There will be no Multiple-Choice options with Hiragana.")
                    .SetSwitchChecked(ViewModel.Settings.MultiChoiceIncludesHiragana),
                RequireViewById<MaterialSwitchSettingView>(Resource.Id.setting_view_answer_katakana)
                    .SetTitle("Answer Questions in Katakana")
                    .SetSubtitle("Multiple-Choice options will contain Katakana.", "There will be no Multiple Choice options with Katakana.")
                    .SetSwitchChecked(ViewModel.Settings.MultiChoiceIncludesKatakana),

                RequireViewById<MaterialSwitchSettingView>(Resource.Id.setting_view_input)
                    .SetTitle("Allow Questions with Text Input")
                    .SetSubtitle("Questions will require keyboard input.", "There will not be any questions that require keyboard input.")
                    .SetSwitchChecked(ViewModel.Settings.AnswerUsingTextInput),
            };

            m_MaterialEditTextSettingView = RequireViewById<MaterialEditTextSettingView>(Resource.Id.setting_view_count)
                .SetTitle("Question Count")
                .SetSubtitle("The amount of questions you would like to answer.").SetText(50.ToString());

            m_MaterialEditTextSettingView.SetInputType(Android.Text.InputTypes.ClassNumber);

            RequireViewById<MaterialTitleContainer>(Resource.Id.title_container_questions).SetTitle("Questions");
            RequireViewById<MaterialTitleContainer>(Resource.Id.title_container_multichoice).SetTitle("Multiple Choice");
            RequireViewById<MaterialTitleContainer>(Resource.Id.title_container_input).SetTitle("Input");
        }

        private void EditText_TextWatcher(bool valid, int value)
        {
            if (m_TextWatcher == null)
                return;

            if (!valid)
            {
                m_TextWatcher.SetEnabled(false);
                m_MaterialEditTextSettingView?.SetText(m_TextWatcher.Range.Clamp(value).ToString());
                m_TextWatcher.SetEnabled(true);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            m_MaterialEditTextSettingView?.AddTextChangedListener(m_TextWatcher);
            m_SwitchSettingViews?.ForEach(x => x.SetOnSwitchCheckedChangeListener(this));
        }
        protected override void OnStop()
        {
            base.OnStop();
            m_MaterialEditTextSettingView?.AddTextChangedListener(null);
            m_SwitchSettingViews?.ForEach(x => x.SetOnSwitchCheckedChangeListener(null));
        }

        public async void Switch_CheckedChange(MaterialSwitchSettingView? view, bool isChecked)
        {
            if (view == null) return;
            if (ViewModel.IsSaving) return;

            Debug.WriteLine($"{view.Id} - {isChecked}");

            switch (view.Id)
            {
                case Resource.Id.setting_view_question_romaji:
                    ViewModel.Settings.AskQuestionsInRomaji = isChecked;
                    break;
                case Resource.Id.setting_view_question_hiragana:
                    ViewModel.Settings.AskQuestionsInHiragana = isChecked;
                    break;
                case Resource.Id.setting_view_question_katakana:
                    ViewModel.Settings.AskQuestionsInKatakana = isChecked;
                    break;
                case Resource.Id.setting_view_multichoice:
                    ViewModel.Settings.AnswerUsingMultiChoice = isChecked;
                    break;
                case Resource.Id.setting_view_answer_romaji:
                    ViewModel.Settings.MultiChoiceIncludesRomaji = isChecked;
                    break;
                case Resource.Id.setting_view_answer_hiragana:
                    ViewModel.Settings.MultiChoiceIncludesHiragana = isChecked;
                    break;
                case Resource.Id.setting_view_answer_katakana:
                    ViewModel.Settings.MultiChoiceIncludesKatakana = isChecked;
                    break;
                case Resource.Id.setting_view_input:
                    ViewModel.Settings.AnswerUsingTextInput = isChecked;
                    break;
                default:
                    return;
            }

            await ViewModel.SaveChangesAsync();
        }
    }
}