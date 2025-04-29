using LearnKana.Domain.Kana;

namespace LearnKana.Domain.Study
{
    public class QuestionResult(KanaSet? answer, QuestionStatus status)
    {
        public static QuestionStatus DetermineQuestionStatus(bool isCorrect) =>
            isCorrect ? QuestionStatus.Correct : QuestionStatus.Incorrect;

        public QuestionStatus Status { get; } = status;
        public KanaSet? Answer { get; } = answer;
    }
}