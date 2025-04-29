using System;

using Android.Graphics;
using Android.Views;
using LearnKana.Domain.Kana;
using LearnKana.Domain.Study;
using LearnKana.Droid.MVVM.Views.Widgets;
using LearnKana.Droid.Values;
using LearnKana.Droid.Values.Resources;

namespace LearnKana.Droid.MVVM.Views.Fragments
{
    public class MultiChoiceQuestionFragment : QuestionFragment, View.IOnClickListener
    {
        public static MultiChoiceQuestionFragment CreateInstance()
        {
            MultiChoiceQuestionFragment fragment = new MultiChoiceQuestionFragment
            {

            };
            return fragment;
        }

        private OptionView? m_OptionView;

        public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate<View>(Resource.Layout.fragment_question_multichoice, container);
            return view;
        }
        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            m_TextViewQuestion?.SetText(Resource.String.quiz_question_multichoice_explanation);

            m_OptionView = view.RequireViewById<OptionView>(Resource.Id.optionview);
            m_OptionView.AddOptionView(Resource.Id.item_card_view_1);
            m_OptionView.AddOptionView(Resource.Id.item_card_view_2);
            m_OptionView.AddOptionView(Resource.Id.item_card_view_3);
            m_OptionView.AddOptionView(Resource.Id.item_card_view_4);

            m_OptionView.SetItemCardBackgroundColor(new ColorResource(Resource.Color.card_background_2));

            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);
            Question question = ViewModel.Quiz.GetCurrentQuestion();
            UpdateQuestionView(question);
        }
        public override void UpdateQuestionView(Question question)
        {
            base.UpdateQuestionView(question);

            m_OptionView?.Reset();

            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);
            ArgumentNullException.ThrowIfNull(question.Choices);
            m_OptionView?.SetOption(0, new OptionView.Option("1", question.Choices[0].Romaji));
            m_OptionView?.SetOption(1, new OptionView.Option("2", question.Choices[1].Romaji));
            m_OptionView?.SetOption(2, new OptionView.Option("3", question.Choices[2].Romaji));
            m_OptionView?.SetOption(3, new OptionView.Option("4", question.Choices[3].Romaji));
        }

        public override void OnStart()
        {
            base.OnStart();
            m_OptionView?.SetOnClickListener(this);
        }
        public override void OnStop()
        {
            base.OnStop();
            m_OptionView?.SetOnClickListener(null);
        }

        public override void OnClick(View? view)
        {
            if (view is ItemCardView card)
                ItemCardView_OnClick(card);
            else
                base.OnClick(view);
        }

        private void ItemCardView_OnClick(ItemCardView view)
        {
            ArgumentNullException.ThrowIfNull(ViewModel.Quiz);
            Question question = ViewModel.Quiz.GetCurrentQuestion();
            if (question.QuestionType != QuestionType.MultipleChoice || question.Choices == null)
                throw new InvalidOperationException();

            int option = view.Id switch
            {
                Resource.Id.item_card_view_1 => 0,
                Resource.Id.item_card_view_2 => 1,
                Resource.Id.item_card_view_3 => 2,
                Resource.Id.item_card_view_4 => 3,
                _ => throw new NotImplementedException()
            };

            KanaCharacter answer = question.Answer;
            KanaCharacter choice = question.Choices[option];

            bool isCorrect = choice == answer;
            if (isCorrect)
            {
                view.Highlight(Color.Green);
                view.SetIcon(Resource.Drawable.ic_circle_check);
                view.SetIconTint(Color.Green);
            }
            else
            {
                view.Highlight(Color.Red);
                view.SetIcon(Resource.Drawable.ic_circle_cross);
                view.SetIconTint(Color.Red);
            }

            QuestionStatus status = QuestionResult.DetermineQuestionStatus(isCorrect);

            ViewModel.Quiz.SetResult(question.QuestionNumber, new QuestionResult(new KanaSet(choice, question.KanaScript), status));
            SetQuestionResult(question, status);
        }
    }
}
