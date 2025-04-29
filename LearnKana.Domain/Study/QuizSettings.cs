namespace LearnKana.Domain.Study
{
    public class QuizSettings
    {
        public int QuestionCount { get; set; } = Quiz.DefaultQuestions;

        public bool AskQuestionsInRomaji { get; set; } = true;
        public bool AskQuestionsInHiragana { get; set; } = true;
        public bool AskQuestionsInKatakana { get; set; } = true;

        public bool AnswerUsingMultiChoice { get; set; } = true;
        public bool MultiChoiceIncludesRomaji { get; set; } = true;
        public bool MultiChoiceIncludesHiragana { get; set; } = true;
        public bool MultiChoiceIncludesKatakana { get; set; } = true;

        public bool AnswerUsingTextInput { get; set; } = true;
    }
}